# Trading System

## Quick Start

1. Run the program by executing `dotnet run` in the project folder.
2. Register a Trader – select "Register Trader" and create an account.
3. Log in with your username and password.
4. Add an item (e.g., "Bicycle") to your collection.
5. View other users' items and send a trade request.
6. Accept or deny trades in your list.
7. Send and read messages to communicate with other Traders.
8. Log out – all data is automatically saved for the next session.

---

## About the Program

A console-based trading application in C# that allows users to:

- Register and log in as a Trader or Admin.
- Upload and trade items.
- Send and manage trade requests.
- Communicate via a messaging system.
- Preserve all data between sessions using file-based storage.

The system simulates a simple yet extendable trading network where security, user roles, and clear logic are central design principles.

---

## Features

### User Management

- **Register a new Trader** – New users can create an account.  
  **Reason:** Separates Admin and Trader roles and ensures items are linked to the correct user.

- **Login/Logout** – Logging in is required to perform actions.  
  **Reason:** Ensures only authorized users can manage their resources.

- **Admin** – The administrator has overview rights (view users, manage status).  
  **Reason:** Allows system management without mixing up roles.

### Item Management

- **Upload Item** – Traders can add items available for trading.
- **View Own Items** – Displays the items a user currently owns.
- **View Other Users' Items** – Search and browse items owned by other Traders.
- **View All Items** – Quick overview of all items in the system.  
  **Reason:** Items are the foundation of trading, and clarity helps users make informed decisions.

### Trade Management

- **Send Trade Request** – Offer one of your items in exchange for another user's item.
- **Accept/Deny** – The recipient can decide whether the trade occurs.
- **Completed Trades** – View the history of accepted or denied trades.  
  **Reason:** Core functionality enabling interactions between users with control and history tracking.

### Messaging

- **Send Message** – Traders can communicate directly.
- **Inbox** – View incoming messages, such as trade notifications.  
  **Reason:** Facilitates negotiation and agreement between users.

---

## Persistence (File-Based Storage)

| File          | Description                                    |
|---------------|-----------------------------------------------|
| `Users.txt`   | Stores username, hashed password, name, role, and status |
| `Items.txt`   | Stores item name, description, and owner      |
| `Messages.txt`| Stores sender, recipient, and message content|
| `Trades.txt`  | Stores trade requests including items and status |

**Reason:** Ensures the system state is preserved between sessions without needing a database.

---

## Technical Overview

- **IUser (interface):** Defines common methods for all users.
- **Trader & Admin (classes):** Implement IUser with different roles.
- **Item (class):** Represents tradeable objects.
- **TradeRequest (class):** Manages requests, accept/deny logic, and item ownership transfers.
- **Message (class):** Represents messages between users.
- **Logger / ItemStorage / TradeStorage / UserStorage (static classes):** Handles file-based save/load for all entities.
- **PasswordHelper (static class):** Handles password hashing and verification for security.

---

## Example Flow

1. Lennart and Roger register as Traders.
2. Lennart uploads "Computer", Roger uploads "Game".
3. Lennart sends a trade request offering "Computer" for "Game".
4. Roger sees the request and accepts.
5. Items automatically swap owners.
6. A message is sent indicating the trade was completed.
7. The trade history is updated and saved.

---

## User Guide – Step by Step

1. Start the program – the main menu is displayed.
2. Register a Trader if you are new; Admin account exists for system management.
3. Log in to access functionalities.
4. Manage your items:
   - Add items you want to trade.
   - View your own items or search for others’ items.
   - List all items in the system.
5. Send a trade request:
   - Select one of your items.
   - Offer it in exchange for another user’s item.
   - Wait for the recipient to accept or deny.
6. Communicate via messages:
   - Send direct messages.
   - Read new messages in your inbox.
7. Log out – all data is saved automatically for the next session.

---

## Design Choice: `else if` Instead of `switch`

**Reasoning:**

- **Flexibility:** Combines conditions (e.g., role + menu choice).  
- **Clarity:** Easier to read with complex rules.  
- **Extendability:** Simplifies adding new conditions.  
- **Safety:** Prevents bugs due to missing `break` statements.  
- **Readability:** Follows the natural flow of the program.

---

## Conclusion

Trading System is a simple yet powerful console application in C#. It demonstrates:

- User roles and security.
- Object-oriented design (classes, interfaces, roles).
- Persistence without a database.
- Communication and trading logic between users.

It can be easily extended with additional features, such as advanced permissions, more message types, or trade statistics and reporting.
