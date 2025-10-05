using System;
using System.Security.Cryptography;
using System.Text;

namespace Trading_System
{
  /// <summary>
  /// PasswordHelper ansvarar för att hantera lösenordssäkerhet.
  /// Den erbjuder två huvudfunktioner:
  /// 1. HashPassword: Konventerar ett vanligt lösenord till en SHA-256 hash.
  /// 2. VerifyPassword: Jämför ett vanligt lösenord med en sparad hash för verifiering.
  /// 
  /// Detta gör att vi aldrig sparar användarens lösenord i klartext, vilket ör en grundeläggande säkerhetsprincip. 
  /// </summary>
  public static class PasswordHelper
  {
    //Skapar en SHA-256 från ett lösenord.
    //Hasing gör det svårt att återställa orginallösenordet.
    //"Password" Lösenordet som ska hashas
    //Haxadecimal sträng som representerar lösenordets hash
    public static string HashPassword(string password)
    {
      //Skapar en SHA-256 instans
      using (SHA256 sha = SHA256.Create())
      {
        //Konventerar varje byte till en haxadecimal strång
        byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        StringBuilder sb = new StringBuilder();
        foreach (var b in bytes) sb.Append(b.ToString("x2")); // "x2" gör att varje byte blir två hex-tecken.

        //Returnerar den kompletta hash-strängen.
        return sb.ToString();
      }
    }

    // Verifierar om ett givet lösenord matchar en sparad hash.
    //"password" Lösenordet användaren matar in
    //"hash" Den sparade hash som ska jämföras med.
    //True om lösenordet meatchar hashen, annars false.
    public static bool VerifyPassword(string password, string hash)
    {
      // Vi hashar det inskrivna lösenordet och jämför den med den sparade hash.
      //Detta säkerställer att vi aldrig jämför eller lagrar lösenord i klartext.
      return HashPassword(password) == hash;
    }
  }
}
