using System.Collections.Generic;

namespace MappingToolsWeb.IndexedDB.Api {

    public class ContentDefinition :IContentDefinition {
        public List<string> FileExtensions { get; set; }
        public List<string> MimeTypes { get; set; }
    }
}