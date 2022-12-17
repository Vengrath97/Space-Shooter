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
        private int score;
        private static List<Rectangle> itemRemover = new();
        public static List<Bullet> bullets = new();
        public static List<Bullet> bulletRemover = new();
        private static List<EnemyShip> enemyShips = new();
        private static List<EnemyShip> enemyShipsRemover = new();
        private readonly DispatcherTimer gameTimer = new();
        private readonly Random rand = new();


        private readonly int GameTickLength = 20;
        private readonly int EnemySpawnRate = 50;
        private int SafetyPeriod = 0;

        private bool moveLeft;
        private bool moveRight;
        private bool moveUp;
        private bool moveDown;
        private readonly PlayerShip playerShip;

        public MainWindow()
        {
            InitializeComponent();
            playerShip = new() { Model = player };
            SetupWindow();
            gameTimer.Interval = TimeSpan.FromMilliseconds(GameTickLength);
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
            Draw.DrawPlayer(playerShip);

        }
        private void GameLoop(object sender, EventArgs e)
        {
            SafetyPeriod -= 1;
            UpdateLabels();
            SpawnEnemies();
            AttemptToMovePlayer();
            MoveObjects();
            RemoveOutOfBounds();
            ClearLists();
            if (playerShip.CurrentHullStrength < 1) GameOver();

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
                playerShip.Shoot(MyCanvas);
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
        private void MakeEnemies()
        {

            ImageBrush enemySprite = new();
            int SpriteID = rand.Next(0, 5);
            enemySprite.ImageSource = new BitmapImage(new Uri(SpriteUri.EnemyShipX[SpriteID], UriKind.Relative));
            Rectangle newEnemy = new()
            {
                Tag = "enemy",
                Height = 50,
                Width = 56,
                Fill = enemySprite
            };

            EnemyShip enemyShip = new() { Model = newEnemy };
            enemyShips.Add(enemyShip);

            Canvas.SetTop(enemyShip.Model, -100);
            Canvas.SetLeft(enemyShip.Model, rand.Next(0, (int)(GlobalVariables.WindowWidth-enemyShip.Model.Width)));

            MyCanvas.Children.Add(newEnemy);
        }
        private void RemoveOutOfBounds()
        {
            foreach (Rectangle item in itemRemover)
            {
                MyCanvas.Children.Remove(item);
            }
            foreach (Bullet item in bulletRemover)
            {
                MyCanvas.Children.Remove(item.Model);
                bullets.Remove(item);
            }
            foreach (EnemyShip item in enemyShipsRemover)
            {
                MyCanvas.Children.Remove(item.Model);
                enemyShips.Remove(item);
            }
        }
        private void MoveObjects()
        {
            foreach(Bullet bullet in bullets)
            {
                bullet.Move();
                if (Canvas.GetTop(bullet.Model) < 0 || Canvas.GetTop(bullet.Model) >= GlobalVariables.WindowHeight)
                {
                    itemRemover.Add(bullet.Model);
                    bulletRemover.Add(bullet);
                }
                CheckForEnemyHits(bullet);
            }
            foreach(EnemyShip ship in enemyShips)
            {
                ship.Move(DirectionDictionary.Direction.Down);
                ship.Shoot(MyCanvas); 
                if (!ship.IsThereSpaceDown())
                {
                    itemRemover.Add(ship.Model);
                    enemyShipsRemover.Add(ship);
                    playerShip.CurrentHullStrength -= GlobalVariables.HullDamageWhenEnemyEscapes;
                }
            }
        }
        private void CheckForEnemyHits(Bullet bullet)
        {
            Rectangle bulletModel = bullet.Model;
            Rect bulletHitBox = new(Canvas.GetLeft(bulletModel), Canvas.GetTop(bulletModel), bulletModel.Width, bulletModel.Height);
            foreach (EnemyShip ship in enemyShips)
            {
                Rect enemyHit = new(Canvas.GetLeft(ship.Model), Canvas.GetTop(ship.Model), ship.Model.Width, ship.Model.Height);
                if (bulletHitBox.IntersectsWith(enemyHit) && !bullet.IsEnemy)
                {
                    score += 10;
                    itemRemover.Add(bullet.Model);
                    bulletRemover.Add(bullet);
                    itemRemover.Add(ship.Model);
                    enemyShipsRemover.Add(ship);
                }
            }
        }
        private void ClearLists()
        {
            bulletRemover = new();
            enemyShipsRemover = new();
        }

        private void SpawnEnemies()
        {
            if (SafetyPeriod < 0)
            {
                MakeEnemies();
                SafetyPeriod += EnemySpawnRate;
            }
        }
        private void UpdateLabels()
        {
            scoreText.Content = "Score: " + score;
            damageText.Content = "Hull: " + playerShip.CurrentHullStrength;
        }
        private void AttemptToMovePlayer()
        {
            if (moveLeft)
            {
                playerShip.Move(DirectionDictionary.Direction.Left);
            }
            if (moveRight)
            {
                playerShip.Move(DirectionDictionary.Direction.Right);
            }
            if (moveUp)
            {
                playerShip.Move(DirectionDictionary.Direction.Up);
            }
            if (moveDown)
            {
                playerShip.Move(DirectionDictionary.Direction.Down);
            }
        }
    }
}
