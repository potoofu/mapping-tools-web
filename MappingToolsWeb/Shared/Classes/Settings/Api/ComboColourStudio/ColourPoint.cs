using Mapping_Tools_Core.BeatmapHelper.ComboColours;
using Mapping_Tools_Core.BeatmapHelper.Enums;
using Mapping_Tools_Core.Tools.ComboColourStudio;
using System.Collections.Generic;

namespace MappingToolsWeb.Shared.Classes.Settings.Api {
    public class ColourPoint : IColourPoint {
        public double Time { get; set; }

        public ColourPointMode Mode { get; set; }

        IReadOnlyList<int> IColourPoint.ColourSequence => ColourSequence;

        public List<int> ColourSequence { get; set; }

        public ColourPoint() {
            ColourSequence = new List<int>();
            Mode = ColourPointMode.Normal;
        }
    }
}