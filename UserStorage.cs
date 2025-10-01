using System;
using System.Collections.Generic;
using System.IO;
using Trading_System;

namespace Trading_System
{

  // UserStorage hanterar lagring och laddning av användarobjekt till/från fil.
  // Används för att bevara användardata (Admin/Trader) mellan programkörningar.
  public static class UserStorage
  {

    // Filen där användare sparas (i samma katalog som programmet körs).
    private static string filePath = "users.txt";

    // Sparar en lista av användare till en textfil.
    // Varje användare lagras på en egen rad i formatet.
    // Role, Username, PasswordHash, Name
    public static void SaveUsers(List<IUser> users)
    {
      // Skapar en StreamWriter som öppnar filen på angiven sökväg.
      // using-blocket säkerställer att filen stängs och resusrser frigörs när vi är klara.
      using (StreamWriter sw = new StreamWriter(filePath))
      {
        // Iterara över varje användare i listan.
        foreach (var u in users)
        {
          // Hör skrivs varje användare data till filen.
          // Exakt format (t.ex. roll;usernamne;passwordHash;name) bör defineras för att kunna läsas korrekt senare.
          // sw.WriteLine($"{u.Role};{u.Username};{u.PasswordHash};{u.Name}");
          // Format: Role|Username|PasswordHash|Name, Sparar användare i textformat. Viktigt: Löserord lagras som hash, inte i klartext.
          sw.WriteLine($"{u.GetRole()}|{u.GetUsername()}|{u.GetPassword()}|{u.GetName()}");
        }
      }
    }

    // Läser in användare från textfilen och retunerar en lista av IUser.
    // Om filen inte finns retuneras en tom lista. 
    public static List<IUser> LoadUsers()
    {
      // Om filen inte finns, retunerar tom lista (ingen exeption kastas).
      List<IUser> users = new List<IUser>();

      //Kontrollera om filen finns. Om inte, returnera en tom lista direkt.
      // (dvs. det finns inga användare sparade ännu).
      if (!File.Exists(filePath)) return users;

      // Öppna filen för läsning med en StreamReader. Using-blocket ser till att filen stängs korrekt när vi är klara.

      using (StreamReader sr = new StreamReader(filePath))
      {
        string line;

        // Läs filen rad för röd tills slutet är nått.
        while ((line = sr.ReadLine()) != null)
        {
          // Förväntat format för varje rad i filen, Role, Username, PasswordHash, Name.
          string[] parts = line.Split('|');

          // Kontrollera att raden innehåller minst 4 delar.
          //Om formatet inte stämmer hoppar vi över raden.
          if (parts.Length < 4) continue;   // Hoppa över rader med fel format.

          // Konventera rden första strängen i parts-arrayen till motsvarande enum-värde av typen Role.
          // Enum.Parse retunerar ett objekt, så vi gör en explicit cast till Role.

          Role role = (Role)Enum.Parse(typeof(Role), parts[0]);
          string username = parts[1]; // andra elementet i parts representerar användarnamnet som en vanlig sträng.
          string passwordHash = parts[2]; // Tredje elementet innehåller användarens lösenord i hashad form.
          string name = parts[3]; // Fjärde elementet är användarens visingsnamn eller fullständinga namn.

          // Skapa korrekt användarobjekt baserat på roll.

          if (role == Role.Admin)
            users.Add(new Admin(username, passwordHash, name));
          else if (role == Role.Trader)
          {
            // Trader-objekt behöver en temporär sträng för lösenord i konstruktorn (eftersom hash sätts separat via SetPasswordHash).
            Trader t = new Trader(username, "temp", name);
            t.SetPasswordHash(passwordHash); // Skriv över med korrekt hash. 
            users.Add(t);
          }
        }
      }

      return users;
    }
  }
}
