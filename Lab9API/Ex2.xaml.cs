using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Lab9API
{
    /// <summary>
    /// Логика взаимодействия для Ex2.xaml
    /// </summary>
    public partial class Ex2 : Window
    {
        public ObservableCollection<CityCoordinate> CityCoordinates { get; set; } = new ObservableCollection<CityCoordinate>();

        public Ex2()
        {
            InitializeComponent();
            CoordinatesDataGrid.ItemsSource = CityCoordinates; // Привязка данных к DataGrid
            LoadCoordinates();
        }
        private void LoadCoordinates()
        {
            // Добавляем координаты Якутска
            CityCoordinates.Add(new CityCoordinate
            {
                City = "Якутск",
                Longitude = 129.7326,
                Latitude = 62.0355
            });

            // Добавляем координаты Магадана
            CityCoordinates.Add(new CityCoordinate
            {
                City = "Магадан",
                Longitude = 150.8035,
                Latitude = 59.5681
            });

            // Сравниваем широты и выводим сообщение
            string result = CityCoordinates[0].Latitude > CityCoordinates[1].Latitude
                ? "Якутск находится севернее Магадана."
                : "Магадан находится севернее Якутска.";
        }

    }
    public class CityCoordinate
    {
        public string City { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
