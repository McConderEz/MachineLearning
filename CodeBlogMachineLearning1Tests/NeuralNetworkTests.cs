﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeBlogMachineLearning1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;
using System.Transactions;

namespace CodeBlogMachineLearning1.Tests
{
    [TestClass()]
    public class NeuralNetworkTests
    {
        [TestMethod()]
        public void FeedForwardTest()
        {

            var outputs = new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };
            var inputs = new double[,]
            {
                // Результат - Пациент болен - 1
                //             Пациент Здоров - 0

                // Неправильная температура T
                // Хороший возраст A
                // Курит S
                // Правильно питается F
               // T  A  S  F
                { 0, 0, 0, 0 },
                { 0, 0, 0, 1 },
                { 0, 0, 1, 0 },
                { 0, 0, 1, 1 },
                { 0, 1, 0, 0 },
                { 0, 1, 0, 1 },
                { 0, 1, 1, 0 },
                { 0, 1, 1, 1 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 1 },
                { 1, 0, 1, 0 },
                { 1, 0, 1, 1 },
                { 1, 1, 0, 0 },
                { 1, 1, 0, 1 },
                { 1, 1, 1, 0 },
                { 1, 1, 1, 1 }
            };

            var topology = new Topology(4, 1, 0.1, 2);
            var neuralNetwork = new NeuralNetwork(topology);
            var difference = neuralNetwork.Learn(outputs, inputs, 10000);
            var results = new List<double>();
            for (int i = 0; i < outputs.Length; i++)
            {
                var row = NeuralNetwork.GetRow(inputs, i);
                var res = neuralNetwork.Predict(row).Output;
                results.Add(res);
            }
            for (int i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(outputs[i], 2);
                var actual = Math.Round(results[i], 2);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void DatasetTest()
        {
            var outputs = new List<double>();
            var inputs = new List<double[]>();
            using (var sr = new StreamReader("heart_cleveland_upload.csv"))
            {
                var header = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var row = sr.ReadLine();
                    
                    var values = row.Split(',').Select(v  => Convert.ToDouble(v.Replace(".",","))).ToList();
                    var output = values.Last();
                    var input = values.Take(values.Count - 1).ToArray();

                    outputs.Add(output);
                    inputs.Add(input);
                }
            }

            var inputSignals = new double[inputs.Count, inputs[0].Length];
            for(int i = 0;i < inputSignals.GetLength(0);i++)
            {
                for(var j = 0;j < inputSignals.GetLength(1); j++)
                {
                    inputSignals[i, j] = inputs[i][j];
                }
            }

            var topology = new Topology(outputs.Count, 1, 1, outputs.Count/2);
            var neuralNetwork = new NeuralNetwork(topology);
            var difference = neuralNetwork.Learn(outputs.ToArray(), inputSignals, 100);

            var results = new List<double>();
            for (int i = 0; i < outputs.Count; i++)
            {                
                var res = neuralNetwork.Predict(inputs[i]).Output;
                results.Add(res);
            }
            for (int i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(outputs[i], 2);
                var actual = Math.Round(results[i], 2);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void RecognizeImage()
        {
            var size = 200;
            var parasitizedPath = @"D:\Parasitized\";
            var unparasitizedPath = @"D:\Uninfected\";

            var converter = new PictureConverter();
            var testparasitizedImageInput = converter.Convert(@"C:\Users\rusta\source\repos\CodeBlogMachineLearning1\CodeBlogMachineLearning1Tests\Images\parasitized.png");
            var testunparasitizedImageInput = converter.Convert(@"C:\Users\rusta\source\repos\CodeBlogMachineLearning1\CodeBlogMachineLearning1Tests\Images\uninfected.png");

            var topology = new Topology(testparasitizedImageInput.Length, 1, 0.1, testparasitizedImageInput.Length / 2);
            var neuralNetwork = new NeuralNetwork(topology);
            double[,] parasitizedInputs = GetData(parasitizedPath, converter, testparasitizedImageInput,size);
            neuralNetwork.Learn(new double[] { 1 }, parasitizedInputs, 1);

            double[,] unparasitizedInputs = GetData(unparasitizedPath, converter, testunparasitizedImageInput, size);
            neuralNetwork.Learn(new double[] { 0 }, unparasitizedInputs, 1);

            var par = neuralNetwork.Predict(testparasitizedImageInput.Select(t => (double)t).ToArray());
            var unpar = neuralNetwork.Predict(testunparasitizedImageInput.Select(t => (double)t).ToArray());
             Assert.AreEqual(1, Math.Round(par.Output, 2));
            Assert.AreEqual(0, Math.Round(unpar.Output, 2));
        }

        private static double[,] GetData(string parasitizedPath, PictureConverter converter, double[] testImageInput,int size)
        {
            var images = Directory.GetFiles(parasitizedPath);
            var result = new double[size,testImageInput.Length];
            for (int i = 0; i < size; i++)
            {
                var image = converter.Convert(images[i]);
                for (int j = 0; j < image.Length; j++)
                {
                    result[i,j] = image[j];
                }
            }

            return result;
        }
    }
}