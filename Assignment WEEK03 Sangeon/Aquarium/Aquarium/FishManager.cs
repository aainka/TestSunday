using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
//Q. What is the difference btw System.WindoARTws and System.Drawing?
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

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
            removeTimer.Interval = unitTime * 50;
            removeTimer.Tick += TimerRemove; removeTimer.Start();
            spawnTimer.Interval= unitTime*intervalTimeUnit;
            spawnTimer.Tick += TimerSpawn; spawnTimer.Start();
        }

        public void Dispose()
        {timer.Stop(); spawnTimer.Stop(); removeTimer.Stop();}

        //TimerEvent
        private void TimerRemove(object? sender, EventArgs e)
        {
            if (fishSchool.fishList == null) { return; }
            for (int tempFishIndex = fishSchool.fishList.Count - 1;
                tempFishIndex >= 0; tempFishIndex--)
            {
                if (fishSchool.fishList[tempFishIndex].CurrentOutOfCanvas()) 
                {
                    Debug.WriteLine(fishSchool.fishList[tempFishIndex].name
                        + " Out of Range");
                    fishSchool.fishList[tempFishIndex].Dispose(); 
                }
            }
        }
        abstract protected void TimerSpawn(object? sender, EventArgs e);
        abstract public void Spawn(Point position, Vector dirVector);

        //Q. Is default accessor of fields and methods "private"?
    }
}
//Q. Is it possible to add a new method or field to an existing class?
//   eg. Image.GetCenter()
