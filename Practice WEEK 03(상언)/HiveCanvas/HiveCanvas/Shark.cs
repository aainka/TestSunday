using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiveCanvas
{
   public  class Shark : Fish
    {
        static List<Shark> listShark = new List<Shark>();

        public Shark(string ImgName) : base(ImgName) { 
            listShark.Add(this);
        }
       
        public override void TimerMove(object sender, EventArgs e)
        {
            foreach( Shark othershark in listShark )
            {
                min_distance = othershark.distance(this);
            }
        }
    }
}
