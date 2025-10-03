using System;
using System.Collections.Generic;
using System.IO;

namespace Trading_System
{
  /// <summary>
  /// Ansvarar för att spara och läsa meddelanden till/från fil
  /// </summary>
  public static class MessageStorage
  {
    private static string filePath = "Messages.txt";

    public static List<Message> LoadMessages()
    {
      List<Message> messages = new List<Message>();
      if (!File.Exists(filePath)) return messages;

      string[] lines = File.ReadAllLines(filePath);
      foreach (string line in lines)
      {
        string[] parts = line.Split('|'); // from|to|content
        if (parts.Length < 3) continue;

        messages.Add(new Message(parts[0], parts[1], parts[2]));
      }
      return messages;
    }

    public static void SaveMessages(List<Message> messages)
    {
      List<string> lines = new List<string>();
      foreach (var m in messages)
      {
        lines.Add($"{m.GetFrom()}|{m.GetTo()}|{m.GetContent()}");
      }
      File.WriteAllLines(filePath, lines);
    }
  }
}
