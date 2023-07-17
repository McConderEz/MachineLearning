using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBlogMachineLearning1
{
    public class Layer
    {
        public List<Neuron> Neurons { get; }
        public int Count => Neurons?.Count ?? 0;

        public Layer(List<Neuron> neurons, NeuronType type = NeuronType.Normal)
        {
            if(Count == 0)
            {
                throw new ArgumentNullException("Список нейронов не может быть пустым!");
            }

            for(var i = 0;i < Neurons?.Count; i++)
            {
                if (neurons[i].NeuronType != type)
                {
                    throw new ArgumentException("Список содержит нейрон несоответствующего типа!");
                }
            }

            Neurons = neurons;
        }

        public List<double> GetSignals()
        {
            var result = new List<double>();
            foreach(var neuron in Neurons)
            {
                result.Add(neuron.Output);
            }
            return result;
        }


    }
}
