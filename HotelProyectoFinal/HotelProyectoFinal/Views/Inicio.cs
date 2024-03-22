using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using HotelProyectoFinal.Views;

namespace HotelProyectoFinal
{
    public partial class Inicio : Form
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;



        public Inicio()
        {
            InitializeComponent();

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            PanelMenu.Controls.Add(leftBorderBtn);


        }

        private struct RGBcolors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 128, 114);
            public static Color color4 = Color.FromArgb(249, 88, 155);
            public static Color color5 = Color.FromArgb(24, 162, 251);
        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn!=null)
            {
                DisableButton();
                currentBtn= (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //Left border button

                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
            }
        }

        private void DisableButton()
        {
            if(currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleRight;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation= TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconPictureLogo_Click(object sender, EventArgs e)
        {

        }


        private void iconButtonRegistro_Click(object sender, EventArgs e)
        {

            ActivateButton(sender, RGBcolors.color1);
            
            // Crear una instancia del formulario secundario

            UCRegistro registro = new UCRegistro();
            PanelVistas.Controls.Clear();
            PanelVistas.Controls.Add(registro);

            // Ajustar el tamaño del formulario secundario al tamaño del panel contenedor
            registro.Dock = DockStyle.Fill;
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }

        private void iconButtonServicios_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBcolors.color2);
            UCServicios servicios = new UCServicios();
            PanelVistas.Controls.Clear();
            PanelVistas.Controls.Add(servicios);
            servicios.Dock = DockStyle.Fill;
        }

        private void iconButtonPago_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBcolors.color3);
        }
    }
}
