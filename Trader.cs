using System;
using System.Collections.Generic;

namespace Trading_System
{
  /// <summary>
  /// Representerar en Trader som kan äga items, skicka och ta emot trade requests.
  /// Ärver IUser för att följa gemensamma användarregler.
  /// </summary>
  public class Trader : IUser
  {
    private string _username;
    private string _password;
    private string _name;
    private bool _isActive;

    // Lista av items som Tradern äger
    private List<Item> _items;

    /// <summary>
    /// Konstruktor för Trader.
    /// </summary>
    public Trader(string username, string password, string name)
    {
      _username = username;
      _password = password;
      _name = name;
      _isActive = true;
      _items = new List<Item>();
    }

    // Getters och Setters
    public string GetUsername() => _username;
    public string GetPassword() => _password;
    public string GetName() => _name;
    public string GetRole() => "Trader";
    public bool GetIsActive() => _isActive;
    public void SetIsActive(bool active) => _isActive = active;

    /// <summary>
    /// Verifierar login med användarnamn och lösenord.
    /// </summary>
    public bool TryLogin(string username, string password)
    {
      return _username == username && _password == password;
    }

    /// <summary>
    /// Visar information om Tradern.
    /// </summary>
    public void Info()
    {
      Console.WriteLine($"Username: {_username} | Name: {_name} | Role: Trader | Active: {_isActive}");
    }

    /// <summary>
    /// Lägger till ett nytt item på Traderns lista.
    /// </summary>
    public void AddItem(string name, string description)
    {
      _items.Add(new Item(name, description, _username));
    }

    /// <summary>
    /// Lägger till ett befintligt Item-objekt (används vid trades)
    /// </summary>
    public void AddExistingItem(Item item)
    {
      _items.Add(item);
    }

    /// <summary>
    /// Tar bort ett item från Traderns lista (används vid trades)
    /// </summary>
    public void RemoveItem(Item item)
    {
      _items.Remove(item);
    }

    /// <summary>
    /// Hämtar Traderns items
    /// </summary>
    public List<Item> GetItems() => _items;

    /// <summary>
    /// Skriver ut alla items Tradern äger.
    /// </summary>
    public void ShowItems()
    {
      if (_items.Count == 0)
      {
        Console.WriteLine("No items.");
        return;
      }

      int index = 1;
      foreach (var item in _items)
      {
        Console.WriteLine($"{index}. {item.GetName()} - {item.GetDescription()} (Owner: {item.GetOwnerUsername()})");
        index++;
      }
    }
  }
}
