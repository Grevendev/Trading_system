using System;

namespace Trading_System
{
  /// <summary>
  /// Representerar ett item som kan bytas mellan Traders.
  /// </summary>
  public class Item
  {
    private string _name;
    private string _description;
    private string _ownerUsername;

    public Item(string name, string description, string ownerUsername)
    {
      _name = name;
      _description = description;
      _ownerUsername = ownerUsername;
    }

    public string GetName() => _name;
    public string GetDescription() => _description;
    public string GetOwnerUsername() => _ownerUsername;

    public void ChangeOwner(string newOwner)
    {
      _ownerUsername = newOwner;
    }
  }
}
