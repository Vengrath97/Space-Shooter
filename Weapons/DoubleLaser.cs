using System;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Space_Shooter
{
    class DoubleLaser : Gun
    {
        private int fireRate = 20;
        private int heat = 0;
        public override void Fire(Canvas canvas, Rectangle model, bool isEnemy = true)
        {
            if (heat == fireRate || !isEnemy)
            {
                for (int i = 0; i < 2; i++)
                {
                    int xSpeed = 10 + i * -20;
                    int ySpeed = -20;
                    if (isEnemy) ySpeed = ySpeed * -1;
                    CreateBullet(canvas, model, xSpeed, ySpeed);

                }
                heat = 0;
            }
            heat += 1;
        }

    }
}
