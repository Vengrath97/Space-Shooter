using System.Windows;
using System.Windows.Controls;
using Space_Shooter.Storage;

namespace Space_Shooter
{
    class BulletOnCanvas : ObjectOnCanvas
    {
        public Bullet RepresentedBullet;

        public BulletOnCanvas(Bullet bullet, Canvas canvas, string uri = "", int height = 20, int width = 20) : base(canvas, uri, height, width)
        {
            RepresentedBullet = bullet;
        }
        public override void Draw(double height, double width)
        {
            if (!isDrawn)
            {
                Canvas.SetTop(CanvasItem, height);
                Canvas.SetLeft(CanvasItem, width);
                Canvas.Children.Add(CanvasItem);
                isDrawn = true;
            }
        }
        public override CollisionData CheckForCollision()
        {
            foreach (ObjectOnCanvas objectOnCanvas in CanvasItems)
            {
                if (objectOnCanvas is ShipOnCanvas && objectOnCanvas is not PlayerShipOnCanvas)
                {
                    //nie mam pojęcia, dlaczego to nie działa.
                    //
                    Rect firstObjectHitbox = GetGitbox();
                    Rect enemyShipHitbox = objectOnCanvas.GetGitbox();
                    if (firstObjectHitbox.IntersectsWith(enemyShipHitbox))
                    {
                        return new CollisionData(this, objectOnCanvas);
                    }
                }
            }
            return null;
        }
    }
}