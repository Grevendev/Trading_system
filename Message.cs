using System;

namespace Trading_System
{
  /// <summary>
  /// Representerar ett meddelande mellan två användare i systemet.
  /// Används för att skicka enkla textmeddelande mellan traders.
  /// </summary>
  public class Message
  {
    //Fält som sparar ansändare, mottagre och meddelandets innehåll
    private string _from;
    private string _to;
    private string _content;


    //Skapar ett nytt meddelande med anvsändare, mottagre och innehåll.
    public Message(string from, string to, string content)
    {
      _from = from;
      _to = to;
      _content = content;
    }

    public string GetFrom() => _from; // Retunear anvsändarens användarnamn.
    public string GetTo() => _to; // Retunerar mottagrens användarnamn.
    public string GetContent() => _content; //Retunerar meddelandets textinnehåll

    // Skriver ut meddelandet i ett tydligt format till konsolen.
    public void Show()
    {
      Console.WriteLine($"From: {_from} | Message: {_content}");
    }
  }
}
