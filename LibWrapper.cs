using Jellyfin.Data.Entities;
using Jellyfin.Data.Enums;
using MediaBrowser.Controller.Dto;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Audio;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Resolvers;
using MediaBrowser.Controller.Sorting;
using MediaBrowser.Model.Configuration;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.IO;
using MediaBrowser.Model.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRawNamePlugin
{
    public class LibWrapper : ILibraryManager
    {
        private ILibraryManager _lm;

        public LibWrapper(ILibraryManager lm)
        {
            _lm = lm;
        }

        public AggregateFolder RootFolder => _lm.RootFolder;

        public bool IsScanRunning => _lm.IsScanRunning;

        public event EventHandler<ItemChangeEventArgs>? ItemAdded
        {
            add
            {
                _lm.ItemAdded += value;
            }

            remove
            {
                _lm.ItemAdded -= value;
            }
        }

        public event EventHandler<ItemChangeEventArgs>? ItemUpdated
        {
            add
            {
                _lm.ItemUpdated += value;
            }

            remove
            {
                _lm.ItemUpdated -= value;
            }
        }

        public event EventHandler<ItemChangeEventArgs>? ItemRemoved
        {
            add
            {
                _lm.ItemRemoved += value;
            }

            remove
            {
                _lm.ItemRemoved -= value;
            }
        }

        public void AddMediaPath(string virtualFolderName, MediaPathInfo mediaPath)
        {
            _lm.AddMediaPath(virtualFolderName, mediaPath);
        }

        public void AddParts(IEnumerable<IResolverIgnoreRule> rules, IEnumerable<IItemResolver> resolvers, IEnumerable<IIntroProvider> introProviders, IEnumerable<IBaseItemComparer> itemComparers, IEnumerable<ILibraryPostScanTask> postscanTasks)
        {
            _lm.AddParts(rules, resolvers, introProviders, itemComparers, postscanTasks);
        }

        public Task AddVirtualFolder(string name, CollectionTypeOptions? collectionType, LibraryOptions options, bool refreshLibrary)
        {
            return _lm.AddVirtualFolder(name, collectionType, options, refreshLibrary);
        }

        public Task<ItemImageInfo> ConvertImageToLocal(BaseItem item, ItemImageInfo image, int imageIndex, bool removeOnFailure = true)
        {
            return _lm.ConvertImageToLocal(item, image, imageIndex, removeOnFailure);
        }

        public void CreateItem(BaseItem item, BaseItem? parent)
        {
            _lm.CreateItem(item, parent);
        }

        public void CreateItems(IReadOnlyList<BaseItem> items, BaseItem? parent, CancellationToken cancellationToken)
        {
            _lm.CreateItems(items, parent, cancellationToken);
        }

        public void DeleteItem(BaseItem item, DeleteOptions options)
        {
            _lm.DeleteItem(item, options);
        }

        public void DeleteItem(BaseItem item, DeleteOptions options, bool notifyParentItem)
        {
            _lm.DeleteItem(item, options, notifyParentItem);
        }

        public void DeleteItem(BaseItem item, DeleteOptions options, BaseItem parent, bool notifyParentItem)
        {
            _lm.DeleteItem(item, options, parent, notifyParentItem);
        }

        public bool FillMissingEpisodeNumbersFromPath(Episode episode, bool forceRefresh)
        {
            return _lm.FillMissingEpisodeNumbersFromPath(episode, forceRefresh);
        }

        public BaseItem? FindByPath(string path, bool? isFolder)
        {
            return _lm.FindByPath(path, isFolder);
        }

        public IEnumerable<BaseItem> FindExtras(BaseItem owner, IReadOnlyList<FileSystemMetadata> fileSystemChildren, IDirectoryService directoryService)
        {
            return _lm.FindExtras(owner, fileSystemChildren, directoryService);
        }

        public QueryResult<(BaseItem Item, ItemCounts ItemCounts)> GetAlbumArtists(InternalItemsQuery query)
        {
            return _lm.GetAlbumArtists(query);
        }

        public QueryResult<(BaseItem Item, ItemCounts ItemCounts)> GetAllArtists(InternalItemsQuery query)
        {
            return _lm.GetAllArtists(query);
        }

        public MusicArtist GetArtist(string name)
        {
            return _lm.GetArtist(name);
        }

        public MusicArtist GetArtist(string name, DtoOptions options)
        {
            return _lm.GetArtist(name, options);
        }

        public QueryResult<(BaseItem Item, ItemCounts ItemCounts)> GetArtists(InternalItemsQuery query)
        {
            return _lm.GetArtists(query);
        }

        public List<Folder> GetCollectionFolders(BaseItem item)
        {
            return _lm.GetCollectionFolders(item);
        }

        public List<Folder> GetCollectionFolders(BaseItem item, IEnumerable<Folder> allUserRootChildren)
        {
            return _lm.GetCollectionFolders(item, allUserRootChildren);
        }

        public CollectionType? GetConfiguredContentType(BaseItem item)
        {
            return _lm.GetConfiguredContentType(item);
        }

        public CollectionType? GetConfiguredContentType(string path)
        {
            return _lm.GetConfiguredContentType(path);
        }

        public CollectionType? GetContentType(BaseItem item)
        {
            return _lm.GetContentType(item);
        }

        public int GetCount(InternalItemsQuery query)
        {
            return _lm.GetCount(query);
        }

        public Genre GetGenre(string name)
        {
            return _lm.GetGenre(name);
        }

        public Guid GetGenreId(string name)
        {
            return _lm.GetGenreId(name);
        }

        public QueryResult<(BaseItem Item, ItemCounts ItemCounts)> GetGenres(InternalItemsQuery query)
        {
            return _lm.GetGenres(query);
        }

        public CollectionType? GetInheritedContentType(BaseItem item)
        {
            return _lm.GetInheritedContentType(item);
        }

        public Task<IEnumerable<Video>> GetIntros(BaseItem item, User user)
        {
            return _lm.GetIntros(item, user);
        }

        public BaseItem? GetItemById(Guid id)
        {
            return _lm.GetItemById(id);
        }

        public T? GetItemById<T>(Guid id) where T : BaseItem
        {
            return _lm.GetItemById<T>(id);
        }

        public T? GetItemById<T>(Guid id, Guid userId) where T : BaseItem
        {
            return _lm.GetItemById<T>(id, userId);
        }

        public T? GetItemById<T>(Guid id, User? user) where T : BaseItem
        {
            return _lm.GetItemById<T>(id, user);
        }

        public List<Guid> GetItemIds(InternalItemsQuery query)
        {
            return _lm.GetItemIds(query);
        }

        public List<BaseItem> GetItemList(InternalItemsQuery query)
        {
            return _lm.GetItemList(query);
        }

        public List<BaseItem> GetItemList(InternalItemsQuery query, bool allowExternalContent)
        {
            return _lm.GetItemList(query, allowExternalContent);
        }

        public List<BaseItem> GetItemList(InternalItemsQuery query, List<BaseItem> parents)
        {
            return _lm.GetItemList(query, parents);
        }

        public QueryResult<BaseItem> GetItemsResult(InternalItemsQuery query)
        {
            if (query.OrderBy.Count > 0)
            {
                var original = query.OrderBy;
                var replasedOrderBy = new List<(ItemSortBy OrderBy, SortOrder SortOrder)>();
                foreach(var order in original)
                {
                    var pair = (order.OrderBy, order.SortOrder);
                    if (order.OrderBy == ItemSortBy.SortName)
                    {
                        pair.OrderBy = ItemSortBy.Name;
                    }
                    replasedOrderBy.Add(pair);
                }
                query.OrderBy = replasedOrderBy;
                var res = _lm.GetItemsResult(query);
                query.OrderBy = original;
            
                var sorted = new QueryResult<BaseItem>(Sort(res.Items, query.User, query.OrderBy).ToList());
                sorted.StartIndex = res.StartIndex;
                sorted.TotalRecordCount = res.TotalRecordCount;
                return sorted;
            }
            return _lm.GetItemsResult(query);
        }

        public LibraryOptions GetLibraryOptions(BaseItem item)
        {
            return _lm.GetLibraryOptions(item);
        }

        public MusicGenre GetMusicGenre(string name)
        {
            return _lm.GetMusicGenre(name);
        }

        public Guid GetMusicGenreId(string name)
        {
            return _lm.GetMusicGenreId(name);
        }

        public QueryResult<(BaseItem Item, ItemCounts ItemCounts)> GetMusicGenres(InternalItemsQuery query)
        {
            return _lm.GetMusicGenres(query);
        }

        public UserView GetNamedView(User user, string name, Guid parentId, CollectionType? viewType, string sortName)
        {
            return _lm.GetNamedView(user, name, parentId, viewType, sortName);
        }

        public UserView GetNamedView(User user, string name, CollectionType? viewType, string sortName)
        {
            return _lm.GetNamedView(user, name, viewType, sortName);
        }

        public UserView GetNamedView(string name, CollectionType viewType, string sortName)
        {
            return _lm.GetNamedView(name, viewType, sortName);
        }

        public UserView GetNamedView(string name, Guid parentId, CollectionType? viewType, string sortName, string uniqueId)
        {
            return _lm.GetNamedView(name, parentId, viewType, sortName, uniqueId);
        }

        public Guid GetNewItemId(string key, Type type)
        {
            return _lm.GetNewItemId(key, type);
        }

        public BaseItem GetParentItem(Guid? parentId, Guid? userId)
        {
            return _lm.GetParentItem(parentId, userId);
        }

        public string GetPathAfterNetworkSubstitution(string path, BaseItem? ownerItem = null)
        {
            return _lm.GetPathAfterNetworkSubstitution(path, ownerItem);
        }

        public List<PersonInfo> GetPeople(BaseItem item)
        {
            return _lm.GetPeople(item);
        }

        public List<PersonInfo> GetPeople(InternalPeopleQuery query)
        {
            return _lm.GetPeople(query);
        }

        public List<Person> GetPeopleItems(InternalPeopleQuery query)
        {
            return _lm.GetPeopleItems(query);
        }

        public List<string> GetPeopleNames(InternalPeopleQuery query)
        {
            return _lm.GetPeopleNames(query);
        }

        public Person? GetPerson(string name)
        {
            return _lm.GetPerson(name);
        }

        public int? GetSeasonNumberFromPath(string path)
        {
            return _lm.GetSeasonNumberFromPath(path);
        }

        public UserView GetShadowView(BaseItem parent, CollectionType? viewType, string sortName)
        {
            return _lm.GetShadowView(parent, viewType, sortName);
        }

        public Studio GetStudio(string name)
        {
            return _lm.GetStudio(name);
        }

        public Guid GetStudioId(string name)
        {
            return _lm.GetStudioId(name);
        }

        public QueryResult<(BaseItem Item, ItemCounts ItemCounts)> GetStudios(InternalItemsQuery query)
        {
            return _lm.GetStudios(query);
        }

        public Folder GetUserRootFolder()
        {
            return _lm.GetUserRootFolder();
        }

        public List<VirtualFolderInfo> GetVirtualFolders()
        {
            return _lm.GetVirtualFolders();
        }

        public List<VirtualFolderInfo> GetVirtualFolders(bool includeRefreshState)
        {
            return _lm.GetVirtualFolders(includeRefreshState);
        }

        public Year GetYear(int value)
        {
            return _lm.GetYear(value);
        }

        public bool IgnoreFile(FileSystemMetadata file, BaseItem parent)
        {
            return _lm.IgnoreFile(file, parent);
        }

        public List<FileSystemMetadata> NormalizeRootPathList(IEnumerable<FileSystemMetadata> paths)
        {
            return _lm.NormalizeRootPathList(paths);
        }

        public ItemLookupInfo ParseName(string name)
        {
            return _lm.ParseName(name);
        }

        public QueryResult<BaseItem> QueryItems(InternalItemsQuery query)
        {
            return _lm.QueryItems(query);
        }

        public void QueueLibraryScan()
        {
            _lm.QueueLibraryScan();
        }

        public void RegisterItem(BaseItem item)
        {
            _lm.RegisterItem(item);
        }

        public void RemoveMediaPath(string virtualFolderName, string mediaPath)
        {
            _lm.RemoveMediaPath(virtualFolderName, mediaPath);
        }

        public Task RemoveVirtualFolder(string name, bool refreshLibrary)
        {
            return _lm.RemoveVirtualFolder(name, refreshLibrary);
        }

        public BaseItem? ResolvePath(FileSystemMetadata fileInfo, Folder? parent = null, IDirectoryService? directoryService = null)
        {
            return _lm.ResolvePath(fileInfo, parent, directoryService);
        }

        public IEnumerable<BaseItem> ResolvePaths(IEnumerable<FileSystemMetadata> files, IDirectoryService directoryService, Folder parent, LibraryOptions libraryOptions, CollectionType? collectionType = null)
        {
            return _lm.ResolvePaths(files, directoryService, parent, libraryOptions, collectionType);
        }

        public BaseItem RetrieveItem(Guid id)
        {
            return _lm.RetrieveItem(id);
        }

        public Task RunMetadataSavers(BaseItem item, ItemUpdateType updateReason)
        {
            return _lm.RunMetadataSavers(item, updateReason);
        }

        public IEnumerable<BaseItem> Sort(IEnumerable<BaseItem> items, User? user, IEnumerable<ItemSortBy> sortBy, SortOrder sortOrder)
        {
            return _lm.Sort(items, user, sortBy, sortOrder);
        }

        public IEnumerable<BaseItem> Sort(IEnumerable<BaseItem> items, User? user, IEnumerable<(ItemSortBy OrderBy, SortOrder SortOrder)> orderBy)
        {
            return _lm.Sort(items, user, orderBy);
        }

        public Task UpdateImagesAsync(BaseItem item, bool forceUpdate = false)
        {
            return _lm.UpdateImagesAsync(item, forceUpdate);
        }

        public Task UpdateItemAsync(BaseItem item, BaseItem parent, ItemUpdateType updateReason, CancellationToken cancellationToken)
        {
            return _lm.UpdateItemAsync(item, parent, updateReason, cancellationToken);
        }

        public Task UpdateItemsAsync(IReadOnlyList<BaseItem> items, BaseItem parent, ItemUpdateType updateReason, CancellationToken cancellationToken)
        {
            return _lm.UpdateItemsAsync(items, parent, updateReason, cancellationToken);
        }

        public void UpdateMediaPath(string virtualFolderName, MediaPathInfo mediaPath)
        {
            _lm.UpdateMediaPath(virtualFolderName, mediaPath);
        }

        public void UpdatePeople(BaseItem item, List<PersonInfo> people)
        {
            _lm.UpdatePeople(item, people);
        }

        public Task UpdatePeopleAsync(BaseItem item, List<PersonInfo> people, CancellationToken cancellationToken)
        {
            return _lm.UpdatePeopleAsync(item, people, cancellationToken);
        }

        public Task ValidateMediaLibrary(IProgress<double> progress, CancellationToken cancellationToken)
        {
            return _lm.ValidateMediaLibrary(progress, cancellationToken);
        }

        public Task ValidatePeopleAsync(IProgress<double> progress, CancellationToken cancellationToken)
        {
            return _lm.ValidatePeopleAsync(progress, cancellationToken);
        }

        public Task ValidateTopLibraryFolders(CancellationToken cancellationToken, bool removeRoot = false)
        {
            return _lm.ValidateTopLibraryFolders(cancellationToken, removeRoot);
        }
    }
}
