namespace Trading_System
{
  public enum Role
  {
    None,
    Admin,
    Trader,
  }

  public interface IUser
  {
    bool TryLogin(string username, string password);
    void Info();
    Role GetRole();
    string GetUsername();

    string GetPassword();
    void SetPassword(string newPassword);

    string GetName();
    void SetName(string name);

    // Admin-behörigheter är borttagna för Trader
  }
}
