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

namespace TestInfer
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;

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
            var hasSkills3 = ((csharpSkill & sqlSkill) == Variable.Bernoulli(1)).Named("hasSkills3");
            
            Variable<bool> isCorrect3 = Variable.New<bool>();

            // add noise factor for has skills
            using (Variable.If(hasSkills3))
            {
                isCorrect3.SetTo(Variable.Bernoulli(0.9));
            }
            using (Variable.IfNot(hasSkills3))
            {
                isCorrect3.SetTo(Variable.Bernoulli(0.2));
            }

            // and factor for csharp skill and sql skill
            var hasSkills4 = ((csharpSkill & sqlSkill) == Variable.Bernoulli(1)).Named("hasSkills4");
            
            Variable<bool> isCorrect4 = Variable.New<bool>();

            // add noise factor for has skills
            using (Variable.If(hasSkills4))
            {
                isCorrect4.SetTo(Variable.Bernoulli(0.9));
            }
            using (Variable.IfNot(hasSkills4))
            {
                isCorrect4.SetTo(Variable.Bernoulli(0.2));
            }

            var engine = new InferenceEngine();

            engine.Algorithm = new ExpectationPropagation();

            engine.NumberOfIterations = 10;

            isCorrect1.ObservedValue = true;
            isCorrect2.ObservedValue = false;
            isCorrect3.ObservedValue = false;
            isCorrect4.ObservedValue = false;

            csharpSkill.AddAttribute(new TraceMessages());
            sqlSkill.AddAttribute(new TraceMessages());
            hasSkills3.AddAttribute(new TraceMessages());
            hasSkills4.AddAttribute(new TraceMessages());

            Console.WriteLine(engine.Infer(csharpSkill));
            Console.WriteLine(engine.Infer(sqlSkill));


        }
    }
}
