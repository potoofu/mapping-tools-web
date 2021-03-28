using MappingToolsWeb.IndexedDB.Records;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MappingToolsWeb.IndexedDB {

    public interface IIndexedDbCache<TKey, TValue, TData> {
        bool IsInitialized { get; set; }

        event EventHandler CacheHasChanged;

        SortedDictionary<TKey, TValue> Cache { get; set; }

        void Initialize(IEnumerable<TData> records);

        void Add(TData data);

        void Add(IEnumerable<TData> data);

        void Remove(TData data);

        void Remove(IEnumerable<TData> data);

        void Replace(TData data);
    }
}