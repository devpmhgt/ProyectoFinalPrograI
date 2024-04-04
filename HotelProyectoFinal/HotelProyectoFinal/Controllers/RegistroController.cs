using HotelProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace HotelProyectoFinal.Controllers
{
    class RegistroController
    {
        DBOperaciones Operaciones = new DBOperaciones();

        public void Reservacion(string Nombres, string Apellidos, string Direccion, string DPI, string nivel, string comentario, DateTime FechaInicio, DateTime FechaFinal)
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



                    if (Operaciones.ObtenerInformacionHuesped(DPI).Count ==0)
                    {
                        Huesped.Nombres = Nombres;
                        Huesped.Apellidos = Apellidos;
                        Huesped.Direccion = Direccion;
                        Huesped.DPI = DPI;


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

                    Registro.IdHuesped = DPI;



                    Registro.FechaHoraReserva = DateTime.Now;
                    Registro.ReservaActiva = "A";
                    db.REGISTROes.Add(Registro);

                    var room = db.HABITACIONs.FirstOrDefault(n => n.Nivel == nivel && n.Comentario == comentario);

                    if (room != null)
                    {

                        DateTime dateTimeInicio = FechaInicio;
                        TimeSpan checkinHour = new TimeSpan(14, 00, 0);
                        dateTimeInicio = dateTimeInicio.Date + checkinHour;

                        DateTime dateTimeFinal = FechaFinal;
                        TimeSpan checkoutHour = new TimeSpan(14, 00, 0);
                        dateTimeFinal = dateTimeFinal.Date + checkinHour;



                        var asignacionExistente = db.ASIGNACIONs
                                                        .Where(a => a.IdHabitacion == room.IdHabitacion &&
                                                                    !(a.REGISTRO.FechaReservaInicio >= dateTimeFinal ||
                                                                      a.REGISTRO.FechaReservaFinal <= dateTimeInicio)).FirstOrDefault();




                        if (asignacionExistente != null)
                        {

                            MessageBox.Show("Habitacion disponible hasta: " + asignacionExistente.REGISTRO.FechaReservaFinal, "Habitacion", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    Transaccion.Pagado = "N";
                    Transaccion.Fecha = Registro.FechaHoraReserva;
                    TransaccionDetalle.IdServicio = 43;
                    TransaccionDetalle.IDTipoDoc = 2;
                    TransaccionDetalle.Cantidad = 1;
                    TransaccionDetalle.Total = Transaccion.Total;
                    TransaccionDetalle.IdTransaccion = Transaccion.IdTransaccion;
                    Asignacion.IdRegistro = Registro.IdRegistro;
                    Asignacion.IdHuesped = Transaccion.IdHuesped = TransaccionDetalle.IdHuesped = DPI;


                    db.ASIGNACIONs.Add(Asignacion);
                    db.TRANSACCIONs.Add(Transaccion);
                    db.TRANSACCION_DETALLE.Add(TransaccionDetalle);
                    db.SaveChanges();

                }

                MessageBox.Show("Reserva efectuada correctamente", "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public List<DTOS.RegistroHuespedDTO> CheckInCheckOut(int IdRegistro, string nombreColumna)
        {
            try
            {
                
                using (DBHOTELEntities db = new DBHOTELEntities())
                {

                    var registro = db.REGISTROes.FirstOrDefault(r => r.IdRegistro == IdRegistro);

                    if (registro != null && nombreColumna == "CHECKIN")
                    {
                        registro.FechaHoraIngreso = DateTime.Now;
                        db.SaveChanges();
                        var registros = Operaciones.ObtenerHuespedesConReserva(registro.IdHuesped);
                        return registros;
                    }
                    else
                    {
                        registro.FechaHoraFinRegistro = DateTime.Now;
                        registro.ReservaActiva = "N";
                        db.SaveChanges();
                        var registros = Operaciones.ObtenerHuespedesConReserva(registro.IdHuesped);
                        return registros;

                    }


                }

               

            }catch (Exception ex)
            {

            }

            return null;
        }


        public void CancelarReservacion(int idRegistro)
        {
            using (DBHOTELEntities db = new DBHOTELEntities())
            {

                var Registro = db.REGISTROes.FirstOrDefault(r => r.IdRegistro == idRegistro);


                if (Registro != null && Registro.ReservaActiva == "A")
                {
                    Registro.ReservaActiva = "N";
                    Registro.FechaReservaFinal = null;
                    Registro.FechaReservaInicio = null;
                    db.SaveChanges();
                }
            }
        }


        public void ModificarReservacion(int idRegistro, DateTime ReservaInicio, DateTime ReservaFinal, string nivel, string habitacion)
        {
            try
            {
                using (DBHOTELEntities db = new DBHOTELEntities())
                {

                    var Registro = db.REGISTROes.FirstOrDefault(r => r.IdRegistro == idRegistro);
                    var Asignacion = db.ASIGNACIONs.FirstOrDefault(a => a.IdRegistro == idRegistro);
                    var Transaccion = db.TRANSACCIONs.FirstOrDefault(t => t.IdRegistro == idRegistro);
                    var TransaccionDetalle = db.TRANSACCIONs.FirstOrDefault(td => td.IdTransaccion == Transaccion.IdTransaccion);
                    var room = db.HABITACIONs.FirstOrDefault(n => n.Nivel == nivel && n.Comentario == habitacion);

                    if (Registro != null && Registro.ReservaActiva == "A" && Registro.FechaHoraIngreso ==null)
                    {

                        if (room != null)
                        {

                            DateTime dateTimeInicio = ReservaInicio;
                            TimeSpan checkinHour = new TimeSpan(14, 00, 0);
                            dateTimeInicio = dateTimeInicio.Date + checkinHour;

                            DateTime dateTimeFinal = ReservaFinal;
                            TimeSpan checkoutHour = new TimeSpan(14, 00, 0);
                            dateTimeFinal = dateTimeFinal.Date + checkinHour;

                            var asignacionExistente = db.ASIGNACIONs
                                                            .Where(a => a.IdHabitacion == room.IdHabitacion &&
                                                                        !(a.REGISTRO.FechaReservaInicio >= dateTimeFinal ||
                                                                          a.REGISTRO.FechaReservaFinal <= dateTimeInicio)).FirstOrDefault();


                            if (asignacionExistente != null && asignacionExistente.HABITACION.Nivel == nivel && asignacionExistente.HABITACION.Comentario == habitacion)
                            {
                                if (!(asignacionExistente.REGISTRO.FechaReservaInicio >= ReservaFinal && asignacionExistente.REGISTRO.FechaReservaFinal <= ReservaInicio))
                                {

                                    Registro.FechaReservaFinal = ReservaFinal;
                                    Registro.FechaReservaInicio = ReservaInicio;
                                    Registro.FechaHoraReserva = DateTime.Now;
                                    Asignacion.IdHabitacion = room.IdHabitacion;
                                    Transaccion.IdHabitacion = room.IdHabitacion;
                                    Transaccion.Fecha = DateTime.Now;
                                    Transaccion.Total = room.Precio;
                                    TransaccionDetalle.Total = room.Precio;

                                    db.SaveChanges();
                                    MessageBox.Show("Modificacion efectuada correctamente", "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    return;
                                }
                            }
                            else
                            {
                                if (asignacionExistente != null)
                                {

                                    MessageBox.Show("Habitacion disponible hasta: " + asignacionExistente.REGISTRO.FechaReservaFinal, "Habitacion", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    return;

                                }

                            }

                            Registro.FechaReservaFinal = ReservaFinal;
                            Registro.FechaReservaInicio = ReservaInicio;
                            Registro.FechaHoraReserva = DateTime.Now;
                            Asignacion.IdHabitacion = room.IdHabitacion;
                            Transaccion.IdHabitacion = room.IdHabitacion;
                            Transaccion.Fecha = DateTime.Now;
                            Transaccion.Total = room.Precio;

                            db.SaveChanges();
                            MessageBox.Show("Modificacion efectuada correctamente", "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }


                    }
                    else
                    {
                        MessageBox.Show("No se puede modificar la reservacion ya que el Usuario ya hizo checkin", "Habitacion", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                }
            }catch(Exception ex)
            {

            }
        }
    }
}
