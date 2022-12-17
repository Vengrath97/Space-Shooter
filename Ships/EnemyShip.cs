using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Space_Shooter
{
    class EnemyShip : IShip
    {
        public int HullStrength;
        public int CurrentHullStrength;
        public int Speed;
        public Rectangle Model;
        private List<Gun> Guns = new();

        public EnemyShip(int hullStrength = 1, int speed = 10)
        {
            Random rand = new();
            HullStrength = hullStrength;
            CurrentHullStrength = HullStrength;
            Speed = speed;
            //Guns.Add(new Laser());
            Guns.Add(new DoubleLaser());
            //Guns.Add(new TripleLaser());
        }
        public void Shoot(Canvas canvas)
        {
            foreach(Gun gun in Guns)
            {
                gun.Fire(canvas, Model,true);
            }
        }

        public void Move(DirectionDictionary.Direction direction)
        {
            switch(direction)
            {
                case DirectionDictionary.Direction.Left:
                    if (IsThereSpaceLeft())
                        moveLeft();
                    break;
                case DirectionDictionary.Direction.Right:
                    if (IsThereSpaceRight())
                        moveRight();
                    break;
                case DirectionDictionary.Direction.Up:
                    if (IsThereSpaceUp())
                        moveUp();
                    break;
                case DirectionDictionary.Direction.Down:
                    if (IsThereSpaceDown())
                        moveDown();
                    break;
                default:
                    break;
            }
        }
        private void moveLeft()
        {
            Canvas.SetLeft(Model, Canvas.GetLeft(Model) - this.Speed);
        }
        private void moveRight()
        {
            Canvas.SetLeft(Model, Canvas.GetLeft(Model) + this.Speed);
        }
        private void moveUp()
        {
            Canvas.SetTop(Model, Canvas.GetTop(Model) - this.Speed);
        }
        private void moveDown()
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
        public bool IsThereSpaceDown()
        {
            return (Canvas.GetTop(Model) + Model.Width < GlobalVariables.WindowHeight + this.Speed);
        }
    }
}