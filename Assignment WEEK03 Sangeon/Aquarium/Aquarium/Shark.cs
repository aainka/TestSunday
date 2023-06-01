using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Aquarium
{
    class Shark : Fish
    {
        public static FishSchool SharkSchool = new FishSchool();
        public readonly static new Size size = new Size(300, 90);
        public readonly static new string name = "Shark";

        //Direction
        private enum Direction { LEFT, RIGHT }
        private Direction direction;

        public Shark(Canvas canvas, Point position, Vector dirVector)
            : base(canvas, position, dirVector)
        {
            LoadImage(Shark.name);

            SharkSchool.Add(this);
            PreyFishSchoolList.Add(Shark.SharkSchool);
            base.size = Shark.size; base.name = Shark.name;
            (normalSpeed, ChasedSpeed, ChasingSpeed) = (10, 100, 50);
            
            //Only Shark
            direction = (angle < 90 && angle > -90) ? Direction.RIGHT : Direction.LEFT;

            timer.Start();
        }

        protected override void NormalMove()
        {
            switch (direction)
            {
                case Direction.LEFT:
                    dirVector = Vector.Multiply(normalSpeed, Fish.stdVector);break;
                case Direction.RIGHT:
                    dirVector = Vector.Multiply(-normalSpeed, Fish.stdVector); break;
            }
        }

        protected override void UpdatePreyFishListSpecific()
        {
            if (PreyFishSchool==null) { return; }

            Direction _direction = (angle < 90 && angle > -90) ? Direction.RIGHT : Direction.LEFT;
            foreach (Fish preyFish in PreyFishSchool.fishlist)
            {
                if ((_direction == Direction.RIGHT && preyFish.position.X < position.X)
                    ||(_direction == Direction.LEFT && preyFish.position.X > position.X))
                { PreyFishSchool.fishlist.Remove(preyFish); }
            }
        }
        protected override void UpdatePredatorFishListSpecific()
        {
            if (PredatorFishSchool == null) { return; }

            Direction _direction = (angle < 90 && angle > -90) ? Direction.RIGHT : Direction.LEFT;
            foreach (Fish predatorFish in PredatorFishSchool.fishlist)
            {
                if ((_direction == Direction.RIGHT && predatorFish.position.X < position.X)
                    || (_direction == Direction.LEFT && predatorFish.position.X > position.X))
                { PredatorFishSchool.fishlist.Remove(predatorFish); }
            }
        }

    }
}
