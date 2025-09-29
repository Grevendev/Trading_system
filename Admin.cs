using System;

namespace Trading_System
{
  public class Admin : IUser
  {
    private string userName;
    private string passwordHash;
    private string name;

    public Admin(string username, string password, string displayName = "")
    {
      userName = username;
      passwordHash = PasswordHelper.HashPassword(password);
      name = displayName != "" ? displayName : username;
    }

    public bool TryLogin(string username, string password)
    {
      if (username != userName) return false;
      return PasswordHelper.VerifyPassword(password, passwordHash);
    }

    public void Info()
    {
      Console.WriteLine($"Name: {name}, Username: {userName}, Role: {GetRole()}");
    }

    public Role GetRole() => Role.Admin;
    public string GetUsername() => userName;
    public string GetPassword() => passwordHash;
    public void SetPassword(string newPassword) => passwordHash = PasswordHelper.HashPassword(newPassword);
    public string GetName() => name;
    public void SetName(string newName) => name = newName;
  }
}
