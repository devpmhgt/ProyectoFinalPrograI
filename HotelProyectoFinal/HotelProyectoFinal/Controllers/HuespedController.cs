using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using HotelProyectoFinal.Models;
using MessageBox = System.Windows.Forms.MessageBox;

namespace HotelProyectoFinal.Controllers
{
    class HuespedController
    {

        public void ModificarInformacionHuesped(string Nombres, string Apellidos, string Direccion, string DPI)
        {
            try
            {
                using (DBHOTELEntities db = new DBHOTELEntities())
                {

                    var huesped = db.HUESPEDs.FirstOrDefault(h => h.DPI == DPI);

                    if (huesped != null)
                    {
                        huesped.Nombres = Nombres;
                        huesped.Apellidos = Apellidos;
                        huesped.Direccion = Direccion;
                        huesped.DPI = DPI;
                        db.SaveChanges();
                        MessageBox.Show("Registro actualizado correctamente.", "Modificacion de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

    }
}
