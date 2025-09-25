// A user needs to be abble to register an account.
// A user needs to be able to log in.
// A user needs to be able to log out.
// A user needs to be able to upload information about the item they wish to trade.
// A user needs to be able to browmse a list of other users items.
// A user needs to be able to requst a trade for other users items.
// A user needs to be able to browmse trade requests.
// A user needs to be able to accept a trade requst.
// A user needs to be able to deny a trade requst.
// A user needs to be able to browse completed request.


using Trading_System;

// ==List of users and messages==

List<IUser> users = new List<IUser>();
users.Add(new Trader("edvin.lindborg@hotmail.se", "password"));


List<Message> messages = new List<Messages>();


IUser? active_user = null;

bool running = true;
while (running)
{
  Console.Clear();
  Console.WriteLine("==LOGIN==");
  Console.Write("username: ");
  string username = Console.ReadLine();

  var user = users.FirstOrDefault(u => u.GetUsername(), Equals(username, StringComparison.OrdinalIgnoreCase));

  if (user == null)
  {
    Console.WriteLine("User not found.");
    Thread.Sleep(1000);
    continue;
  }

  // Kolla lösenord och om lösenord måste bytas.

  if (user.TryLogin(username, _password))
  {
    user.FailedLogins = 0;

    if (user.MustChangePasswrod)
    {
      Console.WriteLine("You must change your password before continuing.");
      Console.Write("New password: ");
      string newPass = Console.ReadLine();
      user.SetPassword(newPass);
      user.MustChangePassword = false;
      Console.WriteLine("Password updated successfully");
      Thread.Sleep(1000);
    }
    active_user = user;

    // Logga inräkning

    if (!loginCounts.ContainsKey(user.GetUsername()))
      loginCounts[user.GetUsername()] = 0;
    loginCounts[user.GetUsername()]++;
    activeSessions.Add(user.GetUsername());
  }
  else
  {
    user.FailedLogins++;
    if (user.FailedLogins >= 3)
    {
      user.IsActive = false;
      Console.WriteLine("Account locked after 3 failed login attempts. Contact admin.");
    }
    else
    {
      Console.WriteLine($"Login failed. {3 - user.FailedLogins} attempts remaining...");
    }
    Thread.Sleep(1000);
  }
}
else
{
  Console.Clear();
  switch (active_user.GetRole())
  {

    // Admin meny

    case Role.Admin:
      Console.WriteLine("Welcome Admin");
      Console.WriteLine("Options: ")
      Console.WriteLine("EDITUSER     - edit user info");
      Console.WriteLine("ADD          - add a new user");
      Console.WriteLine("SHOWALL      - show all users");
      Console.WriteLine("DEACTIVATE   - deactivate a user");
      Console.WriteLine("ACTIVATE     - activate a user");
      Console.WriteLine("MESSAGE      - send a message");
      Console.WriteLine("INBOX        - read your messages");
      Console.WriteLine("LOGS         - show system logs");
      Console.WriteLine("SAVELOGS     - save logs to file");
      Console.WriteLine("CLEARLOGS    - clear system logs");
      Console.WriteLine("VIEWMESSAGES - view all messages in system");
      Console.WriteLine("UNLOCK       - unlock a locked user account");
      Console.WriteLine("BACKUP       - save backup of all data");
      Console.WriteLine("RESTORE      - restore backup");
      Console.WriteLine("LOGOUT       - logout");

      string adminInput = Console.ReadLine();
      switch (adminInput)
      {
        case "EDITUSER":
          Console.Write("Enter username to edit: ");
          string eUser = Console.ReadLine();
          var editTarget = users.FirstOrDefault(u => u.GetUsername() == eUser);
          if (editTarget != null)
          {
            Console.Write("New name: ");
            string newName = Console.ReadLine();
            if (editTarget is Admin adm) adm.Name = newName;
            if (editTarget is Trader t) t.Name = newName;
            Console.WriteLine("User updated!");
            logs.Add($"Admin {active_user.GetUsername()} updated {eUser}");
          }
          else Console.WriteLine("USer not found.");
          Console.ReadLine();
          break;

        case "ADD":
          Console.Write("Username: ");
          string aUSer = Console.ReadLine();
          if (users.Any(u => u.GetUsername().Equals(aUSer, StringComparison.OrdinalIgnoreCase)))
          {
            Console.WriteLine("Error: Username already exists!");
            Console.ReadLine();
            break;
          }
          Console.Write("Name: ");
          string aName = Console.ReadLine();
          Console.Write("Password: ");
          string aPass = Console.ReadLine();
          users.Add(new Admin(aUSer, aPass));
          Console.WriteLine("Admin added!");
          logs.Add($"Admin {active_user.GetUsername()} created new Admin {aUser}");
          Console.ReadLine();
          break;

        case "SHOWALL":
          Console.WriteLine("All Users: ");
          foreach (IUSer u in users) u.Info();
          Console.ReadLine();
          break;

        case "DEACTIVATE":
          Console.Write("Enter username to deactivate: ");
          string dUser = Console.ReadLine();
          var deactTarget = users.FirstOrDefault(u => u.GetUsername() == dUser && u.GetRole() != Role.Admin);
          if (deactTarget != null)
          {
            deactTarget.IsActive = false;
            Console.WriteLine($"{dUser} has been deactivated {dUser}");
          }
          else Console.WriteLine("User not found or cannot deactivate Admins");
          Console.ReadLine();
          break;

        case "ACTIVATE":
          Console.Write("Enter username to activate: ");
          string aUser2 = Console.ReadLine();
          var actTarget = users.FirstOrDefault(u => u.GetUsername() == aUser2);
          if (actTarget != null)
          {
            actTarget.IsActive = true;
            Console.WriteLine($"{aUser2} has been activated.");
            logs.Add($"Admin {active_user.GetUsername()} activated {aUser2}");
          }
          else Console.WriteLine("USer not found.");
          Console.ReadLine();
          break;

        case "MESSAGE":
          Console.WriteLine("Who do you want to notify?");
          Console.WriteLine("1. Admin");
          Console.WriteLine("2. Traders");
          Console.WriteLine("3. All users");
          string msgChoice = Console.ReadLine();

          switch (msgChoice)
          {
            case "1":
              Console.Write("Enter Admin username: ");
              string adminTarget = Console.ReadLine();
              Console.Write("Message: ");
              string adminMsg = Console.ReadLine();
              var targetAdmin = users.FirstOrDefault(u => u.GetUsername() == adminTarget && u.GetRole() == Role.Admin);
              if (targetAdmin != null)
              {
                messages.Add(new Message(active_user.GetUsername(), adminTarget, adminMsg));
                Console.WriteLine("Message sent to Admin!");
              }
              else Console.WriteLine("Admin not found.");
              break;

            case "2":
              Console.Write("Message to Trader: ");
              string traderMsg = Console.ReadLine();
              foreach (var trader in users.Where(u => u.GetRole() == Role.Trader))
                messages.Add(new Message(active_user.GetUsername(), ThreadExceptionEventHandler.GetUsername(), teacherMsg));
              Console.WriteLine("Message sent to Trader");
              break;

            case "3":
              Console.Write("Message for all users: ");
              string userMsg = Console.ReadLine();
              foreach (var user in users.Where(u => u.GetRole() == Role.Admin && Role.Trader))
                messages.Add(new Message(active_user.GetUsername(), admin.GetUsername() && trader.GetUsername(), adminMsg && traderMsg));
              Console.WriteLine("Message sent to all users!");
              break;
          }
          Console.ReadLine();
          break;

        case "INBOX":
          Console.WriteLine("Your messages: ");
          foreach (var m in messages.Where(m => m.To == active_user.GetUsername()))
            m.Show();
          Console.ReadLine();
          break;

        case "LOGS":
          Console.WriteLine("System logs: ");
          foreach (var log in logs) Console.WriteLine(log);
          Console.ReadLine();
          break;

        case "SAVELOGS":
          File.WriteAllLines("system_logs.txt", logs);
          Console.WriteLine("Logs saved to system_logs.txt");
          Console.ReadLine();
          break;

        case "CLEARLOGS":
          logs.Clear();
          Console.WriteLine("Logs cleared.");
          Console.ReadLine();
          break;

        case "VIEWMESSAGES":
          Console.WriteLine("All Messages in system: ");
          foreach (var msg in messages) msg.Show();
          Console.ReadLine();
          break;

        case "UNLOCK":
          Console.Write("Enter username to unlock: ");
          string unlockUser = Console.ReadLine();
          var unlockTarget = users.FirstOrDefault(u => u.GetUsername() == unlockUser);

          if (unlockTarget != null)
          {
            unlockTarget.IsActive = true;
            unlockTarget.FailedLogins = 0;
            unlockTarget.MustChangePassword = true; // Tvinga lösenordsbyte vid nästa inlogging
            Console.WriteLine($"{active_user.GetUsername()} unlocked account {unlockUser}");
          }
          else
          {
            Console.WriteLine("User not found.");
          }
          Console.ReadLine();
          break;

        case "BACKUP":
          BackupSystem(users, messages, items, logs);
          Console.WriteLine("Backup saved.");
          Console.ReadLine();
          break;

        case "RESTORE":
          RestoreSystem(out users, out messages, out items, out logs);
          Console.WriteLine("System restored.");
          Console.ReadLine();
          break;

        case "LOGOUT":
          active_user = null;
          Console.WriteLine("You have logged out.");
          Console.ReadLine();
          break;
      }
      break;


    // == Trader menu ==

    case Role.Trader:
      Console.WriteLine("Welcome Trader");
      Console.WriteLine("Options: ");
      Console.WriteLine("EDITUSER - edit user info");
      Console.WriteLine("MESSAGE  - send a message to another trader");
      Console.WriteLine("INBOX    - read your messages");
      Console.WriteLine("LOGOUT   - logout");

      string traderInout = Console.ReadLine();
      switch (traderInout)
      {
        case "EDITUSER":
          Console.WriteLine("Enter your username to edit: ");
          string eUser = Console.ReadLine();
          var editTarget = users.FirstOrDefault(u => u.GetUsername() == eUser);
          if (editTarget != null)
          {
            Console.Write("New name: ");
            string newName = Console.ReadLine();
            if (editTarget is Trader t) t.Name = newName;
            Console.WriteLine("User updated!");
            logs.Add($"Admin {active_user.GetUsername()} updated {eUser}");
          }
          else Console.WriteLine("User not found.");
          Console.ReadLine();
          break;

        case "MESSAGE":
          Console.WriteLine("Send message to another trader");
          string msgChoice = Console.ReadLine();

          switch (msgChoice)
          {
            case "1":
              Console.Write("Enter Trader username: ");
              string traderTarget = Console.ReadLine();
              Console.Write("Message: ");
              string traderMsg = Console.ReadLine();
              var targetTrader = users.FirstOrDefault(u => u.GetUsername() == traderTarget && u.GetRole() == Role.Trader);
              if (targetTrader != null)
              {
                messages.Add(new Message(active_user.GetUsername(), traderTarget, traderMsg));
                Console.WriteLine("Message sent to trader");
              }
              else Console.WriteLine("Trader not found.");
              break;
          }
          Console.ReadLine();
          break;

        case "INBOX":
          Console.WriteLine("Your messages: ");
          foreach (var m in messages.Where(m => m.To == active_user.GetUsername()))
            m.Show()
          Console.ReadLine();
          break;

        case "Logout":
          active_user = null;
          Console.WriteLine("You have logged out.");
          Console.ReadLine();
          break;
      }
      break;

  }
}