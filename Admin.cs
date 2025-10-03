using System;

namespace Trading_System
{
  /// <summary>
  /// Admin som kan se och administrera användare.
  /// </summary>
  public class Admin : IUser
  {
    private string _username;
    private string _password;
    private string _name;
    private bool _isActive;

    public Admin(string username, string password, string name)
    {
      _username = username;
      _password = password;
      _name = name;
      _isActive = true;
    }

    public string GetUsername() => _username;
    public string GetPassword() => _password;
    public string GetName() => _name;
    public string GetRole() => "Admin";
    public bool GetIsActive() => _isActive;
    public void SetIsActive(bool active) => _isActive = active;

    public bool TryLogin(string username, string password) => _username == username && _password == password;

    public void Info()
    {
      Console.WriteLine($"Username: {_username} | Name: {_name} | Role: Admin | Active: {_isActive}");
    }
  }
}
