using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelProyectoFinal.Controllers;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using HotelProyectoFinal.Models;
using HotelProyectoFinal.Properties;
using System.Security.Cryptography;

namespace HotelProyectoFinal.Views
{

   
    public partial class Pago : UserControl
    {
        TransaccionController Transaccion = new TransaccionController();
        DBOperaciones Operaciones = new DBOperaciones();
        List<DTOS.Transaccion> transacciones = new List<DTOS.Transaccion>();
        List<FACTURA_DETALLE> facturacion = new List<FACTURA_DETALLE>();
        FacturaController Factura = new FacturaController();
        int tipodocumento = 0;
        public Pago()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var huespedes = Operaciones.ObtenerInformacionHuesped(txtCliente.Text);

            if (huespedes.Count() != 0)
            {
                transacciones = Transaccion.ObtenerTodasLasTransacciones(txtCliente.Text);

                if (transacciones != null)
                {
                    ActualizarGrid(transacciones);
                };
            }
            else
            {
                MessageBox.Show("Usuario no registrado");
            }




        }



        public void ActualizarGrid(List<DTOS.Transaccion> Transacciones)
        {
            dataGridView1.Rows.Clear();
            foreach (var transaccion in Transacciones)
            {
                int rowIndex = dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells["ID"].Value = transaccion.IdTransaccion;
                dataGridView1.Rows[rowIndex].Cells["TIPOSERVICIO"].Value = transaccion.TipoServicio;
                dataGridView1.Rows[rowIndex].Cells["FECHA"].Value = transaccion.Fecha;
                dataGridView1.Rows[rowIndex].Cells["PRECIO"].Value = "Q." + transaccion.precio;
                dataGridView1.Rows[rowIndex].Cells["CANTIDAD"].Value = transaccion.Cantidad;
                dataGridView1.Rows[rowIndex].Cells["TOTAL"].Value = "Q." + transaccion.Total;


            }


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton3.Checked==true) {

                txtTotal.Text = "Q." + Transaccion.TotalporPagar(transacciones).ToString();

            }
            else
            {
                txtTotal.Text = "Q.0.00"; 
            }
            dataGridView1.Columns[7].Visible = false;
            btnCalcular.Visible = false;
            lblSubTotal.Visible = false;
            txtSubTotal.Visible= false;

        }

        private void Pago_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void rdbTC_CheckedChanged(object sender, EventArgs e)
        {
            tipodocumento = 1003;
        }

        private void rdbTD_CheckedChanged(object sender, EventArgs e)
        {
            tipodocumento = 1004;
        }

        private void rdbEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            tipodocumento = 1006;
        }

        private void rdbSeparado_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns[7].Visible = true;
            btnCalcular.Visible = true;
            lblSubTotal.Visible = true;
            txtSubTotal.Visible = true;
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            transacciones = Transaccion.ObtenerTodasLasTransacciones(txtCliente.Text);

            if (transacciones != null)
            {
                Factura.AgregarFactura(txtCliente.Text,
                       DateTime.Now,
                       txtNombre.Text,
                       txtDireccion.Text,
                       txtNIT.Text,
                       transacciones);
            };
        }

        private void cbCF_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCF.Checked)
            {
                txtNombre.Text = "C/F";
                txtNIT.Text = "C/F";
                txtDireccion.Text = "Ciudad Guatemala";
            }
            else
            {
                txtNombre.Text = "";
                txtNIT.Text = "";
                txtDireccion.Text = "";
            }
        }
    }
}
