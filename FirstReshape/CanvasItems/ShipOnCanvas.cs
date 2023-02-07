using System.Windows.Controls;

namespace Space_Shooter
{
    class ShipOnCanvas : ObjectOnCanvas
    {
        public Ship RepresentedShip;

        public ShipOnCanvas(Canvas canvas, string uri, int height = 50, int width = 56) : base(canvas, uri, height, width)
        {
            RepresentedShip = new EnemyShip();
        }
        public override void Shoot()
        {
            foreach(Gun gun in RepresentedShip.Guns)
            {
                gun.Fire(Canvas.GetTop(CanvasItem), (Canvas.GetLeft(CanvasItem)+ Canvas.GetLeft(CanvasItem))/2);
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
