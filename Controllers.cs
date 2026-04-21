// =============================================
// HomeController.cs
// =============================================
using System.Web.Mvc;

namespace BidKaro.Controllers
{
    public class HomeController : Controller
    {
        // GET: /
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        // GET: /Home/HowItWorks
        public ActionResult HowItWorks()
        {
            ViewBag.Title = "How It Works";
            return View();
        }

        // GET: /Home/About
        public ActionResult About()
        {
            ViewBag.Title = "About BidKaro";
            return View();
        }

        // GET: /Home/Contact
        public ActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            return View();
        }
    }
}


// =============================================
// AuctionController.cs
// =============================================
using System.Collections.Generic;
using System.Web.Mvc;
using BidKaro.Models;

namespace BidKaro.Controllers
{
    public class AuctionController : Controller
    {
        // GET: /Auction/Live
        public ActionResult Live(string category = null, string city = null, string source = null,
                                  string status = "live", int page = 1, int pageSize = 12,
                                  decimal minPrice = 0, decimal maxPrice = 99999999,
                                  string sort = "ending_soon")
        {
            ViewBag.Title = "Live Auctions";
            ViewBag.Category = category;
            ViewBag.City = city;
            ViewBag.Status = status;
            ViewBag.CurrentPage = page;

            // In production: query DB with EF / Dapper
            var auctions = GetMockAuctions(category, city, source, status, page, pageSize, minPrice, maxPrice, sort);
            return View(auctions);
        }

        // GET: /Auction/Category/{slug}
        public ActionResult Category(string slug)
        {
            ViewBag.Title = slug + " Auctions";
            return RedirectToAction("Live", new { category = slug });
        }

        // GET: /Auction/Detail/{id}
        public ActionResult Detail(int id)
        {
            ViewBag.Title = "Auction Detail";
            var auction = GetMockAuctionById(id);
            if (auction == null) return HttpNotFound();
            return View(auction);
        }

        // POST: /Auction/PlaceBid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceBid(int auctionId, decimal amount)
        {
            // Validate user is logged in (use [Authorize] in production)
            // Validate bid > current + increment
            // Save to DB
            // Push SignalR notification to all connected bidders
            return Json(new
            {
                success = true,
                message = "Bid placed successfully!",
                newAmount = amount,
                totalBids = 35
            });
        }

        // POST: /Auction/Watchlist (AJAX)
        [HttpPost]
        public ActionResult ToggleWatchlist(int auctionId)
        {
            // Toggle watchlist in DB
            return Json(new { success = true, isWatching = true });
        }

        // GET: /Auction/Search (AJAX autocomplete)
        public ActionResult Search(string q)
        {
            var results = new List<object>
            {
                new { id = 1, text = "2020 Maruti Swift VXI - Mumbai" },
                new { id = 2, text = "2019 Honda Activa 5G - Delhi" },
                new { id = 3, text = "2021 Mahindra Bolero - Noida" }
            };
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        // ---- Private helpers (replace with real DB calls) ----
        private List<AuctionListing> GetMockAuctions(string category, string city, string source,
                                                       string status, int page, int pageSize,
                                                       decimal minPrice, decimal maxPrice, string sort)
        {
            return new List<AuctionListing>
            {
                new AuctionListing { Id=1, Title="2019 Honda Activa 5G", City="Delhi", CurrentBid=32500, Bids=12, EndsIn="02:14:33", Source="Bank Repo", KMs=22000, Year=2019, Category="2-Wheeler" },
                new AuctionListing { Id=2, Title="2020 Maruti Swift VXI", City="Mumbai", CurrentBid=485000, Bids=34, EndsIn="00:45:12", Source="NBFC", KMs=38500, Year=2020, Category="4-Wheeler" },
                new AuctionListing { Id=3, Title="2018 Bajaj Pulsar NS200", City="Gurgaon", CurrentBid=68000, Bids=8, EndsIn="01:30:55", Source="Consumer", KMs=55000, Year=2018, Category="2-Wheeler" },
                new AuctionListing { Id=4, Title="2021 Mahindra Bolero", City="Noida", CurrentBid=720000, Bids=21, EndsIn="03:12:08", Source="Insurance", KMs=41200, Year=2021, Category="4-Wheeler" },
                new AuctionListing { Id=5, Title="2017 Tata Ace Gold", City="Bangalore", CurrentBid=310000, Bids=15, EndsIn="00:12:44", Source="Leasing", KMs=78000, Year=2017, Category="Commercial" },
            };
        }

        private AuctionListing GetMockAuctionById(int id)
        {
            return new AuctionListing
            {
                Id = id,
                Title = "2020 Maruti Swift VXI",
                City = "Mumbai",
                CurrentBid = 485000,
                BasePrice = 300000,
                MinIncrement = 5000,
                Bids = 34,
                EndsIn = "00:45:12",
                Source = "NBFC",
                KMs = 38500,
                Year = 2020,
                Category = "4-Wheeler",
                Make = "Maruti Suzuki",
                Model = "Swift VXI",
                Fuel = "Petrol",
                Transmission = "Manual",
                Color = "Pearl Arctic White",
                LotNumber = "BK-2024-00782"
            };
        }
    }
}


// =============================================
// AccountController.cs
// =============================================
using System.Web.Mvc;
using BidKaro.Models;

namespace BidKaro.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public ActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);

            // Authenticate user (use ASP.NET Identity or custom auth)
            // FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Create user in DB, send OTP for mobile verification
            return RedirectToAction("VerifyOTP", new { mobile = model.Mobile });
        }

        // GET: /Account/VerifyOTP
        public ActionResult VerifyOTP(string mobile)
        {
            ViewBag.Mobile = mobile;
            return View();
        }

        // POST: /Account/VerifyOTP
        [HttpPost]
        public ActionResult VerifyOTP(string mobile, string otp)
        {
            // Validate OTP
            return RedirectToAction("Dashboard");
        }

        // GET: /Account/Dashboard
        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            // FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}
