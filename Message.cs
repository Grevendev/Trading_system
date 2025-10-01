namespace Trading_System
{
  //
  //Message
  //
  //Denna klass representerar ett meddelande mellan två användare i trading-systemet.
  //
  //Varje meddelande har:
  //Avsändare (from)
  //Mottagare (to)
  //Själva innehållet (content)
  //
  //Meddeladnen anvädns främst för kommunikation mellan traders
  //när de vill skicka information, förhandla eller notifiera om trade requets.
  public class Message
  {
    //Fält för att lagra information om meddelandet
    private string from; // Anvsändarens användarnamn
    private string to; // Mottagerens användarnamn
    private string content; // Själva meddelandetexten

    //
    //Kontruktor
    //
    //Skapa ett nytt meddelande
    //Parametrar:
    // fromUser: Avsändarens användarnamn
    //toUser: Mottagarens användarnamn
    //content: TExten i meddelandet

    public Message(string fromUser, string toUser, string content)
    {
      from = fromUser;
      to = toUser;
      this.content = content;
    }

    //
    //Getters
    //
    // Dessa metoder används för att läsa information om meddeladnet

    public string GetFrom() => from; // Retunerar avsändarens namn
    public string GetTo() => to; // Returnerar mottagrens namn
    public string GetContent() => content; // Returnerar meddelandetexten

    //
    //Show
    //
    // Skriver ut meddelandet i konsolen på ett lättläst format
    //Exempel: "From: Lennart -> To: Roger | Hej, vill du byta item?"
    //
    //Denna metod används framför allt när en användare vill se sin inbox

    public void Show() => Console.WriteLine($"From: {from} -> To: {to} | {content}");
  }
}
