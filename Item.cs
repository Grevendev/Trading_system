namespace Trading_System
{
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

    public string GetName()
    {
      return _name;
    }

    public string GetDescription()
    {
      return _description;
    }

    public string GetOwnerUsername()
    {
      return _ownerUsername;
    }

    // Byt ägare – returnerar true om ändringen lyckades, annars false
    public bool ChangeOwner(string newOwner)
    {
      if (string.IsNullOrEmpty(newOwner))
      {
        return false; // Ogiltig indata
      }

      _ownerUsername = newOwner;
      return true; // Ägaren uppdaterades
    }

    public void ShowInfo()
    {
      Console.WriteLine($"Item: {_name} | Description: {_description} | Owner: {_ownerUsername}");
    }
  }
}
