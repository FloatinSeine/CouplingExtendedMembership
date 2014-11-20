using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coupling.Domain.Model.Membership;
using Raven.Imports.Newtonsoft.Json;
using Raven.Imports.Newtonsoft.Json.Linq;

namespace Coupling.Domain.Persistence.Raven.Converters
{
    public class LocalMembershipConverter : JsonCreationConverter<LocalMembership>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LocalMembership);
        }

        protected override LocalMembership Create(Type objectType, JObject jObject)
        {
            var pwd = ExtractField("Password", jObject, string.Empty);
            var salt = ExtractField("Salt", jObject, string.Empty);

            var lm = new LocalMembership(pwd, salt);
            return lm;
        }

        private string ExtractField(string fieldName, JObject jObject, string defaultValue)
        {
            if (jObject[fieldName] != null) return jObject[fieldName].Value<string>();
            return defaultValue;
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }
}
