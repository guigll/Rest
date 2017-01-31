using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Rest.Models;

namespace Rest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Pessoa>("Pessoas");
            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
