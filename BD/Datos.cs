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
    public partial class Datos : Form
    {
        Form1 principal;
        public Datos(Form1 p)
        {
            InitializeComponent();
            principal = p;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text!= "" && txtTelefono.Text != "")
            {
                insertarContacto();
                principal.rellenarContactos();
            }
            else
            {
                MessageBox.Show("falta llenar algun dato");
            }
        }

        private void insertarContacto()
        {
            principal.conexion.Open();
            String consulta = "insert into contactos values ('"+txtNombre.Text+"','"+txtTelefono.Text+"')";
            MySqlCommand cmd = new MySqlCommand(consulta, principal.conexion);
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                MessageBox.Show("contacto añadido con exito");
                txtNombre.Clear();
                txtTelefono.Clear();
               
            }

            principal.conexion.Close();
            
        }
    }
}
