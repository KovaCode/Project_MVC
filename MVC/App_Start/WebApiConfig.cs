using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MVC.App_Start
{
        public static class WebApiConfig
        {
            #region Methods

            public static void Register(HttpConfiguration config)
            {
                // Web API configuration and services

                // Web API routes
                config.MapHttpAttributeRoutes();
            }

            #endregion Methods
        }
}