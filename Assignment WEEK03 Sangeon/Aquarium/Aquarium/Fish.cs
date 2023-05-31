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
        public Image normalImage = new Image(); public Image chasingImage=new Image(); public Image chasedImage=new Image();
        //Q. Can I redefine these attributes to static attribute in the child classes? 
        public Random dice= new Random();
        public static Vector stdVector = new Vector(0, 1); 
        

        DispatcherTimer timer = new DispatcherTimer();

        public List<FishSchool> PreyFishSchoolList = new List<FishSchool>();
        public List<FishSchool> PredatorFishSchoolList = new List<FishSchool>();
        public FishSchool PreyFishList = new FishSchool();
        public FishSchool PredatorFishList = new FishSchool();


        public double alertRadius; public double alertChasingSpeed; public double alertChasedSpeed;

        private Size _size;
        public Size size
        {
            get {return _size;}
            set {image.Width = value.Width; image.Height = value.Height; _size = value; }
        }


        //About Movement-------------------------------------------

        //Image Rotation

        private Point _position;
        public Point position
        {
            get
            {
                return _position;
            }
            set
            {
                Canvas.SetLeft(image, value.X - image.Width / 2);
                Canvas.SetTop(image, value.Y - image.Height / 2);
                _position = value;
            }
        }

        private Vector _dirVector;
        public Vector dirVector
        {
            get
            {
                return _dirVector;
            }
            set
            {
                double angle = Vector.AngleBetween(stdVector, value);
                TransformGroup tfGroup = new TransformGroup();
                ScaleTransform scaleTf = (angle>0)? new ScaleTransform(1, 1):new ScaleTransform(-1,1);
                RotateTransform rotateTf = new RotateTransform(angle);
                tfGroup.Children.Add(scaleTf); tfGroup.Children.Add(rotateTf);
                image.LayoutTransform = tfGroup;
                _dirVector = value;
            }
        }

        public Fish(string imgName, Canvas canvas, Point position, Vector dirVector)
        {
            _canvas = canvas;
            image.Source = new BitmapImage(new Uri(@"/Images/" + imgName, UriKind.RelativeOrAbsolute));

            this.position = position; this.dirVector = dirVector;

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

        public bool ContactWith(Fish otherFIsh)//Should be modified considering the rotation
        {
            Vector displacementVector = Point.Subtract(otherFIsh.position, position);
            if (displacementVector.X < (size.Width + otherFIsh.size.Width) / 2
                && displacementVector.Y < (size.Height + otherFIsh.size.Height) / 2)
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
