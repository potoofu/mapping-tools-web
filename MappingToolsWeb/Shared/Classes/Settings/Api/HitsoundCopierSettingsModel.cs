using Mapping_Tools_Core.BeatmapHelper.Enums;
using System.Collections.Generic;

namespace MappingToolsWeb.Shared.Classes.Settings.Api {
    public class HitsoundCopierSettingsModel {
        public CopyingMode CopyingModeSetting { get; set; }
        public double TemporalLeniency { get; set; }
        public bool CopyHitsounds { get; set; }
        public bool CopySliderbodyHitsounds { get; set; }

        public bool CopySampleSets { get; set; }
        public bool CopyVolumes { get; set; }
        public bool AlwaysPreserve5Volume { get; set; }
        public bool CopyStoryboardedSamples { get; set; }
        public bool IgnoreHitsoundSatisfiedSamples { get; set; }
        public bool CopyToSliderTicks { get; set; }
        public bool CopyToSliderSlides { get; set; }
        public bool MuteSliderends { get; set; }

        // Sliderend muting settings
        public HashSet<BeatDivisor> AllBeatDivisors { get; set; } = new HashSet<BeatDivisor>();
        public HashSet<BeatDivisor> MutedBeatDivisors { get; set; } = new HashSet<BeatDivisor>();
        public double MinimumDuration { get; set; }
        public int MutedSampleIndex { get; set; }
        public SampleSet MutedSampleSet { get; set; }

        public enum CopyingMode {
            OverwriteEverything,
            OverwriteOnlyDefined
        }
    }
}