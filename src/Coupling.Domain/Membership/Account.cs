﻿using System;
using System.Collections.Generic;
using Coupling.Domain.DDD;

namespace Coupling.Domain.Membership
{
    public class Account : AggregateRoot
    {
        private readonly List<OAuthMembership> _authMemberships; 

        public Account()
        {
            Created = DateTime.UtcNow;
            Activated = DateTime.MaxValue;
            ActivationToken = string.Empty;
            AccountStatus = AccountStatus.PendingActivation;
            _authMemberships = new List<OAuthMembership>();
        }

        public string Username { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Activated { get; private set; }
        public string ActivationToken { get; private set; }
        public AccountStatus AccountStatus { get; private set; }
        public bool IsActivated { get { return AccountStatus == AccountStatus.Activated; } }
        public List<OAuthMembership> AuthMemberships { get { return _authMemberships; }}
        public LocalMembership Membership { get; private set; }

        private void CreateLocalMembership(string salt, string hashPassword)
        {
            Membership = new LocalMembership(salt, hashPassword);
        }

        public void SetCredentials(string username, string salt, string hashPassword)
        {
            Username = username;
            CreateLocalMembership(salt, hashPassword);
            UpdateVersion();
        }

        public void SetCredentials(string username, string salt, string hashPassword, string activationToken)
        {  
            ActivationToken = activationToken;
            SetCredentials(username, salt, hashPassword);
        }

        public void Activate(string token)
        {
            if (!ActivationToken.Equals(token)) throw new ArgumentException(string.Format("Tokens do not match. Activation Token={0}, token={1}", ActivationToken, token), "token");

            AccountStatus = AccountStatus.Activated;
            Activated = DateTime.UtcNow;
            ActivationToken = string.Empty;
            UpdateVersion();
        }

        public bool IsValidPassword(string passwordHash)
        {
            return Membership != null && Membership.IsValidPassword(passwordHash);
        }

        public bool ResetPassword(string salt, string passwordHash)
        {
            var b = Membership.ResetPassword(salt, passwordHash);
            UpdateVersion();
            return b;
        }

        public bool IsLocalMembershipActived()
        {
            return IsActivated;
        }

        public void AppendOAuthMembership(OAuthMembership membership)
        {
            _authMemberships.Add(membership);
            UpdateVersion();
        }
    }
}