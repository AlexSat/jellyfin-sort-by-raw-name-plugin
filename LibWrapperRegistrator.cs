using HarmonyLib;
using MediaBrowser.Controller;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace SortRawNamePlugin
{
    public class LibWrapperRegistrator : IPluginServiceRegistrator
    {
        public LibWrapperRegistrator() { }

        void IPluginServiceRegistrator.RegisterServices(IServiceCollection serviceCollection, IServerApplicationHost applicationHost)
        {
            var harmony = new Harmony("com.example.patch");
            var originalType = applicationHost.GetExportTypes<ILibraryManager>().Last();
            var mOriginal = AccessTools.Method(originalType, "GetItemsResult");
            var prefix = typeof(LibOverrider).GetMethod("Prefix");
            var postfix = typeof(LibOverrider).GetMethod("Postfix");
            harmony.Patch(mOriginal, new HarmonyMethod(prefix), new HarmonyMethod(postfix));
        }
    }
}
