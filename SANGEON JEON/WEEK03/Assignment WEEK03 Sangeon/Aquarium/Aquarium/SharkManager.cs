using System;
using System.Windows;
using System.Windows.Controls;

namespace Aquarium
{
    public class SharkManager : FishManager
    {
        public static new FishSchool fishSchool=new FishSchool();

        public SharkManager(Canvas canvas,int intervalTimeUnit)
            : base(canvas, intervalTimeUnit) { base.fishSchool = fishSchool; }

        protected override void TimerSpawn(object? sender, EventArgs e)
        {
            double spawnY
                =dice.NextDouble()*(canvas.ActualHeight-2*Shark.size.Height)+ Shark.size.Height;
            double spawnX;
            switch (dice.Next(2))
            {
                case 0://LEFT
                    spawnX= -Shark.size.Width/2;
                    Spawn(new Point(spawnX, spawnY), Fish.stdVector);
                    break;
                case 1://RIGHT
                    spawnX = canvas.ActualWidth+Shark.size.Width / 2;
                    Spawn(new Point(spawnX, spawnY), Vector.Multiply(-1,Fish.stdVector));
                    break;
            }
        }
        public override void Spawn(Point position, Vector dirVector)
        { fishSchool.Add(new Shark(canvas, position, dirVector)); FishManager.PrintNumberOfFish(); }
    }
}
