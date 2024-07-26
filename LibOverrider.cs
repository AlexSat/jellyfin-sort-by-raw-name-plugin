using HarmonyLib;
using Jellyfin.Data.Enums;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Model.Querying;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SortRawNamePlugin
{
    public class LibOverrider
    {

        public static void Prefix(out IReadOnlyList<(ItemSortBy OrderBy, SortOrder SortOrder)> __state, ref InternalItemsQuery query)
        {
            if (query.OrderBy.Count > 0)
            {
                var original = query.OrderBy;
                var replasedOrderBy = new List<(ItemSortBy OrderBy, SortOrder SortOrder)>();
                foreach (var order in original)
                {
                    var pair = (order.OrderBy, order.SortOrder);
                    if (order.OrderBy == ItemSortBy.SortName)
                    {
                        pair.OrderBy = ItemSortBy.Name;
                        query.DtoOptions.Fields = query.DtoOptions.Fields.AddItem(ItemFields.SortName).ToList().AsReadOnly();
                    }
                    replasedOrderBy.Add(pair);
                }
                __state = original;
                query.OrderBy = replasedOrderBy;
            } else
            {
                __state = null;
            }
        }

        public static void Postfix(IReadOnlyList<(ItemSortBy OrderBy, SortOrder SortOrder)> __state, ref InternalItemsQuery query, ref QueryResult<BaseItem> __result, object __instance)
        {
            if (__state != null)
            {
                query.OrderBy = __state;
                var sorted = new QueryResult<BaseItem>(((ILibraryManager)__instance).Sort(__result.Items, query.User, query.OrderBy).ToList());
                sorted.StartIndex = __result.StartIndex;
                sorted.TotalRecordCount = __result.TotalRecordCount;
                __result = sorted;
            }
        }
    }
}
