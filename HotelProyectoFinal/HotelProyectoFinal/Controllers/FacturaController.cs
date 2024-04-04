using HotelProyectoFinal.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static HotelProyectoFinal.DTOS;
using System.Data;

namespace HotelProyectoFinal.Controllers
{
    internal class FacturaController
    {
        public void AgregarFactura(string DPI, DateTime FechaEmision, string nombre, string direccion, string nit, List<DTOS.Transaccion> transacciones)
        {
            try
            {
                using (DBHOTELEntities db= new DBHOTELEntities())
                {
                    if (nombre !="" && nit != "" && direccion !="" && transacciones !=null) { 
                    FACTURA Factura = new FACTURA();
                    TransaccionController Transaccion = new TransaccionController();

                    Factura.IdHuesped = DPI;
                    Factura.FechaEmision = FechaEmision;
                    Factura.Nombre = nombre;
                    Factura.Direccion = direccion;
                    Factura.NIT = nit;
                    db.FACTURAs.Add(Factura);

                    FACTURA_DETALLE detalle = new FACTURA_DETALLE();

                        foreach (var transaccion in transacciones)
                        {
                            detalle.IdFactura = Factura.IdFactura;
                            detalle.IdTransaccion = transaccion.IdTransaccion;
                            detalle.Descripcion = transaccion.Comentario;
                            detalle.Cantidad = transaccion.Cantidad;
                            detalle.Precio = transaccion.precio;
                            detalle.Total = transaccion.Total;
                            detalle.Fecha = transaccion.Fecha;

                            var ActualizarEstadoPagado = db.TRANSACCIONs.FirstOrDefault(m => m.IdTransaccion == transaccion.IdTransaccion);

                            if (ActualizarEstadoPagado != null)
                            {
                                ActualizarEstadoPagado.Pagado = "P";
                            }

                            db.FACTURA_DETALLE.Add(detalle);
                            //db.SaveChanges();
                        }

                        int IdRegistro = transacciones.FirstOrDefault().IdRegistro;
                        var ActualizarTotalPago = db.REGISTROes.FirstOrDefault(m => m.IdRegistro ==IdRegistro);
                        ActualizarTotalPago.TotalPago = Transaccion.TotalporPagar(transacciones);


                        db.SaveChanges();
                        CrearFactura(detalle.IdFactura, transacciones);

                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Porfavor introduzca datos para la facturacion");
                    }
                }


            }catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
        }


        public void CrearFactura(int IdFactura, List<DTOS.Transaccion> transacciones)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

            using (DBHOTELEntities db = new DBHOTELEntities()) {
                var datoscliente = db.FACTURAs.FirstOrDefault(d => d.IdFactura == IdFactura);

            //string PaginaHTML_Texto = "<table border=\"1\"><tr><td>HOLA MUNDO</td></tr></table>";
            var ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "Plantilla.html");
            string PaginaHTML_Texto = File.ReadAllText(ruta);


            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CLIENTE", datoscliente.Nombre);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DOCUMENTO", datoscliente.IdHuesped);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FACTURA", IdFactura.ToString());

            string filas = string.Empty;
            decimal total = 0;


            foreach (var transaccion in transacciones)
                {
                    total += transaccion.Total;
                }

            foreach (var transaccion in transacciones)
            {
                filas += "<tr>";
                filas += "<td>" + transaccion.Cantidad.ToString()+ "</td>";
                filas += "<td>" + transaccion.TipoServicio.ToString() + "</td>";
                filas += "<td>" + transaccion.precio.ToString() + "</td>";
                filas += "<td>" + transaccion.Total.ToString() + "</td>";
                filas += "</tr>";
                total = total;
            }
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS", filas);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTAL", total.ToString());



            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    //Creamos un nuevo documento y lo definimos como PDF
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));


                    //pdfDoc.Add(new Phrase("Hola Mundo"));
                    using (StringReader sr = new StringReader(PaginaHTML_Texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }

            }

            }
        }
    }
}
