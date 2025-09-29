namespace Trading_System;

public enum TradeStatus
{
  Pending,
  Accepted,
  Denied,
}
public class TradeRequest
{
  private string _fromUser;
  private string _toUSer;
  private Item _item;
  private TradeStatus _status;

  public TradeRequest(string fromUser, string toUser, Item item)
}