
namespace MappingToolsWeb.Shared.Classes.Settings.Api {
    public class BeatDivisor {
        public string DisplayName { get; set; }
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public override string ToString() {
            return DisplayName;
        }

        public static BeatDivisor[] AllDivisors = {
            new BeatDivisor {
                DisplayName = "1/1",
                Numerator = 1,
                Denominator = 1
            },
            new BeatDivisor {
                DisplayName = "1/2",
                Numerator = 1,
                Denominator = 2
            },
            new BeatDivisor {
                DisplayName = "1/3",
                Numerator = 1,
                Denominator = 3
            },
            new BeatDivisor {
                DisplayName = "1/4",
                Numerator = 1,
                Denominator = 4
            },
            new BeatDivisor {
                DisplayName = "1/6",
                Numerator = 1,
                Denominator = 6
            },
            new BeatDivisor {
                DisplayName = "1/8",
                Numerator = 1,
                Denominator = 8
            },
            new BeatDivisor {
                DisplayName = "1/12",
                Numerator = 1,
                Denominator = 12
            },
            new BeatDivisor {
                DisplayName = "1/16",
                Numerator = 1,
                Denominator = 16
            },
            new BeatDivisor {
                DisplayName = "1/5",
                Numerator = 1,
                Denominator = 5
            },
            new BeatDivisor {
                DisplayName = "1/7",
                Numerator = 1,
                Denominator = 7
            },
            new BeatDivisor {
                DisplayName = "1/9",
                Numerator = 1,
                Denominator = 9
            },
            new BeatDivisor {
                DisplayName = "1/11",
                Numerator = 1,
                Denominator = 11
            },
            new BeatDivisor {
                DisplayName = "1/13",
                Numerator = 1,
                Denominator = 13
            }
        };
    }
}