using System;

namespace Trading_System    // Admin-klass, som har hand om användare, men har ej tillgång till trading funktioner.
{
  public class Admin : IUser
  {
    private string userName;
    private string passwordHash;
    private string name;
    private bool isActive = true;

    public Admin(string username, string password, string displayName = "")
    {
      userName = username;
      passwordHash = PasswordHelper.HashPassword(password);
      name = displayName != "" ? displayName : username;
    }

    public bool TryLogin(string username, string password)
    {
      if (!isActive) return false;
      if (username != userName) return false;
      return PasswordHelper.VerifyPassword(password, passwordHash);
    }

    public void Info() => Console.WriteLine($"Name: {name}, Username: {userName}, Role: {GetRole()}");

    public Role GetRole() => Role.Admin;
    public string GetUsername() => userName;
    public string GetPassword() => passwordHash;
    public void SetPassword(string newPassword) => passwordHash = PasswordHelper.HashPassword(newPassword);
    public int GetFailedLogins() => 0;
    public void SetFailedLogins(int value) { }
    public bool GetMustChangePassword() => false;
    public void SetMustChangePassword(bool value) { }
    public bool GetIsActive() => isActive;
    public void SetIsActive(bool value) => isActive = value;
    public string GetName() => name;
    public void SetName(string newName) => name = newName;
  }
}
