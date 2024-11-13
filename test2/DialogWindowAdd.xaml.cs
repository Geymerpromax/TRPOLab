using Microsoft.Data.Sqlite;
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

namespace Lab3
{
    /// <summary>
    /// Логика взаимодействия для Ex2.xaml
    /// </summary>
    public partial class DialogWindowAdd : Window
    {
        public DialogWindowAdd()
        {
            InitializeComponent();
        }

        private void AddFilm(object sender, RoutedEventArgs e)
        {
            if (name.Text.Replace(" ", "") == "" || year.Text.Replace(" ", "") == "" || genre.Text.Replace(" ", "") == "" || duration.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Данные ввведены неверно, попробуйте ещё раз");
                return;            
            }
            else
            {
                using (var connection = new SqliteConnection($"Data Source=DB.db"))
                {
                    connection.Open();
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO films (title, year, genre, duration) VALUES ('{name.Text}', '{year.Text}', '{genre.Text}', '{duration.Text}')";
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Запись добавлена, таблица будет обновлена");

            }
            Close();
        }
    }
}
