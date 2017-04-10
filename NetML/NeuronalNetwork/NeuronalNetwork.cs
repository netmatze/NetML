using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public abstract class NeuronalNetwork
    {
        protected FeedforwardLayer inputFeedforwardLayer;

        protected List<FeedforwardLayer> hiddenFeedforwardLayers = new List<FeedforwardLayer>();

        public List<Tuple<int, int, double[,]>> weightMatrixes = new List<Tuple<int, int, double[,]>>();

        public FeedforwardLayer outputFeedforwardLayer;

        public NeuronalNetworkMode NeuronalNetworkMode;

        public abstract void AddInputLayer(FeedforwardLayer feedforwardLayer);

        public abstract void AddHiddenLayer(FeedforwardLayer feedforwardLayer);

        public abstract void AddOutputLayer(FeedforwardLayer feedforwardLayer);

        public abstract void BuildWeightMatrix(FeedforwardLayer feedforwardLayer);

        public abstract void BuildWeightMatrix(FeedforwardLayer feedforwardLayer, FeedforwardLayer previousfeedforwardLayer);

        public abstract void BuildWeightMatrixOutputLayer(FeedforwardLayer feedforwardLayer);

        public abstract void Reset();

        public abstract void RandomFillWeightMatrix();

        public abstract void FillWeightMatrix();

        public abstract double[] ComputeOutputs(double[] input);

        private double learningRate = 0.1;

        public abstract double[] Classify(int[] inputValues);        

        public abstract double[] Classify(double[] inputValues);        

        public abstract void Train(int[] inputValues, int[] outputValues);

        public abstract void Train(double[] inputValues, int[] outputValues);        

        public abstract void Train(double[] inputValues, double[] outputValues);        

        public virtual void Train(double[] inputValues, double[] outputValues, int evolution)
        {

        }

        public abstract void Train(double[] inputValues, double outputValue);       

        public abstract void Train(List<double[]> inputValues, List<double[]> outputValues);        

        public double LearningRate
        {
            get { return learningRate; }
            set { learningRate = value; }
        }

        private int evolutions = 1000;

        public int Evolutions
        {
            get { return evolutions; }
            set { evolutions = value; }
        }
    }
}
