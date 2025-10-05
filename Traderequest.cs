using System;
using System.Collections.Generic;

namespace Trading_System
{
  /// <summary>
  /// Enum som definierar status på en trade request.
  /// </summary>
  public enum TradeStatus
  {
    Pending,  // Förfrågan skickad men inte besvarad
    Accepted, // Förfrågan accepterad och trade genomförd
    Denied    // Förfrågan nekad
  }

  /// <summary>
  /// Representerar en bytesförfrågan mellan två användare.
  /// Innehåller information om:
  /// - Från vilken användare
  /// - Till vilken användare
  /// - Vilket item som efterfrågas
  /// - Vilket item som erbjuds
  /// - Status på förfrågan
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

    /// <summary>
    /// Accepterar trade requesten och byter ägare på items.
    /// Endast om status är Pending.
    /// </summary>
    /// <param name="users">Lista av alla användare i systemet</param>
    public void Accept(List<IUser> users)
    {
      if (_status != TradeStatus.Pending) return;

      // Hitta Trader-objekt baserat på ägare av items
      Trader fromTrader = null;
      Trader toTrader = null;

      foreach (var u in users)
      {
        if (u is Trader tr)
        {
          if (tr.GetUsername() == _fromUser) fromTrader = tr;
          if (tr.GetUsername() == _toUser) toTrader = tr;
        }
      }

      if (fromTrader != null && toTrader != null)
      {
        // Ta bort item från respektive ägare
        fromTrader.RemoveItem(_offeredItem);
        toTrader.RemoveItem(_requestedItem);

        // Byt ägare
        _offeredItem.ChangeOwner(_toUser);
        _requestedItem.ChangeOwner(_fromUser);

        // Lägg till items i nya ägares lista
        fromTrader.GetItems().Add(_requestedItem);
        toTrader.GetItems().Add(_offeredItem);

        // Uppdatera status
        _status = TradeStatus.Accepted;
      }
    }

    /// <summary>
    /// Neka trade request.
    /// Endast status ändras, inga items byts.
    /// </summary>
    public void Deny()
    {
      if (_status != TradeStatus.Pending) return;
      _status = TradeStatus.Denied;
    }

    /// <summary>
    /// Visar detaljer för trade request.
    /// </summary>
    public void Show()
    {
      Console.WriteLine($"From: {_fromUser} -> To: {_toUser}");
      Console.WriteLine($"Requested Item: {_requestedItem.GetName()} | Offered Item: {_offeredItem.GetName()}");
      Console.WriteLine($"Status: {_status}");
      Console.WriteLine("-----------------------------");
    }
  }
}
