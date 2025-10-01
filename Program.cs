using System;
using System.Collections.Generic;
using Trading_System;

class Program
{
  static void Main()
  {
    // Vid programstart
    // 
    // Här laddar vi in all tidigare sparad information så att
    // systemt blir "bestående" mellan körningar. Detta gör att
    // anvvändare, items, meddelande och trade requests inte försvinner
    // när man stänger ner programmet. 


    List<IUser> users = Logger.LoadUsers(); // Ladda alla användare
    List<Message> messages = Logger.LoadMessages(); // Ladda alla meddelande
    List<TradeRequest> tradeRequests = new List<TradeRequest>(); // Skapar lista för trade requests
    Logger.LoadTrades(users, tradeRequests); // Kopplar trades till användarna 
    Logger.LoadItems(users); // Laddar in items kopplade till respektive trader.

    // Variabel som håller koll på den användare som är inloggad just nu.
    // Är den null -> ingen användare inloggad.
    IUser activeUser = null;

    // Boolean som styr huvudloopen. Så länge "running" är true
    //kör programmet. När användaren väljer att avsluta sätts den till false.
    bool running = true;

    //Huvudloop

    // Här körs programmets huvudloop. Den rensar skärmen vid varje iteration
    // och kollar om någon är inloggad eller ej.
    // Beroende på detta visas olika menyer.

    while (running)
    {
      Console.Clear(); // Rensa skärmen för tydlighet.

      // Meny för ej inloggad användare
      //
      // Här vissas alternativ för att registera sig, logga in eller avsluta.
      if (activeUser == null)
      {
        Console.WriteLine("Trading System");
        Console.WriteLine("1. Register new Trader"); // Skapa ny användare
        Console.WriteLine("2. Login"); // Logga in
        Console.WriteLine("0. Exit"); // Avsluta
        Console.Write("Choice: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
          case "1":
            // Nyregistering av trader.
            Console.Write("Enter username: ");
            string newUser = Console.ReadLine();
            Console.Write("Enter password: ");
            string newPass = Console.ReadLine();


            // Kontrollera om användarnamnet redan finns
            bool exists = false;
            foreach (var u in users)
              if (u.GetUsername() == newUser) { exists = true; break; }

            if (exists) Console.WriteLine("User already exists."); // Kan ej skapa dublicerad användare
            else
            {
              // Lägg till en ny trader i listan.
              users.Add(new Trader(newUser, newPass));
              Console.WriteLine("Trader registered successfully.");
            }
            Console.ReadLine(); // Vänta på knapptryck för att ge feedback.
            break;

          case "2":
            // Inlogging.
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            IUser found = null;
            // Leta upp användaren i listan.
            foreach (var u in users)
              if (u.GetUsername() == username) { found = u; break; }
            // Om användaren inte finns.
            if (found == null) Console.WriteLine("User not found.");
            // Om kontot är inaktivt
            else if (!found.GetIsActive()) Console.WriteLine("Account inactive.");
            // Om lösenordet stämmer
            else if (found.TryLogin(username, password)) { activeUser = found; Console.WriteLine("Login successful."); } // Sätt den inloggade användaren
            else Console.WriteLine("Login failed."); // Fel lösenord
            Console.ReadLine();
            break;

          case "0":
            // Avslutar programmet.
            running = false; // Avsluta loopen
            break;
        }
      }
      else
      {
        // Meny för inloggad användare.

        // Här listas alla funktioner som finns när en användare är inloggad.
        // Systemet känner av om det är en Trader eller Admin, men fokus ligger
        // på Trader eftersom de kan hantera item och trades.
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

        // 1. Ladda upp item.
        if (input == "1")
        {
          if (activeUser is Trader trader1)
          {
            // Användaren får ange namn och beskriving på sitt item.
            Console.Write("Item Name: ");
            string name = Console.ReadLine();
            Console.Write("Item Description: ");
            string desc = Console.ReadLine();

            // Lägger till item på användarens lista.
            trader1.AddItem(name, desc);
            Console.WriteLine("Item uploaded.");
          }
          else Console.WriteLine("Only traders can upload items."); // Admins kan ej ha items.
          Console.ReadLine();
        }

        // 2. Visa mina items.
        else if (input == "2")
        {
          if (activeUser is Trader trader2)
          {
            Console.WriteLine("Your items:");
            trader2.ShowItems(); // Skriver ut alla items användaren äger
          }
          else Console.WriteLine("Only traders have items.");
          Console.ReadLine();
        }
        // 3. Visa specifik användares items.
        else if (input == "3")
        {
          Console.Write("Enter username to view items: ");
          string uname = Console.ReadLine();
          IUser userFound = users.Find(u => u.GetUsername() == uname);
          if (userFound is Trader trader) trader.ShowItems();
          else Console.WriteLine("Trader not found.");
          Console.ReadLine();
        }
        // 4. Visa alla items i systemet.
        else if (input == "4")
        {
          foreach (var u in users)
            if (u is Trader tr) { Console.WriteLine($"User: {tr.GetUsername()}"); tr.ShowItems(); } // Skriver ut items per användare.
          Console.ReadLine();
        }
        // 5. Skicka trade request.
        else if (input == "5")
        {
          if (activeUser is Trader traderFrom)
          {
            // Användaren får först välja vilken tradeer de vill skicka förfrågan till.
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
            // Välj trader baserat på index
            int sel = int.Parse(Console.ReadLine()) - 1;
            Trader targetTrader = null;
            foreach (var u in users)
            {
              if (u is Trader t && t != traderFrom && sel-- == 0) { targetTrader = t; break; }
            }

            if (targetTrader != null)
            {
              // Välj vilket item man vill ha
              Console.WriteLine("Select item to request:");
              targetTrader.ShowItems();
              int itemIndex = int.Parse(Console.ReadLine()) - 1;
              if (itemIndex >= 0 && itemIndex < targetTrader.GetItems().Count)
              {
                Item requested = targetTrader.GetItems()[itemIndex];


                // Välj vilket eget item man erbjuder.
                Console.WriteLine("Select your own item to offer:");
                traderFrom.ShowItems();
                int offerIndex = int.Parse(Console.ReadLine()) - 1;
                if (offerIndex >= 0 && offerIndex < traderFrom.GetItems().Count)
                {
                  Item offered = traderFrom.GetItems()[offerIndex];

                  // Skapa en trade request
                  tradeRequests.Add(new TradeRequest(traderFrom.GetUsername(), targetTrader.GetUsername(), requested, offered));
                  messages.Add(new Message(traderFrom.GetUsername(), targetTrader.GetUsername(), $"Trade request sent for item {requested.GetName()} offering {offered.GetName()}")); // Skicka meddelande om trade requst
                  Console.WriteLine("Trade request sent.");
                }
              }
            }
          }
          Console.ReadLine();
        }
        // 6. Visa och svara på inkommande requests.
        else if (input == "6")
        {
          Console.WriteLine("Your incoming trade requests:");
          int rNum = 1;
          foreach (var tr in tradeRequests)
            if (tr.GetToUser() == activeUser.GetUsername()) { Console.WriteLine($"{rNum}."); tr.Show(); rNum++; } // Visa detaljer för requesten

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
                if (resp == "A") tr.Accept(); // Byter ägare på items
                else if (resp == "D") tr.Deny(); // Markera som nekat
              }
            }
          }
          Console.ReadLine();
        }
        // 7. Visa alla användare i systemet.
        else if (input == "7")
        {
          Console.WriteLine("Users:");
          foreach (var u in users) u.Info(); // Skriv ut användardata
          Console.ReadLine();
        }
        // 8. Skicka meddelande till annan användare.
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
        // Visa alla inkomna meddeladne (inkorg)
        else if (input == "9")
        {
          Console.WriteLine("Inbox:");
          foreach (var m in messages)
            if (m.GetTo() == activeUser.GetUsername()) m.Show();
          Console.ReadLine();
        }
        // Visa alla avslutade trade requests.
        else if (input == "10")
        {
          Console.WriteLine("Completed trade requests:");
          foreach (var tr in tradeRequests)
            if (tr.GetStatus() != TradeStatus.Pending) tr.Show();
          Console.ReadLine();
        }
        // 11. Logga ut
        else if (input == "11")
        {
          activeUser = null; // Sätt användaren till null -> utloggad
        }
      }
    }

    // Spara all data innan programmet stängs.
    //
    // Här skriver vi ner all data till fil igen, så att inget
    // går förlorat till nästa gång programmet körs.
    Logger.SaveUsers(users);
    Logger.SaveItems(users);
    Logger.SaveMessages(messages);
    Logger.SaveTrades(tradeRequests);
  }
}
