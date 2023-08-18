using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Block_Break_Game.First
{
    public class Ground
    {
        double BallPosition = 200;
        DispatcherTimer timer = new DispatcherTimer();

        Rectangle _ground = new Rectangle();
        public Canvas _canvas;
        Block gBlock = null;
        Bar xbar = null;


        public Ground(Canvas canvas) {
            _ground.Height = 700;
            _ground.Width = 415;
            _ground.Fill = Brushes.Green;
            _canvas = canvas;
            _canvas.Children.Add(_ground);

            _canvas.MouseMove += (s, e) =>
            {
                Point currentPossition = e.GetPosition(canvas);
                xbar.loc = currentPossition;
                xbar.loc = new Point(xbar.loc.X, 600);
            };
            _canvas.MouseLeftButtonDown += (s, e) =>
            {
                AddBall();
            };

            Block.InitAll(this);
            xbar = new Bar(this, new Point(100, 600));





            foreach (Block p in Block.BlockList)
            {
                p.sight[0] = true; 
            }

            timer.Tick += time_tick;
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();
        }

        public void time_tick(object sender, EventArgs e)
        {

            Ball.AllTick();
            int num = 2;


            //foreach (Block p in Block.BlockList)
            //{
            //    foreach (Ball k in Ball.BallList)
            //    {
            //       num = k.BlockCollision(p);

            //        if(num == 1)
            //        {
            //            if (p.sight[0] == false)
            //            {
            //                num = 2;
            //                k.Move(num);
            //            }
            //            foreach (Ball l in Ball.BallList)
            //            {
            //                l.Move(num);
            //                p.sight[0] = false;
            //            }
            //        }

            //    }
            //}// ball 충돌과 관련

            //foreach (Ball k in Ball.BallList)
            //{
            //    k.Move(num);
            //}// ball 공의 움직임

            foreach ( Block p in Block.BlockList)
            {
                foreach (Ball k in Ball.BallList)
                {
                    if (p.contains(k))
                    {
                        p.Dispose();
                    }
                }
            } //block 충돌 및 제거
        }//시간에 따른 움직임 제어

        public void AddBall()
        {
            Ball.BallList.Add(
                        new Ball(this,
                            new Point(xbar.loc.X, 600),
                            20)
                        );
        }//볼 생성

        public void RightMoveBall()
        {
            BallPosition = BallPosition + 10;
        }
        public void LiftMoveBall()
        {
            BallPosition = BallPosition - 10;
        }

    }
}
