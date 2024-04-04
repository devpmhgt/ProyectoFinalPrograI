using HotelProyectoFinal.Models;
using HotelProyectoFinal.Controllers;
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
using static HotelProyectoFinal.DTOS;


namespace HotelProyectoFinal.Views
{
    public partial class UCRegistro : UserControl
    {
        TransaccionController Transaccion = new TransaccionController();

        DTOS.RegistroHuespedDTO registroHuesped = new DTOS.RegistroHuespedDTO();
        RegistroController registro = new RegistroController();
        HabitacionController habitacion = new HabitacionController();
        HuespedController huesped = new HuespedController();
        DBOperaciones Operaciones = new DBOperaciones();
        List<DTOS.Transaccion> transacciones = new List<DTOS.Transaccion>();
        List<DTOS.RangoFechas> rangofechas = new List<RangoFechas>();
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
                dataGridView1.Rows[rowIndex].Cells["Hbtcion"].Value = registro.Comentario;
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
            //Se hace la reservacion 
            registro.Reservacion(txtNombre.Text,
                                txtApellidos.Text,
                                txtDireccion.Text,
                                txtDPI.Text,
                                cmbxNivel.Text,
                                cmbxTipoHabitacion.Text,
                                dateTimePickerFechaInicio.Value,
                                dateTimePickerFechaFinal.Value);

            limpiarCampos();
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
                        transacciones = Transaccion.ObtenerTodasLasTransacciones(txtDPI.Text);

                        if (transacciones != null)
                        {
                            MessageBox.Show("No se puede hacer checkout dado que el cliente tiene un saldo pendiente");
                            comboBox.SelectedIndex = -1;
                            return;
                        }
                    }

                    if (nombreColumna != "")
                    {

                        DialogResult resultado = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);




                        if (resultado == DialogResult.Yes)
                        {
                            int IdRegistro = Int32.Parse(dataGridView1.Rows[currentRow.Index].Cells[0].Value.ToString());
                            dataGridView1.Rows.Clear();
                            var registros = Operaciones.ObtenerHuespedesConReserva(registro.CheckInCheckOut(IdRegistro, nombreColumna).FirstOrDefault().IdHuesped);
                            ActualizarDataGrid(registros);

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
            cmbxTipoHabitacion.Items.Clear();
            
            //Se obtienen las habitaciones por cada nivel del Hotel
            var habitaciones = habitacion.ObtenerHabitacionesPorNivel(cmbxNivel.Text);
            
            //Se cargan los niveles dependiendo del nivel seleccionado
            foreach (var habitacion in habitaciones)
            {
                cmbxTipoHabitacion.Items.Add(habitacion.Comentario);
            }

            if (habitaciones != null)
            {
                cmbxTipoHabitacion.SelectedIndex = 0;


                if (monthCalendar1.Visible == true)
                {
                    rangofechas = habitacion.ObtenerDisponibilidad(cmbxNivel.Text, cmbxTipoHabitacion.Text);
                    monthCalendar1.RemoveAllBoldedDates();

                    foreach (var rango in rangofechas)
                    {
                        DateTime fechaInicio = rango.FechaInicio;
                        DateTime fechaFin = rango.FechaFinal;
                        // Calcular todas las fechas dentro del rango
                        while (fechaInicio <= fechaFin)
                        {
                            monthCalendar1.AddBoldedDate(fechaInicio);

                            fechaInicio = fechaInicio.AddDays(1);
                        }
                    }
                    monthCalendar1.UpdateBoldedDates();
                }
            }

        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Estás seguro que deseas modificar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (resultado == DialogResult.Yes && buttonCancelar.Visible == false)
            {
                huesped.ModificarInformacionHuesped(txtNombre.Text,
                                                    txtApellidos.Text,
                                                    txtDireccion.Text,
                                                    txtDPI.Text);
            }



            if (resultado == DialogResult.Yes && buttonCancelar.Visible == true)
            {
                registro.ModificarReservacion(registroHuesped.IdRegistro,
                                              dateTimePickerFechaInicio.Value,
                                              dateTimePickerFechaFinal.Value,
                                              cmbxNivel.Text,
                                              cmbxTipoHabitacion.Text);


            }


            dataGridView1.Rows.Clear();

            var registros = Operaciones.ObtenerHuespedesConReserva(txtDPI.Text);


            ActualizarDataGrid(registros);
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegurar que se haya hecho clic en una fila (no en el encabezado)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];

                cmbxNivel.Text = fila.Cells["NIVEL"].Value.ToString();
                cmbxTipoHabitacion.Text = fila.Cells["HABITACION"].Value.ToString();
                dateTimePickerFechaInicio.Value = DateTime.Parse( fila.Cells["DEL"].Value.ToString());
                dateTimePickerFechaFinal.Value = DateTime.Parse(fila.Cells["HASTA"].Value.ToString());


                registroHuesped.IdRegistro = Int32.Parse(fila.Cells["IDREGISTRO"].Value.ToString());
                registroHuesped.Nivel = fila.Cells["NIVEL"].Value.ToString();
                registroHuesped.Comentario = cmbxTipoHabitacion.Text = fila.Cells["HABITACION"].Value.ToString();
                registroHuesped.FechaReservaInicio = DateTime.Parse(fila.Cells["DEL"].Value.ToString());
                registroHuesped.FechaReservaFinal = DateTime.Parse(fila.Cells["HASTA"].Value.ToString());

                registroHuesped.Ingreso = (fila.Cells["INGRESO"].Value !=null)? DateTime.Parse(fila.Cells["INGRESO"].Value.ToString()) : (DateTime?)null;

                buttonCancelar.Visible = true;
            }


        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            if (registroHuesped.Ingreso !=null)
            {
                MessageBox.Show("Error al cancelar la reservacion, el cliente ya hizo checkin", "Cancelacion de Reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            registro.CancelarReservacion(registroHuesped.IdRegistro);
            ActualizarDataGrid(Operaciones.ObtenerHuespedesConReserva(txtDPI.Text));
            buttonCancelar.Visible=false;
            cmbxNivel.Text = "";
            cmbxTipoHabitacion.Text = "";

            MessageBox.Show("La reserva fue cancelada exitosamente", "Cancelacion de Reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePickerFechaFinal_ValueChanged(object sender, EventArgs e)
        {
            habitacion.Disponibilidad(cmbxNivel.Text,
                                              cmbxTipoHabitacion.Text,
                                              dateTimePickerFechaInicio.Value,
                                              dateTimePickerFechaFinal.Value);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                grpBoxDisponibilida.Visible = true;

                rangofechas = habitacion.ObtenerDisponibilidad(cmbxNivel.Text, cmbxTipoHabitacion.Text);
                monthCalendar1.RemoveAllBoldedDates();
                foreach (var rango in rangofechas)
                {
                    DateTime fechaInicio = rango.FechaInicio;
                    DateTime fechaFin = rango.FechaFinal;
                    // Calcular todas las fechas dentro del rango
                    while (fechaInicio <= fechaFin)
                    {
                        monthCalendar1.AddBoldedDate(fechaInicio);
                        
                        fechaInicio = fechaInicio.AddDays(1);
                    }
                }
                monthCalendar1.UpdateBoldedDates();

            }
            else
            {
                grpBoxDisponibilida.Visible = false;
            }
        }

        private void cmbxTipoHabitacion_SelectedIndexChanged(object sender, EventArgs e)
        {
                rangofechas = habitacion.ObtenerDisponibilidad(cmbxNivel.Text, cmbxTipoHabitacion.Text);
                monthCalendar1.RemoveAllBoldedDates();

                foreach (var rango in rangofechas)
                {
                    DateTime fechaInicio = rango.FechaInicio;
                    DateTime fechaFin = rango.FechaFinal;
                    // Calcular todas las fechas dentro del rango
                    while (fechaInicio <= fechaFin)
                    {
                        monthCalendar1.AddBoldedDate(fechaInicio);

                        fechaInicio = fechaInicio.AddDays(1);
                    }
                }
                monthCalendar1.UpdateBoldedDates();

        }
    }
}
