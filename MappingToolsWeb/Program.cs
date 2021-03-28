using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
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
using System.Net.Http.Json;
using System.Collections.Generic;
using MappingToolsWeb.Classes.Models;

namespace MappingToolsWeb {

    public class Program {

        public static async Task Main(string[] args) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            await LoadServices(builder);

            var host = builder.Build();

            ConfigureServices(host);

            await host.RunAsync();
        }

        private static async Task LoadServices(WebAssemblyHostBuilder builder) {
            var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

            builder.Services.AddScoped(sp => httpClient);

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

            try {
                var changelog = await httpClient.GetFromJsonAsync<List<ChangelogModel>>("data/changelog.json");
                builder.Services.AddSingleton(changelog);

            }
            catch(Exception) {
                builder.Services.AddSingleton(new List<ChangelogModel>());
            }
        }

        private static void ConfigureServices(WebAssemblyHost host) {
        }
    }
}