namespace HotelProyectoFinal
{
    partial class UCServicios
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.Buscar = new System.Windows.Forms.Button();
            this.txtDPI = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbxTipoServicio = new System.Windows.Forms.ComboBox();
            this.cmbxServicio = new System.Windows.Forms.ComboBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.NUCantidad = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbxHabitacion = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(53, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cliente";
            // 
            // Buscar
            // 
            this.Buscar.BackColor = System.Drawing.Color.Gold;
            this.Buscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Buscar.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Buscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Buscar.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Buscar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Buscar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Buscar.Location = new System.Drawing.Point(343, 49);
            this.Buscar.Name = "Buscar";
            this.Buscar.Size = new System.Drawing.Size(92, 27);
            this.Buscar.TabIndex = 2;
            this.Buscar.Text = "Buscar";
            this.Buscar.UseVisualStyleBackColor = false;
            this.Buscar.Click += new System.EventHandler(this.Buscar_Click);
            // 
            // txtDPI
            // 
            this.txtDPI.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDPI.Location = new System.Drawing.Point(124, 49);
            this.txtDPI.Name = "txtDPI";
            this.txtDPI.Size = new System.Drawing.Size(199, 26);
            this.txtDPI.TabIndex = 3;
            this.txtDPI.TextChanged += new System.EventHandler(this.txtDPI_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Location = new System.Drawing.Point(57, 334);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(835, 268);
            this.dataGridView1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(299, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Servicios";
            // 
            // cmbxTipoServicio
            // 
            this.cmbxTipoServicio.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxTipoServicio.FormattingEnabled = true;
            this.cmbxTipoServicio.Location = new System.Drawing.Point(47, 195);
            this.cmbxTipoServicio.Name = "cmbxTipoServicio";
            this.cmbxTipoServicio.Size = new System.Drawing.Size(235, 26);
            this.cmbxTipoServicio.TabIndex = 6;
            this.cmbxTipoServicio.SelectedIndexChanged += new System.EventHandler(this.cmbxTipoServicio_SelectedIndexChanged);
            // 
            // cmbxServicio
            // 
            this.cmbxServicio.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxServicio.FormattingEnabled = true;
            this.cmbxServicio.Location = new System.Drawing.Point(303, 195);
            this.cmbxServicio.Name = "cmbxServicio";
            this.cmbxServicio.Size = new System.Drawing.Size(164, 26);
            this.cmbxServicio.TabIndex = 7;
            // 
            // btnAgregar
            // 
            this.btnAgregar.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnAgregar.BackColor = System.Drawing.Color.YellowGreen;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.Location = new System.Drawing.Point(830, 194);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(111, 27);
            this.btnAgregar.TabIndex = 8;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(43, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Categoria";
            // 
            // NUCantidad
            // 
            this.NUCantidad.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NUCantidad.Location = new System.Drawing.Point(493, 195);
            this.NUCantidad.Name = "NUCantidad";
            this.NUCantidad.Size = new System.Drawing.Size(77, 26);
            this.NUCantidad.TabIndex = 10;
            this.NUCantidad.ValueChanged += new System.EventHandler(this.NUCantidad_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(489, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Cantidad";
            // 
            // cmbxHabitacion
            // 
            this.cmbxHabitacion.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxHabitacion.FormattingEnabled = true;
            this.cmbxHabitacion.Location = new System.Drawing.Point(600, 194);
            this.cmbxHabitacion.Name = "cmbxHabitacion";
            this.cmbxHabitacion.Size = new System.Drawing.Size(204, 26);
            this.cmbxHabitacion.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(596, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Habitacion";
            // 
            // UCServicios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(49)))), ((int)(((byte)(117)))));
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbxHabitacion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NUCantidad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.cmbxServicio);
            this.Controls.Add(this.cmbxTipoServicio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtDPI);
            this.Controls.Add(this.Buscar);
            this.Controls.Add(this.label1);
            this.Name = "UCServicios";
            this.Size = new System.Drawing.Size(954, 652);
            this.Load += new System.EventHandler(this.UCServicios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Buscar;
        private System.Windows.Forms.TextBox txtDPI;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbxTipoServicio;
        private System.Windows.Forms.ComboBox cmbxServicio;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NUCantidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbxHabitacion;
        private System.Windows.Forms.Label label5;
    }
}
