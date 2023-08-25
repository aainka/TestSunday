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
            }; // 바 위치 설정

            _canvas.MouseLeftButtonDown += (s, e) =>
            {
                AddBall();
            };//마우스 좌클릭 시 공 생성

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

            int num2 = 2;
            foreach (Ball k in Ball.BallList)
            {
                double positionx1 = k.loc.X - k.xspeed;
                double positiony1 = k.loc.Y - k.yspeed;
                double positionx2 = k.loc.X;
                double positiony2 = k.loc.Y;
                num2 = k.BarCollision(xbar);

                if (num2 == 1)
                {
                 k.BallMove(num2, positionx1, positionx2, positiony1, positiony2,
                            xbar.loc.X, xbar.loc.Y, xbar.loc.X + xbar.Width, xbar.loc.Y + xbar.Height);
                }

            }// bar 충돌과 관련
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
