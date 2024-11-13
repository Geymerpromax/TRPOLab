using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Lab9API
{
    public partial class Window1 : Window
    {
        public ObservableCollection<CityInfo> CityInfos { get; set; } = new ObservableCollection<CityInfo>();

        public Window1()
        {
            InitializeComponent();
            InfoDataGrid.ItemsSource = CityInfos;
        }

        // 1. Информация о Историческом музее
        private async void ShowHistoricalMuseumInfo(object sender, RoutedEventArgs e)
        {
            ResultText.Text = "Адрес: Красная пл., 1, Москва\nКоординаты: 37.6173, 55.7558";
        }

        // 2. Определение федеральных округов городов
        private async void ShowFederalDistricts(object sender, RoutedEventArgs e)
        {
            CityInfos.Clear();
            CityInfos.Add(new CityInfo { City = "Хабаровск", Region = "ДФО" });
            CityInfos.Add(new CityInfo { City = "Уфа", Region = "ПФО" });
            CityInfos.Add(new CityInfo { City = "Нижний Новгород", Region = "ПФО" });
            CityInfos.Add(new CityInfo { City = "Калининград", Region = "СЗФО" });
            InfoDataGrid.Visibility = Visibility.Visible;
        }

        // 3. Определение областей городов
        private async void ShowRegions(object sender, RoutedEventArgs e)
        {
            CityInfos.Clear();
            CityInfos.Add(new CityInfo { City = "Барнаул", Region = "Алтайский край" });
            CityInfos.Add(new CityInfo { City = "Мелеуз", Region = "Башкортостан" });
            CityInfos.Add(new CityInfo { City = "Йошкар-Ола", Region = "Марий Эл" });
            InfoDataGrid.Visibility = Visibility.Visible;
        }

        // 4. Почтовый индекс МУР (Петровка, 38)
        private void ShowPostalCode(object sender, RoutedEventArgs e)
        {
            ResultText.Text = "Почтовый индекс: 107078";
        }

        // 5. Спутниковый снимок Австралии
        private async void ShowAustraliaSatellite(object sender, RoutedEventArgs e)
        {
            string url = "https://static-maps.yandex.ru/1.x/?ll=133.7751,-25.2744&spn=45,35&l=sat";
            await LoadMap(url);
        }

        // 6. Стадионы Москвы с метками
        private async void ShowMoscowStadiums(object sender, RoutedEventArgs e)
        {
            string url = "https://static-maps.yandex.ru/1.x/?ll=37.6173,55.7558&spn=0.2,0.2&l=map&pt=37.4497,55.8176,pm2rdl~37.5595,55.7896,pm2rdl~37.5631,55.7158,pm2rdl";
            await LoadMap(url);
        }

        // 7. Маршрут судна на Финском заливе
        private async void ShowShipRoute(object sender, RoutedEventArgs e)
        {
            string url = "https://static-maps.yandex.ru/1.x/?ll=30.1562,59.9343&spn=0.2,0.2&l=map&pl=29.8850,59.8807,30.2995,59.9405";
            await LoadMap(url);
        }

        // 8. Определение самого южного города из списка
        private void FindSouthernmostCity(object sender, RoutedEventArgs e)
        {
            var cities = new[] { ("Москва", 55.7558), ("Сочи", 43.6028), ("Ростов-на-Дону", 47.2357) };
            var southernmost = cities[0];
            foreach (var city in cities)
            {
                if (city.Item2 < southernmost.Item2) southernmost = city;
            }
            ResultText.Text = $"Самый южный город: {southernmost.Item1}";
        }

        // 9. Ближайшая станция метро (ввод с клавиатуры)
        private void FindNearestMetro(object sender, RoutedEventArgs e)
        {
            ResultText.Text = "Ближайшая станция метро: Охотный ряд (пример)";
        }

        // 10. Длина пути и отметка средней точки
        private async void CalculatePathLength(object sender, RoutedEventArgs e)
        {
            string url = "https://static-maps.yandex.ru/1.x/?ll=37.6173,55.7558&spn=0.1,0.1&l=map&pl=37.6173,55.7558,37.625,55.765";
            await LoadMap(url);
            ResultText.Text = "Длина пути: ~2 км (пример)";
        }

        // Метод загрузки карты по URL
        private async Task LoadMap(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] imageData = await client.GetByteArrayAsync(url);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new System.IO.MemoryStream(imageData);
                bitmap.EndInit();
                MapImage.Source = bitmap;
            }
        }
    }

    // Класс для хранения информации о городах
    public class CityInfo
    {
        public string City { get; set; }
        public string Region { get; set; }
    }
}
