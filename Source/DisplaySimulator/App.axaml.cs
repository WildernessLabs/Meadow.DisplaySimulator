using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DisplaySimulator.ViewModels;
using DisplaySimulator.Views;

namespace DisplaySimulator
{
    public partial class App : Application
    {
        public App()
        {
            RegisterServices();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                    WindowState = Avalonia.Controls.WindowState.Normal,
                    Width = 1280,
                    Height = 720,
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}