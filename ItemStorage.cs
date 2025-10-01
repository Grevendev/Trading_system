using System;
using System.Collections.Generic;
using System.IO;
using Trading_System;

namespace Trading_System
{
  //
  //ItemSorage
  //
  //Den här klassen ansvarar för att spara och ladda
  //alla "Items" (föremål) i systemet.
  //
  //Varför en separat klass?
  //Separation of concerns (jag delar upp logik -> användare sparas i Logger, items i ItemStorgae)
  //Gör koden mer modulär och lättare att underhålla
  //Möjliggör framtida utbyggnad (t.ex. spara i JSON eller databs iställer för textfil).
  public static class ItemStorage
  {
    //Bestämmer filens namn där items sparas.
    //Just nu används en enkel textfil för persistens.
    private static string filePath = "items.txt";
    //
    // SaveItems
    //
    //Syfte:
    //Spara alla items som tillhör traders till en fil.
    //
    // Hur fungerar det?
    //1. Vi går igenom alla användare.
    //2. Om användaren är en Trader (endast de kan äga items), hämtas deras items.
    //3. Varje item sparas som en rad i filen.
    //Format: OwnerUsername|ItemName|ItemDescription
    //
    //Fördelar:
    //Enkel och tydig struktur
    //Lätt att återställa vid laddning

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
              // Skriver ner äagre, namn och beskrirving av item
              sw.WriteLine($"{item.GetOwnerUsername()}|{item.GetName()}|{item.GetDescription()}");
            }
          }
        }
      }
    }

    // LoadItems
    //
    //Syfte:
    //Läsa tillbka alla items från filen och återställa de till rätt trader i systemet.
    //
    //Hur fungerar det?
    //1. Kontrollerar om filen finns - om inte, retunerar direkt.
    //2. Läser varje rad och delar upp innehållet med "Split('|')".
    //3. Identifierar ägaren (Trader) med hjälp av användarnamn.
    //4. Lägger till item i rätt Trader-Objekt.
    //
    //Varför så här?
    //Genom att lagra användarnamn som identifierar kan vi 
    //koppla items till rätt användare utan att behöva
    //spara referenser (vilket inte fungerar mellan programkörningar).
    public static void LoadItems(List<IUser> users)
    {
      if (!File.Exists(filePath)) return;

      using (StreamReader sr = new StreamReader(filePath))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          //Dela upp raden i sina beståndsdelar
          string[] parts = line.Split('|');
          if (parts.Length < 3) continue; // Skydd mot korrupt data

          string owner = parts[0];
          string itemName = parts[1];
          string itemDesc = parts[2];

          // Leta upp rätt användare (ägaren)
          IUser user = null;
          foreach (var u in users)
          {
            if (u.GetUsername() == owner)
            {
              user = u;
              break;
            }
          }
          //Om ägaren är en Trader, lägg till item.

          if (user != null && user is Trader t)
          {
            t.AddItem(itemName, itemDesc);
          }
        }
      }
    }
  }
}
