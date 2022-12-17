using System;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Space_Shooter
{
    class TripleLaser : Gun
    {
        private readonly Random rand = new();
        public override void Fire(Canvas canvas, Rectangle model, bool isEnemy = true)
        {
            if ((rand.Next(0, 100) >= 97) || !isEnemy)
            {
                for (int i = 1; i < 4; i++)
                {
                    Bullet newBullet = new((-10 * i + 20), (isEnemy) ? 12 : -12, 5, 5, isEnemy);
                    Canvas.SetLeft(newBullet.Model, Canvas.GetLeft(model) + model.Width / 2);
                    Canvas.SetTop(newBullet.Model, isEnemy ? (Canvas.GetTop(model) + newBullet.Model.Height) : (Canvas.GetTop(model) - newBullet.Model.Height));
                    canvas.Children.Add(newBullet.Model);
                    MainWindow.bullets.Add(newBullet);
                }
            }
        }
    }
}
