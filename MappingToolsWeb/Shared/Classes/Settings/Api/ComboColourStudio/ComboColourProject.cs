using Mapping_Tools_Core.BeatmapHelper.ComboColours;
using Mapping_Tools_Core.BeatmapHelper.Enums;
using Mapping_Tools_Core.Tools.ComboColourStudio;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MappingToolsWeb.Shared.Classes.Settings.Api {
    public class ComboColourProject : IComboColourProject {

        public List<ComboColour> ComboColours { get; set; }

        [JsonIgnore]
        IReadOnlyList<IComboColour> IHasComboColours.ComboColours => ComboColours;

        public List<ColourPoint> ColourPoints { get; set; }

        [JsonIgnore]
        IReadOnlyList<IColourPoint> IHasColourPoints.ColourPoints => ColourPoints;

        public int MaxBurstLength { get; set; }

        public ComboColourProject() {
            ComboColours = new List<ComboColour>();
            ColourPoints = new List<ColourPoint>();
            MaxBurstLength = 1;
        }
    }
}