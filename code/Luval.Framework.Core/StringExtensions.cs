using System.Security.Cryptography;
using System.Text;

namespace Luval.Framework.Core
{
    public static class StringExtensions
    {

        #region Encryption

        public static string Encrypt(this string textToEncrypt, string password)
        {
            byte[] iv = new byte[16]; // Initialization vector
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(password);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(textToEncrypt);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string Decrypt(this string textToDecrypt, string password)
        {
            byte[] iv = new byte[16]; // Initialization vector
            byte[] buffer = Convert.FromBase64String(textToDecrypt);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(password);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

		#endregion

		public static string GetTextInBetween(this string s, string delimiter)
        {
            if (!s.Contains(delimiter)) return s;
            var startIndex = s.IndexOf(s);
            var endIndex = s.IndexOf(delimiter, startIndex + delimiter.Length);
            return s.Substring(startIndex, endIndex - startIndex).Replace(delimiter, "");
        }
    }
}