namespace Trading_System      //Item-klass, som gör att man kan skapa item med namn och beskriving av itemet. 
{
  public class Item
  {
    private string name;
    private string description;
    private string ownerUsername;

    public Item(string name, string description, string ownerUsername)
    {
      this.name = name;
      this.description = description;
      this.ownerUsername = ownerUsername;
    }

    public string GetName() => name;
    public string GetDescription() => description;
    public string GetOwnerUsername() => ownerUsername;

    public void ChangeOwner(string newOwner) => ownerUsername = newOwner;

    public void ShowInfo() => Console.WriteLine($"Item: {name} | Description: {description} | Owner: {ownerUsername}");
  }
}
