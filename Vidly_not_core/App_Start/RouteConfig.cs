using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly_not_core
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //ahelyett, hogy a lenti "régi típusú" routingot használnánk, az attribútum routing javasolt, amik mellőzik a
            //beégett stringeket, így nem kell emlékezni rá minden esetben amikor átnevezünk egy action-t, hogy itt is
            //meg kell változtatni a megfelelő értékeket.

            //ezzel a hívással lehet engedélyezni az attribútum routing-ot:
            routes.MapMvcAttributeRoutes();



            /* Erre a blokkra már nem lesz szükség az attribut routing után
              
              //számít a sorrend, így a saját custom route-okat mindig korábbra kell tenni, mint a generic ahol van!!!
              routes.MapRoute(
                  name: "MoviesByReleaseDate",
                  url: "movies/released/{year}/{month}",
                  defaults: new { controller = "Movies", action = "ByReleaseDate" },
                  //constraint a formátumra reguláris kifejezéssel
                  //az @ jel azért kell, hogy ne kelljen duplán a \
                  //ez hibát dob, ha nem a 7.2-es language verziót használjuk as minimum
                  new { year = @"\d{4}", month = @"\d{2}" }
              );
            */
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
