using System;
using System.Collections.Generic;

namespace Trading_System
{

  /// <summary>
  /// Klassen Trader representerar en vanlig användare i systemet
  /// som kan ladda upp, visa och byta items med andra.
  /// 
  /// Ärver från IUser för att passa in u systemets användarhantering.
  /// Har egna attribut (usernamne, lösenord, visingsnamn).
  /// 
  /// Den här klassen är centtral i systemet eftersom nästan all aktivitet 
  /// (att lägga till items, skicka trade requests, visa användarinfo)
  /// kretar kring just Trader.
  /// </summary>
  public class Trader : IUser
  {
    // Fält (privata variabler)
    private string userName; // Användarnamn som indentiferar tradern.
    private string passwordHash; // Hashat lösenord för säker inlogging.
    private string name; // Visingsnamn (kan vara skillnad mot username).
    private List<Item> items = new List<Item>(); // Lista över items som ägs av tradern.

    // Konstruktor

    // Skapar en ny trader.
    // Password hashas direkt vid skapandet för säkerhet.
    // Om inget visingsnamn anges används username som namn.

    public Trader(string username, string password, string displayName = "")
    {
      userName = username;
      passwordHash = PasswordHelper.HashPassword(password);
      name = displayName != "" ? displayName : username;
    }

    //  Inlogging & autentisering

    // Försöker logga in tradern genom att jämföra username och lösenord.
    // Returnerar true om både användarnamn och lösenord matchar.

    public bool TryLogin(string username, string password)
    {
      if (username != userName) return false;
      return PasswordHelper.VerifyPassword(password, passwordHash);
    }

    // Skriver ut information om tradern till konsolen.
    // Används främst i admin- eller "view users"-menyerna.

    public void Info()
    {
      Console.WriteLine($"Name: {name}, Username: {userName}, Role: {GetRole()}");
    }

    // Retunerar användarens roll. Här alltid "Trader".
    // Detta används för att kunna särskilja mellan t.ex. Admin och Trader.

    public Role GetRole() => Role.Trader;

    // Getters och setters för användarens grunddata.
    public string GetUsername() => userName;
    public string GetPassword() => passwordHash;

    // Sätter nytt lösenord (hashas direkt).

    public void SetPassword(string newPassword) => passwordHash = PasswordHelper.HashPassword(newPassword);

    // Avänds vid ladding från fil (här sparas redan hashat lösenord).
    // Därför behövs en metod som kan sätta en färdig hash istället för att hasha igen.
    public void SetPasswordHash(string hash) => passwordHash = hash;

    // Nedanstående metoder finns för att följa IUser-interfacet men används ej i Trader.
    public int GetFailedLogins() => 0;
    public void SetFailedLogins(int value) { }
    public bool GetMustChangePassword() => false;
    public void SetMustChangePassword(bool value) { }
    public bool GetIsActive() => true;
    public void SetIsActive(bool value) { }

    public string GetName() => name;
    public void SetName(string newName) => name = newName;

    // Item-hantering
    // Lägger till ett nytt item som tradern äger.
    // Ägaren sätts automatiskt til traderns userame.
    public void AddItem(string name, string description)
    {
      items.Add(new Item(name, description, userName));
    }

    // Retunerar en lista med alla traderns items.
    // Används vid trade requsts eller när man visa sina egna items.

    public List<Item> GetItems() => items;

    // Visar alla traderns items i konsolen.
    // Om listan är tom visas ett tydligt meddeladne.

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
