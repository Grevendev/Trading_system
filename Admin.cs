using System;

namespace Trading_System
{
  public class Admin : IUser
  {
    // Fält
    private string userName;
    private string passwordHash;
    private string name;

    private int failedLogins = 0;
    private bool mustChangePassword = false;
    private bool isActive = true;

    // Konstruktor
    public Admin(string username, string password, string displayName = "")
    {
      userName = username;
      passwordHash = PasswordHelper.HashPassword(password);
      name = displayName != "" ? displayName : username;
    }

    //IUser Implementation

    public bool TryLogin(string username, string password)
    {
      if (!isActive) return false;
      if (username != userName) return false;
      return PasswordHelper.VerifyPassword(password, passwordHash);
    }

    public void Info()
    {
      Console.WriteLine($"Name: {name}, Username: {userName}, Role: {GetRole()}");
    }

    public Role GetRole()
    {
      return Role.Admin;
    }

    public string GetUsername()
    {
      return userName;
    }

    public string GetPassword()
    {
      return passwordHash;
    }

    public void SetPassword(string newPassword)
    {
      passwordHash = PasswordHelper.HashPassword(newPassword);
    }

    //Säkerhetsfält 

    public int GetFailedLogins() { return failedLogins; }
    public void SetFailedLogins(int value) { failedLogins = value; }

    public bool GetMustChangePassword() { return mustChangePassword; }
    public void SetMustChangePassword(bool value) { mustChangePassword = value; }

    public bool GetIsActive() { return isActive; }
    public void SetIsActive(bool value) { isActive = value; }

    // Namn

    public string GetName() { return name; }
    public void SetName(string newName) { name = newName; }
  }
}
