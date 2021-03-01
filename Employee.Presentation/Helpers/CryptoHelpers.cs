using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Presentation.Helpers
{
    public class CryptoHelpers
    {

        private static readonly Lazy<CryptoHelpers> _instance = new Lazy<CryptoHelpers>(() => new CryptoHelpers());

        public static CryptoHelpers Instance = _instance.Value;
        private CryptoHelpers()
        {

        }
        public string MD5(string s)
        {
            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                    builder.Append(b.ToString("x2").ToLower());

                return builder.ToString();
            }
        }
    }
}
