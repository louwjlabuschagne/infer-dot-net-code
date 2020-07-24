using System;
using Microsoft.ML.Probabilistic.Models;
using Microsoft.ML.Probabilistic.Models.Attributes;
using Microsoft.ML.Probabilistic.Distributions;
using Microsoft.ML.Probabilistic.Algorithms;
using Microsoft.ML.Probabilistic.Math;
using Microsoft.ML.Probabilistic.Compiler;
using Microsoft.ML.Probabilistic.Compiler.Transforms;
using Microsoft.ML.Probabilistic.Compiler.Visualizers;
using Range = Microsoft.ML.Probabilistic.Models.Range;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TestInfer
{
    class Program
    {
        static void Main(string[] args)
        {
            Variable<double> X = Variable.GaussianFromMeanAndVariance(12.5, 5*5);
            Variable<double> Y = Variable.GaussianFromMeanAndVariance(15, 5*5);

            Variable<bool> Ywins = (Y > X);

            //Ywins.ObservedValue = true;

            var engine = new InferenceEngine();
            engine.Algorithm = new ExpectationPropagation();

            //Gaussian XperfPosterior = engine.Infer<Gaussian>(X);
            //Gaussian YperfPosterior = engine.Infer<Gaussian>(Y);

            var YwinsPosterior = engine.Infer<Bernoulli>(Ywins);

            //Console.WriteLine(Xperf);
            //Console.WriteLine(Yperf);

            Console.WriteLine(YwinsPosterior);

        }
    }
}
