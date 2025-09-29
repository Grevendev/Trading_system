using System;
using System.Security.Cryptography;
using System.Text;

namespace Trading_System
{
  public static class PasswordHelper
  {
    public static string HashPassword(string password)
    {
      using (var sha = SHA256.Create())
      {
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
      }
    }

    public static bool VerifyPassword(string inputPassword, string storedHash)
    {
      if (string.IsNullOrEmpty(storedHash)) return false;
      return HashPassword(inputPassword) == storedHash;
    }
  }
}
