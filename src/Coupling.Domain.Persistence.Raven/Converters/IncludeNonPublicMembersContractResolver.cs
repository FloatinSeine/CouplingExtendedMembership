using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Raven.Imports.Newtonsoft.Json.Serialization;

namespace Coupling.Domain.Persistence.Raven.Converters
{
    public class IncludeNonPublicMembersContractResolver : DefaultContractResolver
    {
        public IncludeNonPublicMembersContractResolver()
        {
            DefaultMembersSearchFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        }
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var members = base.GetSerializableMembers(objectType);
            return members.Where(m => !m.Name.EndsWith("k__BackingField")).ToList();
        }
    }
}
