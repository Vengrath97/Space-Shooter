using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    abstract class Ship
    {
        public int Hull;
        public int Shields;
        public double Speed;
        public List<Gun> Guns = new();

        public Ship(int hull = 1, int shields = 0, double speed = 10)
        {
            Hull = hull;
            Shields = shields;
            Speed = speed;
        }
        
        public void TakeDamage(int damage)
        {
            if (Shields>0)
            {
                Shields -= damage;
            }
            else
            {
                Hull -= damage;
            }
        }
        public void AddGun(Gun gun)
        {
            Guns.Add(gun);
        }
        public bool IsDead()
        {
            return Hull < 1;
        }
    }
}
