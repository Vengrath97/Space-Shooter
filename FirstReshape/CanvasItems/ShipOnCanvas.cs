using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public virtual void Shoot()
        {
            foreach(Gun gun in RepresentedShip.Guns)
            {
                gun.Fire();
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
