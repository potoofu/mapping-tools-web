using System.Collections.Generic;

namespace MappingToolsWeb.IndexedDB {

    public interface IContentDefinition {
        List<string> FileExtensions { get; set; }
        List<string> MimeTypes { get; set; }
    }
}