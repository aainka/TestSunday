using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows;

namespace Block_Break_Game
{
    class RedBlock //RedBlock Class 생성
    {
        Image RedBlock_img = new Image(); //이미지 파일 생성
        private Canvas canvasA; //canvas 선언
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public Point pos
        {
            get
            {
                return new Point(
                    Canvas.GetLeft(RedBlock_img),
                    Canvas.GetTop(RedBlock_img)
                );
            }
            set
            {
                Canvas.SetLeft(RedBlock_img, value.X);
                Canvas.SetTop(RedBlock_img, value.Y);
            }
        }//RedBlock 좌표 설정



        public RedBlock(Canvas canvas, double x, double y)
        {
            this.canvasA = canvas;
            pos = new Point(x, y);

            RedBlock_img.Source = new BitmapImage(
                   new Uri(@"/images/red block.png", UriKind.RelativeOrAbsolute)
               );

            RedBlock_img.Width = 59;
            canvas.Children.Add(RedBlock_img);

            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Start();

        }//Red Block 좌표 설정
        void timer_Tick(object sender, EventArgs e)
        {
            pos = new Point(pos.X, pos.Y + 26);
        }//밑으로 내려오는 움직임
    }
}
