using System;

namespace Trading_System
{
  /// <summary>
  /// Interface som definierar gemensamma egenskaper och metoder
  /// för alla typer av användare (Admin, Trader).
  /// </summary>
  public interface IUser
  {
    string GetUsername(); //Retunerar användarens unika användarnamn.
    string GetPassword(); // Retunerar användarens lösenord (i klartext i detta fall).
    string GetName(); // Retunerar användarens fullständiga namn.
    string GetRole(); // Retunerar användarens till, t.ex. "Admin" eller "Trader".
    bool GetIsActive(); //Anger om användaren är aktiv (t.ex. inte avtängd).
    void SetIsActive(bool active); // Sätter användarens aktiva status.
    bool TryLogin(string username, string password); // Försöker logga in användaren genom att jämföra användarnamn och lösenord.
    void Info(); // Skriver ut användarens information till konsolen. 
  }
}
