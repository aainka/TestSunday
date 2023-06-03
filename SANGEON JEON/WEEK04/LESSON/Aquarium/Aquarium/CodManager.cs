using System;
using System.Windows;
using System.Windows.Controls;

namespace Aquarium
{
    public class CodManager: FishManager
    {
        public static new FishSchool fishSchool=new FishSchool();

        public CodManager(Canvas canvas, int intervalTimeUnit)
            : base(canvas, intervalTimeUnit) 
        {base.fishSchool=fishSchool;}

        protected override void TimerCreate(object? sender, EventArgs e){; }
        public override void CreateFish(Point position, Vector dirVector)
        { fishSchool.Add(new Cod(canvas, position, dirVector)); FishManager.PrintNumberOfFish(); }
    }
}
