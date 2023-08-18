using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Data.SqlClient;

namespace Block_Break_Game.First
{
    public class Ball : GObject
    {
        public static List<Ball> BallList = new List<Ball>();
        int xspeed = 0;
        int yspeed = -10;

        public Ball(Ground ground, Point ploc, int index): base(ground)
        {
            loc = ploc;
            Height = 10;
            Width = 10;
            block.Fill = Brushes.Blue;
        }

        public void tick()
        {
            // one step move
            loc = new Point(loc.X + xspeed, loc.Y + yspeed);

            // check bound
        }

        public void Move99(int num)
        {
            switch (num)
            {
                case 1:
                    yspeed = -yspeed;
                    xspeed = -xspeed;
                    loc = new Point(loc.Y + yspeed, loc.X + xspeed);
                    break;

                case 2:
                    WallMove();
                    break;
            }
        }
        public void  WallMove()
        {
            if (loc.Y < 0 || loc.Y > 415)
            {
                if (loc.X < 0 || loc.X > 700)
                {
                    yspeed = -yspeed;
                    xspeed = -xspeed;
                    loc = new Point(loc.Y + yspeed, loc.X + xspeed);
                }
                else
                {
                    yspeed = -yspeed;
                    loc = new Point(loc.Y + yspeed, loc.X + xspeed);
                }
            }
            
            if (loc.X < 0 || loc.X > 700)
            {
                if (loc.Y < 0 || loc.Y > 415)
                {
                    xspeed = -xspeed;
                    yspeed = -yspeed;
                    loc = new Point(loc.Y + yspeed, loc.X + xspeed);

                }

                else
                {
                    xspeed = -xspeed;
                    loc = new Point(loc.Y + yspeed, loc.X + xspeed);
                }
            }
            
            if(loc.Y >= 0 && loc.Y <= 415 && loc.X >= 0 && loc.X <= 700)
            {
                loc = new Point(loc.Y + yspeed, loc.X + xspeed);
            }

        }

      

        public int BlockCollision(Block p)
        {
            if (loc.Y >= p.loc.Y && loc.Y <= p.loc.Y + 14)
            {
                if(loc.X >= p.loc.X && loc.X <= p.loc.X + 29)
                {
                    return 1;
                }
            }
            return 2;
        } //충돌 지점이 꼭짓점이면 문제가 발생

        internal static void AllTick()
        {
             foreach( Ball ball in BallList )
            {
                ball.tick();
            }
        }
    }
}
