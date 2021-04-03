using System;

namespace MappingToolsWeb.IndexedDB.Records {

    public interface IFileRecord {
        long? Id { get; set; }
        string Name { get; set; }
        ContentTag Tag { get; set; }
        byte[] Data { get; set; }
        DateTime LastModified { get; set; }
    }
}