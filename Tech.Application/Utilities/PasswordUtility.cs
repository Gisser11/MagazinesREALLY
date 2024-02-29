using System.Security.Cryptography;
using System.Text;

namespace Tech.Application.Utilities;

public class PasswordUtility
{
    public string HashPassword(string pass)
    {
        var encodedPass = Encoding.UTF8.GetBytes(pass);
        var bytes = SHA256.HashData(encodedPass);
        return BitConverter.ToString(bytes).ToLower();
    }

    public bool IsVerifyPassword(string hashUserPass, string userPass)
    {
        var hash = HashPassword(userPass);
        return hashUserPass == hash;
    }
}