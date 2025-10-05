using System;

namespace Trading_System
{
  /// <summary>
  /// Representerar ett item som ägs av en Trader och kan bytas i en trade.
  /// </summary>
  public class Item
  {
    private string _name; // Namnet på föremålet.
    private string _description; // En kort beskriving av föremålet.
    private string _ownerUsername; // Användarnamnet på den som äger förmålet

    // Skapar ett nytt item med namn, beskriving och ägare.
    public Item(string name, string description, string ownerUsername)
    {
      _name = name;
      _description = description;
      _ownerUsername = ownerUsername;
    }

    // Getters
    //Returnerar namnet på föremålet.
    public string GetName() => _name;

    //Retunerar beskrivingen av förmålet.
    public string GetDescription() => _description;

    //Retunerar användarnamnet på ägaren.
    public string GetOwnerUsername() => _ownerUsername;


    //Funktionalitet
    //Byter ägare på föremålet till en ny användare.
    public void ChangeOwner(string newOwner)
    {
      _ownerUsername = newOwner;
    }
  }
}
