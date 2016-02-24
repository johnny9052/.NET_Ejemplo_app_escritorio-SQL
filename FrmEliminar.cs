using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;//SQLSERVER
using System.Data.SqlClient;//SQLSERVER
using System.Configuration;


namespace CursoDotNetProyectoEmpresa
{
    public partial class FrmEliminar : Form
    {
        public FrmEliminar()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!txtId.Text.Equals(""))
            {
                int codigo = Convert.ToInt32(txtId.Text);
                if (buscarRegristo(codigo))
                {
                    DialogResult confirmacion = MessageBox.Show("Confirma que desea eliminarlo?", "No se elimino el registro", MessageBoxButtons.YesNo);

                    if (confirmacion == DialogResult.Yes)
                    {
                        interfazBusqueda();
                        int id = Convert.ToInt32(txtId.Text);

                        if (eliminarRegistro(id))
                        {
                            limpiarCampos();
                            interfazInicial();
                            MessageBox.Show("Eliminado con exito");
                        }
                    }
                    else
                    {
                        limpiarCampos();
                        interfazInicial();
                    }
                }
                else
                {
                    MessageBox.Show("El registro no existe");
                    txtId.Focus();
                }
            }
            else
            {
                MessageBox.Show("Ingrese el valor de busqueda");
            }
        }

        private void FrmEliminar_Load(object sender, EventArgs e)
        {
            interfazInicial();
        }


        public void interfazInicial()
        {
            txtId.Enabled = true;
            txtNombre.Enabled = false;
            txtDireccion.Enabled = false;
            txtEdad.Enabled = false;

        }


        public void interfazBusqueda()
        {
            txtId.Enabled = false;
            txtNombre.Enabled = true;
            txtDireccion.Enabled = true;
            txtEdad.Enabled = true;

            txtNombre.Focus();
        }


        public bool eliminarRegistro(int id)
        {
            //String cadena = "Data Source=KAKASHISITO\\SQLEXPRESS;Initial Catalog=prueba2DB;Integrated Security=True";
            String cadena = ConfigurationManager.ConnectionStrings["conexionBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadena);

            String sql = "DELETE from Personal where id=" + id;

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

            //String cadena = "Data Source=KAKASHISITO\\SQLEXPRESS;Initial Catalog=prueba2DB;Integrated Security=True";
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

    }
}
