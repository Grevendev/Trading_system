using System;
using System.Collections.Generic;
using System.IO;
using Trading_System;

namespace Trading_System
{
  public static class UserStorage
  {
    private static string filePath = "users.txt";

    // Spara användare till fil
    public static void SaveUsers(List<IUser> users)
    {
      using (StreamWriter sw = new StreamWriter(filePath))
      {
        foreach (var u in users)
        {
          // Format: Role|Username|PasswordHash|Name
          sw.WriteLine($"{u.GetRole()}|{u.GetUsername()}|{u.GetPassword()}|{u.GetName()}");
        }
      }
    }

    // Ladda användare från fil
    public static List<IUser> LoadUsers()
    {
      List<IUser> users = new List<IUser>();
      if (!File.Exists(filePath)) return users;

      using (StreamReader sr = new StreamReader(filePath))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          string[] parts = line.Split('|');
          if (parts.Length < 4) continue;

          Role role = (Role)Enum.Parse(typeof(Role), parts[0]);
          string username = parts[1];
          string passwordHash = parts[2];
          string name = parts[3];

          if (role == Role.Admin)
            users.Add(new Admin(username, passwordHash, name));
          else if (role == Role.Trader)
          {
            Trader t = new Trader(username, "temp", name);
            t.SetPasswordHash(passwordHash); // Använd ny metod
            users.Add(t);
          }
        }
      }

      return users;
    }
  }
}
