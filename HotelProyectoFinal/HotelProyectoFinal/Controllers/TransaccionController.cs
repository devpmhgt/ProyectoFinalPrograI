using HotelProyectoFinal.Models;
using HotelProyectoFinal.Views;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;
using MessageBox = System.Windows.Forms.MessageBox;

namespace HotelProyectoFinal.Controllers
{
    class TransaccionController
    {
        DBOperaciones registro = new DBOperaciones();
        public void AgregarServicio(int cantidad, string DPI, int IdRegistro, int IdHabitacion, string servicio, int tipo_documento)
        {
            try
            {
                using (DBHOTELEntities db = new DBHOTELEntities())
                {
                    if(cantidad != 0) {
                        TRANSACCION transaccion = new TRANSACCION();
                        TRANSACCION_DETALLE detalle = new TRANSACCION_DETALLE();


                        transaccion.IdRegistro = IdRegistro;
                        transaccion.IdHuesped = DPI;
                        transaccion.IdHabitacion = IdHabitacion;
                        transaccion.IdTipoDoc = 1;
                        transaccion.Fecha = DateTime.Now;

                        detalle.IdTransaccion = transaccion.IdTransaccion;
                        detalle.Cantidad = cantidad;


                        var precioServicio = (from s in db.SERVICIOs
                                              join ts in db.TipoServicios on s.IdTipoServicio equals ts.IdTipoServicio
                                              where s.Descripcion == servicio
                                              select new
                                              {
                                                  Precio = s.Precio,
                                                  IdServicio = s.IdServicio

                                              }).FirstOrDefault();


                        var factor = (from tipodocumento in db.TIPO_DOCUMENTO
                                      where tipodocumento.IdTipoDoc == tipo_documento
                                      select new
                                      {
                                          Factor = tipodocumento.Factor
                                      }).FirstOrDefault();

                        int multiplicador = 1;

                        if (factor != null)
                        {
                            if (factor.Factor.Equals("-"))
                            {
                                multiplicador = -1;
                            }
                        }

                        detalle.IdServicio = precioServicio.IdServicio;
                        detalle.Total = precioServicio.Precio * (decimal)cantidad *(multiplicador);
                        detalle.IdHuesped = transaccion.IdHuesped;
                        detalle.IdTransaccion = transaccion.IdTransaccion;
                        transaccion.Total = detalle.Total;
                        transaccion.Pagado = "N";
                        detalle.IDTipoDoc = tipo_documento;

                        db.TRANSACCIONs.Add(transaccion);
                        db.TRANSACCION_DETALLE.Add(detalle);

                        db.SaveChanges();
                        MessageBox.Show("Servicio agregado correctamente");

                    }
                    else
                    {
                        MessageBox.Show("Porfavor ingrese una cantidad mayor a 0");
                    }
                }

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

        }


        public List<DTOS.Transaccion> ObtenerTransacciones(string DPI, int IdHabitacion, DateTime FechaInicio, DateTime FechaFinal )
        {

            try
            {
                using (DBHOTELEntities db = new DBHOTELEntities())
                {
                    var transacciones = (from t in db.TRANSACCIONs
                                                              join td in db.TRANSACCION_DETALLE 
                                                              on t.IdTransaccion equals td.IdTransaccion
                                                              join h in db.HABITACIONs
                                                              on t.IdHabitacion equals h.IdHabitacion
                                                              join s in db.SERVICIOs
                                                              on td.IdServicio equals s.IdServicio
                                                              join ts in db.TipoServicios
                                                              on s.IdTipoServicio equals ts.IdTipoServicio
                                                              where t.IdHabitacion == IdHabitacion && t.IdHuesped == DPI && t.Pagado=="N" && t.Fecha >= FechaInicio && t.Fecha <= FechaFinal
                                         select new DTOS.Transaccion
                                                              {
                                                                    IdTransaccion = t.IdTransaccion,
                                                                    IdHuesped  = t.IdHuesped,
                                                                    IdRegistro = t.IdRegistro,
                                                                    Fecha = (DateTime)t.Fecha,
                                                                    Cantidad = (int)td.Cantidad,
                                                                    Total = (decimal)td.Total,
                                                                    precio = (decimal)(s.Precio ?? td.Total ),
                                                                    Comentario = h.Comentario,
                                                                    TipoServicio = s.Descripcion
                                                              }).ToList();

                    return transacciones;
                }

            }catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

            return null;
        }


        public void CancelarServicio(int IdTransaccion)
        {
            try { 
                using (DBHOTELEntities db = new DBHOTELEntities())
                {
                    var datosTransaccion = db.TRANSACCIONs.FirstOrDefault(d => d.IdTransaccion == IdTransaccion);

                    var datosCancelacion = (from t in db.TRANSACCIONs
                                            join ts in db.TRANSACCION_DETALLE
                                            on t.IdTransaccion equals ts.IdTransaccion
                                            join s in db.SERVICIOs
                                            on ts.IdServicio equals s.IdServicio
                                            where t.IdTransaccion == IdTransaccion
                                            select new
                                            {
                                                IdRegistro = t.IdRegistro,
                                                IdHuesped = t.IdHuesped,
                                                IdHabitacion = t.IdHabitacion,
                                                Cantidad = ts.Cantidad,
                                                Servicio = s.Descripcion
                                            }).FirstOrDefault();
                    AgregarServicio((int)datosCancelacion.Cantidad, datosCancelacion.IdHuesped, datosCancelacion.IdRegistro,
                                    (int)datosCancelacion.IdHabitacion, datosCancelacion.Servicio, 1002);

                }

            }
            catch
            {

            }


        }


        public List<DTOS.Transaccion> ObtenerTodasLasTransacciones(string DPI)
        {

            try
            {
                if (DPI!="")
                {
                    using (DBHOTELEntities db = new DBHOTELEntities())
                    {
                        var transacciones = (from t in db.TRANSACCIONs
                                             join td in db.TRANSACCION_DETALLE
                                             on t.IdTransaccion equals td.IdTransaccion
                                             join h in db.HABITACIONs
                                             on t.IdHabitacion equals h.IdHabitacion
                                             join s in db.SERVICIOs
                                             on td.IdServicio equals s.IdServicio
                                             join ts in db.TipoServicios
                                             on s.IdTipoServicio equals ts.IdTipoServicio
                                             join r in db.REGISTROes
                                             on t.IdRegistro equals r.IdRegistro
                                             where /*t.IdHabitacion == IdHabitacion &&*/ t.IdHuesped == DPI && t.Pagado =="N" && r.ReservaActiva == "A" && r.FechaHoraIngreso !=null && r.FechaHoraFinRegistro==null/*&& t.Fecha >= FechaInicio && t.Fecha <= FechaFinal*/
                                             select new DTOS.Transaccion
                                             {
                                                 IdTransaccion = t.IdTransaccion,
                                                 IdHuesped = t.IdHuesped,
                                                 IdRegistro = t.IdRegistro,
                                                 Fecha = (DateTime)t.Fecha,
                                                 Cantidad = (int)td.Cantidad,
                                                 Total = (decimal)td.Total,
                                                 precio = (decimal)(s.Precio ?? td.Total),
                                                 Comentario = h.Comentario,
                                                 TipoServicio = s.Descripcion
                                             }).ToList();

                        return transacciones;
                    }
                }

                MessageBox.Show("Porfavor ingrese un DPI");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

            return null;
        }


        public decimal TotalporPagar(List<DTOS.Transaccion> Transacciones)
        {
            decimal Total = 0;
            try
            {
                if (Transacciones != null)
                {
                    foreach(var transaccion in Transacciones)
                    {
                        Total += transaccion.Total;
                    }
                    return Total;
                }
            }
            catch
            {

            }


            return Total;
        }

    }
}
