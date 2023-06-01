using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Aquarium
{
    class Shark : Fish
    {
        public static FishSchool SharkSchool=new FishSchool();

        public static new Size size = new Size();

        public Shark(Canvas canvas, Point position, Vector dirVector)
            : base(canvas, position, dirVector)
        {
            SharkSchool.Add(this);
            PreyFishSchoolList.Add(Cod.CodSchool);
            base.size = size;
        }

        public override void NormalMove()
        {
            throw new NotImplementedException();

        }

        
    }
}
