using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MappingToolsWeb.IndexedDB {

    public interface IIndexedDbCache<TKey, TValue> {
        bool IsInitialized { get; }

        event EventHandler CacheHasChanged;

        string StoreName { get; }

        Task InitializeAsync();

        bool TryGetValue(TKey key, out TValue value);

        Task AddAsync(TValue data);

        Task AddAsync(IEnumerable<TValue> data);

        Task RemoveAsync(TValue data);

        Task RemoveAsync(IEnumerable<TValue> data);

        Task ReplaceAsync(TValue oldData, TValue newData);

        Task ClearAsync();
    }

    public interface INestedIndexedDbCache<TKey, TValue, TData> :IIndexedDbCache<TKey, TData> {

        bool TryGetValue(TKey key, out TValue value);
    }
}