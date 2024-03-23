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

namespace HotelProyectoFinal
{
    public partial class UCServicios : UserControl
    {

        DBOperaciones Operaciones = new DBOperaciones();
        List<DTOS.AsignacionHabitacionDTO> habitaciones = new List<DTOS.AsignacionHabitacionDTO>();  
        List<REGISTRO> registros = new List<REGISTRO>();    
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
                var transacciones = Operaciones.ObtenerHuespedesConReserva(txtDPI.Text);

                foreach (var transaccion in transacciones)
                {
                    if (transaccion.IdHuesped == txtDPI.Text && transaccion.Comentario == cmbxHabitacion.Text)
                    {
                        using (DBHOTELEntities db = new DBHOTELEntities())
                        {
                            TRANSACCION_DETALLE transaccionDetalle = new TRANSACCION_DETALLE();
                            transaccionDetalle.IdServicio = 1;
                            transaccionDetalle.Cantidad = Int32.Parse(NUCantidad.Value.ToString());
                            transaccionDetalle.IdHuesped = txtDPI.Text;

                            decimal precio = (decimal)(from servicio in db.SERVICIOs
                                             where servicio.IdServicio == transaccionDetalle.IdServicio
                                             select servicio.Precio).FirstOrDefault();

                            transaccionDetalle.Total = transaccionDetalle.Cantidad ;
                        }
                    }
                }
            }

        }

        private void NUCantidad_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
