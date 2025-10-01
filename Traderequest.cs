using System;

namespace Trading_System
{
  // Möjliga statusar för en TradeRequest.
  // Pending = Förfrågan har skickats men inte besvarats ännu.
  // Accepted = Mottagaren har godkänt och bytet genomförts.
  // Denied = Mottagaren har nekat förfrågan.
  public enum TradeStatus
  {
    Pending,
    Accepted,
    Denied
  }

  /// <summary>
  /// Representerar en bytesförfrågan mellan två användare.
  /// Ett TradeRequest innehåller information om:
  /// Vem som skickade förfrågan (FromUser)
  /// Vem som mottog föfrågan (ToUser)
  /// Vilket item som efterfrågas
  /// Vilket item som erbjuds i utbyte
  /// Status för själva förfrågan (Pending, Accepted eller Denied)
  /// 
  /// Klassen fungerar som "kärnan" i byteslogiken och binder ihop användare och deras items.
  /// </summary>

  public class TradeRequest
  {
    // Privat fält som sparar information om användarna och items.
    private string _fromUser; // Användarnamn på den som skickar förfrågan.
    private string _toUser;  // Användarnamn på mottagaren.
    private Item _requestedItem; // Det item som efterfrågas.
    private Item _offeredItem; // Det item som erbjuds i utbyte.
    private TradeStatus _status; // Nuvarande status för förfrågan.

    // Konstruktor för att skapa en ny TradeRequest.
    // Per default sätt status till Pending.

    public TradeRequest(string fromUser, string toUser, Item requestedItem, Item offeredItem)
    {
      _fromUser = fromUser;
      _toUser = toUser;
      _requestedItem = requestedItem;
      _offeredItem = offeredItem;
      _status = TradeStatus.Pending;
    }

    // Publika getters som ger åtkomst till förfrågans data.

    public string GetFromUser() => _fromUser;
    public string GetToUser() => _toUser;
    public Item GetRequestedItem() => _requestedItem;
    public Item GetOfferedItem() => _offeredItem;
    public TradeStatus GetStatus() => _status;

    /// <summary>
    /// Accepterar förfrågan och byter ägare på items.
    /// Viktigt: Bytet sker endast om status fortfarande är Pending.
    /// Annars ignoreras anropet.
    /// 
    /// Logik: 
    /// Den begärda varan byter ägare till den som erbjöd sitt item.
    /// Det erbjudna itemet byter ägare till den som begärde.
    /// Status markeras som Accepted.
    /// 
    /// Detta knyter ihop Item-Klassen med Trader-klassen 
    /// </summary>

    public void Accept()
    {
      if (_status != TradeStatus.Pending) return;


      // Byt ägare på items genom att uppdatera deras OwnerUsername.
      string tempOwner = _requestedItem.GetOwnerUsername();
      _requestedItem.ChangeOwner(_offeredItem.GetOwnerUsername());
      _offeredItem.ChangeOwner(tempOwner);

      _status = TradeStatus.Accepted;
    }

    /// <summary>
    /// Neka förfrågan.
    /// Här görs inget ägarbyte, endast status uppdateras.
    /// </summary>

    public void Deny()
    {
      _status = TradeStatus.Denied;
    }

    /// <summary>
    /// Visar detaljer om trade requesten i konsolen.
    /// Detta används i menyerna när en användare vill granska sina inkommande eller skickade förfrågningar.
    /// </summary>

    public void Show()
    {
      Console.WriteLine($"From: {_fromUser} -> To: {_toUser}");
      Console.WriteLine($"Requested Item: {_requestedItem.GetName()} | Offered Item: {_offeredItem.GetName()}");
      Console.WriteLine($"Status: {_status}");
      Console.WriteLine("-------------------------------");
    }
  }
}
