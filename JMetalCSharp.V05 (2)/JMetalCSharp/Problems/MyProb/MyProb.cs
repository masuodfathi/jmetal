using JMetalCSharp.Core;
using JMetalCSharp.Encoding.SolutionType;
using JMetalCSharp.Utils;
using JMetalCSharp.Utils.Wrapper;
using System;

namespace JMetalCSharp.Problems.MyProb
{
    public class MyProb : Problem
    {
        private static int NumberOfPairs;
        public MyProb(string solutionType,int _NumberOfPairs,int _NumberOfVariables)
        {
            NumberOfPairs = _NumberOfPairs;
            NumberOfVariables = _NumberOfVariables;
            NumberOfObjectives = 2;
            NumberOfConstraints = 0;
            ProblemName = "MyProb";

            UpperLimit = new double[NumberOfVariables];
            LowerLimit = new double[NumberOfVariables];
            for (int var = 0; var < NumberOfVariables; var++)
            {
                LowerLimit[var] = 0.0;
                UpperLimit[var] = 2.0;
            }

            if (solutionType == "BinaryReal")
                SolutionType = new BinaryRealSolutionType(this);
            else if (solutionType == "Real")
                SolutionType = new RealSolutionType(this);
            else if (solutionType == "ArrayReal")
                SolutionType = new ArrayRealSolutionType(this);
            else if (solutionType == "Int")
            {

                SolutionType = new IntSolutionType(this);
            }
            else
            {
                Console.WriteLine("Error: solution type " + solutionType + " is invalid");
                Logger.Log.Error("Error: solution type " + solutionType + " is invalid");
                Environment.Exit(-1);
            }
        }

        
        public override void Evaluate(Solution solution)
        {
            XReal x = new XReal(solution);
            int[,] main = new int[NumberOfVariables, NumberOfPairs];// matrixe avalie
            main = JMetalCSharp.InitVar.initialMain.initialArray(NumberOfVariables, NumberOfPairs); //farakhani method meqdar dehi avalie be matrix
            double[] f = new double[NumberOfObjectives];
            int[] coverage = new int[NumberOfPairs]; // motaqayeri ke neshan dahande tedade pair haye poshesh dade shode mibashad
            int cost = 0;
            for (int var = 0; var < NumberOfVariables; var++)
            {
                if (x.GetValue(var) >= 1)
                {
                    
                    for (int i = 0; i < NumberOfPairs; i++)
                    {
                        if (main[var,i]==1)
                        {
                            coverage[i] = 1;
                        }
                    }
                    
                    cost++;
                }
            }
            for (int j = 0; j < coverage.Length; j++)
            {
                f[0] += coverage[j];
            }            

            
            f[1] = cost;
            Console.WriteLine(f[0] + " : " + f[1]);
            solution.Objective[0] = f[0];
            solution.Objective[1] = f[1];
        }
    }
}
