using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public class FeedforwardNeuronalNetworkResult<T>
    {
        T[] outputValues;

        public T[] OutputValues
        {
            get { return outputValues; }
            set { outputValues = value; }
        }

        T maxValue;

        public T MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        int maxValueOutputPosition;

        public int MaxValueOutputPosition
        {
            get { return maxValueOutputPosition; }
            set { maxValueOutputPosition = value; }
        }

        T predictedValue;

        public T PredictedValue
        {
            get { return predictedValue; }
            set { predictedValue = value; }
        }
    }
}
