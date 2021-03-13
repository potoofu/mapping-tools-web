using System.Collections.Generic;
using System.IO;

namespace MappingToolsWeb.IndexedDB.Api {

    public class ContentTagManager :IContentTagManager {

        public Dictionary<ContentTag, IContentDefinition> TagDefinitions { get; set; } = new Dictionary<ContentTag, IContentDefinition> {
            {
                ContentTag.Audio,
                new ContentDefinition {
                    FileExtensions = new List<string> {".mp3", ".wav", ".ogg"},
                    MimeTypes = new List<string> {""}
                }
            },
            {
                ContentTag.Image,
                new ContentDefinition {
                    FileExtensions = new List<string> {".jpg", ".png"},
                    MimeTypes = new List<string> {""}
                }
            },
            {
                ContentTag.OsuBeatmap,
                new ContentDefinition {
                    FileExtensions = new List<string> {".osu"},
                    MimeTypes = new List<string> {""}
                }
            },
            {
                ContentTag.OsuStoryboard,
                new ContentDefinition {
                    FileExtensions = new List<string> {".osb"},
                    MimeTypes = new List<string> {""}
                }
            },
            {
                ContentTag.Video,
                new ContentDefinition {
                    FileExtensions = new List<string> {".mp4"},
                    MimeTypes = new List<string> {""}
                }
            }
        };

        public ContentTag GetContentTag(string fileName, string mimeType = "") {
            var extension = Path.GetExtension(fileName);

            if( string.IsNullOrWhiteSpace(extension) && string.IsNullOrWhiteSpace(mimeType) ) {
                return ContentTag.Undefined;
            }

            if( string.IsNullOrWhiteSpace(extension) && !string.IsNullOrWhiteSpace(mimeType) ) {
                foreach( var definition in TagDefinitions ) {
                    if( definition.Value.MimeTypes.Contains(mimeType) ) {
                        return definition.Key;
                    }
                }

                return ContentTag.Undefined;
            }

            foreach( var definition in TagDefinitions ) {
                if( definition.Value.FileExtensions.Contains(extension) ) {
                    return definition.Key;
                }
            }

            return ContentTag.Undefined;
        }
    }
}