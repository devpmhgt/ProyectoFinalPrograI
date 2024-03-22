using HotelProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelProyectoFinal
{
    public partial class Registro : UserControl
    {

        DBOperaciones Operaciones = new DBOperaciones();

        public Registro()
        {
            InitializeComponent();
        }

        private void UCRegistro_Load(object sender, EventArgs e)
        {
            Cargarhabitaciones();


        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {


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
            }
            else
            {
                MessageBox.Show("Usuario no registrado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                limpiarCampos();
            }


            var registros = Operaciones.ObtenerHuespedesConReserva(txtDPI.Text);


            ActualizarDataGrid(registros);



        }

        public void ActualizarDataGrid(List<REGISTRO> registros)
        {
            foreach (var registro in registros)
            {
                int rowIndex = dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells["REGISTRO"].Value = registro.IdRegistro;
                dataGridView1.Rows[rowIndex].Cells["DPI"].Value = registro.IdHuesped;
                dataGridView1.Rows[rowIndex].Cells["FECHARESERVACION"].Value = registro.FechaHoraReserva;
                dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].Style.BackColor = Color.Gray;
                dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].ReadOnly = true;

                if (registro.FechaHoraIngreso != null)
                {
                    dataGridView1.Rows[rowIndex].Cells["INGRESO"].Value = registro.FechaHoraIngreso;
                    dataGridView1.Rows[rowIndex].Cells["CHECKIN"].Value = "Si";
                    dataGridView1.Rows[rowIndex].Cells["CHECKIN"].Style.BackColor = Color.Gray;
                    dataGridView1.Rows[rowIndex].Cells["CHECKIN"].ReadOnly = true;
                    dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].ReadOnly = false;
                    dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].Style.BackColor = Color.White;

                }

                if (registro.FechaHoraFinRegistro != null)
                {
                    dataGridView1.Rows[rowIndex].Cells["SALIDA"].Value = registro.FechaHoraFinRegistro;
                    dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].Value = "Si";
                    dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].Style.BackColor = Color.Gray;
                    dataGridView1.Rows[rowIndex].Cells["CHECKOUT"].ReadOnly = true;
                }

            }

            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                // Establece el modo de tamaño automático de la columna en Fill
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
        }



        private void limpiarCampos()
        {
            txtApellidos.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtDPI.Text = "";
            btnModificar.Visible = false;
            lblSinRegistro.Visible = false;
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
        private void button3_Click(object sender, EventArgs e)
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



                    if (Operaciones.ObtenerInformacionHuesped(txtDPI.Text).Count == 0)
                    {
                        Huesped.Nombres = txtNombre.Text;
                        Huesped.Apellidos = txtApellidos.Text;
                        Huesped.Direccion = txtDireccion.Text;
                        Huesped.DPI = txtDPI.Text;

                        //var results = new System.Collections.Generic.List<ValidationResult>();
                        //var context = new ValidationContext(Huesped, serviceProvider: null, items: null);
                        //bool isValid = Validator.TryValidateObject(Huesped, context, results, true);


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

                        var asignacionExistente = db.ASIGNACIONs
                                                    .Where(a => a.IdHabitacion == room.IdHabitacion &&
                                                                (a.REGISTRO.FechaReservaInicio >= dateTimePickerFechaInicio.Value && a.REGISTRO.FechaHoraIngreso <= dateTimePickerFechaFinal.Value))
                                                    .FirstOrDefault();


                        if (asignacionExistente != null)
                        {
                            Asignacion.IdHabitacion = room.IdHabitacion;

                        }
                        else
                        {
                            MessageBox.Show("Habitacion disponible hasta: " + asignacionExistente, "Habitacion", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }

                    Asignacion.IdRegistro = Registro.IdRegistro;
                    Asignacion.IdHuesped = txtDPI.Text;
                    db.ASIGNACIONs.Add(Asignacion);
                    db.SaveChanges();

                    limpiarCampos();


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

        private void cmbxNivel_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void btnModificar_Click(object sender, EventArgs e)
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

        private void groupBoxDatosHuesped_Enter(object sender, EventArgs e)
        {

        }

        private void cmbxTipoHabitacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
