using System.Data;
using System.Runtime.ExceptionServices;
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

namespace Lab1Ex4
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

        public void Result(object sender, RoutedEventArgs e)
        {
            Button button = new Button();
            button = (Button)sender;
            try
            {
                resultField.Content = new DataTable().Compute(firstField.Text.ToString() + button.Content.ToString() + secondField.Text.ToString(), "").ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Window() { IsEnabled = true, Title = "Ошибка" }, $"Исключение: {ex.Message} \nМетод: {ex.TargetSite} \nТрассировка стека: {ex.StackTrace}");
            }
        }
    }
}