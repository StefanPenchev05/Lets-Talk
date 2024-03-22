using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Server.Interface;

namespace Server.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey = "VxKpZVMXggM5vunvgBUlbNQYa1AlGg0C";

        public async Task<string> GenerateTokenAsync(List<string> data, int expireInMinutes)
        {
            // Create a new JWT token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Convert the secret key to bytes using UTF8 encoding
            var key = Encoding.UTF8.GetBytes(_secretKey);

            // Create a list of claims, one for each string in the data list
            var claims = data.Select(datum => new Claim(ClaimTypes.Name, datum)).ToList();

            // Define the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Set the subject of the token to be the claims
                Subject = new ClaimsIdentity(claims),
                // Set the token to expire in 15 minutes
                Expires = DateTime.UtcNow.AddMinutes(expireInMinutes),
                // Set the signing credentials, using the symmetric key and HMAC SHA256 for signing
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create the token using the token descriptor
            var token = await Task.Run(() => tokenHandler.CreateToken(tokenDescriptor));

            // Return the token
            return tokenHandler.WriteToken(token);
        }

        public async Task<List<string>> VerifyTokenAsync(string token)
        {
            // Create a new JWT token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Convert the secret key to bytes using UTF8 encoding
            var key = Encoding.UTF8.GetBytes(_secretKey);

            try
            {
                // Define the token validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    // Validate the issuer signing key
                    ValidateIssuerSigningKey = true,
                    // Set the issuer signing key
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    // Don't validate the issuer
                    ValidateIssuer = false,
                    // Don't validate the audience
                    ValidateAudience = false,
                    // Set the clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };

                // Validate the token and get the principal
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                // Check if the token has a valid security algorithm
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    // If the token has an invalid security algorithm, throw an exception
                    throw new SecurityTokenException("Invalid token");
                }

                // Extract the claims from the principal and convert them to a list of strings
                var data = principal.Claims.Select(claim => claim.Value).ToList();

                // Return the data
                return await Task.FromResult(data);
            }
            catch
            {
                // If any error occurs, return null
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}