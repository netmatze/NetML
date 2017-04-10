using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetML.NeuronalNetworks
{
    public static class NeuronalNetworkModeFactory
    {
        public static NeuronalNetwork CreateInstance(NeuronalNetworkMode neuronalNetworkMode)
        {
            if(neuronalNetworkMode == NeuronalNetworkMode.Cascade)
            {
                return new CascadeFeedforwardNetwork();
            }
            else if(neuronalNetworkMode == NeuronalNetworkMode.Dynamic)
            {
                return new DynamicFeedforwardNetwork();
            }
            return new FeedforwardNetwork();
        }
    }
}
