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
        private readonly List<Gun> Guns = new();

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
                        MoveLeft();
                    break;
                case DirectionDictionary.Direction.Right:
                    if (IsThereSpaceRight())
                        MoveRight();
                    break;
                case DirectionDictionary.Direction.Up:
                    if (IsThereSpaceUp())
                        MoveUp();
                    break;
                case DirectionDictionary.Direction.Down:
                    if (IsThereSpaceDown())
                        MoveDown();
                    break;
                default:
                    break;
            }
        }
        private void MoveLeft()
        {
            Canvas.SetLeft(Model, Canvas.GetLeft(Model) - this.Speed);
        }
        private void MoveRight()
        {
            Canvas.SetLeft(Model, Canvas.GetLeft(Model) + this.Speed);
        }
        private void MoveUp()
        {
            Canvas.SetTop(Model, Canvas.GetTop(Model) - this.Speed);
        }
        private void MoveDown()
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