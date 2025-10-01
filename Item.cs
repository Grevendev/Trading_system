namespace Trading_System
{
  //
  //Item
  //
  //Denna klass reprensterar ett föremål (item) som kan laddas
  //upp av en Trader, och därefter bytas mallan användare via trade-requests.
  //
  //Ett Item består av: 
  //Namn (vad det är för föremål)
  //Beskriving (ytterligare detaljer)
  //Ägare (vilken Trader som äger det just nu)
  //
  //Varför en egen klass?
  //Inkapslar all logik för items.
  //Gör det lättare att hålla ording på vem som äger vad 
  //Underlättar trades eftersom vi enkelt kan byta ägare
  public class Item
  {
    //Fält som beskriver själva föremålet
    private string name; // Namnet på itemet
    private string description; //Beskriving (förklarar vad det är)
    private string ownerUsername; // Vilken använader (Trader) som äger föremålet

    //
    //Konstruktor
    //
    //Används när vi skapar ett nytt föremål.
    //Kräver namn, beskriving och ägarens användarnamn.

    public Item(string name, string description, string ownerUsername)
    {
      this.name = name;
      this.description = description;
      this.ownerUsername = ownerUsername;
    }
    //
    //Get-metoder
    //
    //Används för att hämta värden (read-only access)
    //Bra för att skydda fälten (encapsulation)

    public string GetName() => name;
    public string GetDescription() => description;
    public string GetOwnerUsername() => ownerUsername;
    //
    //ChangeOwner
    //Ändrar ägare på föremålet.
    // Används t.ex. när en trade blir accepterad.

    public void ChangeOwner(string newOwner) => ownerUsername = newOwner;
    //
    //ShowInfo
    //
    //Skriver ut information om förmålet på konsolen.
    //Detta gör det lättare för användaren att se vad 
    //det finns för items, och vem äger de.

    public void ShowInfo() => Console.WriteLine($"Item: {name} | Description: {description} | Owner: {ownerUsername}");
  }
}
