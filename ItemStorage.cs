using System;
using System.Collections.Generic;
using System.IO;
using Trading_System;

namespace Trading_System
{
  public static class ItemStorage
  {
    private static string filePath = "items.txt";

    // Spara alla items till fil
    public static void SaveItems(List<IUser> users)
    {
      using (StreamWriter sw = new StreamWriter(filePath))
      {
        foreach (var u in users)
        {
          if (u is Trader t)
          {
            foreach (var item in t.GetItems())
            {
              // Format: OwnerUsername|ItemName|ItemDescription
              sw.WriteLine($"{item.GetOwnerUsername()}|{item.GetName()}|{item.GetDescription()}");
            }
          }
        }
      }
    }

    // Ladda items från fil
    public static void LoadItems(List<IUser> users)
    {
      if (!File.Exists(filePath)) return;

      using (StreamReader sr = new StreamReader(filePath))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          string[] parts = line.Split('|');
          if (parts.Length < 3) continue;

          string owner = parts[0];
          string itemName = parts[1];
          string itemDesc = parts[2];

          // Hitta ägaren och lägg till item
          IUser user = null;
          foreach (var u in users)
          {
            if (u.GetUsername() == owner)
            {
              user = u;
              break;
            }
          }

          if (user != null && user is Trader t)
          {
            t.AddItem(itemName, itemDesc);
          }
        }
      }
    }
  }
}
