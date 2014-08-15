using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Prova.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.1.min.js"));

            bundles.Add(new StyleBundle("~/bundles/style").Include(
                        "~/Styles/style.css"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}