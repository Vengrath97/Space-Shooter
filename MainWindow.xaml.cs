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
        private int PlayerShipSpeed = 10;
        private int EnemyShipSpeed = 10;
        private int Score;
        private int HullStrength;
        private bool moveLeft;
        private bool moveRight;

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Interval = TimeSpan.FromMilliseconds(GameTickLength);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
            MyCanvas.Focus();
            Draw.DrawBackground(MyCanvas);
            Draw.DrawPlayer(player);
        }
        void DrawBackground()
        {
            ImageBrush bg = new();

            bg.ImageSource = new BitmapImage(new Uri(SpriteUri.Background, UriKind.Relative));
            bg.TileMode = TileMode.Tile;
            bg.Viewport = new Rect(0, 0, 0.15, 0.15);
            bg.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            MyCanvas.Background = bg;
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
            if (e.Key == Key.Space)
            {
                Rectangle newBullet = new()
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                };
                Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);
                Canvas.SetTop(newBullet, Canvas.GetTop(player) - newBullet.Height);

                MyCanvas.Children.Add(newBullet);
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
            Canvas.SetLeft(newEnemy, rand.Next(30, 430));

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
                    if (Canvas.GetTop(x) > 700)
                    {
                        itemRemover.Add(x);
                        HullStrength -= 100;
                    }
                }
            }
        }
        private void CheckForEnemyHits(Rectangle x)
        {
            //enemy hit logic
            Rect bulletHitBox = new(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            foreach (var y in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)y.Tag == "enemy")
                {
                    Rect enemyHit = new(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                    if (bulletHitBox.IntersectsWith(enemyHit))
                    {
                        itemRemover.Add(x);
                        itemRemover.Add(y);
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
            if (moveLeft && IsThereSpaceLeft())
            {
                MovePlayerLeft();
            }
            if (moveRight && IsThereSpaceRight())
            {
                MovePlayerRight();
            }
        }
        private void MovePlayerLeft()
        {
            Canvas.SetLeft(player, Canvas.GetLeft(player) - PlayerShipSpeed);
        }
        private void MovePlayerRight()
        {
            Canvas.SetLeft(player, Canvas.GetLeft(player) + PlayerShipSpeed);
        }
        private bool IsThereSpaceLeft()
        {
            return (Canvas.GetLeft(player) > PlayerShipSpeed);
        }
        private bool IsThereSpaceRight()
        {
            return (Canvas.GetLeft(player) + 90 < Application.Current.MainWindow.Width);
        }
    }
}
