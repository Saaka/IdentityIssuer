using System.Security.Cryptography;
using System.Text;
using IdentityIssuer.Application.Services;

namespace IdentityIssuer.Infrastructure.Helpers
{
    public class HashGenerator : IHashGenerator
    {
        public string Generate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.Default.GetBytes(value));
                var stringBuilder = new StringBuilder();
                foreach (var b in bytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}