using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.Sqlite;

namespace test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
    public partial class Ex1 : Window
    {
        public bool check1Table = false;
        public bool check2Table = false;
        public bool check3Table = false;

        static string dbName;
        static string db2Name;

        static string tableName = "Films";
        static string table2Name = "MusicTrack";
        static string table3Name = "Artist";

        static List<Film> filmsCollection = new List<Film>();
        static List<MusicTrack> musicTrackCollection = new List<MusicTrack>();

        static List<MusicTrack> asunastateMusicTrackCollection = new List<MusicTrack>();
        static List<MusicTrack> zsvMusicTrackCollection = new List<MusicTrack>();

        static List<Artist> artistsCollection = new List<Artist>();


        public Ex1()
        {
            InitializeComponent();

            dbName = "DB.db";
            db2Name = "DB2.db";

            filmsCollection.Add(new Film("А вдруг это любовь?", "1961", "Триллер", "90", 1));
            filmsCollection.Add(new Film("Человек паук", "1959", "Триллер", "26", 2));
            filmsCollection.Add(new Film("Хоровод", "1952", "Комедия", "78", 3));

            filmsCollection.Add(new Film("Астерикс один", "2024", "Хоррор", "120", 4));
            filmsCollection.Add(new Film("Обеликс один", "1952", "Комедия", "140", 5));
            filmsCollection.Add(new Film("Астерикс и Обеликс", "1952", "Комедия", "140", 6));

            musicTrackCollection.Add(new MusicTrack("Роскомсвобода", "2024", "Хип-хоп", "3", 1));
            musicTrackCollection.Add(new MusicTrack("Демонтажник", "2023", "Рэп", "2,50", 2));
            musicTrackCollection.Add(new MusicTrack("Путь", "2022", "Рэп", "2,41", 3));
            musicTrackCollection.Add(new MusicTrack("Звёзды горизонта", "2023", "Рэп", "1,40", 4));

            asunastateMusicTrackCollection.Add(musicTrackCollection[0]);
            asunastateMusicTrackCollection.Add(musicTrackCollection[1]); ;

            zsvMusicTrackCollection.Add(musicTrackCollection[2]);
            zsvMusicTrackCollection.Add(musicTrackCollection[3]);

            artistsCollection.Add(new Artist("Asunastate", 1, asunastateMusicTrackCollection));
            artistsCollection.Add(new Artist("ZSV", 2, zsvMusicTrackCollection));


            CreateBase(dbName);
            CreateBase(db2Name);

            TableIsExists1(dbName, tableName);
            TableIsExists2(db2Name, table2Name);
            TableIsExists3(db2Name, table3Name);

            if (!check1Table)
            {
                CreateFilmsTable(dbName, tableName);
                CreateFilmsTableContent(dbName, tableName, filmsCollection);
            }
            if (!check3Table)
            {
                CreateArtistTable(db2Name, table3Name);
                CreateArtistTableContent(db2Name, table3Name, artistsCollection);
            }
            if (!check2Table)
            {
                CreateMusicTrackTable(db2Name, table2Name);
                CreateMusicTrackTableContent(db2Name, table2Name, musicTrackCollection);
            }

            LoadFilmsToTable();
        }      
        private void TableIsExists1(string dataBaseName, string tableName)
        {          
            bool tableExists = false;

            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();

                // Запрос для проверки существования таблицы
                string query = $"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = '{tableName}';";

                using (var command = new SqliteCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    tableExists = Convert.ToInt32(result) > 0;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            check1Table = tableExists;
        }
        private void TableIsExists2(string dataBaseName, string tableName)
        {
            bool tableExists = false;

            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();

                // Запрос для проверки существования таблицы
                string query = $"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = '{tableName}';";

                using (var command = new SqliteCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    tableExists = Convert.ToInt32(result) > 0;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            check2Table = tableExists;
        }
        private void TableIsExists3(string dataBaseName, string tableName)
        {
            bool tableExists = false;

            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();

                // Запрос для проверки существования таблицы
                string query = $"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = '{tableName}';";

                using (var command = new SqliteCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    tableExists = Convert.ToInt32(result) > 0;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            check3Table = tableExists;
        }
        private void CreateFilmsTableContent(string dataBaseName, string tableName, List<Film> films)
        {
            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                foreach (var film in films)
                {
                    command.CommandText = $"INSERT INTO {tableName} (title, year, genre, duration) VALUES ('{film.Name}', '{film.Year}', '{film.Genre}', '{film.Duration}')";
                    command.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        private void CreateMusicTrackTableContent(string dataBaseName, string tableName, List<MusicTrack> musicTracks)
        {
            int artistId = 100;
            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                foreach (var musicTrack in musicTracks)
                {
                    foreach (var artist in artistsCollection)
                    {
                        foreach(var track in artist.Tracks)
                        {
                            if (track == musicTrack)
                            {
                                artistId = artist.Id;
                                break;
                            }
                        }
                    }
                    command.CommandText = $"INSERT INTO {tableName} (title, year, genre, duration, idArtist) VALUES ('{musicTrack.Name}', '{musicTrack.Year}', '{musicTrack.Genre}', '{musicTrack.Duration}', {artistId})";
                    command.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        private void CreateArtistTableContent(string dataBaseName, string tableName, List<Artist> artists)
        {
            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                foreach (var artist in artists)
                {
                    command.CommandText = $"INSERT INTO {tableName} (name) VALUES ('{artist.Name}')";
                    command.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        private List<string> GetContentFromTableForYear(string dataBaseName, string tableName, string filmsYear)
        {
            if (filmsYear == "")
            {
                filmsYear = "0000";
            }

            List<string> films = new List<string>();
            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                command.CommandText = $"SELECT * FROM {tableName} WHERE year = {filmsYear}";
                using var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    films.Add($"Title: {reader["title"]}, Year: {reader["year"]}");
                }
                reader.Close();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return films;
        }
        private List<string> GetContentFromMusicTrackTableForArtistPick(string dataBaseName, string tableName, string artistName)
        {
            if (artistName == "")
            {
                artistName = "Asunastate";
            }

            List<string> films = new List<string>();
            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                command.CommandText = $"SELECT * FROM {tableName} WHERE year = {artistName}";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    films.Add($"Title: {reader["title"]}, Year: {reader["year"]}");
                }
                reader.Close();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return films;
        }
        private List<string> GetAllContentFromTable(string dataBaseName, string tableName)
        {
            List<string> films = new List<string>();
            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                command.CommandText = $"SELECT * FROM {tableName}";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    films.Add($"Title: {reader["title"]}, Year: {reader["year"]}, Genre: {reader["genre"]}, Duration: {reader["duration"]}");
                }
                reader.Close();
                command.ExecuteNonQuery();
                connection.Close();
            }
            
            return films;
        }
        private List<Film> GetAllContentWithFilmsFormat(string dataBaseName, string tableName)
        {
            List<Film> films = new List<Film>();
            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                command.CommandText = $"SELECT * FROM {tableName}";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    films.Add( new Film($"{reader["title"]}", $"{reader["year"]}", $"{reader["genre"]}", $"{reader["duration"]}", Convert.ToInt32( $"{reader["id"]}")));
                }
                reader.Close();
                command.ExecuteNonQuery();
                connection.Close();
            }

            return films;
        }
        static void CreateFilmsTable(string dataBaseName, string tableName)
        {
            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                command.CommandText = $"CREATE TABLE {tableName}" +
                    "(" +
                    "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                    "title TEXT NOT NULL, " +
                    "year INTEGER NOT NULL, " +
                    "genre TEXT NOT NULL, " +
                    "duration TEXT NOT NULL" +
                    ")";

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        static void CreateMusicTrackTable(string dataBaseName, string tableName)
        {
            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                command.CommandText = $"CREATE TABLE {tableName}" +
                    "(" +
                    "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                    "title TEXT NOT NULL, " +
                    "year INTEGER NOT NULL, " +
                    "genre TEXT NOT NULL, " +
                    "duration TEXT NOT NULL, " +
                    "idArtist INTEGER NOT NULL, " +
                    "FOREIGN KEY(idArtist) REFERENCES Artist(id) ON DELETE CASCADE" +
                    ")";

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        static void CreateArtistTable(string dataBaseName, string tableName)
        {
            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                command.CommandText = $"CREATE TABLE {tableName}" +
                    "(" +
                    "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                    "name TEXT NOT NULL" +                    
                    ")";

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        static void CreateBase(string dataBaseName)
        {
            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;             

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        static void AddFieldTable(string dataBaseName, string tableName, string fieldName, string fieldType, string parametr = "NOT NULL")
        {
            using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                string alterTableQuery = $"ALTER TABLE {tableName} ADD COLUMN {fieldName} {fieldType};";

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private void PrintFilmsIsYear(object sender, RoutedEventArgs e)
        {        
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (Convert.ToInt32(film.Year) > 1997)
                {
                    if (!result.Content.ToString().Contains(film.Name + " " + film.Year + " " + film.Genre))
                    {
                        result.Content += film.Name + " " + film.Year + " " + film.Genre + "\n";
                    }

                }
            }
        }
        private void PrintFilmsWithVopros(object sender, RoutedEventArgs e)
        {
            foreach (var film in GetAllContentFromTable(dbName, tableName))
            {
                if (film.Contains('?'))
                {
                    if (!result2.Content.ToString().Contains(film))
                    {
                        result2.Content += film + "\n";
                    }
                }              
            }
        }
        private void PrintFilmsWithXName(object sender, RoutedEventArgs e)
        {
            foreach (var film in GetAllContentFromTable(dbName, tableName))
            {
                if (film[7].Equals('Х'))
                {
                    if (result3.Content.ToString().Contains(film))
                    {
                        break;
                    }
                    result3.Content += film + "\n";
                }
            }
        }
        private void PrintFilmsWithMoreDuration(object sender, RoutedEventArgs e)
        {
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (Convert.ToInt32(film.Duration) > 60)
                {
                    if (!result4.Content.ToString().Contains(film.Name))
                    {
                        result4.Content += film.Name + "\n";
                    }                   
                }                              
            }
        }
        private void PrintFilmForFilterWithMinID(object sender, RoutedEventArgs e)
        {
            switch (filtr.SelectedIndex)
            {
                case 0:
                    {
                        if (!int.TryParse(filterData.Text.ToString(), out int result))
                        {
                            MessageBox.Show("Некоректные данные для фильтра: Год выпуска");
                            break;
                        } 
                        else
                        {
                            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
                            {
                                if (film.Year == filterData.Text.ToString())
                                {
                                    durationField.Text = film.Duration;
                                    nameField.Text = film.Name;
                                    yearField.Text = film.Year;
                                    genreField.Text = film.Genre;
                                    idField.Text = film.Id.ToString();
                                    break;
                                }
                            }
                        }
                        
                        break;
                    }
                case 1:
                    {

                        foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
                        {
                            if (film.Name.Contains(filterData.Text.ToString()))
                            {
                                durationField.Text = film.Duration;
                                nameField.Text = film.Name;
                                yearField.Text = film.Year;
                                genreField.Text = film.Genre;
                                idField.Text = film.Id.ToString();
                                break;
                            }
                        }

                        break;
                    }
                case 2:
                    {
                        if (!int.TryParse(filterData.Text.ToString(), out int result))
                        {
                            MessageBox.Show("Некоректные данные для фильтра: Год выпуска");
                            break;
                        }
                        else
                        {
                            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
                            {
                                if (film.Duration == filterData.Text.ToString())
                                {
                                    durationField.Text = film.Duration;
                                    nameField.Text = film.Name;
                                    yearField.Text = film.Year;
                                    genreField.Text = film.Genre;
                                    idField.Text = film.Id.ToString();
                                    break;
                                }
                            }
                        }
                        break;
                    }               
                default:
                    MessageBox.Show("Задайте фильтр!");
                    break;
            }           
        }       
        private void PrintFilmsForFilterGenre(object sender, RoutedEventArgs e)
        {
            right.Children.Clear();
            if (filtr1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберете фильтр!");
                return;
            }
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (film.Genre == filtr1.Text.ToString())
                {
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    stackPanel.Children.Add(new Label { Content = film.Id });
                    stackPanel.Children.Add(new Label { Content = film.Name });
                    stackPanel.Children.Add(new Label { Content = film.Genre });
                    stackPanel.Children.Add(new Label { Content = film.Year });
                    right.Children.Add(stackPanel);                                       
                    break;
                }
            }
        }
        private void PrintFilmsAsterWithOutObel(object sender, RoutedEventArgs e)
        {
            foreach (var film in GetAllContentFromTable(dbName, tableName))
            {
                if (film.Contains("Астерикс"))
                {
                    if (film.Contains("Обеликс"))
                    {
                        break;
                    }
                    else
                    {
                        if (!result10.Content.ToString().Contains(film))
                        {
                            result10.Content += film + "\n";
                        }
                    }
                }
            }
        }
        private void AllButtonClick(object sender, RoutedEventArgs e)
        {
            table.Children.Clear();
            Button button = sender as Button;
            List<Film> sorted = new List<Film>();
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (film.Name[0].ToString() == button.Content.ToString())
                {
                    sorted.Add(film);
                }
            }
            List<Film> sortedFilms = sorted.OrderBy(film => film.Name).ToList();
            foreach (var film in sortedFilms)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Children.Add(new Label { Content = film.Id });
                stackPanel.Children.Add(new Label { Content = film.Name });
                stackPanel.Children.Add(new Label { Content = film.Genre });
                stackPanel.Children.Add(new Label { Content = film.Year });
                table.Children.Add(stackPanel);
            }
        }
        private void PrintFilmIsYears(object sender, RoutedEventArgs e)
        {
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (Convert.ToInt32(film.Year) > 1960 && Convert.ToInt32(film.Year) < 2025)
                {
                    if (!result8.Content.ToString().Contains(film.Name + " " + film.Year + " " + film.Genre))
                    {
                        result8.Content += film.Name + " " + film.Year + " " + film.Genre + "\n";
                    }

                }
            }
        }
        private void PrintMusicTracksIsArtist(object sender, RoutedEventArgs e)
        {
            List<string> musics = new List<string>();
            foreach (var artist in artistsCollection)
            {
                if (artist.Name == artistName.Text)
                {                   
                    using (var connection = new SqliteConnection($"Data Source={db2Name}"))
                    {
                        connection.Open();
                        SqliteCommand command = new SqliteCommand();
                        command.Connection = connection;

                        command.CommandText = $"SELECT * FROM {table2Name} WHERE idArtist = {artist.Id}";
                        using var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            musics.Add($"Title: {reader["title"]}, Year: {reader["year"]}, Genre: {reader["genre"]}, Duration: {reader["duration"]}");
                        }
                        reader.Close();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }              
            }
            foreach (var music in musics)
            {
                result7.Content += music;
            }
            if (result7.Content.ToString() == "")
            {
                MessageBox.Show("Исполнителя не сущетвует, или треков не найдено.");
            }
                       
        }
        private void DeleteAll(string dataBaseName, string tableName)
        {
             using (var connection = new SqliteConnection($"Data Source={dataBaseName}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                command.CommandText = $"DELETE FROM {tableName}";
               
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private void PrintIsDelete(object sender, RoutedEventArgs e)
        {
            DeleteAll(dbName, tableName);
            MessageBox.Show("Записи удалены.");
            foreach (var film in GetAllContentFromTable(dbName, tableName))
            {
                if (film == "")
                {
                    MessageBox.Show("Данных не найдено.");
                }
                else
                {
                    resultL8E1.Content += film + "\n";
                }
            }
            
        }
        private void PrintNullFilms(object sender, RoutedEventArgs e)
        {
            
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (film.Duration == null || film.Duration == "0")
                {
                    using (var connection = new SqliteConnection($"Data Source={dbName}"))
                    {
                        connection.Open();
                        SqliteCommand command = new SqliteCommand();
                        command.Connection = connection;

                        command.CommandText = $"UPDATE {tableName} SET duration ='42' WHERE id = '{film.Id}')";
                        command.ExecuteNonQuery();

                        connection.Close();
                    }
                    MessageBox.Show("Данные обновлены");
                }
                else
                {
                    MessageBox.Show("Данные для  обновления не найдены");
                }

            }
            foreach (var film in GetAllContentFromTable(dbName, tableName))
            {
                resultL8E2.Content += film + "\n";
            }

        }
        private void PrintFantastickFilms(object sender, RoutedEventArgs e)
        {
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (film.Genre == "Фантастика")
                {
                    using (var connection = new SqliteConnection($"Data Source={dbName}"))
                    {
                        connection.Open();
                        SqliteCommand command = new SqliteCommand();
                        command.Connection = connection;

                        command.CommandText = $"UPDATE {tableName} SET duration ='{Convert.ToInt32(film.Duration) * 2}' WHERE id = '{film.Id}' AND genre = 'Фантастика')";
                        command.ExecuteNonQuery();

                        connection.Close();
                    }
                    MessageBox.Show("Данные обновлены");
                }
                else
                {
                    MessageBox.Show("Данные для  обновления не найдены");
                }

            }
            foreach (var film in GetAllContentFromTable(dbName, tableName))
            {
                resultL8E3.Content += film + "\n";
            }
        }
        private void PrintIsDeleteAYA(object sender, RoutedEventArgs e)
        {
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (film.Name[0] == 'Я' || film.Name[Name.Length - 1] == 'А')
                {
                    using (var connection = new SqliteConnection($"Data Source={dbName}"))
                    {
                        connection.Open();
                        SqliteCommand command = new SqliteCommand();
                        command.Connection = connection;

                        command.CommandText = $"DELETE FROM {tableName} WHERE id = '{film.Id}'";

                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }         
            MessageBox.Show("Записи удалены.");
            foreach (var film in GetAllContentFromTable(dbName, tableName))
            {              
                
                resultL8E4.Content += film + "\n";
                
            }

        }
        private void PrintIsDeleteBOEVIK(object sender, RoutedEventArgs e)
        {
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (Convert.ToInt32(film.Duration) >= 90)
                {
                    using (var connection = new SqliteConnection($"Data Source={dbName}"))
                    {
                        connection.Open();
                        SqliteCommand command = new SqliteCommand();
                        command.Connection = connection;

                        command.CommandText = $"DELETE FROM {tableName} WHERE id = '{film.Id}'";

                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    MessageBox.Show("Записи удалены.");
                }
                else
                {
                    MessageBox.Show("Записи для удаления не найдены.");

                }
            }
           
            foreach (var film in GetAllContentFromTable(dbName, tableName))
            {

                resultL8E6.Content += film + "\n";

            }

        }
        private void PrintUpdateMusikle(object sender, RoutedEventArgs e)
        {
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (film.Genre == "Мюзикл" && Convert.ToInt32(film.Duration) > 100)
                {
                    using (var connection = new SqliteConnection($"Data Source={dbName}"))
                    {
                        connection.Open();
                        SqliteCommand command = new SqliteCommand();
                        command.Connection = connection;

                        command.CommandText = $"UPDATE {tableName} SET duration ='{100}' WHERE id = '{film.Id}' AND genre = 'Мюзикл')";
                        command.ExecuteNonQuery();

                        connection.Close();
                    }
                    MessageBox.Show("Данные обновлены");
                }
                else
                {
                    MessageBox.Show("Данные для  обновления не найдены");
                }

            }
            foreach (var film in GetAllContentFromTable(dbName, tableName))
            {
                resultL8E7.Content += film + "\n";
            }
        }
        private void PrintShoortingFilm(object sender, RoutedEventArgs e)
        {
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (film.Year == "1973")
                {
                    using (var connection = new SqliteConnection($"Data Source={dbName}"))
                    {
                        connection.Open();
                        SqliteCommand command = new SqliteCommand();
                        command.Connection = connection;

                        command.CommandText = $"UPDATE {tableName} SET duration ='{Convert.ToInt32(film.Duration) / 3}' WHERE id = '{film.Id}')";
                        command.ExecuteNonQuery();

                        connection.Close();
                    }
                    MessageBox.Show("Данные обновлены");
                }
                else
                {
                    MessageBox.Show("Данные для  обновления не найдены");
                }

            }
            foreach (var film in GetAllContentFromTable(dbName, tableName))
            {
                resultL8E8.Content += film + "\n";
            }
        }
        private void PrintOldFantastick(object sender, RoutedEventArgs e)
        {
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (film.Genre == "Фантастика" && Convert.ToInt32(film.Year) < 2000 && Convert.ToInt32(film.Duration) > 90)
                {
                    using (var connection = new SqliteConnection($"Data Source={dbName}"))
                    {
                        connection.Open();
                        SqliteCommand command = new SqliteCommand();
                        command.Connection = connection;

                        command.CommandText = $"DELETE FROM {tableName} WHERE id = '{film.Id}'";
                        command.ExecuteNonQuery();

                        connection.Close();
                    }
                    MessageBox.Show("Данные удалены");
                }
                else
                {
                    MessageBox.Show("Данные для удаления не найдены");
                }

            }
            foreach (var film in GetAllContentFromTable(dbName, tableName))
            {
                resultL8E9.Content += film + "\n";
            }
        }

        private void PrintFilm(object sender, RoutedEventArgs e)
        {
            resluting.Children.Clear();
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {
                if (film.Id.ToString() == idInput.Text)
                {
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    stackPanel.Children.Add(new CheckBox());
                    stackPanel.Children.Add(new Label { Content = film.Id });
                    stackPanel.Children.Add(new Label { Content = film.Name });
                    stackPanel.Children.Add(new Label { Content = film.Genre });
                    stackPanel.Children.Add(new Label { Content = film.Year });
                    resluting.Children.Add(stackPanel);
                }
            }
           
        }

        private void ChangeFilm(object sender, RoutedEventArgs e)
        {
            foreach(StackPanel stack in resluting.Children)
            {
                if (((CheckBox)stack.Children[0]).IsChecked == true)
                {
                    int idChangedFilm = (int)((Label)stack.Children[1]).Content;


                    foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
                    {
                        
                        if (film.Id == idChangedFilm)
                        {
                            using (var connection = new SqliteConnection($"Data Source={dbName}"))
                            {
                                connection.Open();
                                SqliteCommand command = new SqliteCommand();
                                command.Connection = connection;
                                //// Дописать SET изходя из задания
                                command.CommandText = $"UPDATE {tableName} SET " +
                                    $"title = '{new string(film.Name.Reverse().ToArray())}', " +
                                    $"year = '{Convert.ToInt32(film.Year) + 1000}', " +
                                    $"duration = '{Convert.ToInt32(film.Duration) * 2}' " +
                                    $"WHERE id = '{film.Id}'";
                                command.ExecuteNonQuery();

                                connection.Close();
                                MessageBox.Show("Данные обновлены");
                            }                            
                        }                      
                    }
                    foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
                    {
                        if (film.Id == idChangedFilm)
                        {
                            MessageBox.Show("Название: " + film.Name + " Год: " + film.Year + " Жанр: " + film.Genre + " Длительность: " + film.Duration + "\n");
                        }
                    }                  
                    break;
                }
            }

        }     
        private void LoadFilmsToTable()
        {
            int counter = 1;
            foreach (var film in GetAllContentWithFilmsFormat(dbName, tableName))
            {              
                Label labelCounter = new Label 
                { 
                    Content = counter.ToString(),
                    Width = ((Label)titleTable2.Children[0]).Width,
                    Height = ((Label)titleTable2.Children[0]).Height,
                    HorizontalContentAlignment = ((Label)titleTable2.Children[0]).HorizontalContentAlignment,
                    VerticalContentAlignment = ((Label)titleTable2.Children[0]).VerticalContentAlignment,
                    BorderBrush = ((Label)titleTable2.Children[0]).BorderBrush,
                    BorderThickness = ((Label)titleTable2.Children[0]).BorderThickness
                };
                Label labelId = new Label
                {
                    Content = film.Id,
                    Width = ((Label)titleTable2.Children[1]).Width,
                    Height = ((Label)titleTable2.Children[1]).Height,
                    HorizontalContentAlignment = ((Label)titleTable2.Children[1]).HorizontalContentAlignment,
                    VerticalContentAlignment = ((Label)titleTable2.Children[1]).VerticalContentAlignment,
                    BorderBrush = ((Label)titleTable2.Children[1]).BorderBrush,
                    BorderThickness = ((Label)titleTable2.Children[1]).BorderThickness
                };
                Label labelName = new Label
                {
                    Content = film.Name,
                    Width = ((Label)titleTable2.Children[2]).Width,
                    Height = ((Label)titleTable2.Children[2]).Height,
                    HorizontalContentAlignment = ((Label)titleTable2.Children[2]).HorizontalContentAlignment,
                    VerticalContentAlignment = ((Label)titleTable2.Children[2]).VerticalContentAlignment,
                    BorderBrush = ((Label)titleTable2.Children[2]).BorderBrush,
                    BorderThickness = ((Label)titleTable2.Children[2]).BorderThickness
                };
                Label labelYear = new Label
                {
                    Content = film.Year,
                    Width = ((Label)titleTable2.Children[3]).Width,
                    Height = ((Label)titleTable2.Children[3]).Height,
                    HorizontalContentAlignment = ((Label)titleTable2.Children[3]).HorizontalContentAlignment,
                    VerticalContentAlignment = ((Label)titleTable2.Children[3]).VerticalContentAlignment,
                    BorderBrush = ((Label)titleTable2.Children[3]).BorderBrush,
                    BorderThickness = ((Label)titleTable2.Children[3]).BorderThickness
                };
                Label labelGenre = new Label
                {
                    Content = film.Genre,
                    Width = ((Label)titleTable2.Children[4]).Width,
                    Height = ((Label)titleTable2.Children[4]).Height,
                    HorizontalContentAlignment = ((Label)titleTable2.Children[4]).HorizontalContentAlignment,
                    VerticalContentAlignment = ((Label)titleTable2.Children[4]).VerticalContentAlignment,
                    BorderBrush = ((Label)titleTable2.Children[4]).BorderBrush,
                    BorderThickness = ((Label)titleTable2.Children[4]).BorderThickness
                };
                Label labelDuration = new Label
                {
                    Content = film.Duration,
                    Width = ((Label)titleTable2.Children[5]).Width,
                    Height = ((Label)titleTable2.Children[5]).Height,
                    HorizontalContentAlignment = ((Label)titleTable2.Children[5]).HorizontalContentAlignment,
                    VerticalContentAlignment = ((Label)titleTable2.Children[5]).VerticalContentAlignment,
                    BorderBrush = ((Label)titleTable2.Children[5]).BorderBrush,
                    BorderThickness = ((Label)titleTable2.Children[5]).BorderThickness
                };
                Button buttonChanged = new Button
                {
                    Content = "Изменить",
                    Width = ((Label)titleTable2.Children[6]).Width,
                    Height = ((Label)titleTable2.Children[6]).Height,
                    HorizontalContentAlignment = ((Label)titleTable2.Children[6]).HorizontalContentAlignment,
                    VerticalContentAlignment = ((Label)titleTable2.Children[6]).VerticalContentAlignment,
                    BorderBrush = ((Label)titleTable2.Children[6]).BorderBrush,
                    BorderThickness = ((Label)titleTable2.Children[6]).BorderThickness
                };
                CheckBox checkBox = new CheckBox
                {
                    Width = ((Label)titleTable2.Children[7]).Width,
                    Height = ((Label)titleTable2.Children[7]).Height,
                    HorizontalContentAlignment = ((Label)titleTable2.Children[7]).HorizontalContentAlignment,
                    VerticalContentAlignment = ((Label)titleTable2.Children[7]).VerticalContentAlignment,
                    BorderBrush = ((Label)titleTable2.Children[7]).BorderBrush,
                    BorderThickness = ((Label)titleTable2.Children[7]).BorderThickness
                    
                };
                StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal, Children = { labelCounter, labelId , labelName, labelYear, labelGenre, labelDuration, buttonChanged, checkBox } };

                resluting2.Children.Add(stackPanel);
                counter++;
            }
        }
        private void BtnChangeClick(object sender, EventArgs e)
        {
            StackPanel parentStack = ((Button)sender).Parent as StackPanel;

            

        }
        private void AddFilm(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
public class Film
{
    public string Name { get; set; }
    public string Year { get; set; }
    public string Genre { get; set; }
    public string Duration { get; set; }
    public int Id { get; set; }


    public Film(string name, string year, string genre, string duration, int id)
    {
        Name = name;
        Year = year;
        Genre = genre;
        Duration = duration;
        Id = id;
    }
}

public class MusicTrack
{
    public string Name { get; set; }
    public string Year { get; set; }
    public string Genre { get; set; }
    public string Duration { get; set; }
    public int Id { get; set; }


    public MusicTrack(string name, string year, string genre, string duration, int id)
    {
        Name = name;
        Year = year;
        Genre = genre;
        Duration = duration;
        Id = id;
    }
}

public class Artist
{
    public string Name { get; set; }
    public List<MusicTrack> Tracks = new List<MusicTrack>();
    public int Id { get; set; }


    public Artist(string name, int id, List<MusicTrack> tracks)
    {
        Name = name;       
        Id = id;
        Tracks = tracks;
    }
}