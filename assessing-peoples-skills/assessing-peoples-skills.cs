using System;
using Microsoft.ML.Probabilistic.Models;
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

namespace TestInfer
{
    class Program
    {
        static void Main(string[] args)
        {
            // csharp skill prior 
            var csharpSkill = Variable.Bernoulli(0.5).Named("csharpSkill");

            //csharp isCorrect p(isCorrect|csharp)
            //0         0         = 0.8
            //0         1         = 0.2
            //1         0         = 0.1
            //1         1         = 0.9

            Variable<bool> isCorrect1 = Variable.New<bool>();

            // add noise factor for csharp skill
            using (Variable.If(csharpSkill))
            {
                isCorrect1.SetTo(Variable.Bernoulli(0.9));
            }
            using (Variable.IfNot(csharpSkill))
            {
                isCorrect1.SetTo(Variable.Bernoulli(0.2));
            }

            // sql skill prior
            var sqlSkill = Variable.Bernoulli(0.5).Named("sqlSkill");

            Variable<bool> isCorrect2 = Variable.New<bool>();

            // add noise factor for sql skill
            using (Variable.If(sqlSkill))
            {
                isCorrect2.SetTo(Variable.Bernoulli(0.9));
            }
            using (Variable.IfNot(sqlSkill))
            {
                isCorrect2.SetTo(Variable.Bernoulli(0.2));
            }

            //csharp   sql   hasSkills p(hasSkills|csharp, sql)
            //0         0         0           = 1
            //0         0         1           = 0
            //0         1         0           = 1
            //0         1         1           = 0
            //1         0         0           = 1
            //1         0         1           = 0
            //1         1         0           = 0
            //1         1         1           = 1

            // and factor for csharp skill and sql skill
            var hasSkills = ((csharpSkill & sqlSkill) == Variable.Bernoulli(1)).Named("hasSkills");

            Variable<bool> isCorrect3 = Variable.New<bool>();

            // add noise factor for has skills
            using (Variable.If(hasSkills))
            {
                isCorrect3.SetTo(Variable.Bernoulli(0.9));
            }
            using (Variable.IfNot(hasSkills))
            {
                isCorrect3.SetTo(Variable.Bernoulli(0.2));
            }

            var engine = new InferenceEngine();

            engine.Algorithm = new ExpectationPropagation();

            isCorrect1.ObservedValue = true;
            isCorrect2.ObservedValue = false;
            isCorrect3.ObservedValue = false;

            Console.WriteLine(engine.Infer(csharpSkill));
            Console.WriteLine(engine.Infer(sqlSkill));

        }
    }
}
