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
        public static void DrawPlayer(Rectangle player)
        {
            ImageBrush playerImage = new() { ImageSource = new BitmapImage(new Uri(SpriteUri.Player, UriKind.Relative)) };
            player.Fill = playerImage;
        }

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