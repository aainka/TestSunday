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

namespace Construction
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
        double tilewidth = 50.0;
        double tileheight = 50.0;

        Boolean isInt(double n)
        {
            return (n == Math.Truncate(n)) ? true : false;
        }
        private void LoungeSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Border myBorder = sender as Border;
            if (myBorder != null)
            {
                var area = myBorder.ActualWidth * myBorder.ActualHeight;
                double xcount = myBorder.ActualWidth / tilewidth;
                double ycount = myBorder.ActualHeight / tileheight;
                Debug.WriteLine(myBorder.ActualWidth);
                Debug.WriteLine(myBorder.ActualHeight);
                int numTileX = (isInt(xcount)) ? (int)(xcount) : (int)(xcount + 1);
                int numTileY = (isInt(ycount)) ? (int)(ycount) : (int)(ycount + 1);
                int numTile = numTileX * numTileY;

                //if (numTile >= 40){
                //    LoungeText.Text = "Lounge\nFor Sale";
                //    LoungeText.Foreground = Brushes.Red;
                //}else{
                //    LoungeText.Text = String.Format("Lounge\n ({0:N0},\n{1:N0} tiles needed)", area, numTile);
                //    LoungeText.Foreground = Brushes.White;
                //}
            }
        }


        private void RestroomSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Border myBorder = sender as Border;
            if (myBorder != null)
            {
                var area = myBorder.ActualWidth * myBorder.ActualHeight;
                RestroomText.Text = String.Format("Restroom\n ({0:N0})", area);
            }
        }

        private void RestaurantSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Border myBorder = sender as Border;
            if (myBorder != null)
            {
                var area = myBorder.ActualWidth * myBorder.ActualHeight;
                //  RestaurantText.Text = String.Format("Restroom\n ({0:N0})", area);
            }
        }

        private void WhatClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SuperButton.Content = "Restaurant";
            button.Foreground = Brushes.Orange;

        }

        private void LoungeClick(object sender, RoutedEventArgs e)
        {
            LoungeButton.Visibility = Visibility.Hidden;
        }
    }
}

