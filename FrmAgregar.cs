using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //Import para conexion con Access
using System.Data;//SQLSERVER
using System.Data.SqlClient;//SQLSERVER

using System.Configuration;

namespace CursoDotNetProyectoEmpresa
{
    public partial class FrmAgregar : Form
    {

        Validacion validacion = new Validacion();        

        public FrmAgregar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAgregar_Load(object sender, EventArgs e)
        {
            interfazInicial();
        }

        public void interfazInicial()
        {
            txtId.Enabled = true;
            txtNombre.Enabled = false;
            txtDireccion.Enabled = false;
            txtEdad.Enabled = false;
            btnGuardar.Enabled = false;
        }


        public void interfazBusqueda()
        {
            txtId.Enabled = false;
            txtNombre.Enabled = true;
            txtDireccion.Enabled = true;
            txtEdad.Enabled = true;
            btnGuardar.Enabled = true;
            txtNombre.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!txtId.Text.Equals(""))
            {
                int codigo = Convert.ToInt32(txtId.Text);
                if (buscarRegristo(codigo))
                {
                    MessageBox.Show("El registro ya existe");
                    txtId.Focus();
                }
                else
                {
                    interfazBusqueda();
                }
            }
            else
            {
                MessageBox.Show("Ingrese el valor de busqueda");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(txtId.Text);
            String nombre = txtNombre.Text;
            String direccion = txtNombre.Text;
            int edad = Convert.ToInt32(txtEdad.Text);

            if (guardarRegistro(id, nombre, direccion, edad))
            {
                limpiarCampos();
                interfazInicial();
                MessageBox.Show("Almacenado con exito");
            }
        }


        public bool guardarRegistro(int id, String nombre, String direccion, int edad)
        {

            String cadena = ConfigurationManager.ConnectionStrings["conexionBD"].ToString();
            //String cadena = "Data Source=KAKASHISITO\\SQLEXPRESS2;Initial Catalog=ejemploConexionBD;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(cadena);

            String sql = "INSERT INTO personal(id,nombre,direccion,edad)";
            sql = sql + "VALUES ( " + id + ",";
            sql = sql + "'" + nombre + "',";
            sql = sql + "'" + direccion + "',";
            sql = sql + edad + ")";

            SqlCommand comando = new SqlCommand(sql, conexion);

            conexion.Open();

            comando.ExecuteNonQuery();
            conexion.Close();
            return true;
        }

        public void limpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtEdad.Clear();
        }




        private bool buscarRegristo(int codigo)
        {
            /*
            OleDbConnection conexion = new OleDbConnection();
            
            conexion.ConnectionString = " Provider=Microsoft.ACE.OLEBD.12.0; Data Source= e:\\BBDDmaestra.accdb; Persist Security Info = false";
            String sql = "SELECT * from Personal where id = " + codigo;

            OleDbDataAdapter adaptador = new OleDbDataAdapter(sql, conexion);

            DataSet dataset = new DataSet();
            conexion.Open();
            adaptador.Fill(dataset);
            conexion.Close();*/

            //String cadena = "Data Source=KAKASHISITO\\SQLEXPRESS2;Initial Catalog=ejemploConexionBD;Integrated Security=True";
            String cadena = ConfigurationManager.ConnectionStrings["conexionBD"].ToString();
            String sql = "SELECT * from Personal where id = " + codigo;
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataSet dataset = new DataSet();
            adaptador.Fill(dataset);
            conexion.Close();

            if (dataset.Tables[0].Rows.Count == 0)
            {
                dataset.Dispose();
                return false;
            }
            else
            {
                txtNombre.Text = dataset.Tables[0].Rows[0]["nombre"].ToString();
                txtEdad.Text = dataset.Tables[0].Rows[0]["edad"].ToString();
                txtDireccion.Text = dataset.Tables[0].Rows[0]["direccion"].ToString();
                dataset.Dispose();
                return true;
            }


        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)13){ // si es enter
                e.Handled = true; //no ponga el enter en el campo de text
                SendKeys.Send("{TAB}");//se manda un TAB a la interfaz
            }
            else
            {
                e.Handled = false; //ponga el valor ingresado en el campo de text
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            { // si es enter
                e.Handled = true; //no ponga el enter en el campo de text
                SendKeys.Send("{TAB}");//se manda un TAB a la interfaz
            }
            else
            {
                e.Handled = false; //ponga el valor ingresado en el campo de text
            }
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            { // si es enter
                e.Handled = true; //no ponga el enter en el campo de text
                SendKeys.Send("{TAB}");//se manda un TAB a la interfaz
            }
            else
            {
                e.Handled = validacion.numeros(e.KeyChar); //ponga el valor ingresado en el campo de text
            }            
        }



    }
}
