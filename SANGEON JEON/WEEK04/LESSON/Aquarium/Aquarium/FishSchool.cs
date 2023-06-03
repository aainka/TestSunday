using System;
using System.Collections.Generic;
using System.Windows;

namespace Aquarium
{
    public class FishSchool
    {
        public List<Fish>? fishList = new List<Fish>();

        public FishSchool()
        {
            ;
        }

        public FishSchool(List<Fish> fishlist)
        {
            this.fishList = fishlist;
        }

        public (Fish?, double) NearestFish(Point position)
        {
            if (fishList == null) { return (null, Double.MaxValue); }

            Fish minFish = null; double minDist = double.MaxValue;
            double tempDist = 0;
            foreach (Fish tempfish in fishList)
            {
                tempDist = Point.Subtract(tempfish.position, position).Length;
                if (tempDist < minDist) { minDist = tempDist; minFish = tempfish; }
            }
            return (minFish, minDist);
        }

        public void Add(Fish fish)
        {
            if (fishList == null) { fishList = new List<Fish>() { fish }; }
            else { fishList.Add(fish); }
        }

        public void Add(FishSchool fishSchool)
        { fishSchool.fishList.ForEach(fish => fishList.Add(fish)); }
    }

    public class FishSet : List<Fish>
    {
        public (Fish?, double) NearestFish(Fish me)
        {
            Fish minFish = null;
            double minDist = double.MaxValue;
            double tempDist = 0;
            foreach (Fish tempfish in this)
            {
                var distance = tempfish.Distance(me);
                if (distance < minDist)
                {
                    minFish = tempfish;
                    minDist = distance;
                }
            }
            return (minFish, minDist);
        }


        public void Add(FishSet fishSchool)
        { 
            fishSchool.ForEach(fish => this.Add(fish)); 
        }
    }
}
