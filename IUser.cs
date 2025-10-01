namespace Trading_System
{
  // Role
  //
  // En enumeration som represneterar olika typer av oller i systemet. 
  //Detta används för att särskilja mellan t.ex. Admins och Traders.
  //
  //Fördelar med enum:
  //Gör koden mer läsbar
  //Förhindrar "magic strings"
  //Lättare att bygga menylogik baserat på roller
  public enum Role
  {
    None, // Ingen roll (standardvärde, används sällan i praktiken)
    Admin, // Administartör, kan hantera systeminställningar
    Trader // Vanliga användare som kan lägga upp och byta items.
  }

  // IUser (Interface för användare)
  //
  //Detta iterface definierar vilka metoder och egenskaper som alla användartyper måste ha
  // 
  //Fördelar med interface:
  //Gör systemet flexibelt (vin kan Admin och Trader som implementerar samma kontrakt)
  //Gör det möjligt att arbeta med generlla listor av IUser
  //Följer principer "programmera mit ett interface, inte en implementation"

  // Krav i interfcet:
  // Inlogging (TryLogin)
  //Informationsvising (Info)
  //Rollhantering (GetRole)
  //Användarnamn och lösenord
  //Hantering av misslyckade inloggingar
  //Status om kontot måste byta lösenord
  //Aktiv/avaktiverad status
  //Namnhantering (visingsnamn)

  public interface IUser
  {
    //
    //Inlogging
    bool TryLogin(string username, string password);
    void Info();
    //
    // Roll och identitet
    //
    Role GetRole();
    string GetUsername();
    string GetPassword();
    //
    //Lösenorshantering
    //
    void SetPassword(string newPassword);
    //
    //Säkerhet (misslyckade inloggingar)
    //
    int GetFailedLogins();
    void SetFailedLogins(int value);
    //
    //Lösenordsbyten
    //
    bool GetMustChangePassword();
    void SetMustChangePassword(bool value);
    //
    //Kontoaktivitet
    //
    bool GetIsActive();
    void SetIsActive(bool value);
    //
    //Namnhantering
    //
    string GetName();
    void SetName(string name);
  }
}
