using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public partial class Form1 : Form
    {
        public MySqlConnection conexion;
        public Form1()
        {
            InitializeComponent();
            connectarBd();
            rellenarContactos();
        }

        public void rellenarContactos()
        {
            cb.Items.Clear();
            conexion.Open();
            String consulta = "select * from contactos";
            MySqlCommand cmd = new MySqlCommand(consulta, conexion);
           MySqlDataReader rd= cmd.ExecuteReader();

            while (rd.Read())
            {
                cb.Items.Add(rd.GetString(0));
            }
            rd.Close();
            conexion.Close();
        }

        private void connectarBd()
        {
            MySqlConnectionStringBuilder builder =new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "agenda";

            conexion = new MySqlConnection(builder.ToString());
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Datos ventana = new Datos(this);
            ventana.Show();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            String consulta = "select telefono from contactos where nombre =@nom";
            MySqlCommand cmd = new MySqlCommand(consulta, conexion);
            cmd.Parameters.AddWithValue("@nom", cb.SelectedItem);
            cmd.Prepare();
           MySqlDataReader rd= cmd.ExecuteReader();
            while (rd.Read())
            {
                MessageBox.Show("el telefono es" + rd.GetString(0));
                //o MessageBox.Show("el telefono es" + rd.GetString("telefono"));
            }
            rd.Close();
            conexion.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (cb.SelectedIndex != -1)
            {
                conexion.Open();
                String consulta = "delete from contactos where nombre= @nom";
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@nom", cb.SelectedItem);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                MessageBox.Show("ha sido eliminado");
                conexion.Close();
                rellenarContactos();
            }
            else
            {
                MessageBox.Show("debes seleccionar nombre");
            }
        }
    }
}
