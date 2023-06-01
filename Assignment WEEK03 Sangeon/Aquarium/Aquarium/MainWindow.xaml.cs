using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Aquarium
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        { 
            InitializeComponent();
        }

        private void Cave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point cave_position 
                = new Point(Aquarium.ActualWidth / 20+Cave.ActualWidth/2, Aquarium.ActualHeight*4 / 5-Cave.ActualHeight/4);
            Random random = new Random();
            new Cod(Aquarium, cave_position, Vector.Multiply(random.Next(2)*2-1, Fish.stdVector));
        }

        private void AquariumSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(Cave,Aquarium.ActualWidth/20);Canvas.SetBottom(Cave, Aquarium.ActualHeight / 5);
        }
    }
}
