using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.Mathematics
{
    public class UnityMatrix : Matrix
    {
        public UnityMatrix(int rank)
        {
            elements = new double[rank, rank];
            for (int i = 0; i < rank; i++)
            {
                for (int j = 0; j < rank; j++)
                {
                    if (i == j)
                        elements[i, j] = 1;
                }
            }
        }
    }
}
