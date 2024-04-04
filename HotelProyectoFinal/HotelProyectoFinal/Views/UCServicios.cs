using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelProyectoFinal.Models;
using HotelProyectoFinal.Controllers;

namespace HotelProyectoFinal
{
    public partial class UCServicios : UserControl
    {

        DBOperaciones Operaciones = new DBOperaciones();
        List<DTOS.AsignacionHabitacionDTO> habitaciones = new List<DTOS.AsignacionHabitacionDTO>();  
        List<REGISTRO> registros = new List<REGISTRO>();
        TransaccionController Transaccion = new TransaccionController();
        List<DTOS.RegistroHuespedDTO> transacciones = new List<DTOS.RegistroHuespedDTO>();
        ServicioController servicio = new ServicioController();

        int IdHabitacion = 0;
        DateTime FechaInicio = DateTime.Now;
        DateTime FechaFinal = DateTime.Now;
        public UCServicios()
        {
            InitializeComponent();
        }


        private void UCServicios_Load(object sender, EventArgs e)
        {
            CargarCategoriServicios();
        }

        private void CargarCategoriServicios()
        {
            try
            {
                using (DBHOTELEntities db = new DBHOTELEntities())
                {
                    var categorias = (from c in db.TipoServicios
                                     select c.Descripcion).Distinct().ToList();

                    foreach (var categoria in categorias)
                    {
                        cmbxTipoServicio.Items.Add(categoria);
                    }

                    if (categorias.Count > 0)
                    {
                        cmbxTipoServicio.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void cmbxTipoServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (DBHOTELEntities db = new DBHOTELEntities())
            {
                cmbxServicio.Items.Clear();
                //Consultamos las descripciones que esten Disponibles
                var descripciones = from servicio in db.SERVICIOs
                                    join tipoServicio in db.TipoServicios
                                    on servicio.IdTipoServicio equals tipoServicio.IdTipoServicio
                                    where tipoServicio.Descripcion == cmbxTipoServicio.Text
                                    select servicio.Descripcion;

                //Cargamos las descripciones al combobox
                foreach (var descripcion in descripciones)
                {
                    cmbxServicio.Items.Add(descripcion);
                }

                if (descripciones != null)
                {
                    cmbxServicio.SelectedIndex = 0;
                }
            }
        }

        private void txtDPI_TextChanged(object sender, EventArgs e)
        {

        }


        private void Buscar_Click(object sender, EventArgs e)
        {

            registros = Operaciones.ObtenerInformacionHuespedconRegistro(txtDPI.Text);

            if (registros.Count() > 0)
            {
                foreach (var registro in registros)
                {
                    habitaciones.AddRange(Operaciones.ObtenerHabitacionconRegistro(registro.IdRegistro));
                }

                foreach (var habitacion in habitaciones)
                {
                    cmbxHabitacion.Items.Add(habitacion.Comentario);
                }

                cmbxHabitacion.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Usuario sin reservaciones activas", "Informacion no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {


            if (registros.Count() > 0)
            {
                 transacciones = Operaciones.ObtenerHuespedesConReserva(txtDPI.Text);

                foreach (var transaccion in transacciones)
                {
                    if (transaccion.IdHuesped == txtDPI.Text && transaccion.Comentario == cmbxHabitacion.Text)
                    {

                        Transaccion.AgregarServicio(Int32.Parse(NUCantidad.Value.ToString()),
                                                    txtDPI.Text,
                                                    transaccion.IdRegistro,
                                                    transaccion.IdHabitacion,
                                                    cmbxServicio.Text,
                                                    1);

                    }
                }

                if (txtDPI.Text !="")
                {
                    foreach (var transaccion in transacciones)
                    {
                        if (transaccion.Comentario == cmbxHabitacion.Text)
                        {
                            IdHabitacion = transaccion.IdHabitacion;
                            FechaInicio = (DateTime)transaccion.FechaReservaInicio;
                            FechaFinal = (DateTime)transaccion.FechaReservaFinal;
                        }
                    }

                    ActualizarGrid(Transaccion.ObtenerTransacciones(txtDPI.Text, IdHabitacion, FechaInicio, FechaFinal));

                }

            }

        }


        private void cmbxHabitacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (registros.Count() > 0)
            {
                transacciones = Operaciones.ObtenerHuespedesConReserva(txtDPI.Text);


                if (txtDPI.Text != "")
                {
                    foreach (var transaccion in transacciones)
                    {
                        if (transaccion.Comentario == cmbxHabitacion.Text)
                        {
                            IdHabitacion = transaccion.IdHabitacion;
                            FechaInicio = (DateTime)transaccion.FechaReservaInicio;
                            FechaFinal = (DateTime)transaccion.FechaReservaFinal;
                        }
                    }

                    ActualizarGrid(Transaccion.ObtenerTransacciones(txtDPI.Text, IdHabitacion, FechaInicio, FechaFinal));

                }
            }
        }



        private void ActualizarGrid(List<DTOS.Transaccion> Transacciones)
        {
            dataGridView1.Rows.Clear();
            foreach (var transaccion in Transacciones)
            {
                int rowIndex = dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells["ID"].Value = transaccion.IdTransaccion;
                dataGridView1.Rows[rowIndex].Cells["TIPOSERVICIO"].Value = transaccion.TipoServicio;
                dataGridView1.Rows[rowIndex].Cells["FECHA"].Value = transaccion.Fecha;
                dataGridView1.Rows[rowIndex].Cells["PRECIO"].Value = "Q."+transaccion.precio;
                dataGridView1.Rows[rowIndex].Cells["CANTIDAD"].Value = transaccion.Cantidad;
                dataGridView1.Rows[rowIndex].Cells["TOTAL"].Value = "Q." + transaccion.Total;


            }


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void cmbxServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            textPrecio.Text = "Q."+ servicio.ObtenerPrecio(cmbxServicio.Text);
        }


        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegurar que se haya hecho clic en una fila (no en el encabezado)
            {
                DialogResult resultado = MessageBox.Show("Esta seguro que quiere eliminar este servicio", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes) { 
                    DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];

                    Transaccion.CancelarServicio(Int32.Parse( fila.Cells["ID"].Value.ToString()));
                }
            }
        }
    }
}
