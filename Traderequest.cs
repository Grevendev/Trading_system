namespace Trading_System
{
  public enum TradeStatus { Pending, Accepted, Denied }

  public class TradeRequest
  {
    private string fromUser;
    private string toUser;
    private Item requestedItem;
    private Item offeredItem;
    private TradeStatus status;

    public TradeRequest(string fromUser, string toUser, Item requestedItem, Item offeredItem)
    {
      this.fromUser = fromUser;
      this.toUser = toUser;
      this.requestedItem = requestedItem;
      this.offeredItem = offeredItem;
      status = TradeStatus.Pending;
    }

    public void Accept()
    {
      status = TradeStatus.Accepted;
      requestedItem.ChangeOwner(fromUser);
      offeredItem.ChangeOwner(toUser);
    }

    public void Deny() => status = TradeStatus.Denied;

    public void Show()
    {
      Console.WriteLine($"From: {fromUser} -> To: {toUser}, Requested: {requestedItem.GetName()}, Offered: {offeredItem.GetName()}, Status: {status}");
    }

    public TradeStatus GetStatus() => status;
    public string GetToUser() => toUser;
    public string GetFromUser() => fromUser;
    public Item GetRequestedItem() => requestedItem;
    public Item GetOfferedItem() => offeredItem;
  }
}
