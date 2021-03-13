using System.Collections.Generic;

namespace MappingToolsWeb.IndexedDB {

    public enum ContentTag {
        Audio,
        Image,
        OsuBeatmap,
        OsuStoryboard,
        Undefined,
        Video
    }

    public interface IContentTagManager {
        Dictionary<ContentTag, IContentDefinition> TagDefinitions { get; set; }

        ContentTag GetContentTag(string fileName, string mimeType = "");
    }
}