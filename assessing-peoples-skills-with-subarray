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

            int[] numSkillsForEachQuestion = new int[48];
            for (int n = 0; n < 48; n++)
            {
                numSkillsForEachQuestion[n] = skillsIndicesForEachQuestion[n].Length;
            }

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

            int numPeople = 22;
            int numSkills = 7;
            int numQuestions = 48;

            //Range QuestionsSkills = new Range(numSkills);
            Range People = new Range(numPeople);
            Range Skills = new Range(numSkills);
            Range Questions = new Range(numQuestions);

            VariableArray<double> probabilityOfSkillTrue = Variable.Array<double>(Skills).Named("probabilityOfSkillTrue");
            probabilityOfSkillTrue[Skills] = 0.5;

            VariableArray<int> numberOfSkillsForEachQuestion = Variable.Array<int>(Questions).Named("numSkillsForQuestions").Attrib(new DoNotInfer());
            Range QuestionsSkills = new Range(numberOfSkillsForEachQuestion[Questions]).Named("questionsXskills");

            VariableArray<VariableArray<int>, int[][]> skillsNeeded = Variable.Array(Variable.Array<int>(QuestionsSkills), Questions).Named("skillsNeeded").Attrib(new DoNotInfer());

            VariableArray<VariableArray<bool>, bool[][]> skill = Variable.Array(Variable.Array<bool>(Skills), People).Named("skills");
            skill[People][Skills] = Variable.Bernoulli(probabilityOfSkillTrue[Skills]).ForEach(People);

            VariableArray<VariableArray<bool>, bool[][]> isCorrect = Variable.Array(Variable.Array<bool>(Questions), People).Named("isCorrect");

            VariableArray<VariableArray<bool>, bool[][]> HasSkills = Variable.Array(Variable.Array<bool>(Questions), People).Named("HasSkills");

            using (Variable.ForEach(People))
            {
                using (Variable.ForEach(Questions))
                {
                    var relevantSkills = Variable.Subarray(skill[People], skillsNeeded[Questions]).Named("relevantSkills");

                    HasSkills[People][Questions] = Variable.AllTrue(relevantSkills).Named("hasSkills"); ;

                    // add noise factor for has skills
                    using (Variable.If(HasSkills[People][Questions]))
                    {
                        isCorrect[People][Questions].SetTo(Variable.Bernoulli(0.9));
                    }
                    using (Variable.IfNot(HasSkills[People][Questions]))
                    {
                        isCorrect[People][Questions].SetTo(Variable.Bernoulli(0.2));
                    }
                }
            }

            isCorrect.ObservedValue = isCorrectAnswers;

            numberOfSkillsForEachQuestion.ObservedValue = numSkillsForEachQuestion;
            skillsNeeded.ObservedValue = skillsIndicesForEachQuestion;

            var engine = new InferenceEngine();
            engine.Algorithm = new ExpectationPropagation();

            Bernoulli[][] SkillsPosteriors = engine.Infer<Bernoulli[][]>(skill);

            for (int p = 0; p < numPeople; p++)
            {
                Console.WriteLine("Person {0} skills distribution: ", p + 1);
                for (int s = 0; s < numSkills; s++)
                {
                    Console.WriteLine(SkillsPosteriors[p][s]);
                }
                Console.WriteLine("------------------");
            }

        }
    }
}
