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

namespace Practice_2nd_Week
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool newButton;     // 연산자 버튼을 누른 후 새로 숫자가 시작되어야 함을 의미
        private double savedValue;   // 연산자 버튼을 누를 때 현재 txtResult에 있는 값을 저장하는 필드
        private char myOperator;    // 현재 계산할 연산자를 저장하는 char형 변수

        public MainWindow()
        {
            InitializeComponent();
        }

        // 숫자 버튼의 처리
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string number = btn.Content.ToString();
            // 지금 0이거나 새로시작했을경우 값을 바로 적어줌.
            if (txtResult.Text == "0" || newButton == true)
            {
                txtResult.Text = number;
                newButton = false;
            }
            else // 그렇지 않을경우 숫자를 뒤에 계속 붙임 -> char기때문에 가능
            {
                txtResult.Text += number;
            }
        }

        // 연산자 처리
        private void btnOp_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            savedValue = double.Parse(txtResult.Text); // 원래 있던 값을 savedValue에 저장
            myOperator = btn.Content.ToString()[0];    // 연산자를 저장
            newButton = true;   // 계산 완료시 새로시작
        }

        // 소수점의 처리
        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            if (txtResult.Text.Contains(".") == false)
                txtResult.Text += ".";
        }

        // = 버튼의 처리
        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            if (myOperator == '+')
                txtResult.Text = (savedValue + double.Parse(txtResult.Text)).ToString();
            else if (myOperator == '-')
                txtResult.Text = (savedValue - double.Parse(txtResult.Text)).ToString();
            else if (myOperator == '*')
                txtResult.Text = (savedValue * double.Parse(txtResult.Text)).ToString();
            else if (myOperator == '/')
                txtResult.Text = (savedValue / double.Parse(txtResult.Text)).ToString();
        }
    }
}
