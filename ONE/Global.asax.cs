using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ONE
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            AuthorizeRequest += new EventHandler(MvcApplication_AuthorizeRequest);
        }
        void MvcApplication_AuthorizeRequest(object sender, EventArgs e)
        {

            IIdentity id = Context.User.Identity;
            if (id.IsAuthenticated)
            {

                var roles = new ONE._06ClassLibrary.UserAuthtication().GetRoles(id.Name);
                Context.User = new GenericPrincipal(id, roles);
                

            }



        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
