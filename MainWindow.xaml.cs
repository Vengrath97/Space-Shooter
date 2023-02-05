using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Space_Shooter
{
    public partial class MainWindow : Window
    {


        private readonly DispatcherTimer gameTimer = new();
        private readonly Random rand = new();
        private  PlayerShipOnCanvas playerShipOnCanvas;
        private List<ObjectOnCanvas> removalList = new();
        private readonly int EnemySpawnRate = 50;
        private readonly int score = new();
        private int SafetyPeriod = 0;

        private bool moveLeft;
        private bool moveRight;
        private bool moveUp;
        private bool moveDown;


        public MainWindow()
        {
            InitializeComponent();
            InitializePlayerShip();
            SetupWindow();
            gameTimer.Interval = TimeSpan.FromMilliseconds(15);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
            MyCanvas.Focus();
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

        }
        private void GameLoop(object sender, EventArgs e)
        {
            SafetyPeriod -= 1;
            UpdateLabels();
            SpawnEnemies();
            MoveObjects();
            MarkForRemoval();
            if (playerShipOnCanvas.RepresentedShip.Hull < 1) GameOver();

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
            enemyShip.Draw();
        }
        private void MoveObjects()
        {
            foreach(ObjectOnCanvas canvasItem in ObjectOnCanvas.CanvasItems)
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
                if (canvasItem is BulletOnCanvas)
                {
                    canvasItem.CheckForCollision();
                }
            }
        }
        private void UpdateLabels()
        {
            scoreText.Content = "Score: " + score;
            damageText.Content = "Hull: " + playerShipOnCanvas.RepresentedShip.Hull;
        }
        private void MarkForRemoval()
        {
            foreach (ObjectOnCanvas canvasItem in ObjectOnCanvas.CanvasItems)
            {
                if (Canvas.GetTop(canvasItem.CanvasItem) >= GlobalVariables.WindowHeight-1)
                {
                    removalList.Add(canvasItem);
                }
            }
            foreach (ObjectOnCanvas toRemove in removalList)
            {
                toRemove.Dispose();
            }
        }
        private void InitializePlayerShip()
        {
            playerShipOnCanvas = new(MyCanvas, SpriteUri.Player, 56, 50);
            playerShipOnCanvas.RepresentedShip = new PlayerShip();

            BasicLaser StarterGun = new(MyCanvas);
            playerShipOnCanvas.RepresentedShip.Guns.Add(StarterGun);

            playerShipOnCanvas.Draw();
        }
    }
}
