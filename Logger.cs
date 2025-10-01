using System;
using System.Collections.Generic;
using System.IO;
using Trading_System;

namespace Trading_System
{
  //
  // Logger
  //
  // Denna statiska klass hanterar all fil-läsning och fil-skrivning
  //för systemet. Här sparas och laddas:
  //Användare (Traders och Admins)
  //Items
  //Meddelande
  //TradeRequests
  //
  //Genom att seperara filhantering från själva programlogiken
  //följer vi Single Responsibility Principle och gör systemet mer modulärt.
  public static class Logger
  {
    // Filnamn för respektive tpy av data
    private static string userFile = "users.txt";
    private static string itemFile = "items.txt";
    private static string messageFile = "messages.txt";
    private static string tradeFile = "trades.txt";

    // Users
    // 
    //Spara användare till fil
    public static void SaveUsers(List<IUser> users)
    {
      using (StreamWriter sw = new StreamWriter(userFile))
      {
        foreach (var u in users)
        {
          //Skriv alla viktiga attribut med '|' som separator
          sw.WriteLine($"{u.GetRole()}|{u.GetUsername()}|{u.GetPassword()}|{u.GetName()}|{u.GetIsActive()}");
        }
      }
    }
    //Ladda användare från fil

    public static List<IUser> LoadUsers()
    {
      List<IUser> users = new List<IUser>();
      if (!File.Exists(userFile)) return users; // Retunera tom lista om fil inte finns

      string[] lines = File.ReadAllLines(userFile);
      foreach (var line in lines)
      {
        string[] parts = line.Split('|');
        if (parts.Length < 5) continue; // Säkerställer att raden har tillräckligt många delar.

        string username = parts[1];
        string passwordHash = parts[2];
        string name = parts[3];
        Role role = (Role)Enum.Parse(typeof(Role), parts[0]);
        bool isActive = bool.Parse(parts[4]);

        IUser user = null;
        if (role == Role.Admin)
          user = new Admin(username, passwordHash, name); // Skapa Admin
        else if (role == Role.Trader)
        {
          Trader t = new Trader(username, "temp", name); //Temporärt lösenord
          t.SetPasswordHash(passwordHash); // Återställ hash från fil
          user = t;
        }

        if (user != null)
          user.SetIsActive(isActive); // Sätt kontots aktiva status

        users.Add(user);
      }

      return users;
    }

    // 
    // Items
    //
    //Spara alla items till fil
    public static void SaveItems(List<IUser> users)
    {
      using (StreamWriter sw = new StreamWriter(itemFile))
      {
        foreach (var u in users)
        {
          if (u is Trader t)
          {
            foreach (var item in t.GetItems())
            {
              //Spara varje item med ägare, namn och beskriving
              sw.WriteLine($"{item.GetOwnerUsername()}|{item.GetName()}|{item.GetDescription()}");
            }
          }
        }
      }
    }
    //Ladda items frpn fil och associera med rätt Trader

    public static void LoadItems(List<IUser> users)
    {
      if (!File.Exists(itemFile)) return;

      string[] lines = File.ReadAllLines(itemFile);
      foreach (var line in lines)
      {
        string[] parts = line.Split('|');
        if (parts.Length < 3) continue;

        string ownerUsername = parts[0];
        string name = parts[1];
        string desc = parts[2];

        Trader owner = null;
        foreach (var u in users)
        {
          if (u is Trader t && t.GetUsername() == ownerUsername)
          {
            owner = t;
            break;
          }
        }

        if (owner != null)
          owner.AddItem(name, desc); // Lägger till item hos rätt ägare
      }
    }

    // 
    // Messages
    // 
    //Spara meddelanden
    public static void SaveMessages(List<Message> messages)
    {
      using (StreamWriter sw = new StreamWriter(messageFile))
      {
        foreach (var m in messages)
        {
          sw.WriteLine($"{m.GetFrom()}|{m.GetTo()}|{m.GetContent()}");
        }
      }
    }
    // Ladda medelanden
    public static List<Message> LoadMessages()
    {
      List<Message> messages = new List<Message>();
      if (!File.Exists(messageFile)) return messages;

      string[] lines = File.ReadAllLines(messageFile);
      foreach (var line in lines)
      {
        string[] parts = line.Split('|');
        if (parts.Length >= 3)
          messages.Add(new Message(parts[0], parts[1], parts[2]));
      }

      return messages;
    }


    // TradeRequests
    // Spara trade requests

    public static void SaveTrades(List<TradeRequest> trades)
    {
      using (StreamWriter sw = new StreamWriter(tradeFile))
      {
        foreach (var t in trades)
        {
          //Spara information om fromUser, toUser, itemnamn och status
          sw.WriteLine($"{t.GetFromUser()}|{t.GetToUser()}|{t.GetRequestedItem().GetName()}|{t.GetOfferedItem().GetName()}|{t.GetStatus()}");
        }
      }
    }
    // Ladda trade requests och koppla items med rätt traders
    public static void LoadTrades(List<IUser> users, List<TradeRequest> trades)
    {
      if (!File.Exists(tradeFile)) return;

      string[] lines = File.ReadAllLines(tradeFile);
      foreach (var line in lines)
      {
        string[] parts = line.Split('|');
        if (parts.Length < 5) continue;

        string fromUser = parts[0];
        string toUser = parts[1];
        string requestedName = parts[2];
        string offeredName = parts[3];
        TradeStatus status = (TradeStatus)Enum.Parse(typeof(TradeStatus), parts[4]);

        Trader fromTrader = null;
        Trader toTrader = null;
        Item requested = null;
        Item offered = null;
        // Hitta traders som motsvarar fromUser och toUser
        foreach (var u in users)
        {
          if (u is Trader t)
          {
            if (t.GetUsername() == fromUser) fromTrader = t;
            if (t.GetUsername() == toUser) toTrader = t;
          }
        }
        //Koppla items till trade request
        if (fromTrader != null && toTrader != null)
        {
          foreach (var item in toTrader.GetItems())
            if (item.GetName() == requestedName) requested = item;
          foreach (var item in fromTrader.GetItems())
            if (item.GetName() == offeredName) offered = item;

          if (requested != null && offered != null)
          {
            TradeRequest tr = new TradeRequest(fromUser, toUser, requested, offered);
            if (status == TradeStatus.Accepted) tr.Accept(); // Markera som accepterad
            else if (status == TradeStatus.Denied) tr.Deny(); // Markera som nekad
            trades.Add(tr);
          }
        }
      }
    }
  }
}
