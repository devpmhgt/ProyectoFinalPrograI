using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelProyectoFinal.Models;
using HotelProyectoFinal.Properties;

namespace HotelProyectoFinal
{
    internal class DBOperaciones
    {


        public List<DTOS.RegistroHuespedDTO> ObtenerHuespedesConReserva(string dpi)
        {
            try
            {
                using (DBHOTELEntities db = new DBHOTELEntities())
                {

                    var huespedesConReserva = (from registro in db.REGISTROes
                                               join huespede in db.HUESPEDs
                                               on registro.IdHuesped equals huespede.DPI
                                               join asignacion in db.ASIGNACIONs
                                               on registro.IdRegistro equals asignacion.IdRegistro // Cambiado a IdRegistro para asegurar la relación correcta
                                               join habitacion in db.HABITACIONs
                                               on asignacion.IdHabitacion equals habitacion.IdHabitacion
                                               where huespede.DPI == dpi && registro.ReservaActiva != "N"
                                               select new DTOS.RegistroHuespedDTO
                                               {
                                                   IdRegistro = registro.IdRegistro,
                                                   Nombre = huespede.Nombres,
                                                   Apellido = huespede.Apellidos,
                                                   FechaReservacion = registro.FechaHoraReserva,
                                                   FechaReservaFinal = registro.FechaReservaFinal,
                                                   FechaReservaInicio = registro.FechaReservaInicio,
                                                   Ingreso = registro.FechaHoraIngreso,
                                                   Salida = registro.FechaHoraFinRegistro,
                                                   Nivel = habitacion.Nivel,
                                                   Comentario = habitacion.Comentario,
                                                   IdHuesped = registro.IdHuesped
                                               }).ToList();


                    return huespedesConReserva;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return null;
        }


        public List<HUESPED> ObtenerInformacionHuesped(string dpi)
        {
            try
            {
                using (DBHOTELEntities db = new DBHOTELEntities())
                {

                    var huespedes = (from h in db.HUESPEDs
                                     where h.DPI == dpi
                                     select h).Distinct().ToList();



                    return huespedes;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return null;
        }


        public List<REGISTRO> ObtenerInformacionHuespedconRegistro(string dpi)
        {

            try
            {
                using (DBHOTELEntities db = new DBHOTELEntities())
                {

                    var huespedesConReserva = (from h in db.REGISTROes
                                               where h.IdHuesped == dpi && h.ReservaActiva == "A" && h.FechaHoraIngreso != null
                                               select h).ToList();

                    return huespedesConReserva;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return null;

        }


        public List<DTOS.AsignacionHabitacionDTO> ObtenerHabitacionconRegistro(int IdRegistro)
        {
            using (DBHOTELEntities db = new DBHOTELEntities()) {
                var comentarioHabitacion = (from registro in db.REGISTROes
                                            join asignacion in db.ASIGNACIONs
                                            on registro.IdRegistro equals asignacion.IdRegistro
                                            join habitacion in db.HABITACIONs
                                            on asignacion.IdHabitacion equals habitacion.IdHabitacion
                                            where registro.IdRegistro == IdRegistro
                                            select new DTOS.AsignacionHabitacionDTO
                                            { Comentario = habitacion.Comentario,
                                                IdHabitacion = habitacion.IdHabitacion,
                                                IdRegistro = asignacion.IdRegistro,
                                                IdHuesped = asignacion.IdHuesped
                                            }).ToList();

                return comentarioHabitacion;
            }
        }


        public List<string> CargarNiveles()
        {

            try {
                //Creamos una instancia para comunicacion con la BD
                using (DBHOTELEntities db = new DBHOTELEntities())
                {

                    //Consultamos los niveles que hay en el hotel
                    var niveles = (from n in db.HABITACIONs
                                   select n.Nivel).Distinct().ToList();

                    //Cargamos los niveles al combobox
                    return niveles;
                }
            } catch (Exception ex)
            {


            }
            return null;
        }


        public void CancelarReservacion(int idRegistro)
        {
            using (DBHOTELEntities db = new DBHOTELEntities())
            {

              var  Registro = db.REGISTROes.FirstOrDefault(r => r.IdRegistro  == idRegistro);


                if(Registro != null && Registro.ReservaActiva ==  "A")
                {
                    Registro.ReservaActiva = "N";
                    Registro.FechaReservaFinal = null;
                    Registro.FechaReservaInicio = null;
                    db.SaveChanges();
                }
            }
        }
    }
}
