using System;
using System.Collections.Generic;

namespace Trading_System
{
  /// <summary>
  /// Logger som samlar alla Save/Load metoder.
  /// </summary>
  public static class Logger
  {
    public static List<IUser> LoadUsers() => UserStorage.LoadUsers();
    public static void SaveUsers(List<IUser> users) => UserStorage.SaveUsers(users);

    public static void LoadItems(List<IUser> users) => ItemStorage.LoadItems(users);
    public static void SaveItems(List<IUser> users) => ItemStorage.SaveItems(users);

    public static List<TradeRequest> LoadTrades(List<IUser> users) => TradeStorage.LoadTrades(users);
    public static void SaveTrades(List<TradeRequest> trades) => TradeStorage.SaveTrades(trades);

    public static List<Message> LoadMessages()
    {
      // Enkelt messagesystem, implementera likt Items/Trades om du vill spara
      return new List<Message>();
    }
    public static void SaveMessages(List<Message> messages)
    {
      // Implementera sparning om du vill spara meddelanden permanent
    }
  }
}
