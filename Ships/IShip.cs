using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Space_Shooter
{
    interface IShip
    {
        void Shoot(Canvas canvas);
        void Move(DirectionDictionary.Direction direction);
    }
}
