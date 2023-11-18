namespace Calculator;

using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        
        builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Logging.AddDebug();

        //builder.Services.AddSingleton<EquationViewModel>();

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "equation.db3");
        builder.Services.AddSingleton<EquationViewModel>(s => ActivatorUtilities.CreateInstance<EquationViewModel>(s, dbPath));

        return builder.Build();
	}
}
