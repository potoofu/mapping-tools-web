using System.Threading.Tasks;

namespace MappingToolsWeb.Shared.Classes.Settings {

    public interface IMappingToolsSettingsService {
        Task<T> GetSettingsAsync<T>(string key);

        Task WriteToLocalStorageAsync<T>(string key);

        Task LoadFromLocalStorageAsync<T>(string key);

        T GetSettings<T>(string key);

        void WriteToLocalStorage<T>(string key);

        void LoadFromLocalStorage<T>(string key);
    }
}