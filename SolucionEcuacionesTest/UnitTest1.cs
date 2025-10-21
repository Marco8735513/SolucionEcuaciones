using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolucionEcuacionesService;

namespace SolucionEcuacionesTest
{
    [TestClass]
    public class UnitTest1
    {
        // Matriz de prueba que da como resultado [1, 1, 1]
        private double[,] matrizTest = new double[,]
        {
            { 5, 1, 1, 7 },
            { 1, 4, 1, 6 },
            { 2, 1, 5, 8 }
        };

        [TestMethod]
        public void TestEliminacionGauss()
        {
            var metodos = new MetodosNumericos();
            var resultado = metodos.EliminacionGauss((double[,])matrizTest.Clone(), 3);

            Assert.AreEqual(1.0, Math.Round(resultado.Solucion[0], 6));
            Assert.AreEqual(1.0, Math.Round(resultado.Solucion[1], 6));
            Assert.AreEqual(1.0, Math.Round(resultado.Solucion[2], 6));
        }

        [TestMethod]
        public void TestGaussJordan()
        {
            var metodos = new MetodosNumericos();
            var resultado = metodos.GaussJordan((double[,])matrizTest.Clone(), 3);

            Assert.AreEqual(1.0, Math.Round(resultado.Solucion[0], 6));
            Assert.AreEqual(1.0, Math.Round(resultado.Solucion[1], 6));
            Assert.AreEqual(1.0, Math.Round(resultado.Solucion[2], 6));
        }

        [TestMethod]
        public void TestGaussSeidel()
        {
            var metodos = new MetodosNumericos();
            // Tolerancia y máximo de iteraciones
            double tol = 1e-8;
            int maxIter = 1000;

            var iteraciones = metodos.GaussSeidel((double[,])matrizTest.Clone(), 3, tol, maxIter);

            // Tomar última iteración
            double[] resultado = iteraciones[iteraciones.Count - 1].X;

            Assert.AreEqual(1.0, Math.Round(resultado[0], 6));
            Assert.AreEqual(1.0, Math.Round(resultado[1], 6));
            Assert.AreEqual(1.0, Math.Round(resultado[2], 6));
        }

        [TestMethod]
        public void TestInversa()
        {
            var metodos = new MetodosNumericos();

            // Tomar solo la parte cuadrada 3x3
            double[,] matrizCuadrada = new double[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    matrizCuadrada[i, j] = matrizTest[i, j];

            var resultado = metodos.Invierte(matrizCuadrada, 3);
            double[,] inversa = resultado.Inversa;

            // Multiplicar matriz * inversa
            double[,] producto = new double[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    double suma = 0;
                    for (int k = 0; k < 3; k++)
                        suma += matrizCuadrada[i, k] * inversa[k, j];
                    producto[i, j] = suma;
                }

            // Comprobar que producto es aproximadamente la identidad
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    double esperado = (i == j) ? 1.0 : 0.0;
                    Assert.AreEqual(esperado, Math.Round(producto[i, j], 6));
                }
        }
    }
}
