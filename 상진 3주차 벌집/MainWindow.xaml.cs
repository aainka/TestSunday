using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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



namespace csharp_3week
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();


            timer.Tick += timerTick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();
        }



        void timerTick(object sender, EventArgs e)
        {
            Move((random.Next() % 5) + 1);
        }

        private void beerightClick(object sender, MouseButtonEventArgs e)
        {
            Move(6);
        }

        private void beeleftClick(object sender, MouseButtonEventArgs e)
        {
            Move(7);
        }


        void Move(int num)
        {
            double x = Canvas.GetLeft(bee5);
            double y = Canvas.GetTop(bee5);
            double a = Canvas.GetLeft(beetext);
            double b = Canvas.GetTop(beetext);
            a = 0;
            b = 0;

            int step = 50;
            switch (num)
            {
                case 1:
                    Canvas.SetLeft(bee5, x);
                    Canvas.SetTop(bee5, y);
                    break;
                case 2:
                    if (y > Garden.ActualHeight - bee5.ActualHeight - step)
                    {
                        y = y - step;
                        Canvas.SetLeft(bee5, x);
                        Canvas.SetTop(bee5, y);
                        Canvas.SetLeft(beetext, a + x);
                        Canvas.SetTop(beetext, a + y - 20);
                    }
                    else
                    {
                        y = y + step;
                        Canvas.SetLeft(bee5, x);
                        Canvas.SetTop(bee5, y);
                        Canvas.SetLeft(beetext, a + x);
                        Canvas.SetTop(beetext, a + y - 20);
                    }
                    break;
                case 3:
                    if (y < bee5.ActualHeight + step)
                    {
                        y = y + step;
                        Canvas.SetLeft(bee5, x);
                        Canvas.SetTop(bee5, y);
                        Canvas.SetLeft(beetext, a + x);
                        Canvas.SetTop(beetext, a + y - 20);
                    }
                    else
                    {
                        y = y - step;
                        Canvas.SetLeft(bee5, x);
                        Canvas.SetTop(bee5, y);
                        Canvas.SetLeft(beetext, a + x);
                        Canvas.SetTop(beetext, a + y - 20);
                    }
                    break;
                case 4:
                    if (x > Garden.ActualWidth - bee5.ActualWidth - step)
                    {
                        x = x - step;
                        Canvas.SetLeft(bee5, x);
                        Canvas.SetTop(bee5, y);
                        Canvas.SetLeft(beetext, a + x);
                        Canvas.SetTop(beetext, a + y - 20);
                    }
                    else
                    {
                        x = x + step;
                        Canvas.SetLeft(bee5, x);
                        Canvas.SetTop(bee5, y);
                        Canvas.SetLeft(beetext, a + x);
                        Canvas.SetTop(beetext, a + y - 20);
                    }
                    break;
                case 5:
                    if (x < bee5.ActualWidth + step)
                    {
                        x = x + step;
                        Canvas.SetLeft(bee5, x);
                        Canvas.SetTop(bee5, y);
                        Canvas.SetLeft(beetext, a + x);
                        Canvas.SetTop(beetext, a + y - 20);
                    }
                    else
                    {
                        x = x - step;
                        Canvas.SetLeft(bee5, x);
                        Canvas.SetTop(bee5, y);
                        Canvas.SetLeft(beetext, a + x);
                        Canvas.SetTop(beetext, a + y - 20);
                    }
                    break;

                case 6:
                    bee5.Width += 10;
                    break;
                case 7:
                    bee5.Width -= 10;
                    if (bee5.Width <= 10)
                    {
                        bee5.Width = 10;
                    }
                    break;

            }

        }

        int Count = 1;
        private void Hive_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new Bee(Garden, 10, 10, random.Next() % 100 + 10, "홍길동"+(Count++));
        }
    }





    class Bee
    {
        Image image = new Image();
        TextBlock MyName = new TextBlock();

        Random random = new Random();
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        private Canvas _canvas;
        private int speed = 10;
        private int Energy = 3;



        public Point pos
        {
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
                Canvas.SetLeft(MyName, value.X);
                Canvas.SetTop(MyName, value.Y - 20);
            }
        }
        //pos 하나로 해야 기준점이 하나로 고정됨



        public Bee(Canvas canvas, double x, double y, int speed, string name)
        {
            this._canvas = canvas;
            this.speed = speed;
            pos = new Point(x, y);
            image.Source = new BitmapImage(
                   new Uri(@"/img/bee_worker.png", UriKind.RelativeOrAbsolute)
               );
            image.Width = 30;
            image.Height = 30;
            image.MouseDown += Bee_MouseDown;
            MyName.FontSize = 10;
            MyName.VerticalAlignment = VerticalAlignment.Center;
            MyName.HorizontalAlignment = HorizontalAlignment.Center;
            MyName.Background= Brushes.Black;
            MyName.Foreground= Brushes.Yellow;
            MyName.Text = name + (string)"■■■";
            canvas.Children.Add(image);
            canvas.Children.Add(MyName);
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();

        }
        void timer_Tick(object sender, EventArgs e)
        {
            Move();
        }
        private void Bee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            
            if (Energy == 1)
            {
                timer.Stop();
                _canvas.Children.Remove(image);
                _canvas.Children.Remove(MyName);
            }
            else if (Energy == 3) 
            {
                string name = MyName.Name;
                name = name.Substring(0, name.Length);
                Energy-- ;

                MyName.Text = name + (string)"■■□";

                //  "■■□"
                //  "■□□"
                //textblock에서 값을 가져오지 못함

            }
            else if (Energy == 2)
            {
                string name = MyName.Name ;
                Energy--;
                
                name = name.Substring(0, name.Length);

                MyName.Text = name +(string)"■□□";


            }
        }

        public void Move()
        {
            switch ((random.Next() % 5) + 1)
            {
                case 1:
                    pos = new Point(pos.X, pos.Y);

                    break;
                case 2:
                    if (pos.Y > _canvas.ActualHeight - 30 - speed)
                    {
                        pos = new Point(pos.X, pos.Y - speed);

                    }
                    else
                    {
                        pos = new Point(pos.X, pos.Y + speed);

                    }
                    break;
                case 3:
                    if (pos.Y < 30 + speed)
                    {
                        pos = new Point(pos.X, pos.Y + speed);

                    }
                    else
                    {
                        pos = new Point(pos.X, pos.Y - speed);

                    }
                    break;
                case 4:
                    if (pos.X > _canvas.ActualWidth - 30 - speed)
                    {
                        pos = new Point(pos.X - speed, pos.Y);

                    }
                    else
                    {
                        pos = new Point(pos.X + speed, pos.Y);

                    }
                    break;
                case 5:
                    if (pos.X < 30 + speed)
                    {
                        pos = new Point(pos.X + speed, pos.Y);

                    }
                    else
                    {
                        pos = new Point(pos.X - speed, pos.Y);

                    }
                    break;
            }
        }

    }
}
