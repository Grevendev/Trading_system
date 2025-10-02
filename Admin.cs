using System;

namespace Trading_System
{
  //
  //Admin
  //
  // Admin-klassen
  //
  //Viktigt att notera:
  //Admin har inte tillgång till trading funktioner (kan inte äga items, skicka trades, etc.)
  //Admin används för att hantera användare, t.ex. aktivera/inaktivera konton.
  //Ärver (implementerar) gränssnittet IUser, så att systemet kan behandla Admin-objekt och Trader-objekt på ett enhetligt sätt.
  public class Admin : IUser     // Admin implementerar IUser
  {
    //Fält (privata variabler som beskriver Admin)
    private string userName; // Användarnamn för inlogging
    private string passwordHash; // Lösenord (Lagras som hash, inte klartext)
    private string name; // Namn som visas i systemet
    private bool isActive = true; // Anger om kontot är aktivt eller inaktivt

    // Konstruktor
    //
    // Skapar en Admin. Kräver användarnamn och lösenord.
    //DisplayName kan anges för att ha ett annat visingsnman än användarnamnet.
    public Admin(string username, string password, string displayName = "")
    {
      userName = username;
      passwordHash = PasswordHelper.HashPassword(password); // Hasha lösenord för säker lagring
      name = displayName != "" ? displayName : username; // Om inget namn anges används användarnamnet
    }
    // TryLogin
    // Används vid inlogging.
    //Kollar först om kontot är aktivt.
    // Jämför sedan angivet användarnamn och lösenord.
    // Retunerar true om allt stämmer, annars false.
    public bool TryLogin(string username, string password)
    {
      if (!isActive) return false; // Inaktiva konton kan inte logga in
      if (username != userName) return false; // Kontrollera att användarnamnet stämmer
      return PasswordHelper.VerifyPassword(password, passwordHash); // Varifiera lösenordet
    }
    //Info
    // Skriver ut information om Admin till konsolen.
    //Används för att visa vem som är inloggad
    public void Info() => Console.WriteLine($"Name: {name}, Username: {userName}, Role: {GetRole()}");
    // Implementation av IUser
    // Dessa metoder gör att Admin följer samma "Kontrakt" som Trader,
    //vilket gör att båda kan hanteras i samma listor och metoder
    public Role GetRole() => Role.Admin; // Rollen är alltid Admin
    public string GetUsername() => userName; // Returnerar användarnamn
    public string GetPassword() => passwordHash; // Retunerar lösenordhash
    public void SetPassword(string newPassword) => passwordHash = PasswordHelper.HashPassword(newPassword); // Sätter nytt lösenord

    //Följande metoder är inte relevanta för Admin, men krävs av IUser.
    // Vi returnerar därför default-värden eller tomma implementation
    public int GetFailedLogins() => 0;
    public void SetFailedLogins(int value) { }
    public bool GetMustChangePassword() => false;
    public void SetMustChangePassword(bool value) { }

    //Hantering av aktiva/inaktiva status
    public bool GetIsActive() => isActive;
    public void SetIsActive(bool value) => isActive = value;

    //Hantering av namn
    public string GetName() => name;
    public void SetName(string newName) => name = newName;
  }
}
