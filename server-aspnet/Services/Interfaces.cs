using Server.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Server.Interface
{
    // Interface for a service that can compare a password with a hashed password
    public interface IHashService
    {
        Task<string> HashPassword(string password);
        Task<bool> Compare(string password, string hashedPassword);
    }

    // Interface for a service that can send emails asynchronously
    public interface IEmailService
    {
        Task SendEmailAsync(string viewName, string _subject, LoginViewModel model,  Dictionary<string, object> additionalData = null);
    }

    // Interface for a service that can render a view to a string asynchronously
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model, Dictionary<string, object> additionalData = null);
    }

    // Interface for a service that can encrypt and decrypt data asynchronously
    public interface ICryptoService
    {
        Task<byte[]> EncryptAsync(string plainText);
        Task<string> DecryptAsync(byte[] cipherText);
    }

    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(string uuid);
        Task<ClaimsPrincipal> VerifyTokenAsync(string token);
    }
}