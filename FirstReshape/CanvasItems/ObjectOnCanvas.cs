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
    abstract class ObjectOnCanvas
    {
        public Rectangle CanvasItem;
        public Rect Hitbox;
        public string GraphicUri;
        public double SpeedXOffset;
        public double SpeedYOffset;
        internal int Height;
        internal int Width;
        public int SpawnWidth;
        internal bool isDrawn;

        internal readonly Canvas Canvas;
        internal static Random rand = new();
        public static List<ObjectOnCanvas> CanvasItems = new();

        public ObjectOnCanvas(Canvas canvas, string uri, int height = 50, int width = 56, double speedXoffset = 0, double speedYoffset = -10)
        {
            Canvas = canvas;
            Height = height;
            Width = width;
            SpeedXOffset = speedXoffset;
            SpeedYOffset = speedYoffset;
            ImageBrush enemySprite = new();
            GraphicUri = uri;
            enemySprite.ImageSource = new BitmapImage(new Uri(GraphicUri, UriKind.Relative));

            CanvasItem = new()
            {
                Height = Height,
                Width = Width,
                Fill = enemySprite
            };
            Hitbox = new(Canvas.GetLeft(CanvasItem), Canvas.GetTop(CanvasItem), CanvasItem.Width, CanvasItem.Height);
            CanvasItems.Add(this);

        }

        public virtual void Draw()
        {
            if(!isDrawn)
            {
                SpawnWidth = rand.Next(0, (int)(GlobalVariables.WindowWidth - Width));
                Canvas.SetTop(CanvasItem, 0 - Height);
                //Canvas.SetTop(CanvasItem, 100); //debug value
                Canvas.SetLeft(CanvasItem, SpawnWidth);
                Canvas.Children.Add(CanvasItem);
                isDrawn = true;
            }
        }
        public virtual void Move()
        {
            Canvas.SetLeft(CanvasItem, Canvas.GetLeft(CanvasItem) + SpeedXOffset);
            Canvas.SetTop(CanvasItem, Canvas.GetTop(CanvasItem) + SpeedYOffset);
        }
        public virtual void Move(DirectionDictionary.Direction xAxis, DirectionDictionary.Direction yAxis)
        {

        }
        public void Dispose()
        {
            Canvas.Children.Remove(CanvasItem);
            CanvasItems.Remove(this);
        }
        public virtual void OnCollision()
        {
            
        }

        public virtual ShipOnCanvas CheckForCollision()
        {
            return null;
        }
    }
}
