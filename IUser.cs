using System;

namespace Trading_System
{
  /// <summary>
  /// Interface som definierar gemensamma metoder för alla användare.
  /// </summary>
  public interface IUser
  {
    string GetUsername();
    string GetPassword();
    string GetName();
    string GetRole();
    bool GetIsActive();
    void SetIsActive(bool active);
    bool TryLogin(string username, string password);
    void Info();
  }
}
