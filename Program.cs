using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using Microsoft.JSInterop;
using System.Globalization;
using BSTPROJECT.Shared;

namespace BSTPROJECT
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Add your license key here");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
			builder.Services.AddSyncfusionBlazor();	
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            #region Localization
        // Register the Syncfusion locale service to customize the  SyncfusionBlazor component locale culture
        builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));

        // Set the default culture of the application
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

        // Get the modified culture from culture switcher
        var host = builder.Build();
        var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
        var result = await jsInterop.InvokeAsync<string>("cultureInfo.get");
        if (result != null)
        {
            // Set the culture from culture switcher
            var culture = new CultureInfo(result);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
        #endregion

            await builder.Build().RunAsync();
        }
    }
}
