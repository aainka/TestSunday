using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

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
            new SharkCreator(Aquarium);
        }
        

        

        //void Move()
        //{
        //    int randNum = Random.Next();
        //    int step = (randNum % 10) + 5;
        //    var x = Canvas.GetLeft(Bee);
        //    var y = Canvas.GetTop(Bee);
        //    switch (randNum % 4)
        //    {
        //        case 0:
        //            if (x + step <= Aquarium.ActualWidth - Bee.Width) { Canvas.SetLeft(Bee, x + step); }
        //            else { Move(); }
        //            break;
        //        case 1:
        //            if (x - step >= 0) { Canvas.SetLeft(Bee, x - step); }
        //            else { Move(); }
        //            break;
        //        case 2:
        //            if (y + step <= Aquarium.ActualHeight - Bee.Height) { Canvas.SetTop(Bee, y + step); }
        //            else { Move(); }
        //            break;
        //        case 3:
        //            if (y - step >= 0) { Canvas.SetTop(Bee, y - step); }
        //            else { Move(); }
        //            break;
        //    }
        



       
 

        private void UCave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new Anchovy("anchovy.png", Aquarium);
            //Image img = sender as Image;
            //new Prey(Aquarium, Canvas.GetLeft(img), Canvas.GetTop(img), Random.Next(25) + 10);
        }
    }



    class FishXX
    {
      //  int speed = 10;
      //  Canvas _canvas = null;


        //public FishXX(Canvas canvas, double x, double y, int speed)
        //{
        //    this.speed = speed;
        //    this._canvas = canvas;
        //    pos = new Point(x, y);
        //    image.Source = new BitmapImage(
        //           new Uri(@"/Images/bee_worker.png", UriKind.RelativeOrAbsolute)
        //       );
        //    image.Width = width;
        //    image.Height = height;
        //    image.MouseDown += Bee_MouseDown;
        //    canvas.Children.Add(image);
        //    timer.Tick += timer_Tick;
        //    timer.Interval = TimeSpan.FromMilliseconds(1);
        //    timer.Start();
        //}

        //public void Move()
        //{
        //    switch (random.Next(4))
        //    {
        //        case 0:
        //            if (pos.X + speed <= _canvas.ActualWidth - width) { pos = new Point(pos.X + speed, pos.Y); }
        //            else { Move(); }
        //            break;
        //        case 1:
        //            if (pos.X - speed >= 0) { pos = new Point(pos.X - speed, pos.Y); }
        //            else { Move(); }
        //            break;
        //        case 2:
        //            if (pos.Y + speed <= _canvas.ActualHeight - height) { pos = new Point(pos.X, pos.Y + speed); }
        //            else { Move(); }
        //            break;
        //        case 3:
        //            if (pos.Y - speed >= 0) { pos = new Point(pos.X, pos.Y - speed); }
        //            else { Move(); }
        //            break;
        //    }
        //}

        void timer_Tick(object sender, EventArgs e)
        {
         //   Move();
        }
    }
}
