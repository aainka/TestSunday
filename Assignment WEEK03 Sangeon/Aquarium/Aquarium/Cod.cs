using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Aquarium
{

    class Cod : Fish
    {
        public static FishSchool CodSchool = new FishSchool();
        public Cod(string imgName, Canvas canvas) : base(imgName, canvas)
        {
            CodSchool.Add(this);
        }



        public override void NormalMove()
        {
            throw new NotImplementedException();
        }

        public override void TimerMove(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
