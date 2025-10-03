using System;
using System.Collections.Generic;

namespace Trading_System
{
  /// <summary>
  /// Trader som kan äga items och skicka trade requests.
  /// </summary>
  public class Trader : IUser
  {
    private string _username;
    private string _password;
    private string _name;
    private bool _isActive;
    private List<Item> _items;

    public Trader(string username, string password, string name)
    {
      _username = username;
      _password = password;
      _name = name;
      _isActive = true;
      _items = new List<Item>();
    }

    public string GetUsername() => _username;
    public string GetPassword() => _password;
    public string GetName() => _name;
    public string GetRole() => "Trader";
    public bool GetIsActive() => _isActive;
    public void SetIsActive(bool active) => _isActive = active;

    public bool TryLogin(string username, string password)
    {
      return _username == username && _password == password;
    }

    public void Info()
    {
      Console.WriteLine($"Username: {_username} | Name: {_name} | Role: Trader | Active: {_isActive}");
    }

    public void AddItem(string name, string description)
    {
      _items.Add(new Item(name, description, _username));
    }

    public void ShowItems()
    {
      if (_items.Count == 0) { Console.WriteLine("No items."); return; }
      int i = 1;
      foreach (var item in _items)
      {
        Console.WriteLine($"{i}. {item.GetName()} - {item.GetDescription()} (Owner: {item.GetOwnerUsername()})");
        i++;
      }
    }

    public List<Item> GetItems() => _items;
  }
}
