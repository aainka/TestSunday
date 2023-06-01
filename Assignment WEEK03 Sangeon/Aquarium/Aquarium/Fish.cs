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
using System.Diagnostics;

namespace Aquarium
{
   

    public abstract class Fish
    {
        public Canvas? canvas;
        
        public Image image = new Image();
        public BitmapImage normalImage=new BitmapImage();
        public BitmapImage chasingImage = new BitmapImage();
        public BitmapImage chasedImage = new BitmapImage();
        //Q. Can I redefine these attributes to static attribute in the child classes? 
        
        public Random dice= new Random();
        public static Vector stdVector = new Vector(1,0); 
        

        protected DispatcherTimer timer = new DispatcherTimer();

        public List<FishSchool> PreyFishSchoolList = new List<FishSchool>();
        public List<FishSchool> PredatorFishSchoolList = new List<FishSchool>();
        public FishSchool PreyFishList = new FishSchool();
        public FishSchool PredatorFishList = new FishSchool();


        public double alertRadius;
        public double normalSpeed;  public double ChasingSpeed; public double ChasedSpeed;

        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }


        private Size _size;
        public Size size
        {
            get {return _size;}
            set {image.Width = value.Width; image.Height = value.Height; _size = value; }
        }

        private Point _position;
        public Point position
        {
            get
            {
                return _position;
            }
            set
            {
                Canvas.SetLeft(image, value.X - size.Width / 2);
                Canvas.SetTop(image, value.Y - size.Height / 2);
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
                ScaleTransform scaleTf = (angle<90 && angle>-90)? new ScaleTransform(1, 1):new ScaleTransform(1,-1);
                RotateTransform rotateTf = new RotateTransform(angle);
                tfGroup.Children.Add(scaleTf); tfGroup.Children.Add(rotateTf);
                image.LayoutTransform = tfGroup;
                _dirVector = value;
            }
        }

        public double angle
        {get {return Vector.AngleBetween(stdVector, dirVector); }}

        public double speed
        {get { return dirVector.Length; }}



        //BIRTH AND DEATH
        public Fish(Canvas canvas, Point position, Vector dirVector)
        {
            this.canvas = canvas;this.position = position; this.dirVector = dirVector;

            timer.Tick += TimerMove; timer.Interval = TimeSpan.FromMilliseconds(10);
        }

        protected void LoadImage(string name)
        {
            normalImage = new BitmapImage(new Uri(@"/Images/" + name + "_normal.png", UriKind.RelativeOrAbsolute));
            chasedImage = new BitmapImage(new Uri(@"/Images/" + name + "_chased.png", UriKind.RelativeOrAbsolute));
            chasingImage = new BitmapImage(new Uri(@"/Images/" + name + "_chasing.png", UriKind.RelativeOrAbsolute));
            image.Source = normalImage;
            canvas.Children.Add(image);
        }

        public void Dispose()
        {
            timer.Stop();
            canvas.Children.Remove(image);
        }



        //MOVE
        private void TimerMove(object sender, EventArgs e)
        {
            var (preyFishInBusiness, distPreyFishInBusiness)= PreyFishList.NearestFish(position);
            var (predatorFishInBusiness, distPredatorFishInBusiness) = PredatorFishList.NearestFish(position);

            if (predatorFishInBusiness!=null && ContactWith(predatorFishInBusiness)) { Dispose(); }
            if (Min(distPreyFishInBusiness, distPredatorFishInBusiness) > alertRadius) { NormalMove(); }
            else
            {
                if (distPredatorFishInBusiness <= distPreyFishInBusiness) { ChasedMove(predatorFishInBusiness); }
                else { ChasedMove(preyFishInBusiness); }
            }
            
            position = Vector.Add(dirVector, position);
        }

        abstract public void NormalMove();

        public void ChasingMove(Fish preyFishInBusiness)
        {
            if (preyFishInBusiness == null) { return; }

            image.Source = chasingImage;
            Vector displacementVector = Point.Subtract(preyFishInBusiness.position,position);
            double distance = displacementVector.Length;
            displacementVector.Normalize();
            dirVector=Vector.Multiply(-Min(50,ChasingSpeed/distance), displacementVector);
        }

        public void ChasedMove(Fish predatorFishInBusiness)
        {
            if (predatorFishInBusiness == null) { return; }

            image.Source= chasingImage;
            Vector displacementVector = Point.Subtract(predatorFishInBusiness.position, position);
            displacementVector.Normalize();

            dirVector=Vector.Multiply(ChasedSpeed,displacementVector);
        }


        //TOOL
        public bool ContactWith(Fish otherFIsh)//Should be modified considering the rotation
        {
            if(otherFIsh==null) return false;
            
            Vector displacementVector = Point.Subtract(otherFIsh.position, position);
            if (displacementVector.X < (size.Width + otherFIsh.size.Width) / 2
                && displacementVector.Y < (size.Height + otherFIsh.size.Height) / 2)
            { return true; }
            else {  return false; }
        }
    }
}
