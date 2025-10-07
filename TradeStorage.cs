using System;
using System.Collections.Generic;
using System.IO;

namespace Trading_System
{
  //TradeStorage ansvarar för att spara och läsa in bytesförfrågningar (TradeRequests) till/från fil.
  //Detta gör det möjligt att bevara trade-requests mellan programkörningar.
  public static class TradeStorage
  {

    //Fil där alla trade requests sparas.
    private static string filePath = "Trades.txt";

    //Laddar alla trade requests från filen.
    //För varje rad skapas ett TradeRequest-obejekt om både det begärda och det erbjudna itemet finns.
    //"users" Lista av alla användare, används för att matcha items med sina ägare
    //Lista av TradeRequest
    public static List<TradeRequest> LoadTrades(List<IUser> users)
    {
      List<TradeRequest> trades = new List<TradeRequest>();

      //Om filen inte finns, retunera tom
      if (!File.Exists(filePath)) return trades;

      //Läs alla rader från filen.
      string[] lines = File.ReadAllLines(filePath);
      foreach (string line in lines)
      {
        //varje rad har formatet: fromUser|toUser|requestedItemName|offeredItemName|status
        string[] parts = line.Split('|');
        if (parts.Length < 6) continue; //Hoppa över felaktiga rader.

        string fromUser = parts[0];
        string toUser = parts[1];
        string requestedName = parts[2];
        string offeredName = parts[3];
        string statusStr = parts[4];

        Item requested = null;
        Item offered = null;

        //Leta upp de faktiska Item-objeten baserat på namn och ägare.
        foreach (var u in users)
        {
          if (u is Trader tr)
          {
            foreach (var item in tr.GetItems())
            {
              // Matcha begärt item med mottagarens ägande
              if (item.GetName() == requestedName && item.GetOwnerUsername() == toUser) requested = item;

              //Matcha erbjudet item med ansvändarens ägande
              if (item.GetName() == offeredName && item.GetOwnerUsername() == fromUser) offered = item;
            }
          }
        }

        //Om både begärt och erbjudet item hittas, skapa TradeRequest
        if (requested != null && offered != null)
        {
          TradeRequest tr = new TradeRequest(fromUser, toUser, requested, offered);

          //Konventera status-strängen från filen till TradeStatus enum
          TradeStatus status;
          Enum.TryParse(statusStr, out status);

          //Om staus inte är Pending, markera som Denied (kan justeras till Accepted om logiken ändras)
          if (status != TradeStatus.Pending) tr.Deny();
          trades.Add(tr);
        }
      }
      return trades;
    }


    //Sparar alla TradeRequests till fil.
    //Varje TradeRequest sparas på en rad med formatet:
    //fromUser|toUser|requestedItemName|offeredItemName|status
    //"trades"Lista av TradeRequest som ska sparas
    public static void SaveTrades(List<TradeRequest> trades)
    {
      List<string> lines = new List<string>();
      foreach (var tr in trades)
      {
        //Skapa en rad per tarde request
        lines.Add($"{tr.GetFromUser()}|{tr.GetToUser()}|{tr.GetRequestedItem().GetName()}|{tr.GetOfferedItem().GetName()}|{tr.GetStatus()}");
      }
      //Skriv alla rader till filen. 
      File.WriteAllLines(filePath, lines);
    }
  }
}
