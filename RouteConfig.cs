// =============================================
// App_Start/RouteConfig.cs
// =============================================
using System.Web.Mvc;
using System.Web.Routing;

namespace BidKaro
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Auction category shortcut
            routes.MapRoute(
                name: "AuctionCategory",
                url: "auctions/{category}",
                defaults: new { controller = "Auction", action = "Live" }
            );

            // Auction detail
            routes.MapRoute(
                name: "AuctionDetail",
                url: "auction/{id}",
                defaults: new { controller = "Auction", action = "Detail" }
            );

            // Default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}


// =============================================
// Global.asax.cs
// =============================================
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace BidKaro
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}


// =============================================
// App_Start/FilterConfig.cs
// =============================================
using System.Web.Mvc;

namespace BidKaro
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}


// =============================================
// PACKAGES REQUIRED (NuGet)
// =============================================
// Install-Package Microsoft.AspNet.Mvc -Version 5.2.9
// Install-Package Microsoft.AspNet.Web.Optimization -Version 1.1.3
// Install-Package EntityFramework -Version 6.4.4
// Install-Package Microsoft.Owin.Host.SystemWeb -Version 4.2.2
// Install-Package Microsoft.AspNet.Identity.Owin -Version 2.2.4
// Install-Package Microsoft.AspNet.SignalR -Version 2.4.3  (for real-time bidding)
// Install-Package Newtonsoft.Json -Version 13.0.3


// =============================================
// SIGNALR HUB - Real-time Bidding (Optional)
// =============================================
// Hubs/BidHub.cs
// using Microsoft.AspNet.SignalR;
// using System.Threading.Tasks;
//
// namespace BidKaro.Hubs
// {
//     public class BidHub : Hub
//     {
//         public async Task JoinAuction(string auctionId)
//         {
//             await Groups.Add(Context.ConnectionId, "auction-" + auctionId);
//         }
//
//         public async Task PlaceBid(int auctionId, decimal amount)
//         {
//             // Validate, save, then broadcast
//             await Clients.Group("auction-" + auctionId)
//                 .bidUpdated(new {
//                     auctionId,
//                     amount,
//                     bidder = "***" + Context.ConnectionId.Substring(0, 2),
//                     time = System.DateTime.Now
//                 });
//         }
//     }
// }
