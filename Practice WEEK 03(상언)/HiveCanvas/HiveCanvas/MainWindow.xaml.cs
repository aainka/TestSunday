using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace HiveCanvas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        Random Random = new Random();
        public MainWindow()
        {
            InitializeComponent();
            //Image image = new Image();

            //image.Source = new BitmapImage(
            //        new Uri(@"/Images/bee_worker.png", UriKind.RelativeOrAbsolute)
            //    );

           // Garden.Children.Add(image);

            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            //  Move();
           // new Bee(Garden, 10, 10, Random.Next() % 100 + 10);
        }

        private void Bee_MouseDown(object sender, MouseButtonEventArgs e)
        {
         //   ChangeSize();
        }

        void Move()
        {
            int randNum = Random.Next();
            int step = (randNum % 10) + 5;
            var x = Canvas.GetLeft(Bee);
            var y = Canvas.GetTop(Bee);
            switch (randNum % 4)
            {
                case 0:
                    if (x + step <= Garden.ActualWidth - Bee.Width) { Canvas.SetLeft(Bee, x + step); }
                    else { Move(); }
                    break;
                case 1:
                    if (x - step >= 0) { Canvas.SetLeft(Bee, x - step); }
                    else { Move(); }
                    break;
                case 2:
                    if (y + step <= Garden.ActualHeight - Bee.Height) { Canvas.SetTop(Bee, y + step); }
                    else { Move(); }
                    break;
                case 3:
                    if (y - step >= 0) { Canvas.SetTop(Bee, y - step); }
                    else { Move(); }
                    break;
            }
        }



        void ChangeSize()
        {
            int randNum = Random.Next();
            if (Bee.Width <= 50) { Enlarge(); }
            else
            {
                switch (randNum % 2)
                {
                    case 0:
                        Enlarge(); break;
                    case 1:
                        Smallen(); break;
                }
            }

        }

        void Enlarge()
        {
            Bee.Width += 50;
        }

        void Smallen()
        {
            Bee.Width -= 50;
        }

        private void Hive_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new Bee(Garden, 10, 10, Random.Next() % 100 + 10);
        }
    }

    class House
    {

    }

    class Bee
    {
        Image image = new Image();
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        private int speed = 10;

        public Point pos {
            get
            {
                return new Point(
                    Canvas.GetLeft(image),
                    Canvas.GetTop(image)
                );
            }
            set
            {
                Canvas.SetLeft(image, value.X);
                Canvas.SetTop(image, value.Y);
            }
        }
        

        public Bee(Canvas canvas, double x, double y, int speed)
        {
            this.speed = speed;
            pos = new Point(x,y);
            image.Source = new BitmapImage(
                   new Uri(@"/Images/bee_worker.png", UriKind.RelativeOrAbsolute)
               );
            image.Width = 30;
            image.Height = 30;
            canvas.Children.Add(image);
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            Move();
        }

        public void Move()
        {
            pos = new Point(pos.X + speed, pos.Y + speed);
            Debug.WriteLine(" POS = " + pos);
        }
    }
}
