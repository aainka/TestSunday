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

namespace WpfApp22
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
            button.Foreground = Brushes.Blue;
        }

        private void rest(object sender, SizeChangedEventArgs e)
        {
            Border rest = sender as Border;
            double area = rest.ActualHeight * rest.ActualWidth;
            bob.Text = string.Format("{0:N0}", area);
        }
    }
}
