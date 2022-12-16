using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Space_Shooter
{
    public partial class MainWindow : Window
    {
        private readonly List<Rectangle> itemRemover = new();
        public static List<Bullet> bullets = new();
        public static List<Bullet> bulletRemover = new();
        private readonly DispatcherTimer gameTimer = new();
        private readonly Random rand = new();


        private readonly int GameTickLength = 20;
        private int SafetyPeriod = 50;
        private int EnemySpawnRate = 50;
        private int EnemyShipSpeed = 10;

        private int score;

        private bool moveLeft;
        private bool moveRight;
        private bool moveUp;
        private bool moveDown;
        private readonly PlayerShip playerShip;

        public MainWindow()
        {
            InitializeComponent();
            SetupWindow();
            playerShip = new() { Model = player };
            gameTimer.Interval = TimeSpan.FromMilliseconds(GameTickLength);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
            MyCanvas.Focus();
            Draw.DrawBackground(MyCanvas);
            Draw.DrawPlayer(playerShip);
        }
        private void SetupWindow()
        {
            MinWidth = GlobalVariables.WindowWidth;
            Width = GlobalVariables.WindowWidth;
            MaxWidth = GlobalVariables.WindowWidth;
            MinHeight = GlobalVariables.WindowHeight;
            Height = GlobalVariables.WindowHeight;
            MaxHeight = GlobalVariables.WindowHeight;

        }
        private void GameLoop(object sender, EventArgs e)
        {
            SafetyPeriod -= 1;
            UpdateLabels();
            SpawnEnemies();
            AttemptToMovePlayer();
            MoveObjects();
            RemoveOutOfBounds();
            if (playerShip.CurrentHullStrength < 1) GameOver();

        }
        private void GameOver()
        {
            gameTimer.Tick -= GameLoop;
            scoreText.Content = "";
            damageText.Content = "";
            gameOverText.Content = $"Captain, you have survived \nfor a long time.\nYou have scored {score} points";
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
            int EnemySprite = rand.Next(0, 5);
            enemySprite.ImageSource = new BitmapImage(new Uri(SpriteUri.EnemyShipX[EnemySprite], UriKind.Relative));
            Rectangle newEnemy = new()
            {
                Tag = "enemy",
                Height = 50,
                Width = 56,
                Fill = enemySprite
            };

            Canvas.SetTop(newEnemy, -100);
            Canvas.SetLeft(newEnemy, rand.Next(0, (int)(GlobalVariables.WindowWidth-newEnemy.Width)));

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
                bullets.Remove(item);
            }
        }
        private void MoveObjects()
        {
            foreach(Bullet bullet in bullets)
            {
                bullet.Move();
                if (Canvas.GetTop(bullet.Model) < 0)
                {
                    itemRemover.Add(bullet.Model);
                    bulletRemover.Add(bullet);
                }
                CheckForEnemyHits(bullet.Model);
            }
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "enemy")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + EnemyShipSpeed);
                    if (Canvas.GetTop(x) > GlobalVariables.WindowHeight)
                    {
                        itemRemover.Add(x);
                        playerShip.CurrentHullStrength -= GlobalVariables.HullDamageWhenEnemyEscapes;
                    }
                }
            }
        }
        private void CheckForEnemyHits(Rectangle bullet)
        {
            Rect bulletHitBox = new(Canvas.GetLeft(bullet), Canvas.GetTop(bullet), bullet.Width, bullet.Height);
            foreach (var enemy in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)enemy.Tag == "enemy")
                {
                    Rect enemyHit = new(Canvas.GetLeft(enemy), Canvas.GetTop(enemy), enemy.Width, enemy.Height);

                    if (bulletHitBox.IntersectsWith(enemyHit))
                    {
                        itemRemover.Add(bullet);
                        itemRemover.Add(enemy);
                        score += 10;
                    }
                }
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
