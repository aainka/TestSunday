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

//red block size 59*25
//blue ball size 10*10 45도 회전

namespace Block_Break_Game
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
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    new RedBlock(Canvas1, 60*j, 26 * i);
                }
            }
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(999);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            for (int j = 0; j < 10; j++)
            {
                new RedBlock(Canvas1, 60 * j, 0);
            }
        }
    }
}
