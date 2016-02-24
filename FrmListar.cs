using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CursoDotNetProyectoEmpresa
{
    public partial class FrmListar : Form
    {
        public FrmListar()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmListar_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ejemploConexionBDDataSet.personal' Puede moverla o quitarla según sea necesario.
            this.personalTableAdapter1.Fill(this.ejemploConexionBDDataSet.personal);
            // TODO: esta línea de código carga datos en la tabla 'prueba2DBDataSet.Personal' Puede moverla o quitarla según sea necesario.
            this.personalTableAdapter.Fill(this.prueba2DBDataSet.Personal);
            // TODO: esta línea de código carga datos en la tabla 'prueba2DBDataSet.Personal' Puede moverla o quitarla según sea necesario.
            this.personalTableAdapter.Fill(this.prueba2DBDataSet.Personal);

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.personalTableAdapter.FillBy(this.prueba2DBDataSet.Personal);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillByToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.personalTableAdapter.FillBy(this.prueba2DBDataSet.Personal);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
