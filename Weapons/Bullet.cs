using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Space_Shooter
{
    public class Bullet
    {
        public Rectangle Model;
        private readonly int SpeedXOffset;
        private readonly int SpeedYOffset;
        public bool IsEnemy;
        public Bullet(int speedXOffset, int speedYOffset, double bulletHeight = 20, double bulletWidth = 5, bool isEnemy=true)
        {
            this.Model = new()
            {
                Tag = "bullet",
                Height = bulletHeight,
                Width = bulletWidth,
                Fill = Brushes.White,
                Stroke = Brushes.Red
            };
            SpeedXOffset = speedXOffset;
            SpeedYOffset = speedYOffset;
            IsEnemy = isEnemy;
            MainWindow.bullets.Add(this);
        }
        public void Move()
        {
            Canvas.SetLeft(Model, Canvas.GetLeft(Model) + (int)this.SpeedXOffset);
            Canvas.SetTop(Model, Canvas.GetTop(Model) + (int)this.SpeedYOffset);
        }
    }
}
