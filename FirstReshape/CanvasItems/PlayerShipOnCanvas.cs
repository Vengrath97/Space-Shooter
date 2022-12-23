using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace Space_Shooter
{
    class PlayerShipOnCanvas : ShipOnCanvas
    {
        public PlayerShipOnCanvas(Canvas canvas, string uri, int height, int width) : base(canvas, uri, height, width)
        {
            
        }

        public override void Move(DirectionDictionary.Direction xAxis = DirectionDictionary.Direction.Unknown, DirectionDictionary.Direction yAxis = DirectionDictionary.Direction.Unknown)
        {
            if (xAxis == DirectionDictionary.Direction.Unknown)
            {
                SpeedXOffset = 0;
            }
            else
            {
                SpeedXOffset = (xAxis == DirectionDictionary.Direction.Left) ? RepresentedShip.Speed * -1 : RepresentedShip.Speed;
            }


            if (yAxis == DirectionDictionary.Direction.Unknown)
            {
                SpeedYOffset = 0;
            }
            else
            {
                SpeedYOffset = (yAxis == DirectionDictionary.Direction.Up) ? RepresentedShip.Speed * -1 : RepresentedShip.Speed;
            }
            base.Move();
        }
    }
}
