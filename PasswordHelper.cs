using System;
using System.Security.Cryptography;
using System.Text;

namespace Trading_System
{
  /// <summary>
  /// Hanterar hashing och verifiering av lösenord.
  /// </summary>
  public static class PasswordHelper
  {
    public static string HashPassword(string password)
    {
      using (SHA256 sha = SHA256.Create())
      {
        byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        StringBuilder sb = new StringBuilder();
        foreach (var b in bytes) sb.Append(b.ToString("x2"));
        return sb.ToString();
      }
    }

    public static bool VerifyPassword(string password, string hash)
    {
      return HashPassword(password) == hash;
    }
  }
}
