using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace nChain.Common
{
    public class SHA256Manager
    {
        public string GetHash(string data)
        {
            var sha256 = new SHA256Managed();
            var hashBuilder = new StringBuilder();

            var dataBytes = Encoding.Unicode.GetBytes(data);
            var hash = sha256.ComputeHash(dataBytes);

            foreach (var b in hash)
            {
                hashBuilder.Append($"{b:x2}");
            }
            return hashBuilder.ToString();
        }
    }
}
