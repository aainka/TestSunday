using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HiveCanvas
{
    public class Anchovy : Fish
    {
        static public List<Anchovy> listAnchovy = new List<Anchovy>();

        public Anchovy(string ImgName, Canvas canvas) : base(ImgName, canvas)
        {
            listAnchovy.Add(this);
        }

        public override void TimerMove(object sender, EventArgs e)
        {

            X += 50;
            Y+= 50; 
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
