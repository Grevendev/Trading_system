# Trading System

## Quick Start

Run the program by executing `dotnet run` in the project folder.  
Register a Trader – choose "Register Trader" and create an account.  
Log in with your username and password.  
Add an item (e.g., "Bicycle") to your collection.  
View other users' items and send a trade request.  
Accept/Deny trades in your list.  
Send and read messages to communicate with other Traders.  
Log out – all data is automatically saved for the next session.

## About the Program

A console-based trading program in C# that allows users to:

- Register and log in as Trader or Admin
- Upload and trade items
- Send and manage trade requests
- Communicate via a messaging system
- Persist all data between sessions using file-based storage

The system is designed to simulate a simple but extensible trading network between users, with a focus on security, roles, and clear logic.

## Features

### User Management

- **Register new Trader** – New users can create an account.  
  **Why:** Separate roles between Admin and Trader, and link items to the correct user.

- **Login/Logout** – Login is required to perform actions.  
  **Why:** Ensures only the correct user can manage their resources.

- **Admin** – The administrator has oversight rights (view users, manage status).  
  **Why:** Allows system administration without mixing roles.

### Item Management

- **Upload item** – Traders can add objects for trading.  
- **Show own items** – Overview of owned items.  
- **Show other users' items** – Find specific items from other traders.  
- **Show all items** – Quick overview of everything in the system.  
**Why:** Items are the basis for trading, and clear visibility helps users make decisions.

### Trade Management

- **Send trade request** – Request to exchange one of your items for someone else's.  
- **Accept/Deny** – The recipient can decide whether the trade occurs.  
- **Completed trades** – View history of completed (accepted or denied) trades.  
**Why:** Core functionality: enables user interaction with clear history and control.

### Messaging

- **Send message** – Traders can communicate directly.  
- **Inbox** – View incoming messages, e.g., trade notifications.  
**Why:** Communication facilitates negotiation and agreement between users.

### Persistence (File-Based Storage)

- `Users.txt` – Stores usernames, password hashes, name, role, and status.  
- `Items.txt` – Stores all items and their owners.  
- `Messages.txt` – Stores sent messages.  
- `Trades.txt` – Stores all trade requests and their status.  
**Why:** Ensures the system's state is preserved even after the program closes, without a database.

## Technical Details

- **IUser (interface):** Defines common methods for all users.  
- **Trader & Admin (classes):** Implement IUser with different roles.  
- **Item (class):** Represents an object that can be traded.  
- **TradeRequest (class):** Handles requests, accept/deny, and item ownership changes.  
- **Message (class):** Represents messages between users.  
- **Logger / ItemStorage (static classes):** Reads and saves users, items, trades, and messages to files.  
- **PasswordHelper (static class):** Hashes and verifies passwords for security.

## Example Flow

1. Lennart and Roger register as Traders.  
2. Lennart uploads "Computer", Roger uploads "Game".  
3. Lennart sends a trade request: offering "Computer" for "Game".  
4. Roger sees the request and accepts.  
5. Items swap ownership automatically.  
6. A message is sent that the trade is completed.  
7. History is saved in the system.

## User Guide – Step by Step

1. Start the program – you are greeted with the main menu.  
2. Register a Trader if new. Admin account exists for system management.  
3. Log in to access features.  
4. Manage your items:  
   - Add items you want to trade.  
   - Show your own items or search for others.  
   - List all items in the system.  
5. Send a trade request:  
   - Choose one of your items.  
   - Offer it in exchange for another user's item.  
   - Wait for the recipient to accept or deny.  
6. Communicate via messages:  
   - Send direct messages.  
   - Read new messages in your inbox.  
7. Log out – the program saves everything to files for the next session.

### Design Choice: `else if` Instead of `switch`

- **Why:**  
  - Flexibility – can combine conditions (e.g., role + menu choice).  
  - Clarity – easier to read for complex rules.  
  - Extensibility – easy to add new rules.  
  - Safety – avoids bugs due to forgotten `break;`.  
  - Readability – follows the natural flow of the program.

## Conclusion

Trading System is a simple yet powerful console application built in C# that demonstrates:

- User roles and security
- Object-oriented design (classes, interfaces, roles)
- Persistence without a database
- User interaction and trading logic

It is easily extendable with additional features, such as:

- Advanced permission control
- More message types
- Trade statistics and reporting

---

**Author:** Edvin
