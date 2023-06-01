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
        static protected TimeSpan unitTime = TimeSpan.FromMilliseconds(10);

        private int _intervalTimeUnit;
        public int intervalTimeUnit
        {
            get
            {
                return _intervalTimeUnit;
            }
            set
            {
                spawnTimer.Interval = TimeSpan.FromMilliseconds(value);
                _intervalTimeUnit = value;
            }
        }

        protected Random dice=new Random();

        protected Canvas canvas;

        DispatcherTimer removeTimer = new DispatcherTimer();
        DispatcherTimer spawnTimer = new DispatcherTimer();
        public DispatcherTimer timer = new DispatcherTimer();

        public FishManager(Canvas canvas, int intervalTimeUnit)
        {
            this.canvas = canvas;
            this.intervalTimeUnit = intervalTimeUnit;

            timer.Interval = unitTime;
            removeTimer.Interval = unitTime * 100;
            removeTimer.Tick += TimerRemove; removeTimer.Start();
            spawnTimer.Interval= unitTime*intervalTimeUnit;
            spawnTimer.Tick += TimerSpawn; spawnTimer.Start();
        }


        public void Dispose()
        {timer.Stop(); spawnTimer.Stop(); removeTimer.Stop();}
        private void TimerRemove(object? sender, EventArgs e)
        {
            if (fishSchool == null) { return; }
            foreach (Fish fish in fishSchool)
            {if (fish.OutOfCanvasEntirely()) { fish.Dispose(); }}
        }
        
        abstract protected void TimerSpawn(object? sender, EventArgs e);
        public void Spawn(Point position, Vector dirVector)
        {
            object? fish=System.Activator.CreateInstance(fishType, position, dirVector);
            fishSchool.Add(fish);
        }
        //Q. Is default accessor of fields and methods "private"?
    }
}
//Q. Is it possible to add a new method or field to an existing class?
//   eg. Image.GetCenter()
