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
        public int Speed;
        public Rectangle Model;
        
        public PlayerShip(int hullStrength = -1, int speed = -1)
        {
            HullStrength = (hullStrength == -1) ? GlobalVariables.PlayerHullStrength : hullStrength;
            Speed = (speed == -1) ? GlobalVariables.PlayerShipSpeed : speed;
        }
        public void Shoot(Canvas canvas)
        {
            Rectangle newBullet = new()
            {
                Tag = "bullet",
                Height = 20,
                Width = 5,
                Fill = Brushes.White,
                Stroke = Brushes.Red
            };
            Canvas.SetLeft(newBullet, Canvas.GetLeft(Model) + Model.Width / 2);
            Canvas.SetTop(newBullet, Canvas.GetTop(Model) - newBullet.Height);

            canvas.Children.Add(newBullet);
        }

        public void Move(int direction)
        {
            switch(direction)
            {
                case 1:
                    if (IsThereSpaceLeft())
                        MovePlayerLeft();
                    break;
                case 2:
                    if (IsThereSpaceRight())
                        MovePlayerRight();
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
        private bool IsThereSpaceLeft()
        {
            return (Canvas.GetLeft(Model) > this.Speed);
        }
        private bool IsThereSpaceRight()
        {
            return (Canvas.GetLeft(Model) + Model.Width < GlobalVariables.WindowWidth - this.Speed);
        }
    }
}