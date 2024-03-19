using System.Text.RegularExpressions;

namespace Server.Validator
{
    public class LoginValidator
    {
        public string isEmailOrUsername(string emailOrUsername)
        {
            var emailRegEx = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
            if(emailRegEx.IsMatch(emailOrUsername)){
                return "email";
            }
            return "username";
        }
    }
}