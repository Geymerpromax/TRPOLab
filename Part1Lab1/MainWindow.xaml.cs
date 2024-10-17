using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1Ex1
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string dateLeft = leftTextBox.Text;
            string dateRight = rightTextBox.Text;

            bool nullState = leftTextBox.Text == "" && rightTextBox.Text == "";

            if ((string)reverseLine.Content == "<-" && !nullState)
            {
                reverseLine.Content = "->";
                leftTextBox.Text = dateRight;
                rightTextBox.Text = dateLeft;
                return;
            }
            if ((string)reverseLine.Content == "->" && !nullState)
            {
                reverseLine.Content = "<-";
                leftTextBox.Text = dateRight;
                rightTextBox.Text = dateLeft;
                return;
            }


        }
    }
}