using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace MappingToolsWeb.Shared.Classes.Settings.Api {

    public class MappingToolsSettingsService :IMappingToolsSettingsService {
        private const string LOCALSTORAGE_KEY = "settings";

        public MappingToolsSettings Settings { get; set; }

        public event EventHandler SettingsHaveChanged;

        private readonly ILocalStorageService _localStorage;

        public MappingToolsSettingsService(ILocalStorageService localStorage) {
            _localStorage = localStorage;
        }

        public async Task LoadFromLocalStorageAsync() {
            Settings = await _localStorage.GetItemAsync<MappingToolsSettings>(LOCALSTORAGE_KEY) ?? new MappingToolsSettings();
        }

        public async Task WriteToLocalStorageAsync() {
            await _localStorage.SetItemAsync(LOCALSTORAGE_KEY, Settings);

            InvokeChange();
        }

        private void InvokeChange() {
            SettingsHaveChanged?.Invoke(this, null);
        }
    }
}