using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Aquarium
{
    
    class FishSchool
    {
        List<Fish> fishlist = null;

        public FishSchool()
        {
            ;
        }

        public FishSchool(List<Fish> fishlist)
        {
            this.fishlist = fishlist;
        }

        public void NearestFish(Point position, out Fish minFish, out double minDist)
        {
            if (fishlist != null)
            {
                double dist = double.MaxValue; Fish fish = null;
                double tempDist = 0;
                foreach (Fish tempfish in fishlist)
                {
                    tempDist = Sqrt(Pow(position.X - tempfish.position.X, 2) + Pow(position.Y - tempfish.position.Y, 2));
                    if (tempDist < dist) { dist = tempDist; fish = tempfish; }
                }
                minFish = fish; minDist = dist;
            }
            else { minFish = null; minDist = 0; }
            
        }

        public void Add(Fish fish)
        {
            fishlist.Add(fish);
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
