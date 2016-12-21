﻿using Autofac;
using Autofac.Integration.Mvc;
using Domain;
using Domain.Repository;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FinanceManager.Services;

namespace FinanceManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/justified-nav.css"));
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            builder.RegisterType<SessionProvider>().SingleInstance().WithParameter("connectionString", connectionString).SingleInstance();

            #region Register Repository

            builder.RegisterType<IncomeService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<OutGoingService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<SourceOfAmountService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<TypeOfOutgoingService>().AsSelf().InstancePerLifetimeScope();



            builder.RegisterType<IncomeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SourceOfAmountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OutgoingRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TypeOfOutgoingRepository>().InstancePerLifetimeScope();
            #endregion

            var container = builder.Build();


            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}