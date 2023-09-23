using Avalonia.Controls;
using DisplaySimulator.Displays;
using DisplaySimulator.ViewModels;
using Meadow.Foundation.Graphics;

namespace DisplaySimulator.Views
{
    public partial class MainWindow : Window
    {
        MicroGraphics graphics;

        PixelCanvas canvas;

        int scale = 1;

        ISimulatedDisplay simDisplay;

        public MainWindowViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();

            DisplayComboBox.SelectionChanged += DisplayComboBox_SelectionChanged;

            simDisplay = new SSd1306Simulated();

            IncreaseScaleButton.Click += IncreaseScaleButton_Click;
            DecreaseScaleButton.Click += DecreaseScaleButton_Click;
        }

        private void DisplayComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            //branch on the selected item
            switch (DisplayComboBox.SelectedIndex)
            {
                case 0:
                    simDisplay = new Ili9341Simulated();
                    break;
                case 1:
                    simDisplay = new SSd1306Simulated();
                    break;
                case 2:
                    simDisplay = new SSd1309Simulated();
                    break;
                default:
                    simDisplay = new St7789Simulated();
                    break;
            }

            ReloadCanvas();
        }

        private void DecreaseScaleButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (scale > 1) scale--;
            ReloadCanvas();
        }

        private void IncreaseScaleButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (scale < 4) scale++;
            ReloadCanvas();
        }

        void ReloadCanvas()
        {
            displayBorder.Child = null;

            canvas = new PixelCanvas(simDisplay.Width, simDisplay.Height, simDisplay.ColorMode)
            {
                Width = simDisplay.Width * scale,
                Height = simDisplay.Height * scale,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                EnabledColor = simDisplay.ForegroundColor,
                DisabledColor = simDisplay.BackgroundColor,
            };

            graphics = new MicroGraphics(canvas)
            {
                CurrentFont = new Font8x12(),
            };

            displayBorder.Child = canvas;
            displayBorder.Width = canvas.Width + 6;

            Draw();
        }

        void Draw()
        {
            graphics.Clear();

            graphics.DrawText(10, 10, "Hello MicroGraphics", Meadow.Foundation.Color.White, ScaleFactor.X1);

            graphics.DrawRectangle(10, 30, 30, 30, Meadow.Foundation.Color.White, false);

            graphics.DrawCircle(100, 100, 30, Meadow.Foundation.Color.Yellow, true);
            graphics.DrawCircle(110, 100, 30, Meadow.Foundation.Color.YellowGreen, true);
            graphics.DrawCircle(120, 100, 30, Meadow.Foundation.Color.GreenYellow, true);
            graphics.DrawCircle(130, 100, 30, Meadow.Foundation.Color.Green, true);

            graphics.Show();
        }

        public override void Show()
        {
            canvas?.InvalidateVisual();
            base.Show();
        }
    }
}