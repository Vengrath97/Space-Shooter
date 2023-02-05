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
            BulletOnCanvas bullet = new(new(), canvas);
            bullet.Draw(height, width);
        }

    }
}
