using System.Collections.Generic;
using static MappingToolsWeb.Pages.Tools.MapCleaner;

namespace MappingToolsWeb.Shared.Classes.Settings.Api {

    public class MapCleanerSettingsModel {
        public bool ResnapObjects { get; set; }
        public bool ResnapBookmarks { get; set; }
        public bool RemoveMuting { get; set; }
        public bool RemoveUnclickables { get; set; }

        public bool UsesVolumeChangesInSliders { get; set; }
        public bool UsesSampleSetChanges { get; set; }
        public bool UsesVolumeChangesInSpinners { get; set; }

        public HashSet<Signature> SelectedSignatures { get; set; } = new HashSet<Signature>();
    }
}