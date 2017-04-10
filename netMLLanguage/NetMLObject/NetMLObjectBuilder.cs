using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netMLLanguage
{
    public class NetMLObjectBuilder
    {
        public NetMLObjectBuilder()
        {
            this.mainNetMLObject = new NetMLObject();
        }

        private NetMLObject mainNetMLObject;

        public NetMLObject MainNetMLObject
        {
            get { return mainNetMLObject; }
            set { mainNetMLObject = value; }
        }

        public void Create(NetMLObject netMLObject)
        {
            this.mainNetMLObject = new NetMLObject();
        }

        public void CreateAlgorithmusClassification(string algorithmusClassification)
        {
            this.mainNetMLObject.AlgorithmusClassification = algorithmusClassification;
        }

        public void CreateAlgorithmus(string algorithmus)
        {
            this.mainNetMLObject.Algorithmus = algorithmus;
        }

        public void AddOption(string option)
        {
            this.mainNetMLObject.Options.Add(option);
        }

        public void AddValue(double value)
        {
            mainNetMLObject.AddValue(value);
        }

        public void AddValue(bool value)
        {
            mainNetMLObject.AddValue(value);
        }

        public void AddValue(string value)
        {
            mainNetMLObject.AddValue(value);
        }

        public void AddVariable(string name)
        {
            mainNetMLObject.AddVariable(name);
        }
    }
}
