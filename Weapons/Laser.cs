using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Space_Shooter
{
    class Laser : Gun
    {
        public override void Fire(Canvas canvas, Rectangle model)
        {
                Bullet newBullet = new((0), -20);
                Canvas.SetLeft(newBullet.Model, Canvas.GetLeft(model) + model.Width / 2);
                Canvas.SetTop(newBullet.Model, Canvas.GetTop(model) - newBullet.Model.Height);
                canvas.Children.Add(newBullet.Model);
        }
    }
}
