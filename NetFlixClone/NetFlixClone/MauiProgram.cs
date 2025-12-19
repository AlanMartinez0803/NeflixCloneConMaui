using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NetFlixClone.Services;
using NetFlixClone.ViewModels;
using NetFlixClone.Views;

namespace NetFlixClone
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Semibold.ttf", "PoppinsSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            
            builder.Services.AddHttpClient(tmdbService.TmdbClientHttps, httpClientexd => httpClientexd.BaseAddress = new Uri("https://api.themoviedb.org"));
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<tmdbService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<CategoriasViewModel>();
            builder.Services.AddSingleton<CategoriasPage>();
            builder.Services.AddTransientWithShellRoute<DetailsPage, DetailsViewModel>(nameof(DetailsPage));
            return builder.Build();
        }
    }
}
