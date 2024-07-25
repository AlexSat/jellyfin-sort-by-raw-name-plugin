using Jellyfin.Data.Enums;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Model.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SortRawNamePlugin
{
    public class LibOverrider
    {
        private readonly MethodInfo _method;
        private readonly ILibraryManager _lm;

        public LibOverrider(ILibraryManager lm, MethodInfo method)
        {
            _method = method;
            _lm = lm;
        }

        public QueryResult<BaseItem> GetItemsResult(InternalItemsQuery query)
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
                    }
                    replasedOrderBy.Add(pair);
                }
                query.OrderBy = replasedOrderBy;
                var resRaw = _method.Invoke(_lm, [query]); // _lm.GetItemsResult(query);
                var res = (QueryResult<BaseItem>)resRaw;
                query.OrderBy = original;

                var sorted = new QueryResult<BaseItem>(_lm.Sort(res.Items, query.User, query.OrderBy).ToList());
                sorted.StartIndex = res.StartIndex;
                sorted.TotalRecordCount = res.TotalRecordCount;
                return sorted;
            }
            return _lm.GetItemsResult(query);
        }
    }
}
