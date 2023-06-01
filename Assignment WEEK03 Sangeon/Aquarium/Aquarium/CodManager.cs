using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Aquarium
{
    public class CodManager: FishManager
    {
        public static new FishSchool fishSchool;
        public static new Type fishType=typeof(Cod);
        public CodManager(Canvas canvas,  int intervalTimeUnit)
            : base(canvas, intervalTimeUnit)
        {
            this.canvas = canvas;
            this.intervalTimeUnit = intervalTimeUnit;
            base.fishType = fishType;
        }

        protected override void TimerSpawn(object? sender, EventArgs e){;}
    }
}
