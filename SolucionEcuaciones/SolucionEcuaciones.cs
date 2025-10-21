using SolucionEcuacionesService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolucionEcuaciones
{
    public partial class SolucionEcuaciones : Form
    {
        public SolucionEcuaciones()
        {
            InitializeComponent();
            // Centrar el formulario al iniciar
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true; // habilitar scroll automático
        }

        // Cuando el usuario cambia el tamaño de la matriz
        private void numN_ValueChanged(object sender, EventArgs e)
        {
            int n = (int)numN.Value;
            gridMatriz.RowCount = n;
            gridMatriz.ColumnCount = n + 1; // columna extra para término independiente
            gridResultado.RowCount = n;
            gridResultado.ColumnCount = 1;
        }

        private bool ValidarMatriz()
        {
            if (gridMatriz.RowCount == 0 || gridMatriz.ColumnCount == 0)
            {
                MessageBox.Show("Primero debes generar la matriz.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


        private double[,] LeerMatriz()
        {


            int n = (int)numN.Value;
            double[,] matriz = new double[n, n + 1];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n + 1; j++)
                {
                    object valor = gridMatriz.Rows[i].Cells[j].Value;

                    if (valor == null || string.IsNullOrWhiteSpace(valor.ToString()))
                    {
                        MessageBox.Show($"Celda vacía en fila {i + 1}, columna {j + 1}.",
                                        "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null; // salir del método
                    }

                    if (!double.TryParse(valor.ToString(), out double numero))
                    {
                        MessageBox.Show($"El valor en fila {i + 1}, columna {j + 1} no es un número válido.",
                                        "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null; // salir del método
                    }

                    matriz[i, j] = numero;
                }
            }

            return matriz;
        }

        // Mostrar solución vectorial
        private void MostrarResultado(double[] x)
        {
            if (x == null || x.Any(v => double.IsNaN(v)))
            {
                MessageBox.Show("No se pudo calcular la solución.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int n = x.Length;
            gridResultado.RowCount = 1;  // solo una fila para los valores
            gridResultado.ColumnCount = n;

            // Asignar encabezados x1, x2, x3...
            for (int j = 0; j < n; j++)
                gridResultado.Columns[j].HeaderText = $"x{j + 1}";

            // Llenar los valores
            for (int j = 0; j < n; j++)
                gridResultado[j, 0].Value = x[j].ToString("F6");
        }


        // Mostrar matriz (para inversa)
        private void MostrarMatriz(double[,] matriz)
        {
            int filas = matriz.GetLength(0);
            int cols = matriz.GetLength(1);
            gridResultado.RowCount = filas;
            gridResultado.ColumnCount = cols;

            for (int i = 0; i < filas; i++)
                for (int j = 0; j < cols; j++)
                    gridResultado[j, i].Value = matriz[i, j].ToString("F6");
        }



        private void btnGauss_Click(object sender, EventArgs e)
        {
            if (!ValidarMatriz()) return;

            int n = (int)numN.Value;
            double[,] matriz = LeerMatriz();
            if (matriz == null) return;

            var metodos = new MetodosNumericos();
            var resultado = metodos.EliminacionGauss(matriz, n);

            if (resultado == null || resultado.Solucion == null)
            {
                MessageBox.Show("No se pudo calcular la solución.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Aquí va el bloque que muestra todas las iteraciones de x
            int filas = resultado.IteracionesX.Count; // cada fila = vector x intermedio
            int columnas = n; // solo x1, x2, x3
            gridResultado.RowCount = filas;
            gridResultado.ColumnCount = columnas;

            for (int j = 0; j < columnas; j++)
                gridResultado.Columns[j].HeaderText = $"X{j + 1}";

            for (int i = 0; i < filas; i++)
            {
                double[] xPaso = resultado.IteracionesX[i];
                for (int j = 0; j < columnas; j++)
                {
                    gridResultado[j, i].Value = xPaso[j].ToString("F6");
                }
            }

            // Mostrar la solución final debajo del grid
            MostrarResultado(resultado.Solucion);
        }

        private void btnGaussJordan_Click(object sender, EventArgs e)
        {
            if (!ValidarMatriz()) return;

            double[,] matriz = LeerMatriz();
            if (matriz == null) return;

            int n = (int)numN.Value;

            var metodos = new MetodosNumericos();
            var resultado = metodos.GaussJordan(matriz, n);

            MostrarResultado(resultado.Solucion);

            foreach (var paso in resultado.Pasos)
            {
                Console.WriteLine(paso.Descripcion);
                Console.WriteLine(MetodosNumericos.MatrizToString(paso.Matriz));
            }
        }

        private void btn_GenerarMatriz_Click(object sender, EventArgs e)
        {
            int n = (int)numN.Value;

            if (n < 2)
            {
                MessageBox.Show("Elige un número mayor o igual a 2.", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Configurar DataGridView
            gridMatriz.RowCount = n;
            gridMatriz.ColumnCount = n + 1; // columna extra para término independiente y otra para la variable

            gridResultado.RowCount = n;
            gridResultado.ColumnCount = 1;

            // Llenar la matriz con valores aleatorios por defecto
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n + 1; j++)
                {
                    gridMatriz.Rows[i].Cells[j].Value = rnd.Next(1, 10); // coeficientes y término independiente
                }
            }

            // Opcional: poner nombres de columnas
            for (int j = 0; j < n; j++)
                gridMatriz.Columns[j].HeaderText = $"x{j + 1}";
            gridMatriz.Columns[n].HeaderText = "=";


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridMatriz_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txt = e.Control as TextBox;
            if (txt != null)
            {
                // Quitamos cualquier evento anterior para evitar múltiples suscripciones
                txt.KeyPress -= GridMatriz_KeyPress;
                // Asignamos nuestro evento de validación de teclas
                txt.KeyPress += GridMatriz_KeyPress;
            }
        }

        private void GridMatriz_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, punto decimal, signo negativo y teclas de control (retroceso, suprimir, etc.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true; // Cancela la entrada
                MessageBox.Show("Solo se permiten números (use '.' para decimales).",
                    "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void txtN_Click(object sender, EventArgs e)
        {

        }

        private void gridResultado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private bool EsDiagonalmenteDominante(double[,] a)
        {
            int n = a.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                double suma = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                        suma += Math.Abs(a[i, j]);
                }
                if (Math.Abs(a[i, i]) <= suma)
                    return false;
            }
            return true;
        }

        public static List<double[]> GaussSeidel(double[,] a, double eaMax, int maxIter)
        {
            int n = a.GetLength(0);
            double[] x = new double[n];
            double[] ea = new double[n];
            List<double[]> historial = new List<double[]>();

            // Inicializamos x y errores
            for (int i = 0; i < n; i++)
            {
                x[i] = 0.0;
                ea[i] = 0.0; // Primer iteración sin error
            }

            for (int iter = 0; iter < maxIter; iter++)
            {
                bool terminar = true;
                double[] fila = new double[2 * n + 1]; // x + ea + eaMax
                double[] xPrev = new double[n];
                Array.Copy(x, xPrev, n);

                for (int i = 0; i < n; i++)
                {
                    double suma = 0.0;
                    for (int j = 0; j < n; j++)
                    {
                        if (i != j)
                            suma += a[i, j] * x[j];
                    }

                    x[i] = (a[i, n] - suma) / a[i, i];

                    // Calcular error aproximado si no es la primera iteración
                    if (iter == 0)
                        ea[i] = 0.0;
                    else
                        ea[i] = Math.Abs((x[i] - xPrev[i]) / x[i]);

                    if (ea[i] > eaMax)
                        terminar = false;

                    fila[i] = x[i];
                    fila[n + i] = ea[i];
                }

                // Guardar el error máximo de esta iteración
                double eaIterMax = 0;
                for (int i = 0; i < n; i++)
                    if (ea[i] > eaIterMax)
                        eaIterMax = ea[i];
                fila[2 * n] = eaIterMax;

                historial.Add(fila);

                if (terminar)
                    break;
            }

            return historial;
        }



        private void btn_InvertirMatriz_Click(object sender, EventArgs e)
        {
            if (!ValidarMatriz()) return;

            int n = (int)numN.Value;
            double[,] matriz = new double[n, n];

            // Leer solo la parte cuadrada
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matriz[i, j] = gridMatriz.Rows[i].Cells[j].Value == null ? 0 :
                                   Convert.ToDouble(gridMatriz.Rows[i].Cells[j].Value);

            var metodos = new MetodosNumericos();
            var resultado = metodos.Invierte(matriz, n);

            MostrarMatriz(resultado.Inversa);

            foreach (var paso in resultado.Pasos)
            {
                Console.WriteLine(paso.Descripcion);
                Console.WriteLine(MetodosNumericos.MatrizToString(paso.Matriz));
            }
        }
    }
}
