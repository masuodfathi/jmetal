using JMetalCSharp.Core;
using JMetalCSharp.Encoding.SolutionType;
using JMetalCSharp.Utils;
using JMetalCSharp.Utils.Wrapper;
using System;


namespace JMetalCSharp.Problems.Regression
{
    public class Regression : Problem
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// Creates a default instance of the Fonseca problem
        /// </summary>
        /// <param name="solutionType">The solution type must "Real", "BinaryReal, ArrayReal, or ArrayRealC".</param>
        private static int NumberOfPairs;
        public Regression(string solutionType,int NumberofPair)
        {
            NumberOfPairs = NumberofPair;
            NumberOfVariables = 3;
            NumberOfObjectives = 2;
            NumberOfConstraints = 0;
            ProblemName = "Regression";

            UpperLimit = new double[NumberOfVariables];
            LowerLimit = new double[NumberOfVariables];
            for (int var = 0; var < NumberOfVariables; var++)
            {
                LowerLimit[var] = 0;
                UpperLimit[var] = 0;
            }

            if (solutionType == "BinaryReal")
                SolutionType = new BinaryRealSolutionType(this);
            else if (solutionType == "Int")
                SolutionType = new IntSolutionType(this);
            else if (solutionType == "Real")
                SolutionType = new RealSolutionType(this);
            else if (solutionType == "ArrayReal")
                SolutionType = new ArrayRealSolutionType(this);
            else
            {
                Console.WriteLine("Error: solution type " + solutionType + " is invalid");
                Logger.Log.Error("Error: solution type " + solutionType + " is invalid");
                Environment.Exit(-1);
            }
        }

        #endregion

        #region Public Overrides

        /// <summary>
        /// Evaluates a solution
        /// </summary>
        /// <param name="solution">The solution to evaluate</param>
        public override void Evaluate(Solution solution)
        {
            XReal x = new XReal(solution);
            int[,] main = new int[NumberOfVariables, NumberOfPairs];// matrixe avalie
            main = JMetalCSharp.initVar.initialMain.initialArray(NumberOfVariables, NumberOfPairs); //farakhani method meqdar dehi avalie be matrix
            double[] f = new double[NumberOfObjectives];
            int[] coverage = new int[NumberOfPairs]; // motaqayeri ke neshan dahande tedade pair haye poshesh dade shode mibashad
            double cost = 0.0;
            for (int var = 0; var < NumberOfVariables; var++)
            {
                if (x.GetValue(var) >= 1)
                {
                    for (int i = 0; i < NumberOfPairs; i++)
                    {
                        if (main[var, i] == 1)
                        {
                            coverage[i] = 1;
                        }
                    }

                    cost++;
                }
            }
            for (int j = 0; j < coverage.Length; j++)
            {
                if (coverage[j]>=1)
                {
                    f[0]++;
                }
                
            }
            
            for (int j = 0; j < coverage.Length; j++)
            {
                if (coverage[j] == 1)
                {
                    f[0]++;
                }

            }


            int sum2 = 0;
            for (int var = 0; var < NumberOfVariables; var++)
            {
                sum2 += 1;
            }
            double exp2 = Math.Exp((-1.0) * sum2);
            f[1] = cost;

            solution.Objective[0] = f[0];
            solution.Objective[1] = f[1];
        }

        #endregion
    }
}
