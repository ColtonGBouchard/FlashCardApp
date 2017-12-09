using System.Web.Mvc;
using System.Web.Routing;

namespace FlashCardApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ViewDeck",
                "{controller}/{action}/{id}", 
                new { controller = "Deck", action = "ViewCardsFromDeck", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}