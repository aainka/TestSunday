using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HiveCanvas
{
   public  class Shark : Fish
    {
        static List<Shark> listShark = new List<Shark>();

        public Shark(string ImgName, Canvas canvas) : base(ImgName, canvas) { 
            listShark.Add(this);
        }
       
        public override void TimerMove(object sender, EventArgs e)
        {
            //  double minDist =
            X += 50;
            Y+= 50; 

            if ( X > )
            {
                this.Dispose();
            }

            //double mdis = 100000;
            //Shark mshark = null;
            //foreach( Shark othershark in listShark )
            //{
            //    if ( mdis < othershark.distance(this))
            //    {
            //        mdis = othershark.distance(this);
            //        mshark = othershark;
            //    }
            //}
            //if (  mdis <100 )
            //{
            //    mshark.Dispose();
            //}

        }

    }
}
