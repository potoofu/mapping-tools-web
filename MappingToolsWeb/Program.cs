using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Net.Http;
using System.Threading.Tasks;
using TG.Blazor.IndexedDB;
using MappingToolsWeb.IndexedDB;
using MappingToolsWeb.IndexedDB.Api;
using MappingToolsWeb.IndexedDB.Records;
using MappingToolsWeb.Localization;
using MappingToolsWeb.Localization.Api;
using MudBlazor.Services;
using Blazored.LocalStorage;
using AKSoftware.Localization.MultiLanguages;
using System.Linq;

namespace MappingToolsWeb {

    public class Program {

        public static async Task Main(string[] args) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            LoadServices(builder);

            var host = builder.Build();

            ConfigureServices(host);

            await host.RunAsync();
        }

        private static void LoadServices(WebAssemblyHostBuilder builder) {
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddIndexedDB(store => {
                store.DbName = "MappingToolsWebDb";
                store.Version = 1;

                store.Stores.Add(new StoreSchema {
                    Name = "files",
                    PrimaryKey = new IndexSpec {
                        Name = "id",
                        KeyPath = "id",
                        Auto = true
                    }
                });
            });

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddSingleton<IContentTagManager>(sp => new ContentTagManager());

            builder.Services.AddSingleton<IIndexedDbCache<ContentTag, IOrderedFileRecords, IFileRecord>>(sp => new IndexedDbCache());

            builder.Services.AddMudServices();

            var localizationAssembly = AppDomain.CurrentDomain.GetAssemblies().Single(x => x.FullName.StartsWith("MappingToolsWeb.Localization"));

            builder.Services.AddLanguageContainer(localizationAssembly, new Localizer().DefaultCulture);

            builder.Services.AddSingleton<ILocalizer>(new Localizer());
        }

        private static void ConfigureServices(WebAssemblyHost host) {
        }
    }
}