using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using static System.Math;


namespace Aquarium
{
    public abstract class Fish
    {
        Canvas? _canvas = null;
        Image image = new Image();

        RotateTransform rotateTransform = new RotateTransform(0);
        ScaleTransform scaleTransfrom= new ScaleTransform(1,1);
        TransformGroup transformGroup= new TransformGroup();

        //Why can't I append transfromGroup at the declaration section?
        //transformGroup.Children.Add(scaleTransfrom);
        //transformGroup.Children.Add(rotateTransform);

        DispatcherTimer timer = new DispatcherTimer();
        List<FishSchool> PreyFishSchoolList=null; List<FishSchool> PredatorFishSchoolList = null;
        FishSchool PreyFishList = null; FishSchool PredatorFishList = null;


        public Point position
        {
            get
            {
                return new Point(
                    (int)(Canvas.GetLeft(image)+image.Width/2),
                    (int)(Canvas.GetTop(image)+image.Height/2)
                );
            }
            set
            {
                Canvas.SetLeft(image, value.X - image.Width / 2);
                Canvas.SetTop(image, value.Y - image.Height / 2);
            }
        }

        public double angle
        {
            get
            {
                return rotateTransform.Angle;
            }
            set
            {
                rotateTransform.Angle = value;
            }
        }

        public Fish(string imgName, double width, double height, Canvas canvas)
        {
            _canvas = canvas;
            LoadImage(imgName, width, height);
            timer.Tick += TimerMove;
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Start();

        }


        abstract public void TimerMove(object sender, EventArgs e);

        abstract public void NormalMove();
        
        public void ChasingOrChasedMove()
        {
            Fish preyFishInBusiness = null; double distPreyFishInBusiness = double.MaxValue;
            PreyFishList.NearestFish(position, out preyFishInBusiness, out distPreyFishInBusiness);

            Fish predatorFishInBusiness = null; Vector distPredatorFishInBusiness = new Vector(double.MaxValue, 0);
            PredatorFishList.NearestFish(position, out predatorFishInBusiness, out distPredatorFishInBusiness);

            if (distPredatorFishInBusiness.Length <= distPreyFishInBusiness.Length) { ChasingMove(); }
            else { ChasedMove(); }
        }

        public void ChasingMove(Fish targetFish)
        {

        }

        public void ChasedMove(Fish targetFish)
        {

        }

        public void LoadImage(string png, double width, double height)
        {
            image.Source = new BitmapImage(new Uri(@"/Images/" + png, UriKind.RelativeOrAbsolute));
            image.Width = width; image.Height = height;
            transformGroup.Children.Add(scaleTransfrom);
            transformGroup.Children.Add(rotateTransform);
            image.LayoutTransform = transformGroup;
            _canvas.Children.Add(image);
        }
    }
}
