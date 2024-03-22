namespace HotelProyectoFinal
{
    partial class Inicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.PanelVistas = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.PanelMenu = new System.Windows.Forms.Panel();
            this.iconButtonPago = new FontAwesome.Sharp.IconButton();
            this.iconButtonServicios = new FontAwesome.Sharp.IconButton();
            this.iconButtonRegistro = new FontAwesome.Sharp.IconButton();
            this.PanelLogo = new System.Windows.Forms.Panel();
            this.iconPictureLogo = new FontAwesome.Sharp.IconPictureBox();
            this.PanelVistas.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.PanelMenu.SuspendLayout();
            this.PanelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelVistas
            // 
            this.PanelVistas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelVistas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(49)))), ((int)(((byte)(117)))));
            this.PanelVistas.Controls.Add(this.panel1);
            this.PanelVistas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PanelVistas.Location = new System.Drawing.Point(218, 0);
            this.PanelVistas.Name = "PanelVistas";
            this.PanelVistas.Size = new System.Drawing.Size(954, 652);
            this.PanelVistas.TabIndex = 2;
            this.PanelVistas.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.iconPictureBox1);
            this.panel1.Location = new System.Drawing.Point(92, 118);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(745, 474);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(51, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(663, 111);
            this.label1.TabIndex = 3;
            this.label1.Text = "BIENVENIDO";
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(49)))), ((int)(((byte)(117)))));
            this.iconPictureBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Hotel;
            this.iconPictureBox1.IconColor = System.Drawing.SystemColors.ButtonHighlight;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.iconPictureBox1.IconSize = 303;
            this.iconPictureBox1.Location = new System.Drawing.Point(228, 16);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(339, 303);
            this.iconPictureBox1.TabIndex = 2;
            this.iconPictureBox1.TabStop = false;
            // 
            // PanelMenu
            // 
            this.PanelMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PanelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.PanelMenu.Controls.Add(this.iconButtonPago);
            this.PanelMenu.Controls.Add(this.iconButtonServicios);
            this.PanelMenu.Controls.Add(this.iconButtonRegistro);
            this.PanelMenu.Controls.Add(this.PanelLogo);
            this.PanelMenu.Location = new System.Drawing.Point(0, 0);
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(220, 652);
            this.PanelMenu.TabIndex = 3;
            // 
            // iconButtonPago
            // 
            this.iconButtonPago.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButtonPago.FlatAppearance.BorderSize = 0;
            this.iconButtonPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonPago.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonPago.ForeColor = System.Drawing.Color.White;
            this.iconButtonPago.IconChar = FontAwesome.Sharp.IconChar.MoneyBill;
            this.iconButtonPago.IconColor = System.Drawing.Color.White;
            this.iconButtonPago.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonPago.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonPago.Location = new System.Drawing.Point(0, 260);
            this.iconButtonPago.Name = "iconButtonPago";
            this.iconButtonPago.Size = new System.Drawing.Size(220, 60);
            this.iconButtonPago.TabIndex = 3;
            this.iconButtonPago.Text = "Pago";
            this.iconButtonPago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonPago.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonPago.UseVisualStyleBackColor = true;
            this.iconButtonPago.Click += new System.EventHandler(this.iconButtonPago_Click);
            // 
            // iconButtonServicios
            // 
            this.iconButtonServicios.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButtonServicios.FlatAppearance.BorderSize = 0;
            this.iconButtonServicios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonServicios.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonServicios.ForeColor = System.Drawing.Color.White;
            this.iconButtonServicios.IconChar = FontAwesome.Sharp.IconChar.CartPlus;
            this.iconButtonServicios.IconColor = System.Drawing.Color.White;
            this.iconButtonServicios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonServicios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonServicios.Location = new System.Drawing.Point(0, 200);
            this.iconButtonServicios.Name = "iconButtonServicios";
            this.iconButtonServicios.Size = new System.Drawing.Size(220, 60);
            this.iconButtonServicios.TabIndex = 2;
            this.iconButtonServicios.Text = "Servicios";
            this.iconButtonServicios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonServicios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonServicios.UseVisualStyleBackColor = true;
            this.iconButtonServicios.Click += new System.EventHandler(this.iconButtonServicios_Click);
            // 
            // iconButtonRegistro
            // 
            this.iconButtonRegistro.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButtonRegistro.FlatAppearance.BorderSize = 0;
            this.iconButtonRegistro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonRegistro.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonRegistro.ForeColor = System.Drawing.Color.White;
            this.iconButtonRegistro.IconChar = FontAwesome.Sharp.IconChar.UserAlt;
            this.iconButtonRegistro.IconColor = System.Drawing.Color.White;
            this.iconButtonRegistro.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonRegistro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonRegistro.Location = new System.Drawing.Point(0, 140);
            this.iconButtonRegistro.Name = "iconButtonRegistro";
            this.iconButtonRegistro.Size = new System.Drawing.Size(220, 60);
            this.iconButtonRegistro.TabIndex = 1;
            this.iconButtonRegistro.Text = "Registro";
            this.iconButtonRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonRegistro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonRegistro.UseVisualStyleBackColor = true;
            this.iconButtonRegistro.Click += new System.EventHandler(this.iconButtonRegistro_Click);
            // 
            // PanelLogo
            // 
            this.PanelLogo.Controls.Add(this.iconPictureLogo);
            this.PanelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelLogo.Location = new System.Drawing.Point(0, 0);
            this.PanelLogo.Name = "PanelLogo";
            this.PanelLogo.Size = new System.Drawing.Size(220, 140);
            this.PanelLogo.TabIndex = 0;
            // 
            // iconPictureLogo
            // 
            this.iconPictureLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPictureLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.iconPictureLogo.IconChar = FontAwesome.Sharp.IconChar.Hotel;
            this.iconPictureLogo.IconColor = System.Drawing.Color.White;
            this.iconPictureLogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureLogo.IconSize = 131;
            this.iconPictureLogo.Location = new System.Drawing.Point(45, 3);
            this.iconPictureLogo.Name = "iconPictureLogo";
            this.iconPictureLogo.Size = new System.Drawing.Size(131, 131);
            this.iconPictureLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.iconPictureLogo.TabIndex = 0;
            this.iconPictureLogo.TabStop = false;
            this.iconPictureLogo.Click += new System.EventHandler(this.iconPictureLogo_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1172, 652);
            this.Controls.Add(this.PanelMenu);
            this.Controls.Add(this.PanelVistas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Inicio";
            this.Text = "Hotel ";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.PanelVistas.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.PanelMenu.ResumeLayout(false);
            this.PanelLogo.ResumeLayout(false);
            this.PanelLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PanelVistas;
        private System.Windows.Forms.Panel PanelLogo;
        private System.Windows.Forms.Panel PanelMenu;
        private FontAwesome.Sharp.IconButton iconButtonRegistro;
        private FontAwesome.Sharp.IconButton iconButtonPago;
        private FontAwesome.Sharp.IconButton iconButtonServicios;
        private FontAwesome.Sharp.IconPictureBox iconPictureLogo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
    }
}