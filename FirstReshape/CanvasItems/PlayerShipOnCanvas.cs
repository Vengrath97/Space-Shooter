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
            SpeedXOffset = GlobalVariables.PlayerShipSpeed;
            RepresentedShip.Hull = GlobalVariables.PlayerHullStrength;
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
        public override void Draw()
        {
            if (!isDrawn)
            {
                SpawnWidth = (int)(GlobalVariables.WindowWidth - Width)/2;
                Canvas.SetTop(CanvasItem, GlobalVariables.WindowHeight - 3*Height);
                Canvas.SetLeft(CanvasItem, SpawnWidth);
                Canvas.Children.Add(CanvasItem);
                isDrawn = true;
            }
        }
        public override void OnCollision(int damage)
        {
            RepresentedShip.TakeDamage(damage);
        }
    }
}
