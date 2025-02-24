﻿using Microsoft.Data.Sqlite;
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
using System.Xml.Linq;

namespace Lab3
{
    /// <summary>
    /// Логика взаимодействия для Ex2.xaml
    /// </summary>
    public partial class DialogWindowChanged : Window
    {
        public DialogWindowChanged()
        {
            InitializeComponent();
        }

        private void ChangeFilm(object sender, RoutedEventArgs e)
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
                    //// Дописать SET изходя из задания
                    command.CommandText = $"UPDATE films SET " +
                        $"title = '{name.Text}', " +
                        $"year = '{year.Text}', " +
                        $"genre = '{genre.Text}', " +
                        $"duration = '{duration.Text}' " +
                        $"WHERE id = '{App.idFilmsForApdate}'";
                    command.ExecuteNonQuery();

                    connection.Close();
                    MessageBox.Show("Данные обновлены");
                }
                MessageBox.Show("Запись обновлена, таблица будет обновлена");

                App.idFilmsForApdate = new int();
            }
            Close();

        }
    }
}
