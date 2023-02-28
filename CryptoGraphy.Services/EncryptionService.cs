using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptoGraphy.Services
{
    /// <summary>
    /// Encryption of input data to different algorithms including
    /// MD5
    /// SHA1
    /// SHA256
    /// SHA384
    /// SHA512
    /// </summary>
    public static class EncryptionService
    {
        /// <summary>
        /// Convert input to SHA1 Hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns> SHA1 Hashed String</returns>
        public static string GetSHA1Hash(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }

        }
        /// <summary>
        /// Convert input to SHA256 Hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns> SHA256 Hashed String</returns>
        public static string GetSHA256Hash(string input)
        {

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }

        }

        /// <summary>
        /// Convert input to SHA384 Hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns> SHA384 Hashed String</returns>
        public static string GetSHA384Hash(string input)
        {

            using (SHA384 sha384 = SHA384.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = sha384.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }

        }

        /// <summary>
        /// Convert input to SHA512 Hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns> SHA512 Hashed String</returns>
        public static string GetSHA512Hash(string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = sha512.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }

        }

        /// <summary>
        /// Convert input to MD5 Hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns> MD5 Hashed String</returns>
        public static string GetMD5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }

        }
    }
}
