using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace 그래픽_기초1
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

        private void btnclick(object sender, RoutedEventArgs e)
        {
           //  Button button = sender as Button;
           // 버튼이라는 함수로 저장 원래는 버튼 네임만 사용해도 됨 (변수 선언)
            rest.Content = " 식당";
            rest.Foreground = Brushes.White;
            rest.Background = Brushes.Aqua;   
        }


        private void apple_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Border myBorder = sender as Border;
            double area = myBorder.ActualHeight * myBorder.ActualWidth;
            LoungeText.Text = "휴게실\n"+string.Format("{0:N0}",area);
               

        }

        private void Roit_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Border roit = sender as Border; 
            double areaa = roit.ActualHeight * roit.ActualWidth;
            kkk.Text = "화장실\n" + string.Format("{0:N0}", areaa);
        }



        //private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    Button button = sender as button;
        //    button.Content = " 식당 버튼";
        //    button.Foreground = Brushes.Orange;
        //}
    }
}
