using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Week2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();

            Bee.MouseDown += (s, e) =>
            {
                Point p = e.GetPosition(Garden);
                Image image = new Image();
                image.Source = new BitmapImage(
                     new Uri("pack://application:,,,/images/honey.png"));
                image.Width = 50;
                image.Height = 50;
                Canvas.SetTop(image, p.Y);
                Canvas.SetLeft(image, p.X);
                Garden.Children.Add(image);
                
            };
        }
        
        void timer_Tick(object sender, EventArgs e)
        {
            double x = Canvas.GetLeft(Bee);
            double y = Canvas.GetTop(Bee);
            int step =( random.Next() % 50)+5;
            step = 20;
            switch (random.Next() % 5) { 
                case 1:
                    Canvas.SetLeft(Bee, x + step);
                    break; 
                case 2:
                    Canvas.SetTop(Bee, y + step);
                    break;
                case 3:
                    Canvas.SetLeft(Bee, x - step);
                    break;
                case 4:
                    Canvas.SetTop(Bee, y - step);
                    break;
            }
            x = Canvas.GetLeft(Bee);
            y = Canvas.GetTop(Bee);

            if (x < 0 || x > Garden.ActualWidth)
            {
                Canvas.SetLeft(Bee, 10);
            }
            if (y < 0 || y > Garden.ActualHeight)
            {
                Canvas.SetTop(Bee, 10);
            }
            Debug.WriteLine("h = " + Garden.ActualHeight);
        }
    }
}
