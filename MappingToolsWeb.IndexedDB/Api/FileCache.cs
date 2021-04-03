using MappingToolsWeb.IndexedDB.Records;
using MappingToolsWeb.IndexedDB.Records.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Blazor.IndexedDB;

namespace MappingToolsWeb.IndexedDB.Api {

    public class FileCache :INestedIndexedDbCache<ContentTag, IOrderedRecords<IFileRecord>, IFileRecord> {
        public bool IsInitialized { get; private set; }

        public event EventHandler CacheHasChanged;

        private readonly IndexedDBManager _indexedDbMgr;

        public string StoreName => "files";

        private SortedDictionary<ContentTag, IOrderedRecords<IFileRecord>> _cache;

        public FileCache(IndexedDBManager indexedDbMgr) {
            _indexedDbMgr = indexedDbMgr;
        }

        public async Task InitializeAsync() {
            _cache = new SortedDictionary<ContentTag, IOrderedRecords<IFileRecord>>();

            var records = await _indexedDbMgr.GetRecords<FileRecord>(StoreName);

            foreach( var record in records ) {
                if( _cache.ContainsKey(record.Tag) ) {
                    _cache.GetValueOrDefault(record.Tag).Add(record, false);
                    continue;
                }

                var orderedRecords = new OrderedFileRecords();
                orderedRecords.Add(record, false);

                _cache.Add(record.Tag, orderedRecords);
            }

            foreach( var orderedFileRecords in _cache.Values ) {
                orderedFileRecords.Order();
            }

            IsInitialized = true;
        }

        public async Task AddAsync(IFileRecord record) {
            await AddRecord(record);

            _cache.Remove(record.Tag);

            var records = await _indexedDbMgr.GetRecords<FileRecord>(StoreName);

            var taggedRecords = records.Where(o => o.Tag == record.Tag);

            var orderedCache = new OrderedFileRecords();
            orderedCache.Add(taggedRecords);

            _cache.Add(record.Tag, orderedCache);

            InvokeChange();
        }

        public async Task AddAsync(IEnumerable<IFileRecord> records) {
            var contentToRefresh = new List<ContentTag>();

            foreach( var record in records ) {
                await AddRecord(record);

                if( contentToRefresh.Contains(record.Tag) ) {
                    continue;
                }

                contentToRefresh.Add(record.Tag);
            }

            var indexedRecords = await _indexedDbMgr.GetRecords<FileRecord>(StoreName);

            foreach( var content in contentToRefresh ) {
                var taggedRecords = indexedRecords.Where(o => o.Tag == content);

                var orderedCache = new OrderedFileRecords();
                orderedCache.Add(taggedRecords);

                _cache.Add(content, orderedCache);
            }

            InvokeChange();
        }

        public async Task RemoveAsync(IFileRecord record) {
            if( _cache.TryGetValue(record.Tag, out var cached) ) {
                cached.Remove(record);

                if( cached.OrderedRecords.Count == 0 ) {
                    _cache.Remove(record.Tag);
                }
            }

            await RemoveRecord(record);

            InvokeChange();
        }

        public async Task RemoveAsync(IEnumerable<IFileRecord> records) {
            foreach( var record in records ) {
                if( _cache.TryGetValue(record.Tag, out var cached) ) {
                    cached.Remove(record, false);

                    await RemoveRecord(record);

                    if( cached.OrderedRecords.Count == 0 ) {
                        _cache.Remove(record.Tag);
                    }
                }
            }

            foreach( var orderedFileRecords in _cache.Values ) {
                orderedFileRecords.Order();
            }

            InvokeChange();
        }

        public async Task ReplaceAsync(IFileRecord oldRecord, IFileRecord newRecord) {
            if( !_cache.ContainsKey(newRecord.Tag) ) {
                var orderedRecords = new OrderedFileRecords();
                _cache.Add(newRecord.Tag, orderedRecords);
            }

            if( oldRecord.Tag != newRecord.Tag ) {
                if( _cache.TryGetValue(oldRecord.Tag, out var cachedOld) ) {
                    cachedOld.Remove(oldRecord);
                }

                if( _cache.TryGetValue(newRecord.Tag, out var cachedNew) ) {
                    cachedNew.Add(newRecord);
                }
            }
            else {
                if( _cache.TryGetValue(newRecord.Tag, out var cached) ) {
                    cached.Remove(oldRecord, false);
                    cached.Add(newRecord);
                }
            }

            await UpdateRecord(newRecord);

            InvokeChange();
        }

        public IOrderedRecords<IFileRecord> Get(ContentTag key) {
            throw new NotImplementedException();
        }

        public async Task ClearAsync() {
            foreach(var cached in _cache.Values) {
                cached.Clear();
            }

            await _indexedDbMgr.ClearStore(StoreName);

            InvokeChange();
        }

        private async Task UpdateRecord(IFileRecord record) {
            await _indexedDbMgr.UpdateRecord(new StoreRecord<IFileRecord> {
                Storename = StoreName,
                Data = record
            });
        }

        private async Task AddRecord(IFileRecord record) {
            await _indexedDbMgr.AddRecord(new StoreRecord<IFileRecord> {
                Storename = StoreName,
                Data = record
            });
        }

        private async Task RemoveRecord(IFileRecord record) {
            await _indexedDbMgr.DeleteRecord(StoreName, record.Id);
        }

        private void InvokeChange() {
            CacheHasChanged?.Invoke(this, null);
        }

        bool INestedIndexedDbCache<ContentTag, IOrderedRecords<IFileRecord>, IFileRecord>.TryGetValue(ContentTag tag, out IOrderedRecords<IFileRecord> records) {
            return _cache.TryGetValue(tag, out records);
        }

        bool IIndexedDbCache<ContentTag, IFileRecord>.TryGetValue(ContentTag key, out IFileRecord record) {
            throw new NotImplementedException();
        }
    }
}