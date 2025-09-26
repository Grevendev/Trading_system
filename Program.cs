using Trading_System;

//Systemdata
List<IUser> users = new List<IUser>();
List<Message> messages = new List<Message>();

//Skapat en admin från start så att jag alltid kan logga in
users.Add(new Admin("edvin@hotmail.com", "password123", "edvin"));

IUser activeUser = null; // Håller koll på den som är inloggad
bool running = true;

while (running)
{
  Console.Clear();

  //Om ingen avändare är inloggad
  {
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
          Console.Write("Enter password");
          string newPass = Console.ReadLine();

          if (users.Any(u => u.GetUsername().Equals(newUser, StringComparison.OrdinalIgnoreCase)))
          {
            Console.WriteLine("User already exist.");
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

          var user = users.FirstOrDefault(u => u.GetUsername().Equals(username, StringComparison.OrdinalIgnoreCase));

          if (user == null)
          {
            Console.WriteLine("User not found.");
          }
          else if (!user.GetIsActive())
          {
            Console.WriteLine("Account is inactive. Contact admin.");
          }
          else if (user.TryLogin(username, password))
          {
            Console.WriteLine("Login successful.");
            activeUser = user;
          }
          else
          {
            Console.WriteLine("Login failed.");
            user.SetFailedLogins(user.GetFailedLogins() + 1);

            if (user.GetFailedLogins() >= 3)
            {
              user.SetIsActive(false);
              Console.WriteLine("Account locked after 3 failed attempts.");
            }
          }
          Console.ReadLine();
          break;

        case "0":
          running = false;
          break;

        default:
          Console.WriteLine("Invalid choice.");
          Console.ReadLine();
          break;
      }
    }
    else
    {
      // Om användaren är inloggad

      Console.WriteLine($"Welcome {activeUser.GetName()} ({activeUser.GetRole()})");
      Console.WriteLine("1. View Users");
      Console.WriteLine("2. Send Message");
      Console.WriteLine("3. Inbox");
      Console.WriteLine("9. Logout");
      Console.Write("Choice: ");
      string input = Console.ReadLine();

      switch (input)
      {
        case "1":
          Console.WriteLine("Users");
          foreach (var u in users)
          {
            u.Info();
          }
          Console.ReadLine();
          break;

        case "2":
          Console.Write("Send to (username): ");
          string to = Console.ReadLine();
          Console.Write("Message: ");
          string content = Console.ReadLine();

          if (users.Any(u => u.GetUsername().Equals(to, StringComparison.OrdinalIgnoreCase)))
          {
            messages.Add(new Message(activeUser.GetUsername(), to, content));
            Console.WriteLine("Message sent.");
          }
          else
          {
            Console.WriteLine("User not found.");
          }
          Console.ReadLine();
          break;

        case "3":
          Console.WriteLine("Inbox");
          foreach (var m in messages.Where(m => m.GetTo() == activeUser.GetUsername()))
          {
            m.Show();
          }
          Console.ReadLine();
          break;

        case "9":
          Console.WriteLine("Logged out.");
          activeUser = null;
          Console.ReadLine();
          break;

        default:
          Console.WriteLine("Invalid choice.");
          Console.ReadLine();
          break;
      }
    }
  }
}