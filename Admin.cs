using System;

namespace Trading_System
{
  /// <summary>
  /// Representerar en administartör som kan hantera användare, 
  /// men inte delta i trading. Implementerar IUser-gränssnittet.  /// </summary>
  public class Admin : IUser
  {
    //Fält för inloggings- och profilinformation.
    private string _username;
    private string _password;
    private string _name;
    private bool _isActive;

    //Skapar en ny administarör med angivet användarnman, lösenord och namn.
    public Admin(string username, string password, string name)
    {
      _username = username;
      _password = password;
      _name = name;
      _isActive = true; // Aktiverad som standard
    }

    // -- Getters och Setters -- 
    public string GetUsername() => _username;
    public string GetPassword() => _password;
    public string GetName() => _name;
    public string GetRole() => "Admin";
    public bool GetIsActive() => _isActive;
    public void SetIsActive(bool active) => _isActive = active;

    // -- Funktionalitet --

    public bool TryLogin(string username, string password) => _username == username && _password == password;

    // Skriver ut grundläggande information om administratören i konsolen.
    public void Info()
    {
      Console.WriteLine($"Username: {_username} | Name: {_name} | Role: Admin | Active: {_isActive}");
    }
  }
}
