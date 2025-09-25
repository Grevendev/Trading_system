namespace Trading_System;

public enum Role
{
  None,
  Admin,
  Trader,
}
public interface IUser
{
  //Inlogging
  bool TryLogin(string username, string password);
  void Info();
  Role GetRole();
  string GetUsername();

  // Lösenordshantering 

  string GetPassword();
  void SetPassword(string newPassword);

  //Säkerhetsfält ( fält direkt i klassen)

  int GetFailedLogins();
  void SetFailedLogins(int value);

  bool GetMustChangePassword();
  void SetMustChangePassword(bool value);

  bool GetIsActive();
  void SetIsActive(bool value);

  // Namn

  string GetName();
  void SetName(string name);
}
