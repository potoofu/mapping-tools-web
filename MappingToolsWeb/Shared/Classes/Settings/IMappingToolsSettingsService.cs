using MappingToolsWeb.Shared.Classes.Settings.Api;
using System;
using System.Threading.Tasks;

namespace MappingToolsWeb.Shared.Classes.Settings {

    public interface IMappingToolsSettingsService {
        public MappingToolsSettings Settings { get; set; }

        public event EventHandler SettingsHaveChanged;

        Task LoadFromLocalStorageAsync();

        Task WriteToLocalStorageAsync();
    }
}