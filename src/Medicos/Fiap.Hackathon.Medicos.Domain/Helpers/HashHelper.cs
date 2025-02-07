using System.Security.Cryptography;
using System.Text;

namespace Fiap.Hackathon.Medicos.Domain.Helpers
{
    public class HashHelper
    {

        public static string GerarHash(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);

                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
