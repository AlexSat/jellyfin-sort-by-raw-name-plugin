using Jellyfin.Data.Enums;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Sorting;
using MediaBrowser.Model.Serialization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRawNamePlugin
{
    public class SortByNameComparer : IBaseItemComparer
    {
        private readonly ILogger<Plugin> _logger;

        public SortByNameComparer(ILogger<Plugin> logger)
        {
            _logger = logger;
        }

        public ItemSortBy Type => ItemSortBy.SortName;

        public int Compare(BaseItem? x, BaseItem? y)
        {
            ArgumentNullException.ThrowIfNull(x);
            ArgumentNullException.ThrowIfNull(y);
            var first = GetSortName(x);
            var second = GetSortName(y);
            _logger.LogDebug("Comparing: '{First}' vs '{Second}'", first, second);

            return string.Compare(first, second, StringComparison.OrdinalIgnoreCase);
        }

        internal static string GetSortName(BaseItem item)
        {
            if (!string.IsNullOrEmpty(item.ForcedSortName))
            {
                // Need the ToLower because that's what CreateSortName does
                return item.ForcedSortName.ToLowerInvariant();
            }
            else
            {
                return CreateSortName(item);
            }
        }

        internal static string CreateSortName(BaseItem item)
        {
            if (item.Name is null)
            {
                return null; // some items may not have name filled in properly
            }

            if (!item.EnableAlphaNumericSorting)
            {
                return item.Name.TrimStart();
            }

            var sortable = item.Name.Trim().ToLowerInvariant();

            foreach (var search in BaseItem.ConfigurationManager.Configuration.SortRemoveWords)
            {
                // Remove from beginning if a space follows
                if (sortable.StartsWith(search + " ", StringComparison.Ordinal))
                {
                    sortable = sortable.Remove(0, search.Length + 1);
                }

                // Remove from middle if surrounded by spaces
                sortable = sortable.Replace(" " + search + " ", " ", StringComparison.Ordinal);

                // Remove from end if followed by a space
                if (sortable.EndsWith(" " + search, StringComparison.Ordinal))
                {
                    sortable = sortable.Remove(sortable.Length - (search.Length + 1));
                }
            }

            foreach (var removeChar in BaseItem.ConfigurationManager.Configuration.SortRemoveCharacters)
            {
                sortable = sortable.Replace(removeChar, string.Empty, StringComparison.Ordinal);
            }

            foreach (var replaceChar in BaseItem.ConfigurationManager.Configuration.SortReplaceCharacters)
            {
                sortable = sortable.Replace(replaceChar, " ", StringComparison.Ordinal);
            }

            return sortable;
        }
    }
}
