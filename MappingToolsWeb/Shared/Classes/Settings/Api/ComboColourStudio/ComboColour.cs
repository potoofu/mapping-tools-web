using Mapping_Tools_Core.BeatmapHelper.ComboColours;
using Mapping_Tools_Core.BeatmapHelper.Enums;
using Mapping_Tools_Core.Tools.ComboColourStudio;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace MappingToolsWeb.Shared.Classes.Settings.Api {
    public class ComboColour : IComboColour {
        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        [JsonConstructor]
        public ComboColour(byte r, byte g, byte b) {
            R = r;
            G = g;
            B = b;
        }

        public ComboColour(string hex) {
            Color color = (Color)new ColorConverter().ConvertFromString(hex);
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public object Clone() {
            return new ComboColour(R, G, B);
        }

        public bool Equals(IComboColour other) {
            return R == other.R && G == other.G && B == other.B;
        }

        public override string ToString() {
            string hex = R.ToString("X2") + G.ToString("X2") + B.ToString("X2");
            return "#" + hex;
        }
    }
}