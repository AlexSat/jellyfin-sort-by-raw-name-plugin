using Jellyfin.Data.Enums;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller;
using MediaBrowser.Controller.Configuration;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Controller.Sorting;
using MediaBrowser.Model.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SortRawNamePlugin.Configuration;

namespace SortRawNamePlugin
{
    public class Plugin : BasePlugin<PluginConfiguration>
    {

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer) : base(applicationPaths, xmlSerializer)
        {
        }

        public override string Name => "SortByRawName";
        public override Guid Id => Guid.Parse("BDBFD97C-CCD9-4226-B28C-B272F5A1617C");
    }
}
