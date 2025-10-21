using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolucionEcuacionesService
{
    public class MetodosNumericos
    {
        //Un paso intermedio: snapshot de una matriz más una descripción opcional.
        public class PasoMatriz
        {
            public double[,] Matriz { get; set; }
            public string Descripcion { get; set; }
        }

        //Resultado de Eliminación de Gauss
        public class ResultadoGauss
        {
            public List<PasoMatriz> Pasos { get; set; } = new List<PasoMatriz>();
            public double[] Solucion { get; set; }

            // Nueva propiedad: lista de vectores x por cada paso de sustitución
            public List<double[]> IteracionesX { get; set; } = new List<double[]>();
        }

        //Resultado de Gauss-Jordan (y también para inversión)
        public class ResultadoGaussJordan
        {
            public List<PasoMatriz> Pasos { get; set; } = new List<PasoMatriz>();
            public double[] Solucion { get; set; } // para sistemas
            public double[,] Inversa { get; set; } // para inversión si aplica
        }

        //Representa una iteración del método Gauss-Seidel
        public class IteracionGaussSeidel
        {
            public int Iteracion { get; set; }
            public double[] X { get; set; }
            public double[] Error { get; set; }
        }

      

        private static double[,] CloneMatrix(double[,] m)
        {
            var r = new double[m.GetLength(0), m.GetLength(1)];
            for (int i = 0; i < m.GetLength(0); i++)
                for (int j = 0; j < m.GetLength(1); j++)
                    r[i, j] = m[i, j];
            return r;
        }

        private static string FormatoDouble(double v) => v.ToString("F6", CultureInfo.InvariantCulture);

        //Convierte matriz a string (opcional, útil para registros o despliegues simples)
        public static string MatrizToString(double[,] a)
        {
            int r = a.GetLength(0), c = a.GetLength(1);
            var lines = new List<string>();
            for (int i = 0; i < r; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < c; j++)
                    row.Add(FormatoDouble(a[i, j]));
                lines.Add(string.Join(" ", row));
            }
            return string.Join(Environment.NewLine, lines);
        }

        
        // Realiza eliminación de Gauss con pivoteo (guarda pasos intermedios).
        // Entrada: matriz aumentada de tamaño n x (n+1).
        
        public ResultadoGauss EliminacionGauss(double[,] aInput, int n)
        {
            double[,] a = CloneMatrix(aInput);
            var resultado = new ResultadoGauss();

            // Guardar estado inicial
            resultado.Pasos.Add(new PasoMatriz { Matriz = CloneMatrix(a), Descripcion = "Matriz ampliada inicial" });

            EliminacionAdelante(a, n, resultado.Pasos);

            // Sustitución hacia atrás guardando cada paso de x
            double[] x = SustitucionAtras(a, n, resultado.IteracionesX);

            resultado.Solucion = x;
            return resultado;
        }

        
        // Eliminación hacia adelante con pivoteo parcial (modifica 'a' in-place).
        // Guarda cada paso en la lista 'pasos'.
        
        private void EliminacionAdelante(double[,] a, int n, List<PasoMatriz> pasos)
        {
            for (int i = 0; i < n; i++)
            {
                Pivotea(a, i, n, pasos);

                // Si pivote cercano a cero -> sistema singular (se continúa pero puede dar NaN/Inf)
                double pivote = a[i, i];
                if (Math.Abs(pivote) < 1e-15) // cuidado: posible singularidad
                {
                    pasos.Add(new PasoMatriz { Matriz = CloneMatrix(a), Descripcion = $"Pivote en fila {i} es aproximadamente cero ({pivote})" });
                    continue;
                }

                for (int j = i + 1; j < n; j++)
                {
                    double m = a[j, i] / a[i, i];
                    for (int k = i; k <= n; k++) // hasta la columna ampliada
                    {
                        a[j, k] -= m * a[i, k];
                    }
                }

                pasos.Add(new PasoMatriz { Matriz = CloneMatrix(a), Descripcion = $"Después de eliminar columnas con pivote en fila {i}" });
            }
        }

       
        // Pivoteo parcial: busca la fila con el mayor valor absoluto en la columna 'i' y la intercambia con la fila i.
        // Guarda un paso si ocurre intercambio.
       
        private void Pivotea(double[,] a, int i, int n, List<PasoMatriz> pasos)
        {
            int max = i;
            for (int j = i + 1; j < n; j++)
            {
                if (Math.Abs(a[j, i]) > Math.Abs(a[max, i]))
                    max = j;
            }
            if (max != i)
            {
                // swap filas i y max (todas las columnas hasta n)
                for (int k = 0; k <= n; k++)
                {
                    double tmp = a[i, k];
                    a[i, k] = a[max, k];
                    a[max, k] = tmp;
                }
                pasos.Add(new PasoMatriz { Matriz = CloneMatrix(a), Descripcion = $"Intercambio de fila {i} con fila {max} (pivoteo)" });
            }
        }

        // Sustitución hacia atrás sobre una matriz triangular superior ampliada (in-place).
        // Devuelve el vector solución x (size n).

        private double[] SustitucionAtras(double[,] a, int n, List<double[]> iteracionesX)
        {
            double[] x = new double[n];

            for (int i = n - 1; i >= 0; i--)
            {
                double suma = 0;
                for (int j = i + 1; j < n; j++)
                {
                    suma += a[i, j] * x[j];
                }
                x[i] = (Math.Abs(a[i, i]) < 1e-15) ? double.NaN : (a[i, n] - suma) / a[i, i];
            }

            // Ahora el vector x está completo, y solo ahora lo guardamos como primera iteración
            iteracionesX.Add((double[])x.Clone());

            return x;
        }




        // Resuelve sistema por Gauss-Jordan con pivoteo. Entrada: matriz ampliada n x (n+1).
        // Devuelve pasos y la solución x.

        public ResultadoGaussJordan GaussJordan(double[,] aInput, int n)
        {
            if (aInput == null) throw new ArgumentNullException(nameof(aInput));
            if (aInput.GetLength(0) != n || aInput.GetLength(1) != n + 1)
                throw new ArgumentException("La matriz debe ser n x (n+1).");

            double[,] a = CloneMatrix(aInput);
            var resultado = new ResultadoGaussJordan();
            resultado.Pasos.Add(new PasoMatriz { Matriz = CloneMatrix(a), Descripcion = "Matriz ampliada inicial" });

            for (int i = 0; i < n; i++)
            {
                Pivotea(a, i, n, resultado.Pasos);

                double pivote = a[i, i];
                if (Math.Abs(pivote) < 1e-15)
                {
                    resultado.Pasos.Add(new PasoMatriz { Matriz = CloneMatrix(a), Descripcion = $"Pivote en fila {i} ~ 0" });
                    continue;
                }

                // Normalizar fila i (dividir por el pivote) desde i hasta n
                for (int k = i; k <= n; k++)
                    a[i, k] /= pivote;

                resultado.Pasos.Add(new PasoMatriz { Matriz = CloneMatrix(a), Descripcion = $"Fila {i} normalizada (R{i}/pivote)" });

                // Hacer ceros en columna i para todas las otras filas
                for (int j = 0; j < n; j++)
                {
                    if (j == i) continue;
                    double factor = a[j, i];
                    for (int k = i; k <= n; k++)
                        a[j, k] -= factor * a[i, k];
                    resultado.Pasos.Add(new PasoMatriz { Matriz = CloneMatrix(a), Descripcion = $"R{j} = R{j} - ({factor})*R{i}" });
                }
            }

            // Ahora la solución está en la última columna
            double[] x = new double[n];
            for (int i = 0; i < n; i++)
                x[i] = a[i, n];

            resultado.Solucion = x;
            return resultado;
        }

       

       
        // Invierte una matriz cuadrada A (n x n) usando Gauss-Jordan con pivoteo.
        // Devuelve pasos (matriz ampliada en cada paso) y la matriz inversa final.
        public ResultadoGaussJordan Invierte(double[,] aInput, int n)
        {
            if (aInput == null) throw new ArgumentNullException(nameof(aInput));
            if (aInput.GetLength(0) != n || aInput.GetLength(1) != n)
                throw new ArgumentException("La matriz a invertir debe ser n x n.");

            // crear matriz ampliada (n x 2n) con [A | I]
            double[,] ampliada = AmpliaMatriz(aInput, n);
            var resultado = new ResultadoGaussJordan();
            resultado.Pasos.Add(new PasoMatriz { Matriz = CloneMatrix(ampliada), Descripcion = "Matriz ampliada [A | I] inicial" });

            // Procedimiento Gauss-Jordan sobre la ampliada
            for (int i = 0; i < n; i++)
            {
                PivoteaInv(ampliada, i, n, resultado.Pasos);

                double pivote = ampliada[i, i];
                if (Math.Abs(pivote) < 1e-15)
                {
                    resultado.Pasos.Add(new PasoMatriz { Matriz = CloneMatrix(ampliada), Descripcion = $"Pivote (inversa) en fila {i} ~ 0" });
                    continue;
                }

                // normalizar fila i (dividir por pivote) para todas las 2n columnas
                for (int k = 0; k < 2 * n; k++)
                    ampliada[i, k] /= pivote;

                resultado.Pasos.Add(new PasoMatriz { Matriz = CloneMatrix(ampliada), Descripcion = $"Fila {i} normalizada (inversa)" });

                // ceros en columna i para otras filas
                for (int j = 0; j < n; j++)
                {
                    if (j == i) continue;
                    double factor = ampliada[j, i];
                    for (int k = 0; k < 2 * n; k++)
                        ampliada[j, k] -= factor * ampliada[i, k];

                    resultado.Pasos.Add(new PasoMatriz { Matriz = CloneMatrix(ampliada), Descripcion = $"R{j} = R{j} - ({factor})*R{i} (inversa)" });
                }
            }

            // Extraer la parte derecha como inversa
            double[,] inversa = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    inversa[i, j] = ampliada[i, j + n];

            resultado.Pasos.Add(new PasoMatriz { Matriz = CloneMatrix(ampliada), Descripcion = "Matriz final ampliada [I | A^-1]" });
            resultado.Inversa = inversa;
            return resultado;
        }

        
        //Amplía A (n x n) a [A | I] (n x 2n)
        
        public double[,] AmpliaMatriz(double[,] a, int n)
        {
            var ampliada = new double[n, 2 * n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    ampliada[i, j] = a[i, j];
                for (int j = 0; j < n; j++)
                    ampliada[i, n + j] = (i == j) ? 1.0 : 0.0;
            }
            return ampliada;
        }

        
        // Pivoteo para la matriz ampliada (inversa). Reacomoda filas para que el pivote sea el mayor.
        // Guarda paso cuando haya intercambio
        
        private void PivoteaInv(double[,] ampliada, int i, int n, List<PasoMatriz> pasos)
        {
            int max = i;
            for (int fila = i + 1; fila < n; fila++)
            {
                if (Math.Abs(ampliada[fila, i]) > Math.Abs(ampliada[max, i]))
                    max = fila;
            }
            if (max != i)
            {
                for (int k = 0; k < 2 * n; k++)
                {
                    double t = ampliada[i, k];
                    ampliada[i, k] = ampliada[max, k];
                    ampliada[max, k] = t;
                }
                pasos.Add(new PasoMatriz { Matriz = CloneMatrix(ampliada), Descripcion = $"Intercambio filas {i} y {max} (pivoteo inversa)" });
            }
        }

        // Gauss-Seidel paso a paso. Entrada: matriz aumentada n x (n+1).
        // Devuelve la lista de iteraciones (cada una con X y errores aproximados).

        public List<IteracionGaussSeidel> GaussSeidel(double[,] aInput, int n, double eaMax, int maxIter = 1000)
        {
            if (aInput == null) throw new ArgumentNullException(nameof(aInput));
            if (aInput.GetLength(0) != n || aInput.GetLength(1) != n + 1)
                throw new ArgumentException("La matriz debe ser n x (n+1).");

            double[,] a = CloneMatrix(aInput);
            var iteraciones = new List<IteracionGaussSeidel>();

            double[] x = new double[n];
            double[] ea = new double[n];

            // Inicializamos errores en NaN para que la primera iteración salga en blanco
            for (int i = 0; i < n; i++)
            {
                x[i] = 0.0;
                ea[i] = double.NaN;
            }

            bool terminar;
            int iter = 0;
            do
            {
                terminar = true;
                double[] xPrev = (double[])x.Clone();

                for (int i = 0; i < n; i++)
                {
                    double suma = 0.0;
                    for (int j = 0; j < n; j++)
                        if (i != j) suma += a[i, j] * x[j];

                    double nuevoXi = (a[i, n] - suma) / a[i, i];

                    // Solo calculamos error a partir de la segunda iteración
                    if (iter > 0 && Math.Abs(nuevoXi) > double.Epsilon)
                        ea[i] = Math.Abs((nuevoXi - x[i]) / nuevoXi);

                    x[i] = nuevoXi;

                    if (!(iter > 0 && ea[i] < eaMax))
                        terminar = false;
                }

                iteraciones.Add(new IteracionGaussSeidel
                {
                    Iteracion = ++iter,
                    X = (double[])x.Clone(),
                    Error = (double[])ea.Clone()
                });

                if (iter >= maxIter) break;

            } while (!terminar);

            return iteraciones;
        }

    }
}