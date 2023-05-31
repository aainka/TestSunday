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

namespace Aquarium
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Cave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point cave_position = new Point(Aquarium.ActualWidth/2+110, Aquarium.ActualHeight*9/10-20);
            new Cod("cod.png",Aquarium, cave_position, Fish.stdVector);
        }
    }
}
