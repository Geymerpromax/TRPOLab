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
using System.Windows.Shapes;

namespace Lab2Ex2
{
    /// <summary>
    /// Логика взаимодействия для wpfCalendar.xaml
    /// </summary>
    public partial class wpfCalendar : Window
    {
        public wpfCalendar()
        {
            InitializeComponent();
        }

        private void Text(object sender, MouseEventArgs e)
        {
            if (eventName.Text == "Текст события")
            {
                eventName.Text = "";
                eventName.Foreground = Brushes.Black;
            }         
        }

        private void createEvent(object sender, RoutedEventArgs e)
        {
            eventList.Content += $"\n {calendar.SelectedDate.ToString().Remove(9, 8)} {time.Text} - {eventName.Text}";
        }
    }
}
