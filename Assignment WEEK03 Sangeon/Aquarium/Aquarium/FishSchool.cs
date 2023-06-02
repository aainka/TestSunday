using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using System.Diagnostics;

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
            if (fishList == null) { return (null, Double.MaxValue);}

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
        {
            fishSchool.fishList.ForEach(fish => fishList.Add(fish));
        }


        ///=====================================================================================
        //public static FishSchool operator +(FishSchool fishschool, Fish fish)
        //{
        //    fishschool.fishlist.Add(fish);
        //    return fishschool;
        //}

        //public static FishSchool operator +(FishSchool fishschool1, FishSchool fishschool2)
        //{
        //    fishschool2.fishlist.ForEach(fish => fishschool1.fishlist.Add(fish));
        //    return fishschool1;
        //}
    }
}
