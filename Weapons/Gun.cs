using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Space_Shooter
{
    public class Gun
    {
        public virtual void Fire(Canvas canvas, Rectangle model, bool isEnemy=true)
        {

        }
        internal Bullet CreateBullet(Canvas canvas, Rectangle model, int speedXOffset, int speedYOffset, bool isEnemy = true)
        {
            Bullet newBullet = new(speedXOffset, speedYOffset, 20, 5, isEnemy);
            Canvas.SetLeft(newBullet.Model, Canvas.GetLeft(model) + model.Width / 2);
            Canvas.SetTop(newBullet.Model, (Canvas.GetTop(model) + newBullet.Model.Height));
            canvas.Children.Add(newBullet.Model);
            MainWindow.bullets.Add(newBullet);
            return newBullet;
        }
    }
}
