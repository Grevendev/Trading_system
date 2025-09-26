namespace Trading_System;

public class Message
{
  //Fält
  private string from;
  private string to;
  private string content;

  //Konstruktor
  public Message(string formUser, string toUser, string messageContent)
  {
    from = formUser;
    to = toUser;
    content = messageContent;
  }

  //Metoder

  public string GetFrom() { return from; }
  public void SetFrom(string value) { from = value; }

  public string GetTo() { return to; }
  public void SetTo(string value) { to = value; }

  public string GetContent() { return content; }
  public void SetContent(string value) { content = value; }

  public void Show()
  {
    Console.WriteLine($"From: {from} -> To: {to} | {content}");
  }
}