﻿using System;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Space_Shooter
{
    class Laser : Gun
    {
        private int fireRate = 20;
        private int heat = 0;
        public override void Fire(Canvas canvas, Rectangle model, bool isEnemy = true)
        {
            if(heat== fireRate || !isEnemy)
            {
                int xSpeed = 0;
                int ySpeed = -25;
                if (isEnemy) ySpeed = ySpeed * -1;
                CreateBullet(canvas, model, xSpeed, ySpeed);
                heat = 0;
            }
            heat += 1;

        }

    }
}