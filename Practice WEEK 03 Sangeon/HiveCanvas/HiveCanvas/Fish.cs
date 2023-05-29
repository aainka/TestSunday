using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace HiveCanvas
{
    public abstract class Fish
    {

        Image image = new Image();
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
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
            }
        }

        public double X
        {
            get
            {
                return Canvas.GetLeft(image);
            }
            set
            {
                Canvas.SetLeft(image, X);
            }
        }
        public double Y
        {
            get
            {
                return   Canvas.GetTop(image) ;
            }
            set
            {
                Canvas.SetTop(image, Y);
            }
        }

        abstract public void TimerMove(object sender, EventArgs e);

        public Fish(string imgName)
        {
            loadImage(imgName);
            timer.Tick += TimerMove;
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Start();
        }

        public double distance(Fish fish)
        {
            var result = Math.Sqrt((X - fish.X)* (X - fish.X) + (Y - fish.Y)* (Y - fish.Y));
            return result;
        }
        public void loadImage(string png)
        {
            image.Source = new BitmapImage(
                 new Uri(@"/Images/"+png, UriKind.RelativeOrAbsolute)
             );
            image.Width = 30;
            image.Height = 30;
        }

        public void Dispose()
        {
            timer.Stop();
            _canvas.Children.remove(image);
        }
        
    }
}
