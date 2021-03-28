using MappingToolsWeb.IndexedDB.Records;
using System.Collections.Generic;

namespace MappingToolsWeb.IndexedDB {

    public interface IOrderedFileRecords {
        List<IFileRecord> OrderedRecords { get; set; }
        bool IsAscending { get; set; }

        void Add(IFileRecord record, bool order = true);

        void Add(IEnumerable<IFileRecord> records, bool order = true);

        void Remove(IFileRecord record, bool order = true);

        void Remove(IEnumerable<IFileRecord> records, bool order = true);

        void Replace(IFileRecord record, bool order = true);

        void Order();
    }
}