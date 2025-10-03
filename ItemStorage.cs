using System;
using System.Collections.Generic;
using System.IO;

namespace Trading_System
{
  public static class ItemStorage
  {
    private static string filePath = "Items.txt";

    public static void SaveItems(List<IUser> users)
    {
      List<string> lines = new List<string>();
      foreach (var u in users)
      {
        if (u is Trader tr)
        {
          foreach (var item in tr.GetItems())
          {
            lines.Add($"{item.GetName()}|{item.GetDescription()}|{item.GetOwnerUsername()}");
          }
        }
      }
      File.WriteAllLines(filePath, lines);
    }

    public static void LoadItems(List<IUser> users)
    {
      if (!File.Exists(filePath)) return;
      string[] lines = File.ReadAllLines(filePath);
      foreach (string line in lines)
      {
        string[] parts = line.Split('|');
        if (parts.Length < 3) continue;
        string name = parts[0];
        string desc = parts[1];
        string owner = parts[2];

        foreach (var u in users)
        {
          if (u.GetUsername() == owner && u is Trader tr)
          {
            tr.GetItems().Add(new Item(name, desc, owner));
            break;
          }
        }
      }
    }
  }
}
