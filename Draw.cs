using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Space_Shooter
{
    static class Draw
    {
        public static void DrawBackground(Canvas canvas)
        {
            ImageBrush bg = new();

            bg.ImageSource = new BitmapImage(new Uri(SpriteUri.Background, UriKind.Relative));
            bg.TileMode = TileMode.Tile;
            bg.Viewport = new Rect(0, 0, 0.15, 0.15);
            bg.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            canvas.Background = bg;
        }
    }
}