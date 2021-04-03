using System;

namespace MappingToolsWeb.IndexedDB.Records {

    public interface IBackupRecord {
        long? Id { get; set; }
        IFileRecord File { get; set; }
        string ToolName { get; set; }
        DateTime BackedUpOn { get; set; }
    }
}