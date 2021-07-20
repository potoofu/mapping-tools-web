using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MappingToolsWeb.Shared.Classes.Settings.Api {

    public class MappingToolsSettingsService : IMappingToolsSettingsService {
        private Dictionary<string, object> _settings;

        private readonly ILocalStorageService _localStorage;

        public MappingToolsSettingsService(ILocalStorageService localStorage) {
            _localStorage = localStorage;
            _settings = new Dictionary<string, object>();
        }

        public async Task<T> GetSettingsAsync<T>(string key) {
            await LoadFromLocalStorageAsync<T>(key);

            return (T)_settings[key];
        }

        public async Task WriteToLocalStorageAsync<T>(string key) {
            if (!_settings.ContainsKey(key)) return;

            await _localStorage.SetItemAsync<T>(key, (T)_settings[key]);
        }

        public async Task LoadFromLocalStorageAsync<T>(string key) {
            if (_settings.ContainsKey(key)) return;

            _settings.Add(key, await _localStorage.GetItemAsync<T>(key) ?? Activator.CreateInstance<T>());
        }

        public T GetSettings<T>(string key) {
            throw new NotImplementedException();
        }

        public void WriteToLocalStorage<T>(string key) {
            throw new NotImplementedException();
        }

        public void LoadFromLocalStorage<T>(string key) {
            throw new NotImplementedException();
        }
    }
}