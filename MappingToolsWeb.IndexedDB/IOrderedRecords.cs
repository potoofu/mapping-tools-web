using System.Collections.Generic;

namespace MappingToolsWeb.IndexedDB {

    public interface IOrderedRecords<T> {
        List<T> OrderedRecords { get; }

        bool IsAscending { get; set; }

        void Add(T record, bool order = true);

        void Add(IEnumerable<T> records, bool order = true);

        void Remove(T record, bool order = true);

        void Remove(IEnumerable<T> records, bool order = true);

        void Replace(T record, bool order = true);

        void Clear();

        void Order();
    }
}