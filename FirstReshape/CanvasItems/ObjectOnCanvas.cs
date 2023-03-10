using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Space_Shooter.Storage;

namespace Space_Shooter
{
    abstract class ObjectOnCanvas
    {
        public Rectangle CanvasItem;
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

        public Rect GetHitbox()
        {
            return new Rect(Canvas.GetLeft(CanvasItem), Canvas.GetTop(CanvasItem), CanvasItem.Width, CanvasItem.Height);
        }
        public ObjectOnCanvas(Canvas canvas, string uri = "", int height = 50, int width = 56, double speedXoffset = 0, double speedYoffset = -10)
        {
            Canvas = canvas;
            Height = height;
            Width = width;
            SpeedXOffset = speedXoffset;
            SpeedYOffset = speedYoffset;
            ImageBrush enemySprite = new();
            GraphicUri = uri;
            if (uri != "")
            {
                enemySprite.ImageSource = new BitmapImage(new Uri(GraphicUri, UriKind.Relative));
            }
            else
            {
                enemySprite.ImageSource = new BitmapImage(new Uri(SpriteUri.Player, UriKind.Relative));
            }

            CanvasItem = new()
            {
                Height = Height,
                Width = Width,
                Fill = enemySprite
            };
            CanvasItems.Add(this);

        }

        public virtual void Draw()
        {
            if(!isDrawn)
            {
                SpawnWidth = rand.Next(0, (int)(GlobalVariables.WindowWidth - Width));
                Canvas.SetTop(CanvasItem, 1);
                Canvas.SetLeft(CanvasItem, SpawnWidth);
                Canvas.Children.Add(CanvasItem);
                isDrawn = true;
            }
        }
        public virtual void Draw(double height, double width)
        {

        }
        public virtual void Move()
        {
            double newWidth = Canvas.GetLeft(CanvasItem) + SpeedXOffset;
            if (newWidth >= 0 && newWidth<=GlobalVariables.WindowWidth - Width)
                Canvas.SetLeft(CanvasItem, newWidth);

            double newHeight = Canvas.GetTop(CanvasItem) + SpeedYOffset;
            if ( newHeight >= 0 && newHeight <= GlobalVariables.WindowHeight)
                Canvas.SetTop(CanvasItem, newHeight);
        }
        public virtual void Move(DirectionDictionary.Direction xAxis, DirectionDictionary.Direction yAxis)
        {

        }
        public void Dispose()
        {
            Canvas.Children.Remove(CanvasItem);
            CanvasItems.Remove(this);
        }
        public virtual void OnCollision(int damage)
        {
            
        }

        public virtual CollisionData CheckForCollision()
        {
            return null;
        }
        public virtual void Shoot()
        {
        }
    }
}
