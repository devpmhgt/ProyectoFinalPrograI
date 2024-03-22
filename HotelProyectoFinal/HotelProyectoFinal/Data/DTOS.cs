using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HotelProyectoFinal
{
    public class DTOS
    {
        public class AsignacionHabitacionDTO
        {
            public string Comentario { get; set; }
            public int IdHabitacion { get; set; }

            public string IdHuesped { get; set; }   

            public int IdRegistro { get; set; } 
        }



        public class RegistroHuespedDTO
        {
            public int IdRegistro { get; set; } 
            public string Nombre { get; set; }
            public string Apellido { get; set; }

            public Nullable<System.DateTime> FechaReservacion { get; set; }

            public Nullable<System.DateTime> FechaReservaInicio { get; set; }

            public Nullable<System.DateTime> FechaReservaFinal {  get; set; } 

            public Nullable<System.DateTime> Ingreso { get; set; }

            public Nullable<System.DateTime> Salida { get; set; } 

            public string Nivel {  get; set; }
            public string Comentario { get; set; }


        }
    }
}
