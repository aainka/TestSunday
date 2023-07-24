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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace gameproject
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>

    public partial class MainWindow : Window
    {
        Random random = new Random();
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        DoubleAnimation doubleAnimation = new DoubleAnimation();
        public MainWindow()

        {
            InitializeComponent();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();

            doubleAnimation.AccelerationRatio = 1;
            doubleAnimation.From = 50.0;
            doubleAnimation.To = 300.0;




        }
        void timer_Tick(object sender, EventArgs e)
        {
            //Move(random.Next()%4+1);
        }

  


    public void ballDown(object sender, MouseButtonEventArgs e)
        {
            Move(random.Next() % 2 + 5);  
            ball.BeginAnimation(Image.WidthProperty, doubleAnimation);
            ball.BeginAnimation(Image.HeightProperty, doubleAnimation);


          //  new ball(Garden, 4, 10, random.Next() % 100 + 10);

        }
        void Move(int Num)
        {
            double x = Canvas.GetLeft(ball);
            double y = Canvas.GetTop(ball);
            int step = 50;
            double w = ball.Width;
            switch (Num)
            {
                case 1:
                    if (x > Garden.ActualWidth - ball.ActualWidth - step)
                    {

                        Canvas.SetLeft(ball, x - step);
                        break;
                    }
                    Canvas.SetLeft(ball, x + step);
                    break;
                case 2:
                    if (y > Garden.ActualHeight - ball.ActualHeight - step)
                    {
                        Canvas.SetTop(ball, y - step);
                        break;
                    }
                    Canvas.SetTop(ball, y + step);
                    break;
                case 3:
                    if (x < ball.ActualWidth + step)
                    {
                        Canvas.SetLeft(ball, x + step);
                        break;
                    }
                    Canvas.SetLeft(ball, x - step);
                    break;
                case 4:
                    if (y < ball.ActualHeight + step)
                    {
                        Canvas.SetTop(ball, y + step);
                        break;
                    }
                    Canvas.SetTop(ball, y - step);
                    break;
                case 5:
                    ball.Width = w;
                    ball.Width += 10;
                    break;
                case 6:
                    if (ball.Width < 40)
                    {
                        ball.Width = 40;
                    }
                    else
                    {
                        ball.Width -= 10;
                    }
                   break;
            }

        }
    }
}


//private void LivehouseDown(object sender, MouseButtonEventArgs e)
//{

//    new Bee(Garden, 4, 10, random.Next() % 100 + 10);

//}

