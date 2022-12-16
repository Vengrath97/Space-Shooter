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
        private readonly DispatcherTimer gameTimer = new();
        private readonly Random rand = new();


        private readonly int GameTickLength = 20;
        private int SafetyPeriod = 50;
        private int EnemySpawnRate = 50;
        private int EnemyShipSpeed = 10;
        private int Score;
        private int HullStrength;
        private bool moveLeft;
        private bool moveRight;

        PlayerShip playerShip;

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

        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                moveLeft = true;
            }
            if(e.Key == Key.Right)
            {
                moveRight = true;
            }
            if(e.Key == Key.Space)
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
        }
        private void MoveObjects()
        {
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    if (Canvas.GetTop(x) < 0)
                    {
                        itemRemover.Add(x);
                    }
                    CheckForEnemyHits(x);
                }
                if ((string)x.Tag == "enemy")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + EnemyShipSpeed);
                    if (Canvas.GetTop(x) > GlobalVariables.WindowHeight)
                    {
                        itemRemover.Add(x);
                        HullStrength -= 10;
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
                        Score += 10;
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
            scoreText.Content = "Score: " + Score;
            damageText.Content = "Hull: " + HullStrength;
        }
        private void AttemptToMovePlayer()
        {
            if (moveLeft)
            {
                playerShip.Move(1);
            }
            if (moveRight)
            {
                playerShip.Move(2);
            }
        }
        /*
        private void MovePlayerLeft()
        {
            Canvas.SetLeft(playerShip.Model, Canvas.GetLeft(playerShip.Model) - PlayerShipSpeed);
        }
        private void MovePlayerRight()
        {
            Canvas.SetLeft(playerShip.Model, Canvas.GetLeft(playerShip.Model) + PlayerShipSpeed);
        }
        private bool IsThereSpaceLeft()
        {
            return (Canvas.GetLeft(playerShip.Model) > PlayerShipSpeed);
        }
        private bool IsThereSpaceRight()
        {
            return (Canvas.GetLeft(playerShip.Model) + playerShip.Model.Width < GlobalVariables.WindowWidth);
        }*/
    }
}
