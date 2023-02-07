using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Space_Shooter.Storage;

namespace Space_Shooter
{
    public partial class MainWindow : Window
    {


        private CollisionData collisionData;
        private PlayerShipOnCanvas playerShipOnCanvas;
        private readonly DispatcherTimer gameTimer = new();
        private readonly Random rand = new();
        private readonly List<ObjectOnCanvas> removalList = new();
        private readonly int EnemySpawnRate = 50;
        private readonly int score = new();
        private int SafetyPeriod = GlobalVariables.SafetyPeriod;

        private bool moveLeft;
        private bool moveRight;
        private bool moveUp;
        private bool moveDown;

        public MainWindow()
        {
            InitializeComponent();
            SetupWindow();
            InitializePlayerShip();
            RunGameLoop();
        }
        private void SetupWindow()
        {
            MinWidth = GlobalVariables.WindowWidth;
            Width = GlobalVariables.WindowWidth;
            MaxWidth = GlobalVariables.WindowWidth;
            MinHeight = GlobalVariables.WindowHeight;
            Height = GlobalVariables.WindowHeight;
            MaxHeight = GlobalVariables.WindowHeight;
            Draw.DrawBackground(MyCanvas);
            MyCanvas.Focus();

        }
        private void InitializePlayerShip()
        {
            playerShipOnCanvas = new(MyCanvas, SpriteUri.Player, 56, 50);
            playerShipOnCanvas.RepresentedShip = new PlayerShip
            {
                Hull = GlobalVariables.PlayerHullStrength,
                Speed = GlobalVariables.PlayerShipSpeed
            };

            BasicLaser StarterGun = new(MyCanvas);
            playerShipOnCanvas.RepresentedShip.Guns.Add(StarterGun);

            playerShipOnCanvas.Draw();
        }
        private void RunGameLoop()
        {
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }
        private void GameLoop(object sender, EventArgs e)
        {
            SafetyPeriod -= 1;
            UpdateLabels();
            SpawnEnemies();
            MoveObjects();
            EnemiesShoot();
            CheckForCollisions();
            DisposeOutOfBondsItems();
            if (playerShipOnCanvas.RepresentedShip.Hull < 1) GameOver();

        }
        private void UpdateLabels()
        {
            scoreText.Content = "Score: " + score;
            damageText.Content = "Hull: " + playerShipOnCanvas.RepresentedShip.Hull;
        }
        private void SpawnEnemies()
        {
            if (SafetyPeriod < 0)
            {
                MakeEnemies();
                SafetyPeriod += EnemySpawnRate;
            }
        }
        private void MakeEnemies()
        {
            string uri = SpriteUri.EnemyShipX[(int)rand.Next(0, 5)];
            ShipOnCanvas enemyShip = new(MyCanvas, uri);
            enemyShip.SpeedXOffset = 0;
            enemyShip.SpeedYOffset = 10;
            enemyShip.RepresentedShip.Guns.Add(new BasicLaser(MyCanvas));
            enemyShip.Draw();
        }
        private void MoveObjects()
        {
            foreach (ObjectOnCanvas canvasItem in ObjectOnCanvas.CanvasItems)
            {
                if (canvasItem is PlayerShipOnCanvas)
                {
                    DirectionDictionary.Direction xAxis = (moveLeft || moveRight) ? (moveLeft ? DirectionDictionary.Direction.Left : DirectionDictionary.Direction.Right) : DirectionDictionary.Direction.Unknown;

                    DirectionDictionary.Direction yAxis = (moveUp || moveDown) ? (moveUp ? DirectionDictionary.Direction.Up : DirectionDictionary.Direction.Down) : DirectionDictionary.Direction.Unknown;

                    canvasItem.Move(xAxis, yAxis);
                }
                else
                {
                    canvasItem.Move();
                }
            }
        }
        private void EnemiesShoot()
        {
            foreach (ObjectOnCanvas canvasItem in ObjectOnCanvas.CanvasItems)
            {
                if (canvasItem is ShipOnCanvas && canvasItem is not PlayerShipOnCanvas)
                {
                    //idk why this doesn't work
                    //canvasItem.Shoot();
                }
            }
        }
        private void CheckForCollisions()
        {
            foreach (ObjectOnCanvas canvasItem in ObjectOnCanvas.CanvasItems)
            {
                if (canvasItem is BulletOnCanvas)
                {
                    collisionData = canvasItem.CheckForCollision();
                    if (collisionData is not null)
                    {
                        ResolveCollision(collisionData);
                    }
                }
            }
        }
        private void ResolveCollision(CollisionData collisionData)
        {
            if (collisionData.Object1.SpeedYOffset > 0 && collisionData.Object2 is PlayerShipOnCanvas)
            {
                removalList.Add(collisionData.Object1);
                collisionData.Object2.OnCollision(10);
            }
            if (collisionData.Object2 is not PlayerShipOnCanvas)
            {
                removalList.Add(collisionData.Object1);
                removalList.Add(collisionData.Object2);
            }
        }
        private void DisposeOutOfBondsItems()
        {
            foreach (ObjectOnCanvas canvasItem in ObjectOnCanvas.CanvasItems)
            {
                if (Canvas.GetTop(canvasItem.CanvasItem) >= GlobalVariables.WindowHeight - canvasItem.CanvasItem.Height)
                {
                    if (canvasItem is not PlayerShipOnCanvas)
                    {
                        removalList.Add(canvasItem);
                        playerShipOnCanvas.RepresentedShip.TakeDamage(GlobalVariables.HullDamageWhenEnemyEscapes);
                    }
                }
                if (Canvas.GetTop(canvasItem.CanvasItem) <= 10 && canvasItem is BulletOnCanvas)
                {
                    removalList.Add(canvasItem);
                }
            }
            foreach (ObjectOnCanvas toRemove in removalList)
            {
                if (toRemove is not PlayerShipOnCanvas)
                    toRemove.Dispose();
            }
        }
        private void GameOver()
        {
            gameTimer.Tick -= GameLoop;
            scoreText.Content = "";
            damageText.Content = "";
            gameOverText.Content = $"GAME OVER\nSCORE: {score}";
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                moveLeft = true;
            }
            if (e.Key == Key.Right)
            {
                moveRight = true;
            }
            if (e.Key == Key.Up)
            {
                moveUp = true;
            }
            if (e.Key == Key.Down)
            {
                moveDown = true;
            }
            if (e.Key == Key.Space)
            {
                playerShipOnCanvas.Shoot();
            }
        }
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                moveRight = false;
            }
            if (e.Key == Key.Up)
            {
                moveUp = false;
            }
            if (e.Key == Key.Down)
            {
                moveDown = false;
            }

        }
    }
}
