using System;
using System.Collections.Generic;
using System.IO;

namespace Trading_System
{
  /// <summary>
  /// ItemStorage ansvarar för att spara och läsa in items till/från fil.
  /// Detta gör att items bevaras mellan programkörningar utan databas.
  /// </summary>
  public static class ItemStorage
  {
    private static string filePath = "Items.txt"; // Filen där alla items sparas

    /// <summary>
    /// Sparar alla items för alla traders i fil.
    /// </summary>
    /// <param name="users">Lista med alla användare</param>
    public static void SaveItems(List<IUser> users)
    {
      List<string> lines = new List<string>();

      // Loopar igenom alla användare
      foreach (var u in users)
      {
        // Vi sparar endast items för Trader-objekt
        if (u is Trader tr)
        {
          // Loopar igenom varje item tradern äger
          foreach (var item in tr.GetItems())
          {
            // Sparar namn, beskrivning och ägare separerat med '|'
            lines.Add($"{item.GetName()}|{item.GetDescription()}|{item.GetOwnerUsername()}");
          }
        }
      }

      // Skriver alla lines till fil
      File.WriteAllLines(filePath, lines);
    }

    /// <summary>
    /// Läser in items från fil och kopplar dem till rätt trader.
    /// </summary>
    /// <param name="users">Lista med alla användare</param>
    public static void LoadItems(List<IUser> users)
    {
      if (!File.Exists(filePath)) return; // Om filen inte finns, gör ingenting

      string[] lines = File.ReadAllLines(filePath);

      foreach (string line in lines)
      {
        string[] parts = line.Split('|'); // Format: name|description|owner
        if (parts.Length < 3) continue;

        string name = parts[0];
        string description = parts[1];
        string owner = parts[2];

        // Hitta tradern som äger item
        foreach (var u in users)
        {
          if (u is Trader tr && tr.GetUsername() == owner)
          {
            // Lägg till item i traderns lista
            tr.GetItems().Add(new Item(name, description, owner));
            break; // Gå vidare till nästa item
          }
        }
      }
    }
  }
}
