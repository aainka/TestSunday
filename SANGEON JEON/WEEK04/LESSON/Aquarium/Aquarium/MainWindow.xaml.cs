using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Aquarium
{
    public partial class MainWindow : Window
    {
        public CodManager? codManager;SharkManager sharkManager;

        public MainWindow()
        { 
            InitializeComponent();
            codManager=new CodManager(Aquarium,Int32.MaxValue/100);
            sharkManager = new SharkManager(Aquarium, 700);
        }

        private void Cave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Cave_rotate();
        }

        private void AquariumSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(Cave,Aquarium.ActualWidth/20);Canvas.SetBottom(Cave, Aquarium.ActualHeight / 5);
        }

        public void Cave_rotate()
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0.0;
            doubleAnimation.To = 365.0;
            doubleAnimation.AutoReverse = true;
            doubleAnimation.Duration = TimeSpan.FromMilliseconds(5000);

            CaveRotation.BeginAnimation(RotateTransform.AngleProperty,doubleAnimation);
            
        }
    }
}
