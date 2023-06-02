using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Diagnostics;

namespace Aquarium
{
    class Shark : Fish
    {
        public readonly static new Size size = new Size(300, 90);
        public readonly static new string name = "Shark";

        //Direction
        private enum Direction { LEFT, RIGHT }
        private Direction direction;

        public Shark(Canvas canvas, Point position, Vector dirVector)
            : base(canvas, position, dirVector)
        {
            LoadImage(Shark.name);

            PreyFishSchoolList.Add(CodManager.fishSchool);
            base.size = Shark.size; base.name = Shark.name;
            (normalSpeed, chasedAccelation, chasingAccelation) = (5, 1, 0.5);
            (chasingSpeedLimit, chasedSpeedLimit) = (15, 15); alertRadius = 300;
            fishSchool = SharkManager.fishSchool;

            //Only Shark
            direction = (angle < 90 && angle > -90) ? Direction.RIGHT : Direction.LEFT;
        }

        protected override void NormalMove()
        {
            image.Source = normalImage;
            Vector accelerationVector = new Vector();Vector targetDirVector = new Vector();
            switch (direction)
            {
                case Direction.RIGHT:
                    targetDirVector = Vector.Multiply(normalSpeed, Fish.stdVector); 
                    accelerationVector = Vector.Subtract(targetDirVector,dirVector);
                    dirVector = Vector.Add(dirVector,Vector.Multiply(0.2,accelerationVector));
                    break;
                case Direction.LEFT:
                    targetDirVector = Vector.Multiply(-normalSpeed, Fish.stdVector);
                    accelerationVector = Vector.Subtract(targetDirVector, dirVector);
                    dirVector = Vector.Add(dirVector, Vector.Multiply(0.2, accelerationVector));
                    break;
            }
        }

        protected override void UpdatePreyFishListSpecific()
        {
            if (PreyFishSchool.fishList==null) { return; }
            Direction _direction = (angle < 90 && angle > -90) ? Direction.RIGHT : Direction.LEFT;
            //foreach (Fish preyFish in PreyFishSchool.fishList) 
            //A. Never modify the number of the list within for statement
            for (int tempPreyFishIndex=PreyFishSchool.fishList.Count-1; 
                tempPreyFishIndex>=0; tempPreyFishIndex--)
            {
                Fish tempPreyFish = PreyFishSchool.fishList[tempPreyFishIndex];
                if ((_direction == Direction.RIGHT && tempPreyFish.position.X < position.X)
                    ||(_direction == Direction.LEFT && tempPreyFish.position.X > position.X))
                { PreyFishSchool.fishList.Remove(tempPreyFish); }
            }
        }
        protected override void UpdatePredatorFishListSpecific()
        {
            if (PredatorFishSchool.fishList == null) { return; }
            Direction _direction = (angle < 90 && angle > -90) ? Direction.RIGHT : Direction.LEFT;

            for (int tempPredatorFishIndex = PredatorFishSchool.fishList.Count - 1;
                tempPredatorFishIndex >= 0; tempPredatorFishIndex--)
            {
                Fish tempPredatorFish = PredatorFishSchool.fishList[tempPredatorFishIndex];
                if ((_direction == Direction.RIGHT && tempPredatorFish.position.X < position.X)
                    || (_direction == Direction.LEFT && tempPredatorFish.position.X > position.X))
                { PredatorFishSchool.fishList.Remove(tempPredatorFish); }
            }
        }

    }
}
