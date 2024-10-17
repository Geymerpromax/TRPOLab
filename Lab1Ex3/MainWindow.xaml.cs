using System.Data;
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

namespace Lab1Ex3
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

        private void CreateResult(object sender, RoutedEventArgs e)
        {        
            try
            {
                int firstNum = Convert.ToInt32(firstNumField.Text.ToString());
                int secondNum = Convert.ToInt32(secondNumField.Text.ToString());
                firstNumField.Text = firstNum.ToString();
                secondNumField.Text = secondNum.ToString();

                summ.Content = (firstNum + secondNum).ToString();
                raznost.Content = (firstNum - secondNum).ToString();
                proiz.Content = (firstNum * secondNum).ToString();
                chastnoe.Content = (firstNum / secondNum).ToString();
            }
            catch (Exception ex)
            {               
                MessageBox.Show(new Window() { IsEnabled = true, Title = "Ошибка"}, $"Исключение: {ex.Message} \nМетод: {ex.TargetSite} \nТрассировка стека: {ex.StackTrace}");
            }
        }
    }
}