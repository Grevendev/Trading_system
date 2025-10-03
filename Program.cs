using System;
using System.Collections.Generic;
using Trading_System;

class Program
{
  static void Main()
  {
    // Ladda all data vid start
    List<IUser> users = Logger.LoadUsers();
    List<Message> messages = Logger.LoadMessages();
    List<TradeRequest> tradeRequests = Logger.LoadTrades(users);
    Logger.LoadItems(users);

    IUser activeUser = null;
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
              Logger.SaveUsers(users);
            }
            Console.ReadLine();
            break;

          case "2":
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

        // 1. Upload item
        if (input == "1" && activeUser is Trader trader1)
        {
          Console.Write("Item Name: ");
          string name = Console.ReadLine();
          Console.Write("Item Description: ");
          string desc = Console.ReadLine();
          trader1.AddItem(name, desc);
          Logger.SaveItems(users);
          Console.WriteLine("Item uploaded.");
          Console.ReadLine();
        }
        else if (input == "1") { Console.WriteLine("Only traders can upload items."); Console.ReadLine(); }

        // 2. Show my items
        else if (input == "2" && activeUser is Trader trader2)
        {
          Console.WriteLine("Your items:");
          trader2.ShowItems();
          Console.ReadLine();
        }
        else if (input == "2") { Console.WriteLine("Only traders have items."); Console.ReadLine(); }

        // 3. Show specific user's items
        else if (input == "3")
        {
          Console.Write("Enter username: ");
          string uname = Console.ReadLine();
          IUser uFound = null;
          foreach (var u in users)
            if (u.GetUsername() == uname) { uFound = u; break; }

          if (uFound is Trader tr) tr.ShowItems();
          else Console.WriteLine("Trader not found.");
          Console.ReadLine();
        }

        // 4. Show all items
        else if (input == "4")
        {
          foreach (var u in users)
            if (u is Trader tr) { Console.WriteLine($"User: {tr.GetUsername()}"); tr.ShowItems(); }
          Console.ReadLine();
        }

        // 5. Send trade request
        else if (input == "5" && activeUser is Trader traderFrom)
        {
          Console.WriteLine("Select user to request item from:");
          int idx = 1;
          Dictionary<int, Trader> userMap = new Dictionary<int, Trader>();
          foreach (var u in users)
            if (u is Trader t && t != traderFrom) { Console.WriteLine($"{idx}. {t.GetUsername()}"); userMap[idx] = t; idx++; }

          Console.Write("Choice: ");
          int sel = int.Parse(Console.ReadLine());
          if (!userMap.ContainsKey(sel)) { Console.WriteLine("Invalid choice."); Console.ReadLine(); continue; }
          Trader targetTrader = userMap[sel];

          Console.WriteLine("Select item to request:");
          targetTrader.ShowItems();
          Console.Write("Item number: ");
          int itemIndex = int.Parse(Console.ReadLine()) - 1;
          if (itemIndex < 0 || itemIndex >= targetTrader.GetItems().Count) { Console.WriteLine("Invalid item."); Console.ReadLine(); continue; }
          Item requested = targetTrader.GetItems()[itemIndex];

          Console.WriteLine("Select your own item to offer:");
          traderFrom.ShowItems();
          Console.Write("Item number: ");
          int offerIndex = int.Parse(Console.ReadLine()) - 1;
          if (offerIndex < 0 || offerIndex >= traderFrom.GetItems().Count) { Console.WriteLine("Invalid item."); Console.ReadLine(); continue; }
          Item offered = traderFrom.GetItems()[offerIndex];

          tradeRequests.Add(new TradeRequest(traderFrom.GetUsername(), targetTrader.GetUsername(), requested, offered));
          Console.WriteLine("Trade request sent.");
          Logger.SaveTrades(tradeRequests);
          Console.ReadLine();
        }

        // 6. Show trade requests
        else if (input == "6")
        {
          Console.WriteLine("Incoming trade requests:");
          Dictionary<int, TradeRequest> reqMap = new Dictionary<int, TradeRequest>();
          int i = 1;
          foreach (var tr in tradeRequests)
            if (tr.GetToUser() == activeUser.GetUsername() && tr.GetStatus() == TradeStatus.Pending)
            {
              Console.WriteLine($"{i}. From: {tr.GetFromUser()} | Requested: {tr.GetRequestedItem().GetName()} | Offered: {tr.GetOfferedItem().GetName()}");
              reqMap[i] = tr;
              i++;
            }

          if (reqMap.Count == 0) { Console.WriteLine("No pending requests."); Console.ReadLine(); continue; }

          Console.Write("Do you want to respond? (Y/N): ");
          if (Console.ReadLine().ToUpper() == "Y")
          {
            Console.Write("Enter request number: ");
            int choiceNum = int.Parse(Console.ReadLine());
            if (!reqMap.ContainsKey(choiceNum)) { Console.WriteLine("Invalid number."); Console.ReadLine(); continue; }
            TradeRequest selected = reqMap[choiceNum];

            Console.Write("Accept (A) / Deny (D)? ");
            string resp = Console.ReadLine().ToUpper();
            if (resp == "A") selected.Accept(users);
            else if (resp == "D") selected.Deny();
            Logger.SaveTrades(tradeRequests);
            Logger.SaveItems(users);
            Console.WriteLine("Response recorded.");
            Console.ReadLine();
          }
        }

        // 7. View users
        else if (input == "7") { foreach (var u in users) u.Info(); Console.ReadLine(); }

        // 8. Send message
        else if (input == "8")
        {
          Console.Write("Send to username: ");
          string to = Console.ReadLine();
          Console.Write("Message: ");
          string content = Console.ReadLine();
          messages.Add(new Message(activeUser.GetUsername(), to, content));
          Console.WriteLine("Message sent.");
          Console.ReadLine();
        }

        // 9. Inbox
        else if (input == "9") { foreach (var m in messages) if (m.GetTo() == activeUser.GetUsername()) m.Show(); Console.ReadLine(); }

        // 10. Completed trade requests
        else if (input == "10")
        {
          foreach (var tr in tradeRequests)
            if (tr.GetStatus() != TradeStatus.Pending) tr.Show();
          Console.ReadLine();
        }

        // 11. Logout
        else if (input == "11") activeUser = null;
      }
    }

    // Spara allt vid avslut
    Logger.SaveUsers(users);
    Logger.SaveItems(users);
    Logger.SaveTrades(tradeRequests);
  }
}
