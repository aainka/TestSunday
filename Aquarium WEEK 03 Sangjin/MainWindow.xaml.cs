using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Aquarium_3_week
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }
        int BluefinTunaCount = 1;
        int BluefinTunaHp = 7;

        int SalmonCount = 1;
        int SalmonHp = 5;

        int RighteyeFlounderCount = 1;
        int RighteyeFlounderHp = 4;


        private void FishAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (random.Next()%4 + 1)
            {
                    case 1:
                    new BluefinTuna(aquarium, 10, 10, 10, BluefinTunaHp, "참다랑어" + (BluefinTunaCount++));
                    break;
                    case 2:
                    new Salmon(aquarium, 10, 10, 10, SalmonHp, "연어" + (SalmonCount++));
                    break;
                    case 3:
                    new RighteyeFlounder(aquarium, 10, 10, 10, RighteyeFlounderHp, "가자미" + (RighteyeFlounderCount++));
                    break;
            }
        }

        private void FishHpPlus_Click(object sender, MouseButtonEventArgs e)
        {
            BluefinTunaHp++;
            SalmonHp++;
            RighteyeFlounderHp++;
        }

        private void Canvas_size_changed(object sender, SizeChangedEventArgs e)
        {
          Back.Width = aquarium.ActualWidth;
            if (aquarium.ActualWidth < aquarium.ActualHeight*1.3)
            {
                Back.Width = aquarium.ActualHeight * 1.3;
            }

        }
    }

    class BluefinTuna //참다랑어 class 생성
    {
        Image BluefinTuna_Img = new Image(); //이미지 파일 생성
        TextBlock BluefinTuna_Text = new TextBlock();  //참다랑어 이름 파일 생성
        TextBlock BluefinTunaHp_Text = new TextBlock(); //참다랑어 체력 파일 생성

        Random random = new Random(); //랜덤 class 사용
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();//타이머 class 사용

        private Canvas canvasA; //canvas 선언
        private int speed = 10; //참다랑어 속도 설정
        private static int MaxHp = 7; //참다랑어 최대 체력 설정
        private int Energy = MaxHp; //참다랑어 체력 설정



        public Point pos
        {
            get
            {
                return new Point(
                    Canvas.GetLeft(BluefinTuna_Img),
                    Canvas.GetTop(BluefinTuna_Img)
                );
            }
            set
            {
                Canvas.SetLeft(BluefinTuna_Img, value.X);
                Canvas.SetTop(BluefinTuna_Img, value.Y);//참다랑어 좌표 설정

                Canvas.SetLeft(BluefinTuna_Text, value.X);
                Canvas.SetTop(BluefinTuna_Text, value.Y - 40); //참다랑어 이름 좌표 설정

                Canvas.SetLeft(BluefinTunaHp_Text, value.X);
                Canvas.SetTop(BluefinTunaHp_Text, value.Y - 20); //참다랑어 체력 좌표 설정
            }
        }



        public BluefinTuna(Canvas canvas, double x, double y, int speed, int Max_Hp, string name)
        {
            this.canvasA = canvas;
            this.speed = speed;
            MaxHp = Max_Hp;
            this.Energy = Max_Hp;
            pos = new Point(x, y);

            BluefinTuna_Img.Source = new BitmapImage(
                   new Uri(@"/fishimg/bluefin tuna(inversion).png", UriKind.RelativeOrAbsolute)
               );

            BluefinTuna_Img.Width = 100;

            BluefinTuna_Img.MouseLeftButtonDown += BluefinTunaHurt_MouseDown;
            BluefinTuna_Img.MouseRightButtonDown += BluefinTunaHeal_MouseDown;

            BluefinTuna_Text.FontSize = 10;
            BluefinTuna_Text.VerticalAlignment = VerticalAlignment.Center;
            BluefinTuna_Text.HorizontalAlignment = HorizontalAlignment.Center;
            BluefinTuna_Text.Background = Brushes.Black;
            BluefinTuna_Text.Foreground = Brushes.Yellow;
            BluefinTuna_Text.Text = name;

            BluefinTunaHp_Text.FontSize = 10;
            BluefinTunaHp_Text.VerticalAlignment = VerticalAlignment.Center;
            BluefinTunaHp_Text.HorizontalAlignment = HorizontalAlignment.Center;
            BluefinTunaHp_Text.Background = Brushes.Black;
            BluefinTunaHp_Text.Foreground = Brushes.OrangeRed;

            string Hp = "";
            for (int i = 1; i <= Energy; i++)
            {
                Hp = Hp + "■";
            }

            BluefinTunaHp_Text.Text = Hp;

            canvas.Children.Add(BluefinTuna_Img);
            canvas.Children.Add(BluefinTuna_Text);
            canvas.Children.Add(BluefinTunaHp_Text);

            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();

        }
        void timer_Tick(object sender, EventArgs e)
        {
            Move();
        }

        private void BluefinTunaHurt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Energy--;
            string Hp = "";
            for (int i = 1; i <= Energy; i++)
            {
                Hp = Hp + "■";
            }
            for (int i = 1; i <= MaxHp - Energy; i++)
            {
                Hp = Hp + "□";
            }
            BluefinTunaHp_Text.Text = Hp;
            if (Energy == 0)
            {
                timer.Stop();
                canvasA.Children.Remove(BluefinTuna_Img);
                canvasA.Children.Remove(BluefinTuna_Text);
                canvasA.Children.Remove(BluefinTunaHp_Text);
            }
        }

        private void BluefinTunaHeal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Energy++;
            string Hp = "";
            for (int i = 1; i <= Energy; i++)
            {
                Hp = Hp + "■";
            }
            for (int i = 1; i <= MaxHp - Energy; i++)
            {
                Hp = Hp + "□";
            }

            BluefinTunaHp_Text.Text = Hp;

            if (Energy >= MaxHp)
            {
                Energy--;
            }
        }

        public void Move()
        {
            switch ((random.Next() % 5) + 1)
            {
                case 1:
                    if (pos.Y > canvasA.ActualHeight - 50 - speed)
                    {
                        pos = new Point(pos.X, pos.Y - speed);

                    }
                    else
                    {
                        pos = new Point(pos.X, pos.Y + speed);

                    }
                    break;
                case 2:
                    if (pos.Y < 50 + speed)
                    {
                        pos = new Point(pos.X, pos.Y + speed);

                    }
                    else
                    {
                        pos = new Point(pos.X, pos.Y - speed);

                    }
                    break;
                case 3:
                    if (pos.X > canvasA.ActualWidth - 100 - speed)
                    {
                        pos = new Point(pos.X - speed, pos.Y);
                        BluefinTuna_Img.Source = new BitmapImage(
                   new Uri(@"/fishimg/bluefin tuna.png", UriKind.RelativeOrAbsolute));

                    }
                    else
                    {
                        pos = new Point(pos.X + speed, pos.Y);

                    }
                    break;
                case 4:
                    if (pos.X < 100 + speed)
                    {
                        pos = new Point(pos.X + speed, pos.Y);
                        BluefinTuna_Img.Source = new BitmapImage(
                   new Uri(@"/fishimg/bluefin tuna(inversion).png", UriKind.RelativeOrAbsolute));

                    }
                    else
                    {
                        pos = new Point(pos.X - speed, pos.Y);

                    }
                    break;
            }
        }
    }

    class Salmon //연어 class 생성
    {
        Image Salmon_Img = new Image(); //이미지 파일 생성
        TextBlock Salmon_Text = new TextBlock();  //연어 이름 파일 생성
        TextBlock SalmonHp_Text = new TextBlock(); //연어 체력 파일 생성

        Random random = new Random(); //랜덤 class 사용
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();//타이머 class 사용

        private Canvas canvasA; //canvas 선언
        private int speed = 10; //연어 속도 설정
        private static int MaxHp = 7; //연어 최대 체력 설정
        private int Energy = MaxHp; //연어 체력 설정



        public Point pos
        {
            get
            {
                return new Point(
                    Canvas.GetLeft(Salmon_Img),
                    Canvas.GetTop(Salmon_Img)
                );
            }
            set
            {
                Canvas.SetLeft(Salmon_Img, value.X);
                Canvas.SetTop(Salmon_Img, value.Y);//연어 좌표 설정

                Canvas.SetLeft(Salmon_Text, value.X);
                Canvas.SetTop(Salmon_Text, value.Y - 40); //연어 이름 좌표 설정

                Canvas.SetLeft(SalmonHp_Text, value.X);
                Canvas.SetTop(SalmonHp_Text, value.Y - 20); //연어 체력 좌표 설정
            }
        }



        public Salmon(Canvas canvas, double x, double y, int speed, int Max_Hp, string name)
        {
            this.canvasA = canvas;
            this.speed = speed;
            MaxHp = Max_Hp;
            this.Energy = Max_Hp;
            pos = new Point(x, y);

            Salmon_Img.Source = new BitmapImage(
                   new Uri(@"/fishimg/salmon(inversion).png", UriKind.RelativeOrAbsolute)
               );

            Salmon_Img.Width = 50;

            Salmon_Img.MouseLeftButtonDown += SalmonHurt_MouseDown;
            Salmon_Img.MouseRightButtonDown += SalmonHeal_MouseDown;

            Salmon_Text.FontSize = 10;
            Salmon_Text.VerticalAlignment = VerticalAlignment.Center;
            Salmon_Text.HorizontalAlignment = HorizontalAlignment.Center;
            Salmon_Text.Background = Brushes.Black;
            Salmon_Text.Foreground = Brushes.Yellow;
            Salmon_Text.Text = name;

            SalmonHp_Text.FontSize = 10;
            SalmonHp_Text.VerticalAlignment = VerticalAlignment.Center;
            SalmonHp_Text.HorizontalAlignment = HorizontalAlignment.Center;
            SalmonHp_Text.Background = Brushes.Black;
            SalmonHp_Text.Foreground = Brushes.OrangeRed;

            string Hp = "";
            for (int i = 1; i <= Energy; i++)
            {
                Hp = Hp + "■";
            }

            SalmonHp_Text.Text = Hp;

            canvas.Children.Add(Salmon_Img);
            canvas.Children.Add(Salmon_Text);
            canvas.Children.Add(SalmonHp_Text);

            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();

        }
        void timer_Tick(object sender, EventArgs e)
        {
            Move();
        }

        private void SalmonHurt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Energy--;
            string Hp = "";
            for (int i = 1; i <= Energy; i++)
            {
                Hp = Hp + "■";
            }
            for (int i = 1; i <= MaxHp - Energy; i++)
            {
                Hp = Hp + "□";
            }
            SalmonHp_Text.Text = Hp;
            if (Energy == 0)
            {
                timer.Stop();
                canvasA.Children.Remove(Salmon_Img);
                canvasA.Children.Remove(Salmon_Text);
                canvasA.Children.Remove(SalmonHp_Text);
            }
        }

        private void SalmonHeal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Energy++;
            string Hp = "";
            for (int i = 1; i <= Energy; i++)
            {
                Hp = Hp + "■";
            }
            for (int i = 1; i <= MaxHp - Energy; i++)
            {
                Hp = Hp + "□";
            }

            SalmonHp_Text.Text = Hp;

            if (Energy >= MaxHp)
            {
                Energy--;
            }
        }

        public void Move()
        {
            switch ((random.Next() % 4) + 1)
            {
                case 1:
                    if (pos.Y > canvasA.ActualHeight - 50 - speed)
                    {
                        pos = new Point(pos.X, pos.Y - speed);

                    }
                    else
                    {
                        pos = new Point(pos.X, pos.Y + speed);

                    }
                    break;
                case 2:
                    if (pos.Y < 50 + speed)
                    {
                        pos = new Point(pos.X, pos.Y + speed);

                    }
                    else
                    {
                        pos = new Point(pos.X, pos.Y - speed);

                    }
                    break;
                case 3:
                    if (pos.X > canvasA.ActualWidth - 50 - speed)
                    {
                        pos = new Point(pos.X - speed, pos.Y);
                        Salmon_Img.Source = new BitmapImage(
                   new Uri(@"/fishimg/salmon.png", UriKind.RelativeOrAbsolute));

                    }
                    else
                    {
                        pos = new Point(pos.X + speed, pos.Y);

                    }
                    break;
                case 4:
                    if (pos.X < 100 + speed)
                    {
                        pos = new Point(pos.X + speed, pos.Y);
                        Salmon_Img.Source = new BitmapImage(
                   new Uri(@"/fishimg/salmon(inversion).png", UriKind.RelativeOrAbsolute));

                    }
                    else
                    {
                        pos = new Point(pos.X - speed, pos.Y);

                    }
                    break;
            }
        }

    }

    class RighteyeFlounder //가자미 class 생성
    {
        Image RighteyeFlounder_Img = new Image(); //이미지 파일 생성
        TextBlock RighteyeFlounder_Text = new TextBlock();  //가자미 이름 파일 생성
        TextBlock RighteyeFlounderHp_Text = new TextBlock(); //가자미 체력 파일 생성

        Random random = new Random(); //랜덤 class 사용
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();//타이머 class 사용

        private Canvas canvasA; //canvas 선언
        private int speed = 10; //가자미 속도 설정
        private static int MaxHp = 7; //가자미 최대 체력 설정
        private int Energy = MaxHp; //가자미 체력 설정



        public Point pos
        {
            get
            {
                return new Point(
                    Canvas.GetLeft(RighteyeFlounder_Img),
                    Canvas.GetTop(RighteyeFlounder_Img)
                );
            }
            set
            {
                Canvas.SetLeft(RighteyeFlounder_Img, value.X);
                Canvas.SetTop(RighteyeFlounder_Img, value.Y);//가자미 좌표 설정

                Canvas.SetLeft(RighteyeFlounder_Text, value.X);
                Canvas.SetTop(RighteyeFlounder_Text, value.Y - 40); //가자미 이름 좌표 설정

                Canvas.SetLeft(RighteyeFlounderHp_Text, value.X);
                Canvas.SetTop(RighteyeFlounderHp_Text, value.Y - 20); //가자미 체력 좌표 설정
            }
        }



        public RighteyeFlounder(Canvas canvas, double x, double y, int speed, int Max_Hp, string name)
        {
            this.canvasA = canvas;
            this.speed = speed;
            MaxHp = Max_Hp;
            this.Energy = Max_Hp;
            pos = new Point(x, y);

            RighteyeFlounder_Img.Source = new BitmapImage(
                   new Uri(@"/fishimg/Righteye flounder(inversion).png", UriKind.RelativeOrAbsolute)
               );

            RighteyeFlounder_Img.Width = 30;

            RighteyeFlounder_Img.MouseLeftButtonDown += RighteyeFlounderHurt_MouseDown;
            RighteyeFlounder_Img.MouseRightButtonDown += RighteyeFlounderHeal_MouseDown;

            RighteyeFlounder_Text.FontSize = 10;
            RighteyeFlounder_Text.VerticalAlignment = VerticalAlignment.Center;
            RighteyeFlounder_Text.HorizontalAlignment = HorizontalAlignment.Center;
            RighteyeFlounder_Text.Background = Brushes.Black;
            RighteyeFlounder_Text.Foreground = Brushes.Yellow;
            RighteyeFlounder_Text.Text = name;

            RighteyeFlounderHp_Text.FontSize = 10;
            RighteyeFlounderHp_Text.VerticalAlignment = VerticalAlignment.Center;
            RighteyeFlounderHp_Text.HorizontalAlignment = HorizontalAlignment.Center;
            RighteyeFlounderHp_Text.Background = Brushes.Black;
            RighteyeFlounderHp_Text.Foreground = Brushes.OrangeRed;

            string Hp = "";
            for (int i = 1; i <= Energy; i++)
            {
                Hp = Hp + "■";
            }

            RighteyeFlounderHp_Text.Text = Hp;

            canvas.Children.Add(RighteyeFlounder_Img);
            canvas.Children.Add(RighteyeFlounder_Text);
            canvas.Children.Add(RighteyeFlounderHp_Text);

            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();

        }
        void timer_Tick(object sender, EventArgs e)
        {
            Move();
        }

        private void RighteyeFlounderHurt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Energy--;
            string Hp = "";
            for (int i = 1; i <= Energy; i++)
            {
                Hp = Hp + "■";
            }
            for (int i = 1; i <= MaxHp - Energy; i++)
            {
                Hp = Hp + "□";
            }
            RighteyeFlounderHp_Text.Text = Hp;
            if (Energy == 0)
            {
                timer.Stop();
                canvasA.Children.Remove(RighteyeFlounder_Img);
                canvasA.Children.Remove(RighteyeFlounder_Text);
                canvasA.Children.Remove(RighteyeFlounderHp_Text);
            }
        }

        private void RighteyeFlounderHeal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Energy++;
            string Hp = "";
            for (int i = 1; i <= Energy; i++)
            {
                Hp = Hp + "■";
            }
            for (int i = 1; i <= MaxHp - Energy; i++)
            {
                Hp = Hp + "□";
            }

            RighteyeFlounderHp_Text.Text = Hp;

            if (Energy >= MaxHp)
            {
                Energy--;
            }
        }

        public void Move()
        {
            switch ((random.Next() % 5) + 1)
            {
                case 1:
                    if (pos.Y > canvasA.ActualHeight - 30 - speed)
                    {
                        pos = new Point(pos.X, pos.Y - speed);

                    }
                    else
                    {
                        pos = new Point(pos.X, pos.Y + speed);

                    }
                    break;
                case 2:
                    if (pos.Y > canvasA.ActualHeight - 30 - speed)
                    {
                        pos = new Point(pos.X, pos.Y - speed);

                    }
                    else
                    {
                        pos = new Point(pos.X, pos.Y + speed);

                    }
                    break;

                case 3:
                    if (pos.Y < 50 + speed)
                    {
                        pos = new Point(pos.X, pos.Y + speed);

                    }
                    else
                    {
                        pos = new Point(pos.X, pos.Y - speed);

                    }
                    break;
                case 4:
                    if (pos.X > canvasA.ActualWidth - 30 - speed)
                    {
                        pos = new Point(pos.X - speed, pos.Y);
                        RighteyeFlounder_Img.Source = new BitmapImage(
                   new Uri(@"/fishimg/Righteye flounder.png", UriKind.RelativeOrAbsolute));

                    }
                    else
                    {
                        pos = new Point(pos.X + speed, pos.Y);

                    }
                    break;
                case 5:
                    if (pos.X < 30 + speed)
                    {
                        pos = new Point(pos.X + speed, pos.Y);
                        RighteyeFlounder_Img.Source = new BitmapImage(
                   new Uri(@"/fishimg/Righteye flounder(inversion).png", UriKind.RelativeOrAbsolute));

                    }
                    else
                    {
                        pos = new Point(pos.X - speed, pos.Y);

                    }
                    break;
            }
        }

    }


}

