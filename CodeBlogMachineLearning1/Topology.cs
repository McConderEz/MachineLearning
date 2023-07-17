using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBlogMachineLearning1
{
    public class Topology
    {
        public int InputCount { get; }
        public int OutputCount { get; }
        public List<int> HiddenLayers { get; }
        public Topology(int inputCount,int outputCount, params int[] layers)
        {
            if (InputCount == 0 || OutputCount == 0)
                throw new ArgumentNullException("Количество входныъ/выходных нейронов не может быть 0!");
            InputCount = inputCount;
            OutputCount = outputCount;
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(layers);
        }
    }
}
