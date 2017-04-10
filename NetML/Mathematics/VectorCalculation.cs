using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.Mathematics
{
    public static class VectorCalculation
    {
        public static Vector MultiblyWithScalar(this Vector vector, double scalar)
        {
            Vector resultVector = new Vector(vector.Elements.Length);
            for(int i = 0; i < vector.Elements.Length; i++)
            {
                resultVector.Elements[i] = vector.Elements[i] * scalar;
            }
            return resultVector;
        }

        public static double Norm(this Vector vector) //Betrag
        {
            var sum = 0.0;
            for (int i = 0; i < vector.Elements.Length; i++)
            {
                sum += Math.Pow(vector.Elements[i], 2);
            }
            return Math.Sqrt(sum);
        }

        public static double DotProduct(this Vector vector1, Vector vector2)
        {
            if(vector1.Elements.Length == vector2.Elements.Length)
            {
                var sum = 0.0;
                for (int i = 0; i < vector1.Elements.Length; i++)
                {
                    sum += vector1.Elements[i] * vector2.Elements[i];
                }
                return sum;
            }
            else
            {
                throw new NotSupportedException("Vectors do not have the same length");
            }
        }

        public static Vector CrossProduct(this Vector vector1, Vector vector2)
        {
            if (vector1.Elements.Length == vector2.Elements.Length)
            {
                Vector resultVector = new Vector(vector1.Elements.Length);
                for (int i = 0; i < vector1.Elements.Length; i++)
                {
                    var value11 = 0.0;
                    var value12 = 0.0;
                    var value21 = 0.0;
                    var value22 = 0.0;
                    if (i + 1 >= vector1.Elements.Length)
                    {
                        var j = i - vector1.Elements.Length + 1;
                        value11 = vector1.Elements[j];
                    }
                    else
                    {
                        value11 = vector1.Elements[i + 1];
                    }
                    if (i + 2 >= vector2.Elements.Length)
                    {
                        var j = i - vector2.Elements.Length + 2;
                        value12 = vector2.Elements[j];
                    }
                    else
                    {
                        value12 = vector2.Elements[i + 2];
                    }
                    if (i + 2 >= vector1.Elements.Length)
                    {
                        var j = i - vector1.Elements.Length + 2;
                        value21 = vector1.Elements[j];
                    }
                    else
                    {
                        value21 = vector1.Elements[i + 2];
                    }
                    if (i + 1 >= vector2.Elements.Length)
                    {
                        var j = i - vector2.Elements.Length + 1;
                        value22 = vector2.Elements[j];
                    }
                    else
                    {
                        value22 = vector2.Elements[i + 1];
                    }
                    resultVector.Elements[i] = value11 * value12 - value21 * value22;                    
                }
                return resultVector;
            }
            else
            {
                throw new NotSupportedException("Vectors do not have the same length");
            }
        }

        public static Vector Add(this Vector vector1, Vector vector2)
        {
            if (vector1.Elements.Length == vector2.Elements.Length)
            {
                Vector resultVector = new Vector(vector1.Elements.Length);
                for (int i = 0; i < vector1.Elements.Length; i++)
                {
                    resultVector.Elements[i] = vector1.Elements[i] + vector2.Elements[i];
                }
                return resultVector;
            }
            else
            {
                throw new NotSupportedException("Vectors do not have the same length");
            }
        }

        public static Vector Subtract(this Vector vector1, Vector vector2)
        {
            if (vector1.Elements.Length == vector2.Elements.Length)
            {
                Vector resultVector = new Vector(vector1.Elements.Length);
                for (int i = 0; i < vector1.Elements.Length; i++)
                {
                    resultVector.Elements[i] = vector1.Elements[i] - vector2.Elements[i];
                }
                return resultVector;
            }
            else
            {
                throw new NotSupportedException("Vectors do not have the same length");
            }
        }
    }
}
