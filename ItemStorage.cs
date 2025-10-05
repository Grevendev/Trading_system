using System;
using System.Collections.Generic;
using System.IO;

namespace Trading_System
{
  // Hanterar sparning och laddning av items till/från en textfil.
  //Gör det möjligt att bevara traders föremål mellan programstarter. 
  public static class ItemStorage
  {

    private static string filePath = "Items.txt"; // Fil där alla items lagras.


    // Sparar alla tarders items till fil.
    //Varje rad representerar ett föremål i formatet:
    //namn|beskriving|ägarens användarnamn 
    public static void SaveItems(List<IUser> users)
    {
      List<string> lines = new List<string>();

      //Loopar igenom alla användare och plockar ut deras items om det är traders.
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

      //Sparar allt till filen.
      File.WriteAllLines(filePath, lines);
    }

    // Laddar in alla items från filen och tilldelar dem till rätt trader.
    public static void LoadItems(List<IUser> users)
    {
      //Avbryt om ingen fil finns (första körningen)
      if (!File.Exists(filePath)) return;
      string[] lines = File.ReadAllLines(filePath);
      foreach (string line in lines)
      {
        string[] parts = line.Split('|');
        if (parts.Length < 3) continue; //Skygg mot trasiga rader.
        string name = parts[0];
        string desc = parts[1];
        string owner = parts[2];

        // Hitta rätt trader och lägg till itemet i dennes lista
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
