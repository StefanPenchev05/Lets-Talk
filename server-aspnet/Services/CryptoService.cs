using System.Security.Cryptography;
using System.Text;
using Server.Interface;

namespace Server.Services
{
    public class CryptoService : ICryptoService
    {
        // The encryption key
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("=mR\"Bqzhd,{+,#K");

        // The initialization vector
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("HTXv*Lc-&1C}@Jv");

        // Method to encrypt a string using AES encryption
        public async Task<byte[]> EncryptAsync(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                // Create an encryptor to perform the stream transform
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            // Write all data to the stream
                            await sw.WriteAsync(plainText);
                        }

                        // Return the encrypted bytes from the memory stream
                        return ms.ToArray();
                    }
                }
            }
        }

        // Method to decrypt a byte array using AES decryption
        public async Task<string> DecryptAsync(byte[] cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                // Create a decryptor to perform the stream transform
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string
                            return await sr.ReadToEndAsync();
                        }
                    }
                }
            }
        }
    }
}