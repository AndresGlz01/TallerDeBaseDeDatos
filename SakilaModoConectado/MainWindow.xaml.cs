using MySql.Data.MySqlClient;
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

namespace SakilaModoConectado
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

        public MySqlConnection conexion { get; set; }
        List<Country> countries { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string conexionString = "database=sakila;server=localhost;uid=root;password=root";
            conexion = new MySqlConnection(conexionString);
            try
            {
                conexion.Open();
                chkConectado.IsChecked = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            conexion.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "SELECT * FROM country";
            MySqlCommand command = new MySqlCommand(consulta, conexion);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                countries = new List<Country>();
                while (reader.Read())
                {
                    countries.Add(
                        new Country
                        {
                            country_id = (ushort)reader[0],
                            country = (string)reader[1],
                            last_update = (DateTime)reader[2]
                        }
                    );
                }
                gpCountries.ItemsSource = countries;
            }
            reader.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string consulta = $"SELECT * FROM country WHERE country.country LIKE @filtro";
            string filtro =  txtFIltro.Text + "%";
            MySqlCommand command = new MySqlCommand(consulta, conexion);
            command.Parameters.AddWithValue("@filtro", filtro);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                countries = new List<Country>();
                while (reader.Read())
                {
                    countries.Add(
                        new Country
                        {
                            country_id = (ushort)reader[0],
                            country = (string)reader[1],
                            last_update = (DateTime)reader[2]
                        }
                    );
                }
                gpCountries.ItemsSource = countries;
            }
            reader.Close();
        }
    }
}
