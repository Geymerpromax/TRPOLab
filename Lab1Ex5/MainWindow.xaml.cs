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

namespace Lab1Ex5
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

        private void Resulting(object sender, RoutedEventArgs e)
        {
            result.Content = "Ваш заказ:\n";
            if ((bool)chiz.IsChecked)
            {
                result.Content += "Чизбургер\n";
            }
            if ((bool)gamb.IsChecked)
            {
                result.Content += "Гамбургер\n";
            }
            if ((bool)koka.IsChecked)
            {
                result.Content += "Кока-кола\n";
            }
            if ((bool)naget.IsChecked)
            {
                result.Content += "Нагеттсы\n";
            }
        }
    }
}