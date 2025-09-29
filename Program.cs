using System;
using System.Collections.Generic;
using Trading_System;

class Program
{
  static void Main()
  {
    List<IUser> users = new List<IUser>();
    List<Message> messages = new List<Message>();
    List<TradeRequest> tradeRequests = new List<TradeRequest>();

    users.Add(new Admin("edvin@hotmail.com", "password123", "edvin"));

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
            for (int i = 0; i < users.Count; i++)
            {
              if (users[i].GetUsername() == newUser)
              {
                exists = true;
                break;
              }
            }

            if (exists)
            {
              Console.WriteLine("User already exists.");
            }
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
            for (int i = 0; i < users.Count; i++)
            {
              if (users[i].GetUsername() == username)
              {
                found = users[i];
                break;
              }
            }

            if (found == null)
            {
              Console.WriteLine("User not found.");
            }
            else if (found.TryLogin(username, password))
            {
              Console.WriteLine("Login successful.");
              activeUser = found;
            }
            else
            {
              Console.WriteLine("Login failed.");
            }
            Console.ReadLine();
            break;

          case "0":
            running = false;
            break;
        }
      }
      else
      {
        Console.WriteLine($"Welcome {activeUser.GetName()} ({activeUser.GetRole()})");
        Console.WriteLine("1. Upload Item");
        Console.WriteLine("2. Show My Items");
        Console.WriteLine("3. Show User Items");
        Console.WriteLine("4. Show All Items");
        Console.WriteLine("5. Send Trade Request");
        Console.WriteLine("6. Show Trade Requests");
        Console.WriteLine("9. Logout");
        Console.Write("Choice: ");
        string input = Console.ReadLine();

        if (input == "1")
        {
          if (activeUser is Trader t)
          {
            Console.Write("Item Name: ");
            string name = Console.ReadLine();
            Console.Write("Item Description: ");
            string desc = Console.ReadLine();
            t.AddItem(name, desc);
            Console.WriteLine("Item uploaded.");
          }
          else
            Console.WriteLine("Only traders can upload items.");
          Console.ReadLine();
        }
        else if (input == "2")
        {
          if (activeUser is Trader t)
          {
            Console.WriteLine("Your items:");
            t.ShowItems();
          }
          else
            Console.WriteLine("Only traders have items.");
          Console.ReadLine();
        }
        else if (input == "3")
        {
          Console.Write("Enter username to view items: ");
          string uname = Console.ReadLine();
          IUser userFound = null;
          for (int i = 0; i < users.Count; i++)
          {
            if (users[i].GetUsername() == uname)
            {
              userFound = users[i];
              break;
            }
          }

          if (userFound != null && userFound is Trader traderUser)
          {
            traderUser.ShowItems();
          }
          else
            Console.WriteLine("Trader not found.");
          Console.ReadLine();
        }
        else if (input == "4")
        {
          for (int i = 0; i < users.Count; i++)
          {
            if (users[i] is Trader tr)
            {
              Console.WriteLine($"User: {tr.GetUsername()}");
              tr.ShowItems();
            }
          }
          Console.ReadLine();
        }
        else if (input == "5")
        {
          if (activeUser is Trader t)
          {
            Console.WriteLine("Select user to request item from:");
            for (int i = 0; i < users.Count; i++)
            {
              if (users[i] is Trader && users[i] != activeUser)
                Console.WriteLine($"{i + 1}. {users[i].GetUsername()}");
            }
            int userIndex = int.Parse(Console.ReadLine()) - 1;
            if (userIndex >= 0 && userIndex < users.Count && users[userIndex] is Trader targetTrader)
            {
              Console.WriteLine("Select item to request:");
              targetTrader.ShowItems();
              int itemIndex = int.Parse(Console.ReadLine()) - 1;
              if (itemIndex >= 0 && itemIndex < targetTrader.GetItems().Count)
              {
                Item requested = targetTrader.GetItems()[itemIndex];

                Console.WriteLine("Select your own item to offer:");
                t.ShowItems();
                int offerIndex = int.Parse(Console.ReadLine()) - 1;
                if (offerIndex >= 0 && offerIndex < t.GetItems().Count)
                {
                  Item offered = t.GetItems()[offerIndex];

                  tradeRequests.Add(new TradeRequest(t.GetUsername(), targetTrader.GetUsername(), requested, offered));
                  messages.Add(new Message(t.GetUsername(), targetTrader.GetUsername(), $"Trade request sent for item {requested.GetName()} offering {offered.GetName()}"));
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
          for (int i = 0; i < tradeRequests.Count; i++)
          {
            if (tradeRequests[i].GetToUser() == activeUser.GetUsername())
              tradeRequests[i].Show();
          }

          Console.WriteLine("Do you want to respond? (Y/N)");
          if (Console.ReadLine().ToUpper() == "Y")
          {
            Console.Write("Enter request number to respond: ");
            int rIndex = int.Parse(Console.ReadLine()) - 1;
            if (rIndex >= 0 && rIndex < tradeRequests.Count && tradeRequests[rIndex].GetToUser() == activeUser.GetUsername())
            {
              Console.Write("Accept (A) / Deny (D)? ");
              string resp = Console.ReadLine().ToUpper();
              if (resp == "A")
                tradeRequests[rIndex].Accept();
              else if (resp == "D")
                tradeRequests[rIndex].Deny();
            }
          }
          Console.ReadLine();
        }
        else if (input == "9")
        {
          activeUser = null;
        }
      }
    }
  }
}
