using System.Security.Cryptography;

namespace Fiap.Hackathon.Medicos.Domain.Helpers
{
    public static class PasswordHelper
    {
        private const int SaltSize = 16; // Tamanho do salt em bytes
        private const int KeySize = 32;  // Tamanho da chave derivada em bytes
        private const int Iterations = 10000; // Número de iterações para o PBKDF2

        public static string HashPassword(string password)
        {
            // Gera um salt criptograficamente seguro
            using var rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            // Deriva a chave usando PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(KeySize);

            // Combina o salt e o hash em um único array
            byte[] hashBytes = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

            // Retorna o resultado como uma string Base64
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Converte a string Base64 de volta para bytes
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // Extrai o salt do hash armazenado
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Deriva a chave para a senha fornecida usando o salt extraído
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(KeySize);

            // Compara o hash derivado com o hash armazenado
            for (int i = 0; i < KeySize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                    return false;
            }

            return true;
        }
    }
}
