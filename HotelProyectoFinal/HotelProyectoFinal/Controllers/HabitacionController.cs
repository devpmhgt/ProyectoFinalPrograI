using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelProyectoFinal.Models;


namespace HotelProyectoFinal.Controllers
{
    class HabitacionController
    {
    
        public List<HABITACION> ObtenerHabitacionesPorNivel(string Nivel)
        {
            try
            {
                using (DBHOTELEntities db = new DBHOTELEntities())
                {

                    

                    //Consultamos las habitaciones que esten Disponibles
                    var habitaciones = (from h in db.HABITACIONs
                                       where h.Nivel == Nivel
                                       select h).ToList();

                    return habitaciones;
                }
            }
            catch (Exception ex)
            {

            }


            return null;
        }


        public void Disponibilidad(string nivel, string comentario, DateTime FechaInicio, DateTime FechaFinal)
        {
            using (DBHOTELEntities db = new DBHOTELEntities())
            {

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

                    MessageBox.Show("Habitacion disponible");

                }


            }
        }


        public List<DTOS.RangoFechas> ObtenerDisponibilidad(string nivel, string comentario)
        {
            try
            {
                using (DBHOTELEntities db = new DBHOTELEntities())
                {
                    var room = db.HABITACIONs.FirstOrDefault(n => n.Nivel == nivel && n.Comentario == comentario);



                    var fechas = (from a in db.ASIGNACIONs
                                          join r in db.REGISTROes 
                                          on a.IdRegistro equals r.IdRegistro
                                          where a.IdHabitacion == room.IdHabitacion && r.ReservaActiva == "A"
                                          select new DTOS.RangoFechas
                                          {
                                              FechaInicio = (DateTime)r.FechaReservaInicio,
                                              FechaFinal = (DateTime)r.FechaReservaFinal
                                          }).ToList();

                    return fechas;
                }



            }
            catch (Exception ex) { 
            
            
            }
            return null;
        }
    }


}
