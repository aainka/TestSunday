using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Aquarium
{
    public class FishSchool
    {
        public List<Fish>? fishlist = null;

        public FishSchool()
        {
            ;
        }

        public FishSchool(List<Fish> fishlist)
        {
            this.fishlist = fishlist;
        }

        public (Fish?, double) NearestFish(Point position)
        {
            if (fishlist == null) { return (null, Double.MaxValue); }

            Fish minFish = null; double minDist = double.MaxValue;
            double dist = double.MaxValue; Fish fish = null;
            double tempDist = 0;
            foreach (Fish tempfish in fishlist)
            {
                tempDist = Point.Subtract(tempfish.position, position).Length;
                if (tempDist < dist) { dist = tempDist; fish = tempfish; }
            }
            return (minFish, minDist);
        }

        public void Add(Fish fish)
        {
            if (fishlist == null) { fishlist = new List<Fish>() { fish }; }
            else { fishlist.Add(fish); }
        }

        public void Add(FishSchool fishSchool)
        {
            fishSchool.fishlist.ForEach(fish => fishlist.Add(fish));
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
