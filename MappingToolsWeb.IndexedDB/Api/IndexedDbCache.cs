using MappingToolsWeb.IndexedDB.Records;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MappingToolsWeb.IndexedDB.Api {

    public class IndexedDbCache :IIndexedDbCache<ContentTag, IOrderedFileRecords, IFileRecord> {
        public bool IsInitialized { get; set; }
        public SortedDictionary<ContentTag, IOrderedFileRecords> Cache { get; set; } = new SortedDictionary<ContentTag, IOrderedFileRecords>();

        public event EventHandler CacheHasChanged;

        public void Initialize(IEnumerable<IFileRecord> records) {
            Add(records);

            IsInitialized = true;
        }

        public void Add(IFileRecord record) {
            if( Cache.ContainsKey(record.Tag) ) {
                Cache.GetValueOrDefault(record.Tag).Add(record);
            }
            else {
                var orderedRecords = new OrderedFileRecords();
                orderedRecords.Add(record);

                Cache.Add(record.Tag, orderedRecords);
            }

            InvokeChange();
        }

        public void Add(IEnumerable<IFileRecord> records) {
            foreach( var record in records ) {
                if( Cache.ContainsKey(record.Tag) ) {
                    Cache.GetValueOrDefault(record.Tag).Add(record, false);
                    continue;
                }

                var orderedRecords = new OrderedFileRecords();
                orderedRecords.Add(record, false);

                Cache.Add(record.Tag, orderedRecords);
            }

            foreach( var orderedFileRecords in Cache.Values ) {
                orderedFileRecords.Order();
            }

            InvokeChange();
        }

        public void Remove(IFileRecord record) {
            var records = Cache.GetValueOrDefault(record.Tag);

            if( records != null ) {
                records.Remove(record);

                if( records.OrderedRecords.Count == 0 ) {
                    Cache.Remove(record.Tag);
                }
            }

            InvokeChange();
        }

        public void Remove(IEnumerable<IFileRecord> records) {
            foreach( var record in records ) {
                if( Cache.ContainsKey(record.Tag) ) {
                    var orderedCacheRecords = Cache.GetValueOrDefault(record.Tag);
                    orderedCacheRecords.Remove(record, false);

                    if( orderedCacheRecords.OrderedRecords.Count == 0 ) {
                        Cache.Remove(record.Tag);
                    }
                }
            }

            foreach( var orderedFileRecords in Cache.Values ) {
                orderedFileRecords.Order();
            }

            InvokeChange();
        }

        private void InvokeChange() {
            CacheHasChanged?.Invoke(this, null);
        }
    }
}