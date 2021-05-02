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
using MappingToolsWeb.Shared.Classes.Models;
using System.Threading;
using MappingToolsWeb.Shared.Classes.Settings;
using MappingToolsWeb.Shared.Classes.Settings.Api;
using BlazorDownloadFile;
using MudBlazor;

namespace MappingToolsWeb {

    public class Program {

        public static async Task Main(string[] args) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            await LoadServices(builder);

            var host = builder.Build();

            await ConfigureServices(host);

            await host.RunAsync();
        }

        private static async Task LoadServices(WebAssemblyHostBuilder builder) {
            var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            var localizationAssembly = AppDomain.CurrentDomain.GetAssemblies().Single(x => x.FullName.StartsWith("MappingToolsWeb.Localization"));
            var localizer = new Localizer();

            builder.Services.AddScoped(sp => httpClient);

            builder.Services.AddIndexedDB(store => {
                store.DbName = "MappingToolsWebDb";
                store.Version = 1;

                store.Stores.Add(new StoreSchema {
                    Name = "backups",
                    PrimaryKey = new IndexSpec {
                        Name = "id",
                        KeyPath = "id",
                        Auto = true
                    }
                });

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

            builder.Services.AddScoped<IMappingToolsSettingsService, MappingToolsSettingsService>();

            builder.Services.AddScoped<IContentTagManager, ContentTagManager>();

            builder.Services.AddScoped<INestedIndexedDbCache<ContentTag, IOrderedRecords<IFileRecord>, IFileRecord>, FileCache>();

            builder.Services.AddLanguageContainer(localizationAssembly, localizer.DefaultCulture);

            builder.Services.AddScoped<ILocalizer, Localizer>();

            builder.Services.AddBlazorDownloadFile();

            Thread.CurrentThread.CurrentCulture = localizer.DefaultCulture;
            Thread.CurrentThread.CurrentUICulture = localizer.DefaultCulture;

            try {
                var changelog = await httpClient.GetFromJsonAsync<List<ChangelogModel>>("https://api.github.com/repos/misakura-rin/mapping-tools-web/releases");
                builder.Services.AddSingleton(changelog);
            }
            catch( Exception ) {
                builder.Services.AddScoped<List<ChangelogModel>>();
            }

            builder.Services.AddMudServices(config => {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
            });
        }

        private static async Task ConfigureServices(WebAssemblyHost host) {
        }
    }
}