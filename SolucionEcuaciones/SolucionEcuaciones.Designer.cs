using SolucionEcuacionesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SolucionEcuaciones
{
    partial class SolucionEcuaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolucionEcuaciones));
            this.btnGauss = new System.Windows.Forms.Button();
            this.btnGaussJordan = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGaussSeidel = new System.Windows.Forms.Button();
            this.btn_GenerarMatriz = new System.Windows.Forms.Button();
            this.gridMatriz = new System.Windows.Forms.DataGridView();
            this.gridResultado = new System.Windows.Forms.DataGridView();
            this.numN = new System.Windows.Forms.NumericUpDown();
            this.lbl_Ingrese = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_InvertirMatriz = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridMatriz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridResultado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numN)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGauss
            // 
            this.btnGauss.BackColor = System.Drawing.Color.YellowGreen;
            this.btnGauss.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGauss.Location = new System.Drawing.Point(754, 82);
            this.btnGauss.Name = "btnGauss";
            this.btnGauss.Size = new System.Drawing.Size(139, 45);
            this.btnGauss.TabIndex = 16;
            this.btnGauss.Text = "Metodo de Gauss";
            this.btnGauss.UseVisualStyleBackColor = false;
            this.btnGauss.Click += new System.EventHandler(this.btnGauss_Click);
            // 
            // btnGaussJordan
            // 
            this.btnGaussJordan.BackColor = System.Drawing.Color.SlateBlue;
            this.btnGaussJordan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGaussJordan.Location = new System.Drawing.Point(754, 134);
            this.btnGaussJordan.Name = "btnGaussJordan";
            this.btnGaussJordan.Size = new System.Drawing.Size(139, 45);
            this.btnGaussJordan.TabIndex = 17;
            this.btnGaussJordan.Text = "Metodo de Gauss Jordan";
            this.btnGaussJordan.UseVisualStyleBackColor = false;
            this.btnGaussJordan.Click += new System.EventHandler(this.btnGaussJordan_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(190, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Ingresa Un Matriz";
            // 
            // btnGaussSeidel
            // 
            this.btnGaussSeidel.BackColor = System.Drawing.Color.Gold;
            this.btnGaussSeidel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGaussSeidel.Location = new System.Drawing.Point(899, 134);
            this.btnGaussSeidel.Name = "btnGaussSeidel";
            this.btnGaussSeidel.Size = new System.Drawing.Size(138, 45);
            this.btnGaussSeidel.TabIndex = 19;
            this.btnGaussSeidel.Text = "Metodo de Gauss Seidel";
            this.btnGaussSeidel.UseVisualStyleBackColor = false;
            this.btnGaussSeidel.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_GenerarMatriz
            // 
            this.btn_GenerarMatriz.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_GenerarMatriz.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GenerarMatriz.Location = new System.Drawing.Point(475, 85);
            this.btn_GenerarMatriz.Name = "btn_GenerarMatriz";
            this.btn_GenerarMatriz.Size = new System.Drawing.Size(137, 42);
            this.btn_GenerarMatriz.TabIndex = 20;
            this.btn_GenerarMatriz.Text = "GenerarMatriz";
            this.btn_GenerarMatriz.UseVisualStyleBackColor = false;
            this.btn_GenerarMatriz.Click += new System.EventHandler(this.btn_GenerarMatriz_Click);
            // 
            // gridMatriz
            // 
            this.gridMatriz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMatriz.Location = new System.Drawing.Point(94, 185);
            this.gridMatriz.Name = "gridMatriz";
            this.gridMatriz.RowHeadersWidth = 51;
            this.gridMatriz.RowTemplate.Height = 24;
            this.gridMatriz.Size = new System.Drawing.Size(1217, 344);
            this.gridMatriz.TabIndex = 21;
            this.gridMatriz.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // gridResultado
            // 
            this.gridResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridResultado.Location = new System.Drawing.Point(94, 553);
            this.gridResultado.Name = "gridResultado";
            this.gridResultado.RowHeadersWidth = 51;
            this.gridResultado.RowTemplate.Height = 24;
            this.gridResultado.Size = new System.Drawing.Size(1217, 252);
            this.gridResultado.TabIndex = 22;
            this.gridResultado.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridResultado_CellContentClick);
            // 
            // numN
            // 
            this.numN.Location = new System.Drawing.Point(208, 94);
            this.numN.Name = "numN";
            this.numN.Size = new System.Drawing.Size(244, 22);
            this.numN.TabIndex = 23;
            // 
            // lbl_Ingrese
            // 
            this.lbl_Ingrese.AutoSize = true;
            this.lbl_Ingrese.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Ingrese.Location = new System.Drawing.Point(205, 51);
            this.lbl_Ingrese.Name = "lbl_Ingrese";
            this.lbl_Ingrese.Size = new System.Drawing.Size(235, 18);
            this.lbl_Ingrese.TabIndex = 24;
            this.lbl_Ingrese.Text = "Ingrese el tamaño de la matriz";
            this.lbl_Ingrese.Click += new System.EventHandler(this.txtN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(164, 532);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 18);
            this.label1.TabIndex = 25;
            this.label1.Text = "Resultado De La Matriz";
            // 
            // btn_InvertirMatriz
            // 
            this.btn_InvertirMatriz.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btn_InvertirMatriz.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_InvertirMatriz.Location = new System.Drawing.Point(899, 82);
            this.btn_InvertirMatriz.Name = "btn_InvertirMatriz";
            this.btn_InvertirMatriz.Size = new System.Drawing.Size(138, 45);
            this.btn_InvertirMatriz.TabIndex = 26;
            this.btn_InvertirMatriz.Text = "Metodo Invertir Matriz";
            this.btn_InvertirMatriz.UseVisualStyleBackColor = false;
            this.btn_InvertirMatriz.Click += new System.EventHandler(this.btn_InvertirMatriz_Click);
            // 
            // SolucionEcuaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 935);
            this.Controls.Add(this.btn_InvertirMatriz);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_Ingrese);
            this.Controls.Add(this.numN);
            this.Controls.Add(this.gridResultado);
            this.Controls.Add(this.gridMatriz);
            this.Controls.Add(this.btn_GenerarMatriz);
            this.Controls.Add(this.btnGaussSeidel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnGaussJordan);
            this.Controls.Add(this.btnGauss);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SolucionEcuaciones";
            this.Text = "Solucion de Ecuaciones";
            ((System.ComponentModel.ISupportInitialize)(this.gridMatriz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridResultado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidarMatriz()) return;

            int n = (int)numN.Value;
            double[,] matriz = LeerMatriz();
            if (matriz == null) return;

            double eaMax = 0.0001; // tolerancia
            var metodos = new MetodosNumericos();
            var iteraciones = metodos.GaussSeidel(matriz, n, eaMax);

            if (iteraciones.Count == 0)
            {
                MessageBox.Show("No se pudo obtener solución.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Configurar gridResultado para mostrar todas las iteraciones
            int columnas = n * 2; // X1..Xn | Ea1..Ean | EaMax
            gridResultado.RowCount = iteraciones.Count;
            gridResultado.ColumnCount = columnas;

            // Asignar encabezados
            for (int j = 0; j < n; j++)
                gridResultado.Columns[j].HeaderText = $"X{j + 1}";
            for (int j = 0; j < n; j++)
                gridResultado.Columns[n + j].HeaderText = $"Ea{j + 1}";
           

            // Llenar el grid con todas las iteraciones
            for (int i = 0; i < iteraciones.Count; i++)
            {
                var iter = iteraciones[i];
                for (int j = 0; j < n; j++)
                    gridResultado[j, i].Value = iter.X[j].ToString("F6");
                for (int j = 0; j < n; j++)
                    gridResultado[n + j, i].Value = iter.Error[j].ToString("F6");

               
            }
        }

        #endregion
        private System.Windows.Forms.Button btnGauss;
        private System.Windows.Forms.Button btnGaussJordan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGaussSeidel;
        private System.Windows.Forms.Button btn_GenerarMatriz;
        private System.Windows.Forms.DataGridView gridMatriz;
        private System.Windows.Forms.DataGridView gridResultado;
        private System.Windows.Forms.NumericUpDown numN;
        private System.Windows.Forms.Label lbl_Ingrese;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_InvertirMatriz;
    }
}

