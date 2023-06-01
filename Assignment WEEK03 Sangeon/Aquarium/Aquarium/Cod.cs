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
        public static FishSchool CodSchool = new FishSchool();
        public readonly static new Size size = new Size(100, 50);
        public readonly static new string name="Cod";

        public Cod(Canvas canvas, Point position, Vector dirVector) 
            : base(canvas, position, dirVector)
        {
            LoadImage(Cod.name);

            CodSchool.Add(this);
            PredatorFishSchoolList.Add(Shark.SharkSchool);
            base.size = Cod.size; base.name = Cod.name;
            (normalSpeed,ChasedSpeed,ChasingSpeed) = (5,50,25);

            timer.Start();
        }

        protected override void NormalMove()
        {
            double nextAngle=angle+ dice.NextDouble()*10-5;
            double nextSpeed = normalSpeed + dice.NextDouble() * 3-1.5;

            Matrix tf = Matrix.Identity; tf.Scale(nextSpeed, nextSpeed); tf.Rotate(nextAngle);
            dirVector = tf.Transform(stdVector);

            if (OutOfCanvasPartial()) { dirVector = -dirVector; }
        }

        protected override void UpdatePreyFishListSpecific() {; }
        protected override void UpdatePredatorFishListSpecific() {; }

    }
}
