using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeBlogMachineLearning1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBlogMachineLearning1.Tests
{
    [TestClass()]
    public class PictureConverterTests
    {
        [TestMethod()]
        public void ConvertTest()
        {
            var converter = new PictureConverter();
            var inputs = converter.Convert(@"C:\Users\rusta\source\repos\CodeBlogMachineLearning1\CodeBlogMachineLearning1Tests\Images\parasitized.png");
            converter.Save(@"D:\image.png", inputs);
        }
    }
}