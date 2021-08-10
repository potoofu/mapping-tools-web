using Mapping_Tools_Core.BeatmapHelper.ComboColours;
using Mapping_Tools_Core.BeatmapHelper.Enums;
using Mapping_Tools_Core.Tools.ComboColourStudio;
using System.Collections.Generic;

namespace MappingToolsWeb.Shared.Classes.Settings.Api {
    public class ComboColourStudioSettingsModel {
        public ComboColourProject Project { get; set; }

        public ComboColourStudioSettingsModel() {
            Project = new ComboColourProject();
            Project.ComboColours.Add(new ComboColour(255, 0, 0));
            Project.ComboColours.Add(new ComboColour(0, 255, 0));
            Project.ComboColours.Add(new ComboColour(0, 0, 255));
            Project.ColourPoints.Add(new ColourPoint { Time = 10, Mode = ColourPointMode.Normal, ColourSequence = { 1, 2, 3 } });
        }
    }
}