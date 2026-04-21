# BidKaro – ASP.NET MVC Vehicle Auction Platform
## Inspired by BidKaro.net | Built for .NET Framework 4.8

---

## 📁 Project Structure

```
BidKaro/
├── App_Start/
│   └── RouteConfig.cs          ← URL routing config
├── Controllers/
│   └── Controllers.cs          ← HomeController, AuctionController, AccountController
├── Models/
│   └── Models.cs               ← AuctionListing, BidHistory, User, ViewModels, DbContext
├── Views/
│   ├── Shared/
│   │   └── _Layout.cshtml      ← Master layout (navbar, footer)
│   ├── Home/
│   │   └── Index.cshtml        ← Homepage (hero, categories, live auctions, CTA)
│   ├── Auction/
│   │   ├── Live.cshtml         ← Auction listing with filters
│   │   └── Detail.cshtml       ← Single auction with live bidding panel
│   └── Account/
│       ├── Login.cshtml        ← Login page
│       └── Register.cshtml     ← Registration page
├── wwwroot/
│   ├── css/site.css            ← Complete stylesheet
│   └── js/site.js              ← Countdown timers, filters, interactivity
└── Web.config                  ← SQL connection, auth, EF config
```

---

## 🚀 Getting Started

### Prerequisites
- Visual Studio 2019/2022
- .NET Framework 4.8
- SQL Server / SQL Server Express / LocalDB
- NuGet Package Manager

### 1. Create the Project
```
File → New Project → ASP.NET Web Application (.NET Framework)
→ Select "MVC" template
→ Framework: .NET Framework 4.8
→ Project Name: BidKaro
```

### 2. Install NuGet Packages
Open Package Manager Console and run:
```powershell
Install-Package Microsoft.AspNet.Mvc -Version 5.2.9
Install-Package EntityFramework -Version 6.4.4
Install-Package Microsoft.AspNet.Identity.Owin -Version 2.2.4
Install-Package Microsoft.Owin.Host.SystemWeb -Version 4.2.2
Install-Package Microsoft.AspNet.SignalR -Version 2.4.3
Install-Package Newtonsoft.Json -Version 13.0.3
```

### 3. Copy Source Files
Replace the generated files with files from this project (or add them alongside).

### 4. Database Setup
```powershell
# In Package Manager Console:
Enable-Migrations
Add-Migration InitialCreate
Update-Database
```

### 5. Run the Project
Press `F5` or `Ctrl+F5` in Visual Studio.

---

## 📄 Pages & Features

| Page | URL | Description |
|------|-----|-------------|
| Home | `/` | Hero search, categories, live auctions preview |
| Live Auctions | `/Auction/Live` | Filter sidebar + auction grid with timers |
| Auction Detail | `/Auction/Detail/{id}` | Gallery, specs, live bid panel, bid history |
| Login | `/Account/Login` | Split-screen auth page |
| Register | `/Account/Register` | Registration with validation |
| Dashboard | `/Account/Dashboard` | User's bids, watchlist, profile |

---

## 🔧 Key Features Implemented

### Frontend
- ✅ Sticky navbar with dropdown menus
- ✅ Hero section with vehicle search filters
- ✅ Live countdown timers (auto-decrement)
- ✅ Auction cards with real-time bid display
- ✅ Filter sidebar (category, city, price, year, source)
- ✅ Grid/list view toggle
- ✅ Watchlist (heart) toggle
- ✅ Responsive (mobile breakpoints)
- ✅ Animated scroll reveals
- ✅ Bid input with quick-add buttons (+5K, +10K, etc.)
- ✅ Tab-based detail page (Overview / Specs / Inspection / History)

### Backend (ASP.NET MVC .NET Framework)
- ✅ MVC Controllers (Home, Auction, Account)
- ✅ Entity Framework 6 models & DbContext
- ✅ Data Annotations validation
- ✅ Forms Authentication
- ✅ AntiForgeryToken on POST forms
- ✅ Custom route configuration
- ✅ AJAX endpoints (PlaceBid, Watchlist, Search)
- ✅ SignalR hub skeleton for real-time bidding

---

## 🔐 Security Checklist
- [ ] HTTPS enforced (web.config rewrite rule included)
- [ ] Anti-CSRF tokens on all POST forms
- [ ] [Authorize] on protected actions
- [ ] SQL Injection prevention via Entity Framework parameterized queries
- [ ] Password hashing with BCrypt or ASP.NET Identity
- [ ] OTP-based mobile verification
- [ ] Session timeout configured (60 mins)

---

## 🗄️ Database Tables

```sql
-- AuctionListings
-- BidHistory  
-- Users
-- (Add: Categories, Cities, Documents, Payments, Watchlist)
```

---

## 🛠️ Production Recommendations

1. **Real-time bidding**: Enable the SignalR `BidHub` and connect clients via JavaScript
2. **Payment gateway**: Integrate Razorpay or PayU for bid deposits
3. **OTP service**: Use MSG91 or Twilio for mobile OTP
4. **Image storage**: Azure Blob Storage or AWS S3 for vehicle photos
5. **Search**: Elasticsearch or SQL Full-Text Search for vehicle search
6. **Caching**: Redis for live auction data (bid counts, current price)

---

## 📱 App Downloads (as on BidKaro.net)
- Google Play: `in.car.bidkaro`
- Apple App Store: `id1625093106`
