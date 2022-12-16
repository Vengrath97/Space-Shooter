using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Space_Shooter
{
    class TripleLaser : Gun
    {
        public override void Fire(Canvas canvas, Rectangle model)
        {
            for (int i = 1; i < 4; i++)
            {
                Bullet newBullet = new((-10 * i + 20), -20, 5, 5);
                Canvas.SetLeft(newBullet.Model, Canvas.GetLeft(model) + model.Width / 2);
                Canvas.SetTop(newBullet.Model, Canvas.GetTop(model) - newBullet.Model.Height);
                canvas.Children.Add(newBullet.Model);
            }
        }
    }
}
