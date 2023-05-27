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

namespace WpfBasicControlApp1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WhatClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SuperButton.Content = "식당";
            button.Foreground = Brushes.Orange;

        }

        private void 식당(object sender, SizeChangedEventArgs e)
        {
            


        }

        private void rest(object sender, SizeChangedEventArgs e)
        {
            Border rest = sender as Border;
            Double border = rest.ActualHeight * rest.ActualWidth;
            

            
            
        }

        private void final(object sender, SizeChangedEventArgs e)
        {
            Border final = sender as Border;
            Double area = final.ActualHeight * final.ActualWidth;
            tori.Text = String.Format("{0:N0}", area);
        }
    }
}
