using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netMLLanguage
{
    public class NetMLObject
    {
        private Dictionary<string, string> stringValues = new Dictionary<string, string>();
        private Dictionary<string, bool> boolValues = new Dictionary<string, bool>();
        private Dictionary<string, double> doubleValues = new Dictionary<string, double>();

        public Dictionary<string, double> DoubleValues
        {
            get { return doubleValues; }
            set { doubleValues = value; }
        }

        private string algorithmus;

        public string Algorithmus
        {
            get { return algorithmus; }
            set { algorithmus = value; }
        }

        private string algorithmusClassification;

        public string AlgorithmusClassification
        {
            get { return algorithmusClassification; }
            set { algorithmusClassification = value; }
        }

        private Stack<string> variables = new Stack<string>();

        private List<string> options = new List<string>();

        public List<string> Options
        {
            get { return options; }
            set { options = value; }
        }

        public void AddVariable(string name)
        {
            variables.Push(name);
        }

        public void AddValue(double value)
        {
            var name = variables.Pop();
            doubleValues.Add(name, value);
        }

        public void AddValue(bool value)
        {
            var name = variables.Pop();
            boolValues.Add(name, value);
        }

        public void AddValue(string value)
        {
            var name = variables.Pop();
            stringValues.Add(name, value);
        }
    }
}
