using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Space_Shooter
{
    class BasicLaser : Gun
    {
        private readonly Canvas canvas;
        public BasicLaser(Canvas canvas)
        {
            this.canvas = canvas;
        }
        public override void Fire(double height, double width)
        {
            BulletOnCanvas y = new(new(), canvas);
            y.Draw(height, width);
        }

    }
}
