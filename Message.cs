namespace Trading_System
{
  public class Message
  {
    private string from;
    private string to;
    private string content;

    public Message(string fromUser, string toUser, string messageContent)
    {
      from = fromUser;
      to = toUser;
      content = messageContent;
    }

    public string GetFrom() => from;
    public string GetTo() => to;
    public string GetContent() => content;

    public void Show()
    {
      Console.WriteLine($"From: {from} -> To: {to} | {content}");
    }
  }
}
