using System;
using System.Collections.Generic;
using System.IO;

namespace Trading_System
{
  /// <summary>
  /// Hanterar persistens för användare.
  /// UserStorage ansvarar för att läsa och skriva användare (Admin eller Trader) till en textfil.
  /// Detta gör att användardata bevaras mellan körningar utan databas.
  /// </summary>
  public static class UserStorage
  {
    private static string filePath = "Users.txt"; // Fil där alla användare sparas

    /// <summary>
    /// Laddar alla användare från filen.
    /// </summary>
    /// <returns>Lista med IUser-objekt (Admin eller Trader).</returns>
    public static List<IUser> LoadUsers()
    {
      List<IUser> users = new List<IUser>();

      // Om filen inte finns, returnera en tom lista (första gången programmet körs)
      if (!File.Exists(filePath)) return users;

      // Läs in alla rader från filen
      string[] lines = File.ReadAllLines(filePath);

      foreach (string line in lines)
      {
        // Dela upp raden i fält baserat på '|' separator
        // Förväntat format: username|password|name|role|isActive
        string[] parts = line.Split('|');
        if (parts.Length < 5) continue; // Hoppa över ogiltiga rader

        string username = parts[0];
        string password = parts[1];
        string name = parts[2];
        string role = parts[3];
        bool isActive = bool.Parse(parts[4]);

        // Skapa ett Admin-objekt om rollen är Admin
        if (role == "Admin")
        {
          Admin admin = new Admin(username, password, name);
          admin.SetIsActive(isActive); // Återställ status (aktiv/inaktiv)
          users.Add(admin);
        }
        // Skapa ett Trader-objekt om rollen är Trader
        else if (role == "Trader")
        {
          Trader trader = new Trader(username, password, name);
          trader.SetIsActive(isActive); // Återställ status
          users.Add(trader);
        }
      }

      return users;
    }

    /// <summary>
    /// Sparar alla användare till fil.
    /// </summary>
    /// <param name="users">Lista med IUser-objekt (Admin eller Trader).</param>
    public static void SaveUsers(List<IUser> users)
    {
      List<string> lines = new List<string>();

      foreach (IUser u in users)
      {
        // Formatera raden enligt: username|password|name|role|isActive
        // Detta gör det enkelt att läsa in datan nästa gång programmet startar
        lines.Add($"{u.GetUsername()}|{u.GetPassword()}|{u.GetName()}|{u.GetRole()}|{u.GetIsActive()}");
      }

      // Skriv alla rader till filen, ersätter tidigare innehåll
      File.WriteAllLines(filePath, lines);
    }
  }
}
