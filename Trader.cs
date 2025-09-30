using System;
using System.Collections.Generic;

namespace Trading_System
{
  public class Trader : IUser
  {
    private string userName;
    private string passwordHash;
    private string name;
    private List<Item> items = new List<Item>();

    public Trader(string username, string password, string displayName = "")
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

    public Role GetRole() => Role.Trader;
    public string GetUsername() => userName;
    public string GetPassword() => passwordHash;

    public void SetPassword(string newPassword) => passwordHash = PasswordHelper.HashPassword(newPassword);

    // Ny metod för att direkt sätta hash (för loading från fil)
    public void SetPasswordHash(string hash) => passwordHash = hash;

    public int GetFailedLogins() => 0;
    public void SetFailedLogins(int value) { }
    public bool GetMustChangePassword() => false;
    public void SetMustChangePassword(bool value) { }
    public bool GetIsActive() => true;
    public void SetIsActive(bool value) { }

    public string GetName() => name;
    public void SetName(string newName) => name = newName;

    // --- Item-hantering ---
    public void AddItem(string name, string description)
    {
      items.Add(new Item(name, description, userName));
    }

    public List<Item> GetItems() => items;

    public void ShowItems()
    {
      if (items.Count == 0)
      {
        Console.WriteLine("No items found.");
        return;
      }

      for (int i = 0; i < items.Count; i++)
      {
        Console.WriteLine($"{i + 1}. {items[i].GetName()} - {items[i].GetDescription()} (Owner: {items[i].GetOwnerUsername()})");
      }
    }
  }
}
