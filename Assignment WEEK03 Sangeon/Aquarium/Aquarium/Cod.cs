using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Aquarium
{

    class Cod : Fish
    {
        public static FishSchool CodSchool = new FishSchool();

        public Cod(string imgName, Canvas canvas, (double, double) size, Point position, Vector dirVector) 
            : base(imgName, canvas, size, position, dirVector)
        {
            
            CodSchool.Add(this);
            PredatorFishSchoolList.Add(Shark.SharkSchool);
            //fishkind = FishKind.Cod;
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
                (position.X - size.Item1 / 2 < 0) //Out of Left
                || (position.X + size.Item1 / 2 > _canvas.ActualWidth) //Out of Right
                || (position.Y - size.Item2 / 2 < 0) //Out of Top
                || (position.Y + size.Item2 / 2 > _canvas.ActualHeight)
               ) { return true; }

            return false;
        }


    }
}
