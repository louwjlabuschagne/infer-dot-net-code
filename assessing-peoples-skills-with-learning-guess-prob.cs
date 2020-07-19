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
            int[][] skillsIndicesForEachQuestion = new int[48][] {
                new int [] { 0 },
                new int [] { 0 },
                new int [] { 0, 6 },
                new int [] { 0, 6 },
                new int [] { 0 },
                new int [] { 0 },
                new int [] { 0, 6 },
                new int [] { 1, 6 },
                new int [] { 1, 6 },
                new int [] { 1 },
                new int [] { 1 },
                new int [] { 1, 6 },
                new int [] { 1, 6 },
                new int [] { 1, 6 },
                new int [] { 1 },
                new int [] { 1, 6 },
                new int [] { 1, 6 },
                new int [] { 1 },
                new int [] { 2 },
                new int [] { 2 },
                new int [] { 2 },
                new int [] { 2 },
                new int [] { 2 },
                new int [] { 0 },
                new int [] { 0 },
                new int [] { 0 },
                new int [] { 3 },
                new int [] { 3 },
                new int [] { 3 },
                new int [] { 3 },
                new int [] { 3 },
                new int [] { 3 },
                new int [] { 3 },
                new int [] { 3 },
                new int [] { 4 },
                new int [] { 1, 4 },
                new int [] { 4 },
                new int [] { 4 },
                new int [] { 4 },
                new int [] { 4 },
                new int [] { 5 },
                new int [] { 5 },
                new int [] { 5 },
                new int [] { 5 },
                new int [] { 5 },
                new int [] { 5 },
                new int [] { 5 },
                new int [] { 5 }
            };

        bool[][] isCorrectAnswers = new bool[22][]
            {
                new bool [] { true, true, true, true, true, false, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true, false, true, true, true, true, true,
                  true, true, false, false, true, true, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true },
                new bool [] { false, true, true, true, true, true, true, true, true,
                  true, true, false, true, true, true, false, false, true,
                  true, true, true, false, false, true, true, true, true,
                  true, true, false, true, false, true, true, false, true,
                  false, true, false, true, false, true, false, false, false,
                  false, false, true },
                new bool [] { false, true, false, false, true, false, true, true, true,
                  true, false, false, false, false, false, true, true, false,
                  true, true, false, false, false, false, false, true, true,
                  true, true, false, true, false, true, true, true, false,
                  false, false, true, true, true, true, true, true, false,
                  false, false, true },
                new bool [] { true, true, true, true, true, false, true, true, true,
                  true, true, true, false, true, true, true, true, true,
                  true, true, false, false, true, true, true, true, true,
                  true, true, false, true, true, false, true, false, false,
                  true, true, true, false, true, true, true, true, false,
                  true, false, true },
                new bool [] { true, true, true, true, true, true, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true, false, true, true, true, true, true,
                  true, true, false, false, false, true, true, true, true,
                  true, true, true, true, true, true, true, true, false,
                  true, true, false },
                new bool [] { false, false, true, false, false, false, false, false, false,
                  false, true, false, false, false, false, false, false, false,
                  false, false, false, false, false, true, false, false, false,
                  false, false, true, false, false, false, false, false, false,
                  false, true, false, false, false, false, false, false, false,
                  false, false, false },
                new bool [] { true, true, true, true, true, true, true, true, true,
                  true, true, true, true, false, true, true, true, true,
                  true, true, true, false, true, true, true, true, true,
                  true, true, false, false, true, true, true, false, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true },
                new bool [] { true, true, false, false, true, true, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, false, false, false, true, true, true, true,
                  false, false, true, false, true, true, true, true, false,
                  true, true, true },
                new bool [] { true, true, false, true, true, true, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true, false, true, false, true, false, true,
                  true, true, false, true, true, true, true, true, true,
                  true, true, true, true, false, true, true, true, true,
                  true, true, true },
                new bool [] { true, true, true, true, true, true, true, true, true,
                  true, true, true, false, true, true, true, true, true,
                  true, false, false, false, true, true, true, true, true,
                  true, true, false, true, true, true, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  false, false, true },
                new bool [] { false, true, true, true, true, false, true, true, true,
                  true, false, true, true, true, true, true, true, true,
                  true, true, true, false, false, true, true, true, true,
                  true, true, false, true, true, true, true, true, false,
                  true, true, true, false, true, true, true, false, true,
                  false, true, false},
                new bool [] { false, false, false, true, true, true, true, true, true,
                  true, true, false, false, false, false, true, false, true,
                  true, true, true, false, true, true, true, true, true,
                  true, true, false, false, false, false, false, false, true,
                  true, true, true, true, true, false, false, false, true,
                  true, false, false},
                new bool [] { true, true, true, true, true, false, true, true, true,
                  true, true, true, true, false, true, true, true, true,
                  true, true, true, false, true, true, true, true, true,
                  true, true, false, false, true, true, true, true, false,
                  true, true, true, false, true, true, true, true, true,
                  true, false, true},
                new bool [] { true, false, true, true, false, false, true, true, true,
                  true, true, true, false, false, true, true, true, true,
                  true, true, false, true, true, false, true, true, true,
                  true, true, false, true, false, true, true, true, true,
                  false, true, false, false, true, true, true, true, false,
                  true, false, false },
                new bool [] { true, true, true, true, true, false, true, true, true,
                  true, false, false, true, false, true, false, true, true,
                  true, false, true, false, true, false, true, true, true,
                  true, true, false, false, true, true, true, false, true,
                  true, true, true, false, true, true, true, true, true,
                  true, false, true },
                new bool [] { true, true, true, false, true, true, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true, false, false, true, true, true, true,
                  true, true, true, true, true, true, true, false, true,
                  true, true, true, false, true, true, true, true, true,
                  true, true, true },
                new bool [] { true, true, true, true, true, true, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  false, true, true, false, true, true, true, true, true,
                  true, true, false, false, true, true, true, true, true,
                  true, true, true, false, true, true, true, true, true,
                  true, false, true },
                new bool [] { true, true, true, true, true, false, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true, false, true, true, true, true, true,
                  true, true, false, false, true, true, true, true, true,
                  false, true, true, true, true, true, true, false, true,
                  true, false, false },
                new bool [] { true, true, false, true, true, false, true, false, true,
                  true, true, false, true, true, true, true, true, true,
                  true, true, true, false, true, true, true, true, true,
                  true, true, false, true, true, true, true, false, true,
                  true, true, true, false, true, true, true, true, false,
                  false, false, true },
                new bool [] { true, true, true, true, true, true, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, false, true, true, true, true, false, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true },
                new bool [] { true, true, true, true, true, true, false, true, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true, false, true, true, true, true, true,
                  true, true, false, true, true, true, true, true, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true },
                new bool [] { true, true, false, true, true, true, true, true, true,
                  true, true, true, true, false, true, true, true, false,
                  true, true, true, false, false, true, true, true, true,
                  true, true, false, true, true, true, true, false, true,
                  true, true, true, true, true, true, true, true, true,
                  true, true, true } };

            int numSkills = 7;
            int numQuestions = 48;
            int numPeople = 22;

            Range QuestionsSkills = new Range(numSkills);
            Range Questions = new Range(numQuestions);
            Range People = new Range(numPeople);

            // 2.5 / (2.5 + 7.5) = 0.25 mean
            VariableArray<double> probGuess = Variable.Array<double>(Questions).Named("probGuess");
            probGuess[Questions] = Variable.Beta(2.5, 7.5).ForEach(Questions);

            //VariableArray<double> probNoMistake = Variable.Array<double>(Questions).Named("probNoMistake");
            //probNoMistake[Questions] = Variable.Bernoulli().ForEach(Questions);

            VariableArray<double> probabilityOfSkillTrue = Variable.Array<double>(QuestionsSkills).Named("probabilityOfSkillTrue");

            VariableArray<VariableArray<bool>, bool[][]> Skills = Variable.Array(Variable.Array<bool>(QuestionsSkills), People).Named("Skills");
            Skills[People][QuestionsSkills] = Variable.Bernoulli(probabilityOfSkillTrue[QuestionsSkills]).ForEach(People);

            VariableArray<VariableArray<bool>, bool[][]> HasSkills = Variable.Array(Variable.Array<bool>(Questions), People).Named("HasSkills");

            for (int p = 0; p < numPeople; p++)
            {
                for (int q = 0; q < numQuestions; q++)
                {
                    if (skillsIndicesForEachQuestion[q].Length > 1)
                    {
                        var skill1 = skillsIndicesForEachQuestion[q][0];
                        var skill2 = skillsIndicesForEachQuestion[q][1];
                        HasSkills[p][q] = ((Skills[p][skill1] & Skills[p][skill2]) == Variable.Bernoulli(1));

                    }
                    else
                    {
                        var skill1 = skillsIndicesForEachQuestion[q][0];
                        HasSkills[p][q] = Skills[p][skill1];
                    }
                }
            }

            VariableArray<VariableArray<bool>, bool[][]> isCorrect = Variable.Array(Variable.Array<bool>(Questions), People).Named("isCorrect");

            using (Variable.ForEach(People))
            {
                using (Variable.ForEach(Questions))
                {
                    // add noise factor for has skills
                    using (Variable.If(HasSkills[People][Questions]))
                    {
                        //prob no mistake
                        isCorrect[People][Questions].SetTo(Variable.Bernoulli(0.9));
                    }
                    using (Variable.IfNot(HasSkills[People][Questions]))
                    {
                        //prob guess
                        //isCorrect[People][Questions].SetTo(Variable.Bernoulli(0.2));
                        isCorrect[People][Questions].SetTo(Variable.Bernoulli(probGuess[Questions]));
                    }
                }
            }

            probabilityOfSkillTrue.ObservedValue = Enumerable.Repeat(0.5, numSkills).ToArray();

            isCorrect.ObservedValue = isCorrectAnswers;

            var engine = new InferenceEngine();
            engine.Algorithm = new ExpectationPropagation();

            Bernoulli[][] SkillsPosteriors = engine.Infer<Bernoulli[][]>(Skills);

            for (int p = 0; p < numPeople; p++)
            {
                Console.WriteLine("Person {0} skills distribution: ", p + 1);
                for (int s = 0; s < numSkills; s++)
                {
                    Console.WriteLine(SkillsPosteriors[p][s]);
                }
                Console.WriteLine("------------------");
            }

            Beta[] probGuessPosteriors = engine.Infer<Beta[]>(probGuess);

            for (int q = 0; q < numQuestions; q++)
            {
                Console.WriteLine("Question {0} guess distribution: ", q + 1);
                Console.WriteLine(probGuessPosteriors[q]);
                Console.WriteLine("------------------");
            }

        }
    }
}
