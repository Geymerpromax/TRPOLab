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

namespace Lab1Ex2
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

        private void PrintResult(object sender, RoutedEventArgs e)
        {
            try
            {
                if (leftTextBox.Text.ToString().Equals(""))
                {
                    MessageBox.Show("Выражение не обработано, проверте наличие ошибок");
                }
                else
                {
                    rightTextBox.Text = Convert.ToDouble(new DataTable().Compute(leftTextBox.Text.ToString(), "")).ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Исключение: {ex.Message} \nМетод: {ex.TargetSite} \nТрассировка стека: {ex.StackTrace}");
            }


        }
    }
}