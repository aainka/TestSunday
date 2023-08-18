using gameproject.code;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace gameproject
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>

    public partial class MainWindow : Window
    {

        Random random = new Random();
        Ground xground = null;

        public MainWindow()
        {
            InitializeComponent();
            xground = new Ground(Canvas1);
            this.PreviewKeyDown += (s, e) =>
            {
                if (Keyboard.IsKeyDown(Key.Space))
                {
                    xground.AddBall();
                }

                if (Keyboard.IsKeyDown(Key.Right))
                {
                    xground.RightMoveBall();
                }

                if ((Keyboard.IsKeyDown(Key.Left)))
                {
                    xground.LiftMoveBall();
                }
            };



        }



    }
}


