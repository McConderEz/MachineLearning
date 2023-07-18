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
        public double LearningRate { get; }
        public List<int> HiddenLayers { get; }
        public Topology(int inputCount,int outputCount,double learningRate ,params int[] layers)
        {
            if (inputCount == 0 || outputCount == 0)
                throw new ArgumentNullException("Количество входныъ/выходных нейронов не может быть 0!");
            InputCount = inputCount;
            OutputCount = outputCount;
            LearningRate = learningRate;
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(layers);
        }
    }
}
