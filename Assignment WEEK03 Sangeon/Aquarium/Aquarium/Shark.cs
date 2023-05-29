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
        public static List<Shark> SharkList = new List<Shark>();
        public Shark(string imgName, Canvas canvas) : base(imgName, canvas)
        {
            AnchovyList.Add(this);
        }

        public override Fish FishInMyBusiness(List<Fish> fishlist)
        {
            throw new NotImplementedException();
        }

        public override void TimerMove(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
