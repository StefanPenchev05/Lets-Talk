using System.Security.Cryptography;
using System.Text;
using Server.Interface;

namespace Server.Services
{
    public class HashService : IHashService
    {
        public async Task<string> HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(password)))
                {
                    var hashedBytes = await sha256.ComputeHashAsync(stream);
                    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }
        }

        public async Task<bool> Compare(string password, string hashedPassword)
        {
            return await HashPassword(password) == hashedPassword;
        }
    }
}