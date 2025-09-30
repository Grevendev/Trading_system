Welcome


Trading System

Är ett konsolbaserat tradingprogram i C#.
Som låter användare att regitrera sig, logga in, byta objektm skicka meddelande och hantera trade requests.
Programmet använder filbaserad lagring föra att all deta ska finnas kvar mellan sessioner. 

Funktioner och användarhantering. 

Registrera ny Trader: Tillåter nya användare att skapa ett komto. 
Varför; För att seperara roller mellan admin och traders, samt för att kunna koppla items till rätt användare. 

Login/Logout: Användare loggar in med sitt konto flr att få tillgång till funktion.
Varför: Säkerstöller att endast rätt användare kan uföra actions som påverkar deras items eller trade requests. 

Admin: Administratören har specialrättigheter, t.ex. för att kunna se alla användare.
Varför: För att möjliggöra övervakning och management utan att blanda in traders i administrationsuppgifter.

Item hantering: 
Ladda upp item: Traders kan lägga till objekt de vill byta. 
Varför: Nödvändigt för trading-logiken och för att kunna erbjuda saker till andra användare. 

Visa egna items: Lista alla items som användare äger. 
Varför: Ger översikt över egna resurser. 

Visa andra användares items: Sök på en specifik användare och se deras items.
Varför: För att kuna välja vad man vill byta mot.

Visa alla items: Lista alla items i systemet. 
Varför: Ger en snabb överblick och möjligöra fler trade-möjligheter. 

Trade Management

Skcika trade request: Skicka en förfrågan om att byta ett eget item mot någon annans.
Varför: Kärnan i systemet, möjliggör interaktion mellan användare. 

Acceptera/Avslå request: Den mottagande användaren kan acceptera eller neka en förfrågan. 
varför: Säkerställer att trades endast genomförs med båda parters samtycke. 

Färdiga trades: Lista alla trades som har accepterats eller nekats.
Varför: För att kunna se historik och följa upp tidigare utbyten.

Meddelanden

Skicka meddelande: Traders kan kommunicera med varandra.
Varför: Underlättar samförstånd och koordinering av trades.

Inbox: Se meddelanden man fått.
Varför: För att hålla koll på kommunikation och notiser om trade requests.

Persistens (filbaserad lagring)

Users.txt: Lagrar användarnamn, lösenord (hashat), namn, roll och aktiv status.

Items.txt: Lagrar items och vilken användare som äger dem.

Messages.txt: Lagrar skickade meddelanden.

Trades.txt: Lagrar trade requests och deras status.

Varför: För att data ska finnas kvar även efter att programmet stängts.

Tekniska Förklaringar

IUser (interface): Definierar gemensamma egenskaper och metoder för alla användare.
Varför: Gör att programmet kan hantera både traders och admins på ett enhetligt sätt.

Trader och Admin (klasser): Implementerar IUser med specifika metoder för respektive roll.
Varför: Traders har items och kan göra trades, medan Admin kan övervaka systemet.

Item (klass): Representerar objekt som kan bytas.
Varför: Objekt är själva grunden för trading-logiken.

TradeRequest (klass): Representerar en tradeförfrågan mellan två användare.
Varför: Hanterar logik för offer, request, accept och deny.

Message (klass): Representerar meddelanden mellan användare.
Varför: Möjliggör kommunikation kopplad till trade-förfrågningar.

Logger / UserStorage / ItemStorage (statisk klass): Ansvarar för att spara och läsa användare, items, trades och meddelanden till/från filer.
Varför: Säkerställer persistens och enkel filhantering utan databas.

PasswordHelper (statisk klass): Hashar och verifierar lösenord.
Varför: Ger grundläggande säkerhet för användares lösenord.

Exempelflöde

Alice och Bob registrerar sig som traders.

Alice laddar upp “Bok”, Bob laddar upp “Spel”.

Alice skickar en trade request till Bob, erbjuder “Bok” för “Spel”.

Bob ser requesten, accepterar.

Ägarskapet av items byts automatiskt och båda får meddelande om trade.

Flödesschema för TradeRequest
[Trader A]          [Trader B]
   |                   |
   |-- Skicka request ->|
   |                   |
   |<-- Visa request ---|
   |                   |
   |-- Acceptera / Neka |
   |                   |
   |-- Om Acceptera -> Byt items
   |                   |
   |-- Skicka meddelande


Beskrivning:

Trader A skickar en trade request till Trader B.

Trader B ser requesten i sin lista.

Trader B kan acceptera eller neka requesten.

Om accept → ägarskap av items byts automatiskt och båda får meddelande.

Historiken sparas i systemet så det går att se alla färdiga trades.
