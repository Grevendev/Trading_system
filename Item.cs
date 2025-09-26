using System;

namespace Trading_System;
{
  public class Item
{
  //Fält
  private int id;
  private string title;
  private string description;
  private string ownerUsername; // Vem som äger ett item
  private bool isAvailable = true;

  private static int nextId = 1;
  public Item(string title, string description, string ownerUsername)
  {
    this.id = nextId++;
    this.title = title;
    this.description = description;
    this.ownerUsername = ownerUsername;
    this.isAvailable = true;
  }
  //Metoder

  public int GetId() { return id; }
  public string GetTitle() { return title; }
  public void SetTitle(string newTitle) { title = newTitle; }

  public string GetDescription() { return description; }
  public void SetDescription(string newDescription) { description = newDescription; }

  public string GetOwnerUsername() { return ownerUsername; }
  public void SetOwnerUsername(string newOwner) { isAvailable = newOwner; }

  public bool GetIsAvailable() { return isAvailable; }
  public void SetIsAvailable(bool available) { isAvailable = available; }

  public void Info()
  {
    string status = isAvailable ? "Available" : "Traded";
    Console.WriteLine($"ID: {id}, Title: {title}, Owner: {ownerUsername}, Status: {status}");
  }
}
}