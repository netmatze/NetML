using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public class NeuronCounter
    {
        public NeuronCounter(int inputNeuronCount, int outputNeuronCount)
        {
            this.inputNeuronCount = inputNeuronCount;
            this.outputNeuronCount = outputNeuronCount;
        }

        private int inputNeuronCount;

        public int InputNeuronCount
        {
            get { return inputNeuronCount; }
        }

        private int outputNeuronCount;

        public int OutputNeuronCount
        {
            get { return outputNeuronCount; }
        }

        protected int secondhiddenNeuronCount;

        protected int firsthiddenNeuronCount;

        protected int thirdhiddenNeuronCount;
    }

    public class OneHiddenLayerNeuronCounter : NeuronCounter
    {
        public int FirstLayerHiddenNeuronCount
        {
            get { return firsthiddenNeuronCount; }
        }

        public OneHiddenLayerNeuronCounter(int inputNeuronCount, int outputNeuronCount, int firsthiddenNeuronCount) :
            base(inputNeuronCount, outputNeuronCount)
        {
            this.firsthiddenNeuronCount = firsthiddenNeuronCount;
        }
    }

    public class TwoHiddenLayerNeuronCounter : OneHiddenLayerNeuronCounter
    {
        public int SecondLayerHiddenNeuronCount
        {
            get { return secondhiddenNeuronCount; }
        }

        public TwoHiddenLayerNeuronCounter(int inputNeuronCount, int outputNeuronCount, int firsthiddenNeuronCount, int secondhiddenNeuronCount)
            : base(inputNeuronCount, outputNeuronCount, firsthiddenNeuronCount)
        {
            this.secondhiddenNeuronCount = secondhiddenNeuronCount;
        }
    }

    public class ThirdHiddenLayerNeuronCounter : TwoHiddenLayerNeuronCounter
    {
        public int ThirdLayerHiddenNeuronCount
        {
            get { return thirdhiddenNeuronCount; }
        }

        public ThirdHiddenLayerNeuronCounter(int inputNeuronCount, int outputNeuronCount, int firsthiddenNeuronCount, int secondhiddenNeuronCount, int thirdhiddenNeuronCount)
            : base(inputNeuronCount, outputNeuronCount, firsthiddenNeuronCount, secondhiddenNeuronCount)
        {
            this.thirdhiddenNeuronCount = thirdhiddenNeuronCount;
        }
    }
}
