using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace Agile_2018
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
            RouteTable.Routes.MapPageRoute("Login", "", "~/Login.aspx");
            RouteTable.Routes.MapPageRoute("Login2", "Login", "~/Login.aspx");
            RouteTable.Routes.MapPageRoute("AllProjects", "AllProjects", "~/Pages/AllProjects.aspx");
            RouteTable.Routes.MapPageRoute("ViewProject", "ViewProject", "~/Pages/ViewProject.aspx");
            RouteTable.Routes.MapPageRoute("Profile", "Profile", "~/Pages/Profile.aspx");
        }
    }
}
