using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.Mathematics
{
    public static class MatrixCalculation
    {
        public static Matrix MultiblyWithScalar(this Matrix matrix1, double scalar)
        {
            throw new NotImplementedException();
        }

        public static Matrix DotProduct(this Matrix matrix1, Matrix matrix2)
        {
            throw new NotImplementedException();
        }

        public static Matrix Multibly(this Matrix matrix1, Matrix matrix2)
        {
            int rows = matrix1.Elements.GetUpperBound(0) + 1;
            int cols = matrix1.Elements.GetUpperBound(1) + 1;
            int rightRows = matrix2.Elements.GetUpperBound(0) + 1;
            int rightCols = matrix2.Elements.GetUpperBound(1) + 1;
            Matrix resultMatrix = new Matrix();
            if (rows == rightRows || cols == rightCols)
            {
                var resultRowsCount = rows;
                if (rightRows < rows)
                {
                    resultRowsCount = rightRows;
                }
                var resultColsCount = cols;
                if (rightCols < cols)
                {
                    resultColsCount = rightCols;
                }
                resultMatrix.Elements = new double[resultRowsCount, resultColsCount];
                for (int i = 0; i < resultRowsCount; i++)
                {
                    for (int j = 0; j < resultColsCount; j++)
                    {
                        resultMatrix.Elements[i, j] =
                            GenerateMultibleValue(i, j, matrix1, matrix2, resultColsCount);
                    }
                }
            }
            return resultMatrix;
        }

        private static double GenerateMultibleValue(int x, int y, Matrix leftMatrix, Matrix rightMatrix, int rank)
        {
            double value1 = 0;
            for (int i = 0; i < rank; i++)
            {
                value1 = value1 + leftMatrix.Elements[i, y] * rightMatrix.Elements[x, i];
            }
            return value1;
        }

        public static Matrix Add(this Matrix matrix1, Matrix matrix2)
        {
            int rank = matrix1.Elements.Length / matrix2.Elements.Rank;
            Matrix resultMatrix = new Matrix();
            resultMatrix.Elements = new double[rank, rank];
            for (int y = 0; y < rank; y++)
            {
                for (int x = 0; x < rank; x++)
                {
                    resultMatrix.Elements[x, y] =
                        matrix1.Elements[x, y] + matrix2.Elements[x, y];
                }
            }
            return resultMatrix;
        }

        public static Matrix SubMatrix(this Matrix matrix1, Matrix matrix2)
        {
            int rank = matrix1.Elements.Length / matrix2.Elements.Rank;
            Matrix resultMatrix = new Matrix();
            resultMatrix.Elements = new double[rank, rank];
            for (int y = 0; y < rank; y++)
            {
                for (int x = 0; x < rank; x++)
                {
                    resultMatrix.Elements[x, y] =
                        matrix1.Elements[x, y] - matrix2.Elements[x, y];
                }
            }
            return resultMatrix;
        }

        public static double Determinante(this Matrix matrix)
        {
            return matrix.CalculateDeterminante();
        }

        public static Matrix Invert(this Matrix matrix)
        {
            UnityMatrix unityMatrix = new UnityMatrix(matrix.Elements.GetUpperBound(0) + 1);
            var determinante = matrix.CalculateDeterminante();
            Matrix resultMatrix = new Matrix();
            resultMatrix.Elements = new double[matrix.Elements.GetUpperBound(0) + 1, matrix.Elements.GetUpperBound(0) + 1];
            for (int i = 0; i <= resultMatrix.Elements.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= resultMatrix.Elements.GetUpperBound(0); j++)
                {
                    var x = CalculateInvertMatrixValue(matrix, matrix.Elements, i, j, determinante, unityMatrix.Elements);
                    resultMatrix.Elements[i, j] = x;
                }
            }
            return resultMatrix;
        }

        private static double CalculateInvertMatrixValue(Matrix matrix, double[,] matrixValues, 
            int x, int y, double determinante, double[,] unityMatrix)
        {
            Matrix resultMatrix = new Matrix();
            resultMatrix.Elements = new double[matrix.Elements.GetUpperBound(0) + 1, matrix.Elements.GetUpperBound(0) + 1];
            for (int i = 0; i <= resultMatrix.Elements.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= resultMatrix.Elements.GetUpperBound(0); j++)
                {
                    if (i == x)
                    {
                        resultMatrix.Elements[j, i] = unityMatrix[j, y];
                    }
                    else
                    {
                        resultMatrix.Elements[j, i] = matrixValues[j, i];
                    }
                }
            }
            var resultDeterminante = resultMatrix.CalculateDeterminante(); // Determinante(resultMatrix.Elements);
            return resultDeterminante / determinante;
        }
    }
}
