using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Configuration;
using System.Diagnostics;

namespace Aquarium
{

    class Cod : Fish
    {
        public readonly static new Size size = new Size(100, 50);
        public readonly static new string name="Cod";

        public Cod(Canvas canvas, Point position, Vector dirVector) 
            : base(canvas, position, dirVector)
        {
            LoadImage(Cod.name);

            PredatorFishSchoolList.Add(SharkManager.fishSchool);
            base.size = Cod.size; base.name = Cod.name;
            (normalSpeed,chasedAccelation,chasingAccelation) = (5,1,1);
            (chasingSpeedLimit, chasedSpeedLimit) = (10,10); alertRadius = 350;
            fishSchool = CodManager.fishSchool;
        }

        protected override void NormalMove()
        {
            image.Source = normalImage;
             
            double nextAngle=angle+ dice.NextDouble()*10-5;
            double nextSpeed = normalSpeed + dice.NextDouble() * 3-1.5;

            Matrix tf = Matrix.Identity; tf.Scale(nextSpeed, nextSpeed); tf.Rotate(nextAngle);
            dirVector = tf.Transform(stdVector);

            if (NextMoveOutOfCanvas()) { dirVector = -dirVector; }
        }

        protected override void UpdatePreyFishListSpecific() {; }
        protected override void UpdatePredatorFishListSpecific() {; }

        bool NextMoveOutOfCanvas()
        {
            Point nextPosition = Point.Add(position, dirVector);
            if (
                (nextPosition.X + size.Width / 2 < 0) //Out of Left
                || (nextPosition.X - size.Width / 2 > _canvas.ActualWidth) //Out of Right
                || (nextPosition.Y + size.Height / 2 < 0) //Out of Top
                || (nextPosition.Y - size.Height / 2 > _canvas.ActualHeight)//Out of Bottom
               ) { return true; }

            return false;
        }

    }
}
