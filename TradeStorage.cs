using System;
using System.Collections.Generic;
using System.IO;

namespace Trading_System
{
  public static class TradeStorage
  {
    private static string filePath = "Trades.txt";

    public static List<TradeRequest> LoadTrades(List<IUser> users)
    {
      List<TradeRequest> trades = new List<TradeRequest>();
      if (!File.Exists(filePath)) return trades;

      string[] lines = File.ReadAllLines(filePath);
      foreach (string line in lines)
      {
        string[] parts = line.Split('|');
        if (parts.Length < 6) continue;

        string fromUser = parts[0];
        string toUser = parts[1];
        string requestedName = parts[2];
        string offeredName = parts[3];
        string statusStr = parts[4];

        Item requested = null;
        Item offered = null;
        foreach (var u in users)
        {
          if (u is Trader tr)
          {
            foreach (var item in tr.GetItems())
            {
              if (item.GetName() == requestedName && item.GetOwnerUsername() == toUser) requested = item;
              if (item.GetName() == offeredName && item.GetOwnerUsername() == fromUser) offered = item;
            }
          }
        }

        if (requested != null && offered != null)
        {
          TradeRequest tr = new TradeRequest(fromUser, toUser, requested, offered);
          TradeStatus status;
          Enum.TryParse(statusStr, out status);
          if (status != TradeStatus.Pending) tr.Deny(); // Denied/Accepted kan justeras här om behövs
          trades.Add(tr);
        }
      }
      return trades;
    }

    public static void SaveTrades(List<TradeRequest> trades)
    {
      List<string> lines = new List<string>();
      foreach (var tr in trades)
      {
        lines.Add($"{tr.GetFromUser()}|{tr.GetToUser()}|{tr.GetRequestedItem().GetName()}|{tr.GetOfferedItem().GetName()}|{tr.GetStatus()}");
      }
      File.WriteAllLines(filePath, lines);
    }
  }
}
