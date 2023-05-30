using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Aquarium
{
    class Shark : Fish
    {
        public static FishSchool SharkSchool=new FishSchool();

        public Shark(string imgName, Canvas canvas) : base(imgName, canvas)
        {
            SharkSchool.Add(this);
        }

        public override void NormalMove()
        {
            throw new NotImplementedException();
        }

        
    }
}
