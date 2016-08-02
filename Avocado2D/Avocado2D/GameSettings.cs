using Avocado2D.Graphics.Viewports;
using Microsoft.Xna.Framework;

namespace Avocado2D
{
    public class GameSettings
    {
        public Point VirtualResolution { get; set; }

        public Point WindowResolution { get; set; }

        public ViewportType ViewportType { get; set; }

        public GameSettings()
        {
            VirtualResolution = new Point(1280, 720);
            WindowResolution = new Point(1280, 720);
            ViewportType = ViewportType.Scaling;
        }
    }
}