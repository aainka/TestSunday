using System;
//Q. What is the difference btw System.WindoARTws and System.Drawing?
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Aquarium
{
    abstract public class FishManager
        //Q. Is class SharkFishManager : FishManager better?
    {
        protected Canvas canvas;
        protected Random dice = new Random();

       
        protected int intervalTimeUnit;
        static protected TimeSpan unitTime = TimeSpan.FromMilliseconds(10);

        DispatcherTimer removeTimer = new DispatcherTimer();
        DispatcherTimer createTimer = new DispatcherTimer();
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
            createTimer.Interval= unitTime*intervalTimeUnit;
            createTimer.Tick += TimerCreate; createTimer.Start();
        }

        public void Dispose()
        {timer.Stop(); createTimer.Stop(); removeTimer.Stop();}

        //TimerEvent
        private void TimerRemove(object? sender, EventArgs e)
        {
            if (fishSchool.fishList == null) { return; }
            for (int tempFishIndex = fishSchool.fishList.Count - 1;
                tempFishIndex >= 0; tempFishIndex--)
            {
                if (fishSchool.fishList[tempFishIndex].OutOfCanvas()) 
                {
                    Debug.WriteLine(fishSchool.fishList[tempFishIndex].name+ " Out of Range");
                    fishSchool.fishList[tempFishIndex].Dispose(); FishManager.PrintNumberOfFish();
                    //Q. Is there way I can make class enumerable?
                }
            }
        }
        abstract protected void TimerCreate(object? sender, EventArgs e);
        abstract public void CreateFish(Point position, Vector dirVector);

        //Q. Is default accessor of fields and methods "private"? -> A. Yes

        static public void PrintNumberOfFish()
        {
            Debug.WriteLine("Cod: {0}, Shark: {1}",
                    CodManager.fishSchool.fishList.Count(), SharkManager.fishSchool.fishList.Count());
        }
    }
}
//Q. Is it possible to add a new method or field to an existing class?
//   eg. Image.GetCenter()
