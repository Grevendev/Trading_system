using System;
using System.Security.Cryptography;
using System.Text;

namespace Trading_System
{
  // Passwordhelper

  // Detta är en statisk klass som ansvarar för hantering av lösenord i systemet.
  //
  // Vi vill aldrig spara lösenord i klartext eftersom det är
  // en stor säkerhetsrisk. Istället använder vi en hashfunktion
  //(i detta fall SHA-256) för att kspa en "enkelrikatd representation" av lösenordet.
  //
  // Viktigt: en hash går inte att ågterställa till det ursprungliga lösenordet,
  //vilket gör det säkare. För att verifiera lösenord, hashar man användarens
  //inmating och jämför den med den sparade hashen.
  public static class PasswordHelper
  {
    //
    // HashPassword
    //
    // Tar in ett lösenord i klartext (string)
    //1. Gör om lösenordet till en byte-array med UTF8.
    //2. Skapar en hash med SHA-256.
    //3. Gör om den hashade byten till en Base64-sträng (så att vi kan spara den som text i fil/databas).
    //
    // Retunerar: En sträng med det hashade lösenordet.

    public static string HashPassword(string password)
    {
      // Skapar en SHA256-instans för att beräkna hash
      using (var sha = SHA256.Create())
      {
        // Hashar lösenordet (konventerat till bytes)
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

        // Gör om resultatet till Base64-sträng så det kan lagras
        return Convert.ToBase64String(bytes);
      }
    }

    // 
    //VerifyPassword
    //
    // Används vid inlogging.
    //Tar in ett lösenord som användaren skriver in (klartext)
    //samt den hash som är sparad i systemet.
    //
    //Steg: 
    //1. Kollar om en storedHash är tomt eller null (då är lösenordet ogiltigt).
    //2. hashar inputPassword med samma metod (HashPassword).
    //3. Jämför den nya hashen med den lagrade hashen.
    // Om de är lika betyder det att lösenordet var korrekt.
    //
    // Retunerar: true om lösenordet stämmer, annars false.

    public static bool VerifyPassword(string inputPassword, string storedHash)
    {
      if (string.IsNullOrEmpty(storedHash)) return false;

      // hashar användarens inmatade lösenord och jämför med lagrat hash.
      return HashPassword(inputPassword) == storedHash;
    }
  }
}
