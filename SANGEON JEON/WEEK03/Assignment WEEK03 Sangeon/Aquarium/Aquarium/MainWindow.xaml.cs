﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            Point cave_position 
                = new Point(Aquarium.ActualWidth / 20+Cave.ActualWidth/2, Aquarium.ActualHeight*4 / 5-Cave.ActualHeight/4);
            Random random = new Random();
            codManager.CreateFish(cave_position, Vector.Multiply(random.Next(2)*2-1, Fish.stdVector));
        }

        private void AquariumSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(Cave,Aquarium.ActualWidth/20);Canvas.SetBottom(Cave, Aquarium.ActualHeight / 5);
        }
    }
}
