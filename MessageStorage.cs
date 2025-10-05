using System;
using System.Collections.Generic;
using System.IO;

namespace Trading_System
{
  /// <summary>
  /// Hanterar sparning och inläsning av meddelanden till och från filsystemet.
  /// Gör att meddelande mellan användare bevaras mellan programstarter.
  /// </summary>
  public static class MessageStorage
  {
    private static string filePath = "Messages.txt"; // Filoens namn där alla meddelanden lagras.

    //Läser in alla sparade meddelanden från filen och retunerar dem som en lista.
    //Om filen inte finns, retunerars en tom lista.
    public static List<Message> LoadMessages()
    {
      List<Message> messages = new List<Message>();

      //Kontrollerar om filen existerar, annars finns inget att läsa.
      if (!File.Exists(filePath)) return messages;

      string[] lines = File.ReadAllLines(filePath);

      //Varje rad representerar ett meddelande i formatet: from|to|content
      foreach (string line in lines)
      {
        string[] parts = line.Split('|');
        if (parts.Length < 3) continue; //Hoppa över felaktiga rader.

        messages.Add(new Message(parts[0], parts[1], parts[2]));
      }
      return messages;
    }

    //Sparar alla meddelande till fil.
    //Varje meddelande sparas på en ny rad i formatet: to|from|content
    public static void SaveMessages(List<Message> messages)
    {
      List<string> lines = new List<string>();

      //Konventerar alla Message-objet till textformat.
      foreach (var m in messages)
      {
        lines.Add($"{m.GetFrom()}|{m.GetTo()}|{m.GetContent()}");
      }
      //Skriver över filen med den uppdaterade listan av meddelanden.
      File.WriteAllLines(filePath, lines);
    }
  }
}
