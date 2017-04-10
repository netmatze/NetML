using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.Mathematics
{
    public class Matrix
    {
        protected double[,] elements;

        public double[,] Elements
        {
            get { return elements; }
            set { elements = value; }
        }

        private double determinante;

        public double Determinante
        {
            get { return determinante; }
            set { determinante = value; }
        }

        private double TwoTwoDeterminante(double[,] matrix)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }

        public double CalculateDeterminante()
        {
            double determinante = 0;
            int rank = elements.GetUpperBound(0);
            if (rank == 1)
            {
                this.determinante = TwoTwoDeterminante(elements);
                return this.determinante;
            }
            else
            {
                bool signSwitcher = true;
                for (int i = 0; i <= elements.GetUpperBound(0); i++)
                {
                    double[,] generatedMatrixElement = new double[
                       elements.GetUpperBound(0), elements.GetUpperBound(1)];
                    var item = elements[0, i];
                    for (int j = 0; j <= elements.GetUpperBound(0); j++)
                    {
                        for (int m = 1; m <= elements.GetUpperBound(1); m++)
                        {
                            if (j != i)
                            {
                                if (j < i)
                                    generatedMatrixElement[m - 1, j] = elements[m, j];
                                else
                                    generatedMatrixElement[m - 1, j - 1] = elements[m, j];
                            }
                        }
                    }
                    if (!signSwitcher)
                    {
                        var det = DeterminanteIntern(generatedMatrixElement);
                        determinante -= item * det;
                    }
                    else
                    {
                        var det = DeterminanteIntern(generatedMatrixElement);
                        determinante += item * det;
                    }
                    signSwitcher = !signSwitcher;
                }
                this.determinante = determinante;
                return determinante;
            }
        }        

        private double DeterminanteIntern(double[,] matrix)
        {
            double determinante = 0;
            int rank = matrix.GetUpperBound(0);
            if (rank == 1)
            {
                return TwoTwoDeterminante(matrix);
            }
            else
            {
                bool signSwitcher = true;
                for (int i = 0; i <= matrix.GetUpperBound(0); i++)
                {
                    double[,] generatedMatrixElement = new double[
                       matrix.GetUpperBound(0), matrix.GetUpperBound(1)];
                    var item = matrix[i, 0];
                    for (int j = 0; j <= matrix.GetUpperBound(0); j++)
                    {
                        for (int m = 1; m <= matrix.GetUpperBound(1); m++)
                        {
                            if (j != i)
                            {
                                if (j < i)
                                    generatedMatrixElement[m - 1, j] = matrix[m, j];
                                else
                                    generatedMatrixElement[m - 1, j - 1] = matrix[m, j];
                            }
                        }
                    }
                    if (!signSwitcher)
                    {
                        determinante -= item * DeterminanteIntern(generatedMatrixElement);
                    }
                    else
                    {
                        determinante += item * DeterminanteIntern(generatedMatrixElement);
                    }
                    signSwitcher = !signSwitcher;
                }
                return determinante;
            }
        }
    }
}
