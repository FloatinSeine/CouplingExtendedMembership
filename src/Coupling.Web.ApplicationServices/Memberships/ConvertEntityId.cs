using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coupling.Web.ApplicationServices.Memberships
{
    class ConvertEntityId
    {

        public int Convert(Guid guid)
        {
            var bytes = guid.ToByteArray();
            var i = ((int)bytes[0]) | ((int)bytes[1] << 8) | ((int)bytes[2] << 16) | ((int)bytes[3] << 24);
            return BitConverter.ToInt32(bytes, 0);
        }

        public Guid Convert(int i)
        {
            var bytes = new byte[4];
            bytes[0] = (byte)i;
            bytes[1] = (byte)(i >> 8);
            bytes[2] = (byte)(i >> 16);
            bytes[3] = (byte)(i >> 24);
            bytes[4] = (byte)(i >> 32);

            var g = new Guid(bytes);

            return g;
        }
    }
}
