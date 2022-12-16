using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Space_Shooter
{
    class PlayerShip : IShip
    {
        public int HullStrength;
        public int CurrentHullStrength;
        public int Speed;
        public Rectangle Model;
        private static List<Gun> Guns = new();

        public PlayerShip(int hullStrength = -1, int speed = -1)
        {
            HullStrength = (hullStrength == -1) ? GlobalVariables.PlayerHullStrength : hullStrength;
            CurrentHullStrength = HullStrength;
            Speed = (speed == -1) ? GlobalVariables.PlayerShipSpeed : speed;
            //Guns.Add(new Laser());
            //Guns.Add(new DoubleLaser());
            Guns.Add(new TripleLaser());
        }
        public void Shoot(Canvas canvas)
        {
            foreach(Gun gun in Guns)
            {
                gun.Fire(canvas, Model);
            }
        }

        public void Move(DirectionDictionary.Direction direction)
        {
            switch(direction)
            {
                case DirectionDictionary.Direction.Left:
                    if (IsThereSpaceLeft())
                        MovePlayerLeft();
                    break;
                case DirectionDictionary.Direction.Right:
                    if (IsThereSpaceRight())
                        MovePlayerRight();
                    break;
                case DirectionDictionary.Direction.Up:
                    if (IsThereSpaceUp())
                        MovePlayerUp();
                    break;
                case DirectionDictionary.Direction.Down:
                    if (IsThereSpaceDown())
                        MovePlayerDown();
                    break;
                default:
                    break;
            }
        }
        private void MovePlayerLeft()
        {
            Canvas.SetLeft(Model, Canvas.GetLeft(Model) - this.Speed);
        }
        private void MovePlayerRight()
        {
            Canvas.SetLeft(Model, Canvas.GetLeft(Model) + this.Speed);
        }
        private void MovePlayerUp()
        {
            Canvas.SetTop(Model, Canvas.GetTop(Model) - this.Speed);
        }
        private void MovePlayerDown()
        {
            Canvas.SetTop(Model, Canvas.GetTop(Model) + this.Speed);
        }
        private bool IsThereSpaceLeft()
        {
            return (Canvas.GetLeft(Model) > this.Speed);
        }
        private bool IsThereSpaceRight()
        {
            return (Canvas.GetLeft(Model) + Model.Width < GlobalVariables.WindowWidth - this.Speed);
        }
        private bool IsThereSpaceUp()
        {
            return (Canvas.GetTop(Model) > 0 + this.Speed);
        }
        private bool IsThereSpaceDown()
        {
            return (Canvas.GetTop(Model) + Model.Width < GlobalVariables.WindowHeight + this.Speed);
        }
    }
}