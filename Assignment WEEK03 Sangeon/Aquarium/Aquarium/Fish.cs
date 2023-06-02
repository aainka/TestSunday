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
    abstract public class Fish
    {
        protected Canvas _canvas;
        
        public Image image = new Image();
        public BitmapImage normalImage=new BitmapImage();
        public BitmapImage chasingImage = new BitmapImage();
        public BitmapImage chasedImage = new BitmapImage();
        //Q. Can I redefine these attributes to static attribute in the child classes? 
        
        public Random dice= new Random();
        public static Vector stdVector = new Vector(1,0); 
        
        protected List<FishSchool> PreyFishSchoolList = new List<FishSchool>();
        protected List<FishSchool> PredatorFishSchoolList = new List<FishSchool>();
        protected FishSchool PreyFishSchool = new FishSchool();
        protected FishSchool PredatorFishSchool = new FishSchool();

        private FishSchool _fishSchool;
        protected FishSchool fishSchool
        { get { return _fishSchool; } set { _fishSchool = value; } }


        public double alertRadius;
        public double normalSpeed;  public double chasingAccelation; public double chasedSpeed;

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

        protected double angle
        {get {return Vector.AngleBetween(stdVector, dirVector); }}
        protected double speed
        {get { return dirVector.Length; }}



        //BIRTH AND DEATH
        public Fish(Canvas canvas, Point position, Vector dirVector)
        {
            _canvas = canvas; this.position = position; this.dirVector = dirVector;

            FishManager.timer.Tick += TimerMove;

            Debug.WriteLine("Cod: {0}, Shark: {1}",
                    CodManager.fishSchool.fishList.Count(), SharkManager.fishSchool.fishList.Count());
        }

        protected void LoadImage(string name)
        {
            normalImage = new BitmapImage(new Uri(@"/Images/" + name.ToLower() + "_normal.png", UriKind.RelativeOrAbsolute));
            chasedImage = new BitmapImage(new Uri(@"/Images/" + name.ToLower() + "_chased.png", UriKind.RelativeOrAbsolute));
            chasingImage = new BitmapImage(new Uri(@"/Images/" + name.ToLower() + "_chasing.png", UriKind.RelativeOrAbsolute));
            image.Source = normalImage;
            _canvas.Children.Add(image);
        }
        public void Dispose()
        {
            _canvas.Children.Remove(image);
            FishManager.timer.Tick-= TimerMove;
            fishSchool.fishList.Remove(this);
        }

        //MOVE
        private void TimerMove(object? sender, EventArgs e)
        {
            UpdateFishListGeneral();UpdateFishListSpecific();

            var (preyFishInBusiness, distPreyFishInBusiness)= PreyFishSchool.NearestFish(position);
            var (predatorFishInBusiness, distPredatorFishInBusiness) = PredatorFishSchool.NearestFish(position);

            if (predatorFishInBusiness!=null && ContactWith(predatorFishInBusiness)) 
            {
                Dispose();
                Debug.WriteLine(name + " Eaten");
                Debug.WriteLine("Cod: {0}, Shark: {1}",
                    CodManager.fishSchool.fishList.Count(), SharkManager.fishSchool.fishList.Count());
            }
            if (Min(distPreyFishInBusiness, distPredatorFishInBusiness) > alertRadius) { NormalMove(); }
            else
            {
                if (distPredatorFishInBusiness <= distPreyFishInBusiness) { ChasedMove(predatorFishInBusiness); }
                else { ChasingMove(preyFishInBusiness); }
            }
            
            position = Vector.Add(dirVector, position);
        }

        abstract protected void NormalMove();
        private void ChasedMove(Fish predatorFishInBusiness)
        {
            if (predatorFishInBusiness == null) { return; }

            image.Source = chasedImage;
            Vector displacementVector = Point.Subtract(predatorFishInBusiness.position,position);
            double distance = displacementVector.Length;
            displacementVector.Normalize();
            dirVector=Vector.Multiply(-Min(50, chasedSpeed/ distance), displacementVector);
        }
        private void ChasingMove(Fish preyFishInBusiness)
        {
            if (preyFishInBusiness == null) { return; }

            image.Source= chasingImage;
            Vector displacementVector = Point.Subtract(preyFishInBusiness.position, position);
            Vector displacementNormalizedVector
                = Vector.Multiply(1/displacementVector.Length, displacementVector);
            
            dirVector=Vector.Add(dirVector,Vector.Multiply(chasingAccelation, displacementNormalizedVector));
        }

        //PREY & PREDATOR UPDATE
        private void UpdateFishListGeneral()
        {
            UpdatePreyFishListGeneral();UpdatePredatorFishListGeneral();
        }
        private void UpdatePreyFishListGeneral()
        {
            PreyFishSchool = new FishSchool();
            foreach (FishSchool fishSchool in PreyFishSchoolList)
            {
                if (fishSchool.fishList == null) { continue; }
                foreach (Fish preyFish in fishSchool.fishList)
                {
                    PreyFishSchool.Add(preyFish);
                }
            }
        }
        private void UpdatePredatorFishListGeneral()
        {
            PredatorFishSchool = new FishSchool();
            foreach (FishSchool fishSchool in PredatorFishSchoolList)
            {
                if (fishSchool.fishList == null) { continue; }
                foreach (Fish predatorFish in fishSchool.fishList)
                {
                    PredatorFishSchool.Add(predatorFish);
                }
            }
        }
        private void UpdateFishListSpecific()
        {
            UpdatePreyFishListSpecific(); UpdatePredatorFishListSpecific();
        }
        abstract protected void UpdatePreyFishListSpecific();
        abstract protected void UpdatePredatorFishListSpecific();

        //TOOL
        public bool ContactWith(Fish otherFIsh)//Should be modified considering the rotation
        {
            if(otherFIsh==null) return false;
            
            Vector displacementVector = Point.Subtract(otherFIsh.position, position);
            if (displacementVector.Length<100){ return true; }
            else {  return false; }
        }

        public bool CurrentOutOfCanvas()
        {
            if (
                (position.X + size.Width / 2 < 0) //Out of Left
                || (position.X - size.Width / 2 > _canvas.ActualWidth) //Out of Right
                || (position.Y + size.Height / 2 < 0) //Out of Top
                || (position.Y - size.Height / 2 > _canvas.ActualHeight)//Out of Bottom
               ) { return true; }

            return false;
        }
    }
}
