using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Aquarium
{
    public class SharkManager : FishManager
    {
        public static new FishSchool fishSchool;
        public static new Type fishType = typeof(Shark);
        public SharkManager(Canvas canvas,int intervalTimeUnit)
            : base(canvas, intervalTimeUnit)
        {
            this.canvas = canvas;
        }
        protected override void TimerSpawn(object? sender, EventArgs e)
        {
            double spawnHeight
                =dice.NextDouble()*(canvas.ActualHeight-2*Shark.size.Height)+canvas.ActualHeight;
            switch (dice.Next(2))
            {
                case 0://LEFT
                    
                    break;
                case 1://RIGHT
                    break;
            }
        }
    }
}
