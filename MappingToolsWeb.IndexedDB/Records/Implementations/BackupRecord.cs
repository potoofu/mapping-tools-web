using System;

namespace MappingToolsWeb.IndexedDB.Records.Implementations {

    public class BackupRecord :IBackupRecord {
        public long? Id { get; set; }

        public FileRecord File { get; set; }
        public string ToolName { get; set; }

        public DateTime BackedUpOn { get; set; }
    }
}