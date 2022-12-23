using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Space_Shooter
{
    class BulletOnCanvas : ObjectOnCanvas
    {
        public Bullet RepresentedBullet;

        public BulletOnCanvas(Bullet bullet, Canvas canvas, string uri, int height = 20, int width = 20) : base(canvas, uri, height, width)
        {
            RepresentedBullet = bullet;
        }
        public override void Draw()
        {
            if (!isDrawn)
            {
                SpawnWidth = rand.Next(0, (int)(GlobalVariables.WindowWidth - Width));
                Canvas.SetTop(CanvasItem, 0 - Height);
                Canvas.SetLeft(CanvasItem, SpawnWidth);
                Canvas.Children.Add(CanvasItem);
                isDrawn = true;
            }
        }
        public override ShipOnCanvas CheckForCollision()
        {
            foreach (ShipOnCanvas enemyShip in ObjectOnCanvas.CanvasItems)
            {
                if (!(enemyShip.RepresentedShip is PlayerShip))
                {
                    Rect enemyHitbox = new(Canvas.GetLeft(enemyShip.CanvasItem), Canvas.GetTop(enemyShip.CanvasItem), enemyShip.CanvasItem.Width, enemyShip.CanvasItem.Height);
                    if (Hitbox.IntersectsWith(enemyShip.Hitbox))
                    {
                        return enemyShip;
                    }
                }
            }
            return null;
        }
    }
}