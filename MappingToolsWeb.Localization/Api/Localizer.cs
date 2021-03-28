using System.Collections.Generic;
using System.Globalization;

namespace MappingToolsWeb.Localization.Api {

    public class Localizer :ILocalizer {
        public CultureInfo DefaultCulture { get; } = CultureInfo.CreateSpecificCulture("en-US");

        public List<CultureInfo> SupportedCultures { get; } = new List<CultureInfo> {
            CultureInfo.CreateSpecificCulture("en-US"),
            CultureInfo.CreateSpecificCulture("de-DE")
        };
    }
}