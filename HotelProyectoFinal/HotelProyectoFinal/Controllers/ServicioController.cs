using HotelProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProyectoFinal.Controllers
{
    internal class ServicioController
    {


        public string ObtenerPrecio(string servicio)
        {
            try 
            { 
                using (DBHOTELEntities db = new DBHOTELEntities())
                {
                    var precioServicio = (from s in db.SERVICIOs
                                          where s.Descripcion == servicio
                                          select new
                                          {
                                              Precio = s.Precio,
                                              IdServicio = s.IdServicio

                                          }).FirstOrDefault();




                    if (precioServicio != null)
                    {
                        return precioServicio.Precio.ToString();
                    }
                }

               
             }catch (Exception ex) 
            { 
            
            
            
             }
            return null;
        }
    }
}
