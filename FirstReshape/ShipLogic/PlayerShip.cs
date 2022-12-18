using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    class PlayerShip : Ship
    {
        public List<PowerUpOnCanvas> PowerUps = new();

        public void GivePowerUp(PowerUpOnCanvas powerUp)
        {
            PowerUps.Add(powerUp);
        }

    }
}
