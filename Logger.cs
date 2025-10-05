using System;
using System.Collections.Generic;

namespace Trading_System
{
  /// <summary>
  /// Samlad klass för laddning och sparning av data.
  /// Fungerar som en central plats där alla Save/Load-metoder anropas.
  /// </summary>
  public static class Logger
  {

    // Läser in alla användare från fil via UserStorage.
    public static List<IUser> LoadUsers() => UserStorage.LoadUsers();

    //Sparar alla användare till fill via UserStorage.
    public static void SaveUsers(List<IUser> users) => UserStorage.SaveUsers(users);


    // Läser in alla items från fil via ItemStorage.
    public static void LoadItems(List<IUser> users) => ItemStorage.LoadItems(users);

    // Sparar alla items till fil via ItemStorage.
    public static void SaveItems(List<IUser> users) => ItemStorage.SaveItems(users);


    // Läser in alla trades (trade requests) via TradeStorage.
    public static List<TradeRequest> LoadTrades(List<IUser> users) => TradeStorage.LoadTrades(users);

    //Sparar alla trades till fil via TradeStorage.
    public static void SaveTrades(List<TradeRequest> trades) => TradeStorage.SaveTrades(trades);

    //Laddar alla meddelande (om funktionen används). 
    //Just nu retuneras en tom lista då meddelande inte sparas permanent.
    public static List<Message> LoadMessages()
    {
      // Här kan man lägga till filinläsning för meddelande om man vill utöka systemet.
      return new List<Message>();
    }
    //Sparar alla meddelande  (om funktionen används).
    //Just nu görs ingen filskivning, men kan enkelt läggas till senare.
    public static void SaveMessages(List<Message> messages)
    {
      // Här kan man implementera filskriving för meddelanden. 
    }
  }
}
