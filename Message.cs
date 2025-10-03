using System;

namespace Trading_System
{
  /// <summary>
  /// Representerar ett meddelande mellan användare.
  /// </summary>
  public class Message
  {
    private string _from;
    private string _to;
    private string _content;

    public Message(string from, string to, string content)
    {
      _from = from;
      _to = to;
      _content = content;
    }

    public string GetFrom() => _from;
    public string GetTo() => _to;
    public string GetContent() => _content;

    public void Show()
    {
      Console.WriteLine($"From: {_from} | Message: {_content}");
    }
  }
}
