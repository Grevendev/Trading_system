namespace Trading_System
{
  public class Message
  {
    private string from;
    private string to;
    private string content;

    public Message(string fromUser, string toUser, string content)
    {
      from = fromUser;
      to = toUser;
      this.content = content;
    }

    public string GetFrom() => from;
    public string GetTo() => to;
    public string GetContent() => content;

    public void Show() => Console.WriteLine($"From: {from} -> To: {to} | {content}");
  }
}
