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
using System.Security.Cryptography.X509Certificates;
using System.Net.NetworkInformation;

namespace Aquarium
{
    
   

    public abstract class Fish
    {
        public Canvas? _canvas = null;
        public Image image = new Image();
        public Random dice= new Random();
        public static Vector stdVector = new Vector(0, 1); 

        //public enum FishKind
        //{None, Shark, Cod }

        //public static Dictionary<FishKind, (int, int)> FishSize = new Dictionary<FishKind, (int, int)> 
        //{
        //    { FishKind.None, (0,0)},
        //    { FishKind.Shark,(150, 100)},
        //    { FishKind.Cod, (100, 100) }
        //};

        

        DispatcherTimer timer = new DispatcherTimer();

        public List<FishSchool> PreyFishSchoolList = null;
        public List<FishSchool> PredatorFishSchoolList = null;
        public FishSchool PreyFishList = null;
        public FishSchool PredatorFishList = null;


        public double alertRadius; public double alertChasingSpeed; public double alertChasedSpeed;

        public (double,double) size
        {
            get
            {
                return size;
            }
            set
            {
                image.Width = value.Item1; image.Height = value.Item2;
                size = value;
            }
        }


        //About Movement-------------------------------------------

        //Image Rotation

        public Point position
        {
            get
            {
                return position;
            }
            set
            {
                Canvas.SetLeft(image, value.X - image.Width / 2);
                Canvas.SetTop(image, value.Y - image.Height / 2);
                position = value;
            }
        }

        public Vector dirVector
        {
            get
            {
                return dirVector;
            }
            set
            {
                double angle = Vector.AngleBetween(stdVector, value);
                TransformGroup tfGroup = new TransformGroup();
                ScaleTransform scaleTf = new ScaleTransform(1, 1);
                RotateTransform rotateTf = new RotateTransform(angle);
                


                tfGroup.Children.Add(scaleTf); tfGroup.Children.Add(rotateTf);
                image.LayoutTransform = tfGroup;
                dirVector = value;
            }
        }

        public Fish(string imgName, Canvas canvas, (double,double) size, Point position, Vector dirVector)
        {
            _canvas = canvas;
            image.Source = new BitmapImage(new Uri(@"/Images/" + imgName, UriKind.RelativeOrAbsolute));

            this.size = size; this.position = position; this.dirVector = dirVector;

            timer.Tick += TimerMove;
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Start();

        }


        private void TimerMove(object sender, EventArgs e)
        {
            var (preyFishInBusiness, distPreyFishInBusiness)= PreyFishList.NearestFish(position);
            var (predatorFishInBusiness, distPredatorFishInBusiness) = PredatorFishList.NearestFish(position);

            if (ContactWith(predatorFishInBusiness)) { Dispose(); }
            if (Min(distPreyFishInBusiness, distPredatorFishInBusiness) > alertRadius) { NormalMove(); }
            else
            {
                if (distPredatorFishInBusiness <= distPreyFishInBusiness) { ChasedMove(predatorFishInBusiness); }
                else { ChasedMove(preyFishInBusiness); }
            }
            
            position = Vector.Add(dirVector, position);
        }

        abstract public void NormalMove();
        

        public void ChasingMove(Fish preyFish)
        {
               
        }

        public void ChasedMove(Fish predatorFish)
        {

        }

        public bool ContactWith(Fish fishInBusiness)
        {
            Vector displacementVector = Point.Subtract(fishInBusiness.position, position);
            if (displacementVector.X < (size.Item1 + fishInBusiness.size.Item1) / 2
                && displacementVector.Y < (size.Item2 + fishInBusiness.size.Item2) / 2)
            { return true; }
            return false;
        }

        public void Dispose()
        {
            timer.Stop();
            _canvas.Children.Remove(image);
        }

    }
}
