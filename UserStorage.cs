using System;
using System.Collections.Generic;
using System.IO;

namespace Trading_System
{
  /// <summary>
  /// Ansvarar för att läsa och spara användare från/till fil
  /// Gör det möjligt att persistenta användardata mellan sessioner
  /// </summary>
  public static class UserStorage
  {
    private static string filePath = "Users.txt"; // Fil där användare sparas

    /// <summary>
    /// Laddar användare från fil
    /// </summary>
    /// <returns>Lista av IUser</returns>
    public static List<IUser> LoadUsers()
    {
      List<IUser> users = new List<IUser>();
      if (!File.Exists(filePath)) return users;

      string[] lines = File.ReadAllLines(filePath);

      foreach (string line in lines)
      {
        string[] parts = line.Split('|'); // username|password|name|role|isActive
        if (parts.Length < 5) continue;

        string username = parts[0];
        string password = parts[1];
        string name = parts[2];
        string role = parts[3];
        bool isActive = bool.Parse(parts[4]);

        if (role == "Admin")
        {
          Admin admin = new Admin(username, password, name);
          admin.SetIsActive(isActive);
          users.Add(admin);
        }
        else if (role == "Trader")
        {
          Trader trader = new Trader(username, password, name);
          trader.SetIsActive(isActive);
          users.Add(trader);
        }
      }
      return users;
    }

    /// <summary>
    /// Sparar användare till fil
    /// </summary>
    public static void SaveUsers(List<IUser> users)
    {
      List<string> lines = new List<string>();
      foreach (IUser u in users)
      {
        lines.Add($"{u.GetUsername()}|{u.GetPassword()}|{u.GetName()}|{u.GetRole()}|{u.GetIsActive()}");
      }
      File.WriteAllLines(filePath, lines);
    }
  }
}
