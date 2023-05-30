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
        public static Vector stdVec = new Vector(0, 1); 

        //public enum FishKind
        //{None, Shark, Cod }

        //public static Dictionary<FishKind, (int, int)> FishSize = new Dictionary<FishKind, (int, int)> 
        //{
        //    { FishKind.None, (0,0)},
        //    { FishKind.Shark,(150, 100)},
        //    { FishKind.Cod, (100, 100) }
        //};

        

        DispatcherTimer timer = new DispatcherTimer();

        List<FishSchool> PreyFishSchoolList=null; List<FishSchool> PredatorFishSchoolList = null;
        FishSchool PreyFishList = null; FishSchool PredatorFishList = null;


        public double alertRadius;

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

        private Fish preyFishInBusiness
        {
            get
            {
                return PreyFishList.NearestFish(position, out preyFishInBusiness, out distPreyFishInBusiness);
            }
        }


        //About Movement-------------------------------------------
        public Point position
        {
            get
            {
                return new Point(
                    (int)(Canvas.GetLeft(image) + image.Width / 2),
                    (int)(Canvas.GetTop(image) + image.Height / 2)
                );
            }
            set
            {
                Canvas.SetLeft(image, value.X - image.Width / 2);
                Canvas.SetTop(image, value.Y - image.Height / 2);
            }
        }

        private double angle;
        private double speed;

        //Image Rotation
        private RotateTransform rotateTransform = new RotateTransform();
        private ScaleTransform scaleTransfrom = new ScaleTransform();
        private TransformGroup transformGroup = new TransformGroup();

        public Vector dirVec
        {
            get
            {
                return dirVec;
            }
            set
            {
                angle = Vector.AngleBetween(stdVec,value);
                speed = value.Length;
                rotateTransform = new RotateTransform(angle);
                scaleTransfrom = new ScaleTransform(speed,speed);
                dirVec = value;
            }
        }

        public Fish(string imgName, (double,double) size, Point position, Vector dirVector, Canvas canvas)
        {
            _canvas = canvas;
            image.Source = new BitmapImage(new Uri(@"/Images/" + imgName, UriKind.RelativeOrAbsolute));

            this.size = size; this.position = position; this.dirVec = dirVec;

            transformGroup.Children.Add(scaleTransfrom); transformGroup.Children.Add(rotateTransform);
            image.LayoutTransform = transformGroup;

            timer.Tick += TimerMove;
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Start();

        }

        private void TimerMove(object sender, EventArgs e)
        {
            var (preyFishInBusiness, distPreyFishInBusiness)= PreyFishList.NearestFish(position);
            var (predatorFishInBusiness, distPredatorFishInBusiness) = PredatorFishList.NearestFish(position);

            if 

            if (distPredatorFishInBusiness <= distPreyFishInBusiness) { ChasedMove(predatorFishInBusiness); }
            else { ChasedMove(preyFishInBusiness); }
        }

        abstract public void NormalMove();
        
        public void ChasingMove(Fish preyFish)
        {
               
        }

        public void ChasedMove(Fish predatorFish)
        {

        }

    }
}
