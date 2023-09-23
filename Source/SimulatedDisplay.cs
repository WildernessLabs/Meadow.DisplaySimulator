using Meadow.Foundation;
using Meadow.Foundation.Graphics;

namespace DisplaySimulator
{
    internal struct SimulatedDisplay
    {
        public ColorMode ColorMode { get; set; }

        public Color ForegroundColor { get; set; }
        public Color BackgroundColor { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}