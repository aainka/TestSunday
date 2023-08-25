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

        public int xspeed = 2;
        public int yspeed = -3;

        public Ball(Ground ground, Point ploc, int index): base(ground)
        {
            loc = ploc;
            Height = 10;
            Width = 10;
            block.Fill = Brushes.Blue;
        }

        public void BallMove(int num,
            double a , double b , double c, double d,
            double e , double f , double g ,double h)
        {
            switch (num)
            {
                case 1:
                    graph(a, b, c, d, e, f, g, h);
                    break;

                case 2:
                    WallMove();
                    break;
            }
        }
        public void  WallMove()
        {
            if (loc.X < 0 || loc.X > 415)
            {
                if (loc.Y < 0 || loc.Y > 700)
                {
                    xspeed = -xspeed;
                    yspeed = -yspeed;

                    loc = new Point(loc.X + xspeed, loc.Y + yspeed);
                }
                else
                {
                    xspeed = -xspeed;
                    loc = new Point(loc.X + xspeed, loc.Y + yspeed);
                }
            }
            
            if (loc.Y < 0 || loc.Y > 700)
            {
                if (loc.X < 0 || loc.X > 415)
                {
                    xspeed = -xspeed;
                    yspeed = -yspeed;
                    loc = new Point(loc.X + xspeed, loc.Y + yspeed);

                }

                else
                {
                    yspeed = -yspeed;
                    loc = new Point(loc.X + xspeed, loc.Y + yspeed);
                }
            }
            
            if(loc.X >= 0 && loc.X <= 415 && loc.Y >= 0 && loc.Y <= 700)
            {
                loc = new Point(loc.X + xspeed, loc.Y + yspeed);
            }

        }

        public int BlockCollision(Block p )
        {
            if (loc.Y + Height/2  >= p.loc.Y  && loc.Y + Height / 2 <= p.loc.Y + p.Height )
            {
                if(loc.X + Width/2 >= p.loc.X && loc.X + Width / 2 <= p.loc.X + p.Width  )
                {
                    return 1;
                }
            }
            return 2;
        } 

        public int BarCollision(Bar p)
        {
            if (loc.Y + Height / 2 >= p.loc.Y && loc.Y + Height / 2 <= p.loc.Y + p.Height)
            {
                if (loc.X + Width / 2 >= p.loc.X && loc.X + Width / 2 <= p.loc.X + p.Width)
                {
                    return 1;
                }
            }
            return 2;
        }

        internal static void AllTick()
        {
            
            int num = 2;

            foreach (Ball k in Ball.BallList)
            {
                double positionx1 = k.loc.X - k.xspeed;
                double positiony1 = k.loc.Y - k.yspeed;
                double positionx2 = k.loc.X;
                double positiony2 = k.loc.Y;
                    foreach (Block p in Block.BlockList)
                    {
                        num = k.BlockCollision(p);

                        if (num == 1)
                        {
                            if (p.sight[0] == false)
                            {
                                num = 2;
                                k.BallMove(num, positionx1, positionx2, positiony1, positiony2,
                                    p.loc.X, p.loc.Y, p.loc.X + p.Width, p.loc.Y + p.Height);
                            }
                            else
                            {
                                k.BallMove(num, positionx1, positionx2, positiony1, positiony2,
                                    p.loc.X, p.loc.Y, p.loc.X + p.Width, p.loc.Y + p.Height);
                                p.sight[0] = false;
                            }
                        }

                    }
            }// ball 충돌과 관련

            foreach (Ball k in Ball.BallList)
            {
                k.BallMove(num , 0, 0 ,0 ,0, 0, 0 ,0 ,0);
            }

        }

        public void graph(double positionx1, double positionx2, double positony1, double positony2,
            double blockposx1, double blockposy1, double blockposx2, double blockposy2)
        {
            double xslope = (positony1- positony2) /(positionx1-positionx2);//y =ax+b =>y - ax = b
            double yslope = 1/xslope;//x = ay + b => x - ay = b
            double xconstant = positony1 - xslope * positionx1;
            double yconstant = positionx1 - yslope * positony1;

            //posX2 - PosX1 > 0 => ->
            //posX2 - PosX1 < 0 => <-
            //posY2 - PosY1 > 0 => 아래 진행
            //posY2 - PosY1 < 0 => 위로 진행
            if (positionx1 - positionx2 == 0) 
            {
                yspeed = -yspeed;
            }// 일직선으로 날라갈 때

            if (positionx2-positionx1 > 0 && positony2 - positony1 > 0) 
            {
                if (xslope*blockposx1 + xconstant > blockposy2 && xslope * blockposx1 + xconstant < blockposy1)
                {
                    xspeed = -xspeed;
                    //loc = new Point(blockposx1, xslope * blockposx1 + xconstant);                    
                }
                if (yslope*blockposy1 + yconstant > blockposx1 && yslope * blockposy1 + yconstant < blockposx2)
                {
                    yspeed = -yspeed;
                    //loc = new Point(yslope*blockposy1 + yconstant, blockposy1);  
                }
            }//-> 아래

            if(positionx2-positionx1 > 0 && positony2 -positony1 < 0)
            {
                if (xslope * blockposx1 + xconstant > blockposy2 && xslope * blockposx1 + xconstant < blockposy1)
                {
                    xspeed = -xspeed;
                    //loc = new Point(blockposx1, xslope * blockposx1 + xconstant);                    
                }
                if (yslope * blockposy2 + yconstant > blockposx1 && yslope * blockposy2 + yconstant < blockposx2)
                {
                    yspeed = -yspeed;
                    //loc = new Point(yslope*blockposy2 + yconstant, blockposy2);  
                }
            }//->위

            if (positionx2 - positionx1 < 0 && positony2 - positony1 > 0)
            {
                if (xslope * blockposx2 + xconstant > blockposy2 && xslope * blockposx2 + xconstant < blockposy1)
                {
                    xspeed = -xspeed;
                    //loc = new Point(blockposx2, xslope * blockposx2 + xconstant);                    
                }
                if (yslope * blockposy1 + yconstant > blockposx1 && yslope * blockposy1 + yconstant < blockposx2)
                {
                    yspeed = -yspeed;
                    //loc = new Point(yslope*blockposy1 + yconstant, blockposy1);  
                }
            }//<-아래

            if (positionx2 - positionx1 < 0 && positony2 - positony1 < 0)
            {
                if (xslope * blockposx2 + xconstant > blockposy2 && xslope * blockposx2 + xconstant < blockposy1)
                {
                    xspeed = -xspeed;
                    //loc = new Point(blockposx2, xslope * blockposx2 + xconstant);                    
                }
                if (yslope * blockposy2 + yconstant > blockposx1 && yslope * blockposy2 + yconstant < blockposx2)
                {
                    yspeed = -yspeed;
                    //loc = new Point(yslope*blockposy2 + yconstant, blockposy2);  
                }
            }//<-위

        }
    }
}
