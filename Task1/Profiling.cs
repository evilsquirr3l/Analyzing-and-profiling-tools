using System.Security.Cryptography;

namespace Task1;

public static class Profiling
{
    public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
    {
        var iterate = 10000;

        var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);

        byte[] hash = pbkdf2.GetBytes(20);
        byte[] hashBytes = new byte[36];

        Array.Copy(salt, 0, hashBytes, 0, 16);

        Array.Copy(hash, 0, hashBytes, 16, 20);
        
        var passwordHash = Convert.ToBase64String(hashBytes);
        
        return passwordHash;
    }
    
    public static string GeneratePasswordHashUsingSaltUpdated(string passwordText, byte[] salt)
    {
        var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, 10000, HashAlgorithmName.SHA512);
        var hash = pbkdf2.GetBytes(20);

        var hashBytes = new byte[salt.Length + hash.Length];
        Buffer.BlockCopy(salt, 0, hashBytes, 0, salt.Length);
        Buffer.BlockCopy(hash, 0, hashBytes, salt.Length, hash.Length);

        return Convert.ToBase64String(hashBytes);
    }
}