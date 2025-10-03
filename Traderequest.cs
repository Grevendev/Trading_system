using System;
using System.Collections.Generic;

namespace Trading_System
{
  public enum TradeStatus { Pending, Accepted, Denied }

  /// <summary>
  /// Representerar en bytesförfrågan mellan två Traders.
  /// </summary>
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

    public void Accept(List<IUser> users)
    {
      if (_status != TradeStatus.Pending) return;

      // Hitta Trader-objekt för ägaren
      Trader fromTrader = null;
      Trader toTrader = null;
      foreach (var u in users)
      {
        if (u.GetUsername() == _fromUser) fromTrader = u as Trader;
        if (u.GetUsername() == _toUser) toTrader = u as Trader;
      }

      if (fromTrader != null && toTrader != null)
      {
        // Byt ägare
        _requestedItem.ChangeOwner(_fromUser);
        _offeredItem.ChangeOwner(_toUser);

        // Uppdatera ägarnas item-listor
        fromTrader.GetItems().Remove(_offeredItem);
        fromTrader.GetItems().Add(_requestedItem);

        toTrader.GetItems().Remove(_requestedItem);
        toTrader.GetItems().Add(_offeredItem);

        _status = TradeStatus.Accepted;
      }
    }

    public void Deny() => _status = TradeStatus.Denied;

    public void Show()
    {
      Console.WriteLine($"From: {_fromUser} -> To: {_toUser}");
      Console.WriteLine($"Requested Item: {_requestedItem.GetName()} | Offered Item: {_offeredItem.GetName()}");
      Console.WriteLine($"Status: {_status}");
      Console.WriteLine("-------------------------------");
    }
  }
}
