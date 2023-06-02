using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Aquarium
{
    public class CodManager: FishManager
    {
        public static new FishSchool fishSchool=new FishSchool();

        public CodManager(Canvas canvas, int intervalTimeUnit)
            : base(canvas, intervalTimeUnit) 
        {base.fishSchool=fishSchool;}

        protected override void TimerSpawn(object? sender, EventArgs e){; }
        public override void Spawn(Point position, Vector dirVector)
        { fishSchool.Add(new Cod(canvas, position, dirVector)); FishManager.PrintNumberOfFish(); }
    }
}
