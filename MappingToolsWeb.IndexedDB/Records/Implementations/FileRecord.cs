using System;

namespace MappingToolsWeb.IndexedDB.Records.Implementations {

    public class FileRecord :IFileRecord {
        public long? Id { get; set; }
        public string Name { get; set; }
        public ContentTag Tag { get; set; }
        public byte[] Data { get; set; }
        public DateTime LastModified { get; set; }
    }
}