using MySql.Data.MySqlClient;
using SakilaModoConectado.Models;
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

namespace SakilaModoConectado
{
    /// <summary>
    /// Lógica de interacción para WndActor.xaml
    /// </summary>
    public partial class WndActor : Window
    {
        public MySqlConnection _conection { get; set; }
        List<Actor> _actors { get; set; }

        public WndActor()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string conexionString = "database=sakila;server=localhost;uid=root;password=root";
            _conection = new MySqlConnection(conexionString);
            try
            {
                _conection.Open();
                status.Text = "You're connected!";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            _conection.Clone();
            status.Text = "";
        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "SELECT * FROM actor WHERE first_name LIKE @filtro";
            MySqlCommand command = new MySqlCommand(consulta, _conection);
            command.Parameters.AddWithValue("@filtro", txtFiltro.Text + "%");
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                _actors = new List<Actor>();
                while (reader.Read())
                {
                    _actors.Add(
                        new Actor
                        {
                            actor_id = (ushort)reader[0],
                            first_name = (string)reader[1],
                            last_name = (string)reader[2],
                            last_update = (DateTime)reader[3]
                        }
                    );
                }
                dgActors.ItemsSource = _actors;
            }
            reader.Close();
        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "SELECT * FROM actor";
            MySqlCommand command = new MySqlCommand(consulta, _conection);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                _actors = new List<Actor>();
                while (reader.Read())
                {
                    _actors.Add(
                        new Actor
                        {
                            actor_id = (ushort)reader[0],
                            first_name = (string)reader[1],
                            last_name = (string)reader[2],
                            last_update = (DateTime)reader[3]
                        }
                    );
                }
                dgActors.ItemsSource = _actors;
            }
            reader.Close();
        }
    }
}
