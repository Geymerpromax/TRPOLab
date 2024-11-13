using System.IO;
using System.Net;
using System.Net.Http;
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

namespace Lab9API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Ex1 : Window
    {
        public Ex1()
        {
            InitializeComponent();
            Ex2 ex2 = new Ex2();
            ex2.Show();
            Window1 window1 = new Window1();
            window1.Show();
        }

        // Асинхронный метод загрузки карты по URL
        private async Task LoadMapAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Загружаем данные изображения по указанному URL
                    byte[] imageData = await client.GetByteArrayAsync(url);

                    // Загружаем изображение из массива байт
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(imageData);
                    bitmap.EndInit();

                    // Отображаем загруженную карту
                    MapImage.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке карты: {ex.Message}");
                }
            }
        }

        // Загрузка схемы МГУ
        private async void LoadMoscowMap_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://static-maps.yandex.ru/1.x/?ll=37.530887,55.703118&spn=0.02,0.02&l=map";
            await LoadMapAsync(url);
        }

        // Загрузка спутникового снимка Эйфелевой башни
        private async void LoadEiffelTowerMap_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://static-maps.yandex.ru/1.x/?ll=2.2945,48.8584&spn=0.005,0.005&l=sat";
            await LoadMapAsync(url);
        }

        // Загрузка спутникового снимка Авачинского вулкана
        private async void LoadAvachinskyMap_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://static-maps.yandex.ru/1.x/?ll=158.8363,53.2569&spn=0.1,0.1&l=sat";
            await LoadMapAsync(url);
        }

        // Загрузка спутникового снимка космодрома Байконур
        private async void LoadBaikonurMap_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://static-maps.yandex.ru/1.x/?ll=63.3421,45.9206&spn=0.1,0.1&l=sat";
            await LoadMapAsync(url);
        }
    }
}