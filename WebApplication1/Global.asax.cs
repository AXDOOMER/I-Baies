﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication1
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		protected void Session_Start()
		{
			Session["UserValid"] = false;
			string DB_Path = Server.MapPath(@"~\App_Data\MainDB.mdf");
			Session["MainDB"] = @"Data Source=(LocalDB)\mssqllocaldb;AttachDbFilename='" + DB_Path + "'; Integrated Security=true";
		}
	}
}
