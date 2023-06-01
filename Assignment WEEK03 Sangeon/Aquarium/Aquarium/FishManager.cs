using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
//Q. What is the difference btw System.Windows and System.Drawing?
using static System.Net.Mime.MediaTypeNames;

namespace Aquarium
{
    abstract public class FishManager
        //Q. Is class SharkFishManager : FishManager better?
    {
        protected Canvas canvas;
        protected Random dice = new Random();

        static protected TimeSpan unitTime = TimeSpan.FromMilliseconds(10);
        protected int intervalTimeUnit;

        DispatcherTimer removeTimer = new DispatcherTimer();
        DispatcherTimer spawnTimer = new DispatcherTimer();
        public static DispatcherTimer timer = new DispatcherTimer();

        FishSchool _fishSchool;
        public FishSchool fishSchool
        {get { return _fishSchool; }set { _fishSchool = value; }}

        public FishManager(Canvas canvas, int intervalTimeUnit)
        {
            this.canvas = canvas;
            this.intervalTimeUnit = intervalTimeUnit;

            FishManager.timer.Interval = unitTime; timer.Start();
            removeTimer.Interval = unitTime * 100;
            removeTimer.Tick += TimerRemove; removeTimer.Start();
            spawnTimer.Interval= unitTime*intervalTimeUnit;
            spawnTimer.Tick += TimerSpawn; spawnTimer.Start();
        }

        public void Dispose()
        {timer.Stop(); spawnTimer.Stop(); removeTimer.Stop();}

        //TimerEvent
        private void TimerRemove(object? sender, EventArgs e)
        {
            if (fishSchool.fishlist == null) { return; }
            foreach (Fish fish in fishSchool.fishlist)
            {if (fish.OutOfCanvasEntirely()) { fish.Dispose(); }}
        }
        abstract protected void TimerSpawn(object? sender, EventArgs e);
        abstract public void Spawn(Point position, Vector dirVector);
        //{
        //    object? fish=System.Activator.CreateInstance(fishType, position, dirVector);
        //    fishSchool.Add(fish);
        //}
        //Q. Is default accessor of fields and methods "private"?
    }
}
//Q. Is it possible to add a new method or field to an existing class?
//   eg. Image.GetCenter()
