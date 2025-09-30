namespace Trading_System
{
  public enum Role
  {
    None,
    Admin,
    Trader
  }

  public interface IUser
  {
    bool TryLogin(string username, string password);
    void Info();
    Role GetRole();
    string GetUsername();
    string GetPassword();
    void SetPassword(string newPassword);
    int GetFailedLogins();
    void SetFailedLogins(int value);
    bool GetMustChangePassword();
    void SetMustChangePassword(bool value);
    bool GetIsActive();
    void SetIsActive(bool value);
    string GetName();
    void SetName(string name);
  }
}
