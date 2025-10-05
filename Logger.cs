using System;
using System.Collections.Generic;

namespace Trading_System
{
  /// <summary>
  /// Logger ansvarar för att samla alla Save/Load-funktioner.
  /// Den fungerar som ett gränssnitt mot filhanteringen.
  /// </summary>
  public static class Logger
  {
    /// <summary>
    /// Laddar alla användare från fil via UserStorage.
    /// </summary>
    /// <returns>Lista med IUser</returns>
    public static List<IUser> LoadUsers()
    {
      return UserStorage.LoadUsers();
    }

    /// <summary>
    /// Sparar alla användare via UserStorage.
    /// </summary>
    /// <param name="users">Lista med IUser</param>
    public static void SaveUsers(List<IUser> users)
    {
      UserStorage.SaveUsers(users);
    }

    /// <summary>
    /// Laddar alla items för användare via ItemStorage.
    /// </summary>
    /// <param name="users">Lista med IUser</param>
    public static void LoadItems(List<IUser> users)
    {
      ItemStorage.LoadItems(users);
    }

    /// <summary>
    /// Sparar alla items för användare via ItemStorage.
    /// </summary>
    /// <param name="users">Lista med IUser</param>
    public static void SaveItems(List<IUser> users)
    {
      ItemStorage.SaveItems(users);
    }

    /// <summary>
    /// Laddar alla trade requests via TradeStorage.
    /// </summary>
    /// <param name="users">Lista med alla användare</param>
    /// <returns>Lista med TradeRequest</returns>
    public static List<TradeRequest> LoadTrades(List<IUser> users)
    {
      return TradeStorage.LoadTrades(users);
    }

    /// <summary>
    /// Sparar alla trade requests via TradeStorage.
    /// </summary>
    /// <param name="trades">Lista med TradeRequest</param>
    public static void SaveTrades(List<TradeRequest> trades)
    {
      TradeStorage.SaveTrades(trades);
    }

    /// <summary>
    /// Laddar alla meddelanden från fil via MessageStorage.
    /// </summary>
    /// <returns>Lista med Message</returns>
    public static List<Message> LoadMessages()
    {
      return MessageStorage.LoadMessages();
    }

    /// <summary>
    /// Sparar alla meddelanden till fil via MessageStorage.
    /// </summary>
    /// <param name="messages">Lista med Message</param>
    public static void SaveMessages(List<Message> messages)
    {
      MessageStorage.SaveMessages(messages);
    }
  }
}
