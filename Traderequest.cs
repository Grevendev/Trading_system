using System;

namespace Trading_System
{
  public enum TradeStatus
  {
    Pending,
    Accepted,
    Denied
  }

  public class TradeRequest
  {
    private string _fromUser;
    private string _toUser;
    private Item _requestedItem;
    private Item _offeredItem;
    private TradeStatus _status;

    public TradeRequest(string fromUser, string toUser, Item requestedItem, Item offeredItem)
    {
      _fromUser = fromUser;
      _toUser = toUser;
      _requestedItem = requestedItem;
      _offeredItem = offeredItem;
      _status = TradeStatus.Pending;
    }

    public string GetFromUser() => _fromUser;
    public string GetToUser() => _toUser;
    public Item GetRequestedItem() => _requestedItem;
    public Item GetOfferedItem() => _offeredItem;
    public TradeStatus GetStatus() => _status;

    public void Accept()
    {
      if (_status != TradeStatus.Pending) return;

      // Byt ägare på items
      string tempOwner = _requestedItem.GetOwnerUsername();
      _requestedItem.ChangeOwner(_offeredItem.GetOwnerUsername());
      _offeredItem.ChangeOwner(tempOwner);

      _status = TradeStatus.Accepted;
    }

    public void Deny()
    {
      _status = TradeStatus.Denied;
    }

    public void Show()
    {
      Console.WriteLine($"From: {_fromUser} -> To: {_toUser}");
      Console.WriteLine($"Requested Item: {_requestedItem.GetName()} | Offered Item: {_offeredItem.GetName()}");
      Console.WriteLine($"Status: {_status}");
      Console.WriteLine("-------------------------------");
    }
  }
}
