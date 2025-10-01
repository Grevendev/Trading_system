using System;
using System.Collections.Generic;
using Trading_System;

class Program
{
  static void Main()
  {
    List<IUser> users = Logger.LoadUsers();
    List<Message> messages = Logger.LoadMessages();
    List<TradeRequest> tradeRequests = new List<TradeRequest>();
    Logger.LoadTrades(users, tradeRequests);
    Logger.LoadItems(users);

    IUser activeUser = null;
    bool running = true;

    while (running)
    {
      Console.Clear();
      if (activeUser == null)
      {
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
              users.Add(new Trader(newUser, newPass));
              Console.WriteLine("Trader registered successfully.");
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
        // Inloggad meny 
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
        else if (input == "3")
        {
          Console.Write("Enter username to view items: ");
          string uname = Console.ReadLine();
          IUser userFound = users.Find(u => u.GetUsername() == uname);
          if (userFound is Trader trader) trader.ShowItems();
          else Console.WriteLine("Trader not found.");
          Console.ReadLine();
        }
        else if (input == "4")
        {
          foreach (var u in users)
            if (u is Trader tr) { Console.WriteLine($"User: {tr.GetUsername()}"); tr.ShowItems(); }
          Console.ReadLine();
        }
        else if (input == "5")
        {
          if (activeUser is Trader traderFrom)
          {
            Console.WriteLine("Select user to request item from:");
            int index = 1;
            foreach (var u in users)
            {
              if (u is Trader t && t != traderFrom)
              {
                Console.WriteLine($"{index}. {t.GetUsername()}");
                index++;
              }
            }

            int sel = int.Parse(Console.ReadLine()) - 1;
            Trader targetTrader = null;
            foreach (var u in users)
            {
              if (u is Trader t && t != traderFrom && sel-- == 0) { targetTrader = t; break; }
            }

            if (targetTrader != null)
            {
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
                  messages.Add(new Message(traderFrom.GetUsername(), targetTrader.GetUsername(), $"Trade request sent for item {requested.GetName()} offering {offered.GetName()}"));
                  Console.WriteLine("Trade request sent.");
                }
              }
            }
          }
          Console.ReadLine();
        }
        else if (input == "6")
        {
          Console.WriteLine("Your incoming trade requests:");
          int rNum = 1;
          foreach (var tr in tradeRequests)
            if (tr.GetToUser() == activeUser.GetUsername()) { Console.WriteLine($"{rNum}."); tr.Show(); rNum++; }

          Console.WriteLine("Do you want to respond? (Y/N)");
          if (Console.ReadLine().ToUpper() == "Y")
          {
            Console.Write("Enter request number to respond: ");
            int rIndex = int.Parse(Console.ReadLine()) - 1;
            if (rIndex >= 0 && rIndex < tradeRequests.Count)
            {
              var tr = tradeRequests[rIndex];
              if (tr.GetToUser() == activeUser.GetUsername())
              {
                Console.Write("Accept (A) / Deny (D)? ");
                string resp = Console.ReadLine().ToUpper();
                if (resp == "A") tr.Accept();
                else if (resp == "D") tr.Deny();
              }
            }
          }
          Console.ReadLine();
        }
        else if (input == "7")
        {
          Console.WriteLine("Users:");
          foreach (var u in users) u.Info();
          Console.ReadLine();
        }
        else if (input == "8")
        {
          Console.Write("Send to (username): ");
          string to = Console.ReadLine();
          Console.Write("Message: ");
          string content = Console.ReadLine();

          var recipient = users.Find(u => u.GetUsername() == to);
          if (recipient != null)
          {
            messages.Add(new Message(activeUser.GetUsername(), to, content));
            Console.WriteLine("Message sent.");
          }
          else Console.WriteLine("User not found.");
          Console.ReadLine();
        }
        else if (input == "9")
        {
          Console.WriteLine("Inbox:");
          foreach (var m in messages)
            if (m.GetTo() == activeUser.GetUsername()) m.Show();
          Console.ReadLine();
        }
        else if (input == "10")
        {
          Console.WriteLine("Completed trade requests:");
          foreach (var tr in tradeRequests)
            if (tr.GetStatus() != TradeStatus.Pending) tr.Show();
          Console.ReadLine();
        }
        else if (input == "11")
        {
          activeUser = null;
        }
      }
    }

    // Save all data before exit.
    Logger.SaveUsers(users);
    Logger.SaveItems(users);
    Logger.SaveMessages(messages);
    Logger.SaveTrades(tradeRequests);
  }
}
