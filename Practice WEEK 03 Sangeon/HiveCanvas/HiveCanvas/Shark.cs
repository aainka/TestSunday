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
            double minDist =
            foreach(Anchovy anchovy in Anchovy.listAnchovy) 
            { 
                
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
