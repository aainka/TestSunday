using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Configuration;

namespace Aquarium
{

    class Cod : Fish
    {
        public static FishSchool CodSchool = new FishSchool();
        public static new Size size = new Size(100, 50);
        static readonly Image normalImage;

        

        public Cod(string imgName, Canvas canvas, Point position, Vector dirVector) 
            : base(imgName, canvas, position, dirVector)
        {
            CodSchool.Add(this);
            PredatorFishSchoolList.Add(Shark.SharkSchool);
            base.size = size;
        }


        public override void NormalMove()
        {
            double angle=dice.NextDouble()*365;
            double speed = dice.NextDouble() * 15 + 10;

            Matrix tf = Matrix.Identity;tf.Rotate(angle);tf.Scale(speed, speed);
            dirVector = tf.Transform(stdVector);

            if (OutOfCanvas(Point.Add(position, dirVector))) { NormalMove(); }
        }

        private bool OutOfCanvas(Point position)
        {
            if (
                (position.X - size.Width / 2 < 0) //Out of Left
                || (position.X + size.Width / 2 > _canvas.ActualWidth) //Out of Right
                || (position.Y - size.Height / 2 < 0) //Out of Top
                || (position.Y + size.Height / 2 > _canvas.ActualHeight)
               ) { return true; }

            return false;
        }


    }
}
