﻿using System;
using System.Collections.Generic;
using System.Linq;
using Coupling.Domain.DDD;

namespace Coupling.Domain.Model.Membership
{
    public sealed class Account : AggregateRoot
    {
        internal Account()
        {
            Created = DateTime.UtcNow;
            Activated = DateTime.MaxValue;
            ActivationToken = string.Empty;
            AccountStatus = AccountStatus.PendingActivation;
            AuthMemberships = new List<OAuthMembership>();
            Roles = new List<string>();
        }

        public string Username { get; private set; }
        public int UserId { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Activated { get; private set; }
        public string ActivationToken { get; private set; }
        public AccountStatus AccountStatus { get; private set; }

        public List<OAuthMembership> AuthMemberships { get; private set; }
        public LocalMembership Membership { get; private set; }
        public IList<string> Roles { get; private set; }

        internal void SetCredentials(int userId, string username, string salt, string hashPassword)
        {
            UserId = userId;
            Username = username;
            ChangePassword(salt, hashPassword);
            UpdateVersion();
        }

        internal void SetCredentials(int userId, string username, string salt, string hashPassword, string activationToken)
        {  
            ActivationToken = activationToken;
            SetCredentials(userId, username, salt, hashPassword);
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
            if (Membership == null) return false;

            var b = Membership.IsValidPassword(passwordHash);
            if (b) Membership.FailedPasswordMatch();
            else Membership.ResetPasswordMatches();

            UpdateVersion();
            return b;
        }

        public void ChangePassword(string salt, string password)
        {
            Membership = new LocalMembership(salt, password);
            UpdateVersion();
        }

        public void AppendOAuthMembership(OAuthMembership membership)
        {
            AuthMemberships.Add(membership);
            UpdateVersion();
        }

        public void AppendRoles(string[] roles)
        {
            foreach (var r in roles.Where(r => !Roles.Contains(r)))
            {
                Roles.Add(r);
            }
        }

        public void RemoveRoles(string[] roles)
        {
            foreach (var r in roles)
            {
                Roles.Remove(r);
            }
        }
    }
}
