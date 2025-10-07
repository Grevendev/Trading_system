using System;
using System.Collections.Generic;
using Trading_System;


// Ladda all sparad data vid programstart
List<IUser> users = Logger.LoadUsers();
List<Message> messages = MessageStorage.LoadMessages();
List<TradeRequest> tradeRequests = Logger.LoadTrades(users);
Logger.LoadItems(users);

IUser activeUser = null; // Håller reda på inloggad användare
bool running = true;

while (running)
{
  Console.Clear();

  if (activeUser == null)
  {
    // Meny för ej inloggad användare
    Console.WriteLine("Trading System");
    Console.WriteLine("1. Register new Trader");
    Console.WriteLine("2. Login");
    Console.WriteLine("0. Exit");
    Console.Write("Choice: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
      case "1":
        // Skapa ny trader
        Console.Write("Enter username: ");
        string newUser = Console.ReadLine();
        Console.Write("Enter password: ");
        string newPass = Console.ReadLine();

        bool exists = false;
        foreach (var u in users)
          if (u.GetUsername() == newUser) { exists = true; break; }

        if (exists) Console.WriteLine("User already exists.");
        else
        {
          users.Add(new Trader(newUser, newPass, newUser));
          Console.WriteLine("Trader registered successfully.");
        }
        Console.ReadLine();
        break;

      case "2":
        // Logga in
        Console.Write("Username: ");
        string username = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        IUser found = null;
        foreach (var u in users)
          if (u.GetUsername() == username) { found = u; break; }

        if (found == null) Console.WriteLine("User not found.");
        else if (!found.GetIsActive()) Console.WriteLine("Account inactive.");
        else if (found.TryLogin(username, password)) { activeUser = found; Console.WriteLine("Login successful."); }
        else Console.WriteLine("Login failed.");
        Console.ReadLine();
        break;

      case "0":
        running = false;
        break;
    }
  }
  else
  {
    // Meny för inloggad användare
    Console.WriteLine($"Welcome {activeUser.GetName()} ({activeUser.GetRole()})");
    Console.WriteLine("1. Upload Item");
    Console.WriteLine("2. Show My Items");
    Console.WriteLine("3. Show User Items");
    Console.WriteLine("4. Show All Items");
    Console.WriteLine("5. Send Trade Request");
    Console.WriteLine("6. Show Trade Requests");
    Console.WriteLine("7. View Users");
    Console.WriteLine("8. Send Message");
    Console.WriteLine("9. Inbox");
    Console.WriteLine("10. Completed Trade Requests");
    Console.WriteLine("11. Logout");
    Console.Write("Choice: ");
    string input = Console.ReadLine();

    // 1. Ladda upp item
    if (input == "1")
    {
      if (activeUser is Trader trader1)
      {
        Console.Write("Item Name: ");
        string name = Console.ReadLine();
        Console.Write("Item Description: ");
        string desc = Console.ReadLine();

        trader1.AddItem(name, desc);
        Console.WriteLine("Item uploaded.");
      }
      else Console.WriteLine("Only traders can upload items.");
      Console.ReadLine();
    }

    // 2. Visa mina items
    else if (input == "2")
    {
      if (activeUser is Trader trader2)
      {
        Console.WriteLine("Your items:");
        trader2.ShowItems();
      }
      else Console.WriteLine("Only traders have items.");
      Console.ReadLine();
    }

    // 3. Visa specifik användares items
    else if (input == "3")
    {
      Console.Write("Enter username to view items: ");
      string uname = Console.ReadLine();
      IUser userFound = null;
      foreach (var u in users)
        if (u.GetUsername() == uname) { userFound = u; break; }

      if (userFound is Trader trader) trader.ShowItems();
      else Console.WriteLine("Trader not found.");
      Console.ReadLine();
    }

    // 4. Visa alla items
    else if (input == "4")
    {
      foreach (var u in users)
        if (u is Trader tr) { Console.WriteLine($"User: {tr.GetUsername()}"); tr.ShowItems(); }
      Console.ReadLine();
    }

    // 5. Skicka trade request
    else if (input == "5")
    {
      if (activeUser is Trader traderFrom)
      {
        Console.WriteLine("Select user to request item from:");
        int index = 1;
        List<Trader> tradersList = new List<Trader>();
        foreach (var u in users)
        {
          if (u is Trader t && t != traderFrom)
          {
            Console.WriteLine($"{index}. {t.GetUsername()}");
            tradersList.Add(t);
            index++;
          }
        }

        Console.Write("Choice: ");
        int sel = int.Parse(Console.ReadLine()) - 1;
        if (sel >= 0 && sel < tradersList.Count)
        {
          Trader targetTrader = tradersList[sel];
          Console.WriteLine("Select item to request:");
          targetTrader.ShowItems();
          int itemIndex = int.Parse(Console.ReadLine()) - 1;
          if (itemIndex >= 0 && itemIndex < targetTrader.GetItems().Count)
          {
            Item requested = targetTrader.GetItems()[itemIndex];

            Console.WriteLine("Select your own item to offer:");
            traderFrom.ShowItems();
            int offerIndex = int.Parse(Console.ReadLine()) - 1;
            if (offerIndex >= 0 && offerIndex < traderFrom.GetItems().Count)
            {
              Item offered = traderFrom.GetItems()[offerIndex];

              tradeRequests.Add(new TradeRequest(traderFrom.GetUsername(), targetTrader.GetUsername(), requested, offered));
              messages.Add(new Message(traderFrom.GetUsername(), targetTrader.GetUsername(),
                  $"Trade request sent for item {requested.GetName()} offering {offered.GetName()}"));
              Console.WriteLine("Trade request sent.");
            }
          }
        }
      }
      Console.ReadLine();
    }

    // 6. Visa och svara på inkommande trade requests
    else if (input == "6")
    {
      List<TradeRequest> incoming = new List<TradeRequest>();
      int rNum = 1;
      foreach (var tr in tradeRequests)
      {
        if (tr.GetToUser() == activeUser.GetUsername() && tr.GetStatus() == TradeStatus.Pending)
        {
          Console.WriteLine($"{rNum}. From: {tr.GetFromUser()} Requested: {tr.GetRequestedItem().GetName()} Offered: {tr.GetOfferedItem().GetName()}");
          incoming.Add(tr);
          rNum++;
        }
      }

      if (incoming.Count == 0) { Console.WriteLine("No incoming trade requests."); Console.ReadLine(); continue; }

      Console.WriteLine("Do you want to respond? (Y/N)");
      if (Console.ReadLine().ToUpper() == "Y")
      {
        Console.Write("Enter request number to respond: ");
        int rIndex = int.Parse(Console.ReadLine()) - 1;
        if (rIndex >= 0 && rIndex < incoming.Count)
        {
          var tr = incoming[rIndex];
          Console.Write("Accept (A) / Deny (D)? ");
          string resp = Console.ReadLine().ToUpper();
          if (resp == "A") tr.Accept(users);
          else if (resp == "D") tr.Deny();
        }
      }
      Console.ReadLine();
    }

    // 7. Visa alla användare
    else if (input == "7")
    {
      Console.WriteLine("Users:");
      foreach (var u in users) u.Info();
      Console.ReadLine();
    }

    // 8. Skicka meddelande
    else if (input == "8")
    {
      Console.Write("Send to (username): ");
      string to = Console.ReadLine();
      Console.Write("Message: ");
      string content = Console.ReadLine();

      IUser recipient = null;
      foreach (var u in users)
        if (u.GetUsername() == to) { recipient = u; break; }

      if (recipient != null)
      {
        messages.Add(new Message(activeUser.GetUsername(), to, content));
        MessageStorage.SaveMessages(messages);
        Console.WriteLine("Message sent.");
      }
      else Console.WriteLine("User not found.");
      Console.ReadLine();
    }

    // 9. Inbox
    else if (input == "9")
    {
      Console.WriteLine("Inbox:");
      foreach (var m in messages)
        if (m.GetTo() == activeUser.GetUsername()) m.Show();
      Console.ReadLine();
    }

    // 10. Completed trade requests
    else if (input == "10")
    {
      Console.WriteLine("Completed trade requests:");
      foreach (var tr in tradeRequests)
        if (tr.GetStatus() != TradeStatus.Pending) tr.Show();
      Console.ReadLine();
    }

    // 11. Logout
    else if (input == "11")
    {
      activeUser = null;
    }
  }
}

// Spara all data innan programmet stängs
Logger.SaveUsers(users);
Logger.SaveItems(users);
Logger.SaveTrades(tradeRequests);
MessageStorage.SaveMessages(messages);


