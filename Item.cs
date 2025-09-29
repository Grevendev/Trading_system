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

    public string GetName() => _name;
    public string GetDescription() => _description;
    public string GetOwnerUsername() => _ownerUsername;

    public bool ChangeOwner(string newOwner)
    {
      if (string.IsNullOrEmpty(newOwner)) return false;
      _ownerUsername = newOwner;
      return true;
    }

    public void ShowInfo()
    {
      Console.WriteLine($"Item: {_name} | Description: {_description} | Owner: {_ownerUsername}");
    }
  }
}
