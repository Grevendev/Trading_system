
Trading System

Snabbstart
1. Starta programmet genom att köra dotnet run i projektmappen.
2. Registrera en Trader – välj "Registrera Trader" och skapa ett konto.
3. Logga in med ditt användarnamn och lösenord.
4. Lägg till ett item (ex. "Cykel") i din samling.
5. Visa andras items och skicka en trade request.
6. Acceptera/Neka trades i din lista.
7. Skicka och läs meddelanden för att kommunicera med andra Traders.
8. Logga ut – all data sparas automatiskt till nästa gång.

Om programmet:

Ett konsolbaserat tradingprogram i C# som låter användare:
- Registrera och logga in som Trader eller Admin
- Ladda upp och byta objekt (items)
- Skicka och hantera trade requests
- Kommunicera via ett meddelandesystem
- Behålla all data mellan sessioner tack vare filbaserad lagring

Systemet är byggt för att simulera ett enkelt men utbyggbart handelsnätverk mellan användare, där säkerhet, roller och tydlig logik är centrala designval.

Funktioner:

Användarhantering:

- Registrera ny Trader – Nya användare kan skapa ett konto.
Varför: Separera roller mellan Admin och Trader, samt koppla items till rätt användare.

- Login/Logout – Inloggning krävs för att utföra handlingar.
Varför: Säkerställer att endast rätt användare kan hantera sina resurser.

- Admin – Administratören har översiktsrättigheter (se användare, hantera status).
Varför: Möjliggör systemadministration utan att blanda ihop rollerna.

Item-hantering:

- Ladda upp item – Traders kan lägga till objekt att byta.
- Visa egna items – Översikt över vad man äger.
- Visa andra användares items – Hitta specifika objekt hos andra traders.
- Visa alla items – Snabb överblick över allt som finns i systemet.

Varför: Items är grunden för trading, och tydlig översikt gör att användare kan fatta beslut om byten.

Trade Management:

- Skicka trade request – Be om att byta ett av dina objekt mot någon annans.
- Acceptera/Neka – Mottagaren kan avgöra om bytet ska ske.
- Färdiga trades – Se historik över avslutade (accepterade eller nekade) trades.

Varför: Kärnan i systemet: möjliggör interaktion mellan användare, med tydlig historik och kontroll.

Meddelanden: 

- Skicka meddelande – Traders kan kommunicera direkt.
- Inbox – Se inkommande meddelanden, t.ex. notifieringar om trades.

Varför: Kommunikation underlättar förhandling och samförstånd mellan användare.

Persistens (filbaserad lagring): 

- Users.txt – sparar användarnamn, lösenord (hash), namn, roll och status.
- Items.txt – sparar alla items och deras ägare.
- Messages.txt – sparar skickade meddelanden.
- Trades.txt – sparar alla trade requests och deras status.

Varför: Gör att systemets tillstånd bevaras även efter programmet avslutas, utan att behöva en databas.

Tekniska förklaringar: 

- IUser (interface): Definierar gemensamma metoder för alla användare.
- Trader & Admin (klasser): Implementerar IUser med olika roller.
- Item (klass): Representerar objekt som kan bytas.
- TradeRequest (klass): Hanterar förfrågningar, accept/deny och ägarbyte av items.
- Message (klass): Representerar meddelanden mellan användare.
- Logger / ItemStorage (statiska klasser): Läser och sparar användare, items, trades och meddelanden till filer.
- PasswordHelper (statisk klass): Hashar och verifierar lösenord för säkerhet.

Exempelflöde: 

1. Lennart och Roger registrerar sig som Traders.
2. Lennart laddar upp "Dator", Roger laddar upp "Spel".
3. Lennart skickar en trade request: erbjuder "Dator" för "Spel".
4. Roger ser förfrågan och accepterar.
5. Items byter ägare automatiskt.
6. Ett meddelande skickas om att traden är genomförd.
7. Historiken sparas i systemet.

Användarguide – steg för steg:

1. Starta programmet – du möts av huvudmenyn.
2. Registrera en Trader om du är ny. Admin-konto finns för systemhantering.
3. Logga in för att komma åt funktioner.
4. Hantera dina items:
- Lägg till objekt du vill byta bort.
- Visa dina egna items eller sök efter andras.
- Lista alla items i systemet.
5. Skicka en trade request:
- Välj ett av dina items.
- Erbjud det i utbyte mot någon annans item.
- Vänta på att mottagaren accepterar eller nekar.
6. Kommunicera via meddelanden:
- Skicka direktmeddelanden.
- Läs nya meddelanden i din inbox.
7. Logga ut – programmet sparar allt till filer för nästa gång.

Designval: else if istället för switch: 

I menyhanteringen används else if-satser istället för switch-case. Detta är ett medvetet designval.

Varför?
- Flexibilitet – kan kombinera villkor (t.ex. roll + menyval).
- Tydlighet – enklare att läsa vid komplexa regler.
- Utbyggbarhet – lättare att lägga till nya regler.
- Säkerhet – inga buggar pga glömda break;.
- Läsvänlighet – följer programmets naturliga flöde.

Slutsats: 

Trading System är ett enkelt men kraftfullt konsolprogram byggt i C#.

Det visar:
- Användarroller och säkerhet
- Objektorienterad design (klasser, interface, roller)
- Persistens utan databas
- Kommunikation och handelslogik mellan användare

Det är lätt att bygga ut systemet med fler funktioner, exempelvis:
- Avancerad rättighetskontroll
- Fler typer av meddelanden
- Statistik och rapportering av trades



