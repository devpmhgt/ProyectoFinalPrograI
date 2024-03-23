using HotelProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelProyectoFinal.Views
{
    public partial class UCRegistro : UserControl
    {

        DTOS.RegistroHuespedDTO CancelarReservacion = new DTOS.RegistroHuespedDTO();
        DBOperaciones Operaciones = new DBOperaciones();
        public UCRegistro()
        {
            InitializeComponent();
        }

        private void UCRegistro_Load_1(object sender, EventArgs e)
        {
            Cargarhabitaciones();


        }


        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            if (Operaciones.ObtenerHuespedesConReserva(txtDPI.Text).Count > 0)
            {
                lblSinRegistro.Text = "Usuario con reservacion activa";
                lblSinRegistro.ForeColor = Color.Green;
                lblSinRegistro.Visible = true;
                btnModificar.Visible = true;
            }

            var huespedes = Operaciones.ObtenerInformacionHuesped(txtDPI.Text);
            if (huespedes.Count > 0)
            {
                foreach (var huesped in huespedes)
                {
                    txtNombre.Text = huesped.Nombres;
                    txtApellidos.Text = huesped.Apellidos;
                    txtDireccion.Text = huesped.Direccion;
                    txtDPI.Text = huesped.DPI;

                }

                btnModificar.Visible = true;
                cmbxNivel.Text = "";
                cmbxTipoHabitacion.Text = "";

            }
            else
            {
                MessageBox.Show("Usuario no registrado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                limpiarCampos();
            }


            var registros = Operaciones.ObtenerHuespedesConReserva(txtDPI.Text);


            ActualizarDataGrid(registros);



        }

        public void ActualizarDataGrid(List<DTOS.RegistroHuespedDTO> registros)
        {
            dataGridView1.Rows.Clear();
            foreach (var registro in registros)
            {
                int rowIndex = dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells["IDREGISTRO"].Value = registro.IdRegistro;
                dataGridView1.Rows[rowIndex].Cells["NIVEL"].Value = registro.Nivel;
                dataGridView1.Rows[rowIndex].Cells["HABITACION"].Value = registro.Comentario;
                dataGridView1.Rows[rowIndex].Cells["FECHARESERVACION"].Value = registro.FechaReservacion;
                dataGridView1.Rows[rowIndex].Cells["DEL"].Value = registro.FechaReservaInicio;
                dataGridView1.Rows[rowIndex].Cells["HASTA"].Value = registro.FechaReservaFinal;
                dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].ReadOnly = true;


                if (registro.Ingreso != null)
                {
                    dataGridView1.Rows[rowIndex].Cells["INGRESO"].Value = registro.Ingreso;
                    dataGridView1.Rows[rowIndex].Cells["CHECKIN"].Value = "Si";
                    dataGridView1.Rows[rowIndex].Cells["CHECKIN"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[rowIndex].Cells["CHECKIN"].ReadOnly = true;
                    dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].ReadOnly = false;
                    dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].Style.BackColor = Color.White;

                }

                if (registro.Salida != null)
                {
                    dataGridView1.Rows[rowIndex].Cells["SALIDA"].Value = registro.Salida;
                    dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].Value = "Si";
                    dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].ReadOnly = true;
                }

            }


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }


        private void limpiarCampos()
        {
            txtApellidos.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtDPI.Text = "";
            btnModificar.Visible = false;
            lblSinRegistro.Visible = false;
            buttonCancelar.Visible = false;
            dataGridView1.Rows.Clear();

        }


        public void Cargarhabitaciones()
        {
            var niveles = Operaciones.CargarNiveles();
            foreach (var nivel in niveles)
            {
                cmbxNivel.Items.Add(nivel);
            }

            if (niveles.Count > 0)
            {
                cmbxNivel.SelectedIndex = 0;
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void btnReservacion_Click(object sender, EventArgs e)
        {
            try
            {



                using (DBHOTELEntities db = new DBHOTELEntities())
                {

                    HUESPED Huesped = new HUESPED();
                    REGISTRO Registro = new REGISTRO();
                    ASIGNACION Asignacion = new ASIGNACION();
                    TRANSACCION Transaccion = new TRANSACCION();
                    TRANSACCION_DETALLE TransaccionDetalle = new TRANSACCION_DETALLE(); 



                    if (Operaciones.ObtenerInformacionHuesped(txtDPI.Text).Count == 0)
                    {
                        Huesped.Nombres = txtNombre.Text;
                        Huesped.Apellidos = txtApellidos.Text;
                        Huesped.Direccion = txtDireccion.Text;
                        Huesped.DPI = txtDPI.Text;

                        var results = new System.Collections.Generic.List<ValidationResult>();
                        var context = new ValidationContext(Huesped, serviceProvider: null, items: null);
                        bool isValid = Validator.TryValidateObject(Huesped, context, results, true);


                        if (!isValid)
                        {
                            // Mostrar los mensajes de error en la vista
                            foreach (var validationResult in results)
                            {
                                MessageBox.Show(validationResult.ErrorMessage, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            return;
                        }

                        db.HUESPEDs.Add(Huesped);
                    }

                    Registro.IdHuesped = txtDPI.Text;
                    Registro.FechaHoraReserva = DateTime.Now;
                    Registro.ReservaActiva = "A";
                    db.REGISTROes.Add(Registro);

                    var room = db.HABITACIONs.FirstOrDefault(n => n.Nivel == cmbxNivel.Text && n.Comentario == cmbxTipoHabitacion.Text);

                    if (room != null)
                    {

                        DateTime dateTimeInicio = dateTimePickerFechaInicio.Value;
                        TimeSpan checkinHour = new TimeSpan(14, 00, 0); 
                        dateTimeInicio = dateTimeInicio.Date + checkinHour;

                        DateTime dateTimeFinal = dateTimePickerFechaFinal.Value;
                        TimeSpan checkoutHour = new TimeSpan(14, 00, 0);
                        dateTimeFinal = dateTimeFinal.Date + checkinHour;



                        var asignacionExistente = db.ASIGNACIONs
                                                        .Where(a => a.IdHabitacion == room.IdHabitacion &&
                                                                    !(a.REGISTRO.FechaReservaInicio >= dateTimeFinal ||
                                                                      a.REGISTRO.FechaReservaFinal <= dateTimeInicio)).FirstOrDefault();




                        if (asignacionExistente != null)
                        {

                            MessageBox.Show("Habitacion disponible hasta: "  + asignacionExistente.REGISTRO.FechaReservaFinal, "Habitacion", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;

                        }


                        
                        
                        Transaccion.IdHabitacion = room.IdHabitacion;
                        Transaccion.Total = room.Precio;
                        Registro.FechaReservaInicio = dateTimeInicio;
                        Registro.FechaReservaFinal = dateTimeFinal;  
                        Asignacion.IdHabitacion = room.IdHabitacion;
                    
                  
                    }


                    Transaccion.IdRegistro = Registro.IdRegistro;
                    Transaccion.IdTipoDoc = 2;
                    Transaccion.Fecha = Registro.FechaHoraReserva;
                    TransaccionDetalle.IdServicio = 8;
                    TransaccionDetalle.IDTipoDoc = 2;
                    TransaccionDetalle.Cantidad = 1;
                    TransaccionDetalle.Total = Transaccion.Total;

                    Asignacion.IdRegistro = Registro.IdRegistro;
                    Asignacion.IdHuesped = Transaccion.IdHuesped = TransaccionDetalle.IdHuesped = txtDPI.Text;
                     
                    
                    db.ASIGNACIONs.Add(Asignacion);
                    db.TRANSACCIONs.Add(Transaccion);
                    db.TRANSACCION_DETALLE.Add(TransaccionDetalle);
                    db.SaveChanges();

                }
                limpiarCampos();

                MessageBox.Show("Reserva efectuada correctamente", "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado una opción válida

            try
            {
                if (sender is ComboBox comboBox && comboBox.SelectedItem == "Si")
                {

                    DataGridViewRow currentRow = dataGridView1.CurrentRow;
                    DataGridViewCell currentCell = dataGridView1.CurrentCell;
                    DataGridViewColumn currentColumn = currentCell.OwningColumn;

                    string nombreColumna = dataGridView1.Columns[currentColumn.Index].Name;
                    string mensaje = "¿Estás seguro de Registrar la hora de entrada de este usuario?";

                    if (nombreColumna == "CHECKOUT")
                    {
                        mensaje = "¿Estás seguro de Registrar la hora de salida de este usuario?";
                    }

                    if (nombreColumna != "")
                    {

                        DialogResult resultado = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            using (DBHOTELEntities db = new DBHOTELEntities())
                            {
                                int IdRegistro = Int32.Parse(dataGridView1.Rows[currentRow.Index].Cells[0].Value.ToString());
                                var registro = db.REGISTROes.FirstOrDefault(r => r.IdRegistro == IdRegistro);

                                if (registro != null && nombreColumna == "CHECKIN")
                                {
                                    registro.FechaHoraIngreso = DateTime.Now;
                                    db.SaveChanges();
                                    dataGridView1.Rows.Clear();
                                    var registros = Operaciones.ObtenerHuespedesConReserva(registro.IdHuesped);
                                    ActualizarDataGrid(registros);
                                }
                                else
                                {
                                    registro.FechaHoraFinRegistro = DateTime.Now;
                                    registro.ReservaActiva = "N";
                                    db.SaveChanges();
                                    dataGridView1.Rows.Clear();
                                    var registros = Operaciones.ObtenerHuespedesConReserva(registro.IdHuesped);
                                    ActualizarDataGrid(registros);

                                }


                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox comboBox)
            {
                // Desuscribir cualquier suscripción previa para evitar múltiples suscripciones
                comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;

                // Suscribir al evento SelectedIndexChanged del ComboBox
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            }
        }

        private void cmbxNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (DBHOTELEntities db = new DBHOTELEntities())
            {

                cmbxTipoHabitacion.Items.Clear();

                //Consultamos las habitaciones que esten Disponibles
                var habitaciones = from h in db.HABITACIONs
                                   where h.Nivel == cmbxNivel.Text
                                   select h;

                //Cargamos las habitaciones al combobox
                foreach (var habitacion in habitaciones)
                {
                    cmbxTipoHabitacion.Items.Add(habitacion.Comentario);
                }

                if (habitaciones != null)
                {
                    cmbxTipoHabitacion.SelectedIndex = 0;
                }
            }
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Estás seguro que deseas modificar este usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (resultado == DialogResult.Yes)
            {

                try
                {
                    using (DBHOTELEntities db = new DBHOTELEntities())
                    {

                        var huesped = db.HUESPEDs.FirstOrDefault(h => h.DPI == txtDPI.Text);

                        if (huesped != null)
                        {
                            huesped.Nombres = txtNombre.Text;
                            huesped.Apellidos = txtApellidos.Text;
                            huesped.Direccion = txtDireccion.Text;
                            huesped.DPI = txtDPI.Text;
                            db.SaveChanges();
                            MessageBox.Show("Registro actualizado correctamente.", "Modificacion de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarCampos();

                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegúrate de que se haya hecho clic en una fila (no en el encabezado)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];

                cmbxNivel.Text = fila.Cells["NIVEL"].Value.ToString();
                cmbxTipoHabitacion.Text = fila.Cells["HABITACION"].Value.ToString();
                dateTimePickerFechaInicio.Value = DateTime.Parse( fila.Cells["DEL"].Value.ToString());
                dateTimePickerFechaFinal.Value = DateTime.Parse(fila.Cells["HASTA"].Value.ToString());


                CancelarReservacion.IdRegistro = Int32.Parse(fila.Cells["IDREGISTRO"].Value.ToString());
                CancelarReservacion.Nivel = fila.Cells["NIVEL"].Value.ToString();

                CancelarReservacion.Ingreso = (fila.Cells["INGRESO"].Value !=null)? DateTime.Parse(fila.Cells["INGRESO"].Value.ToString()) : (DateTime?)null;



                buttonCancelar.Visible = true;
            }


        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            if (CancelarReservacion.Ingreso !=null)
            {
                MessageBox.Show("Error al cancelar la reservacion, el cliente ya hizo checkin", "Cancelacion de Reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Operaciones.CancelarReservacion(CancelarReservacion.IdRegistro);
            ActualizarDataGrid(Operaciones.ObtenerHuespedesConReserva(txtDPI.Text));
            buttonCancelar.Visible=false;
            cmbxNivel.Text = "";
            cmbxTipoHabitacion.Text = "";

            MessageBox.Show("La reserva fue cancelada exitosamente", "Cancelacion de Reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
