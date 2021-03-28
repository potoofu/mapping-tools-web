using System.Collections.Generic;
using System.Globalization;

namespace MappingToolsWeb.Localization {

    public interface ILocalizer {
        CultureInfo DefaultCulture { get; }

        List<CultureInfo> SupportedCultures { get; }
    }
}