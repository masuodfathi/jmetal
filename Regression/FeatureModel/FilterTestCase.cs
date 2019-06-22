using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureClass
{
    public class FilterTestCase
    {
        public int[,] Matrix;//matrix ke be nsgaii ersal mishavad
        public List<string> RetestableTestCases = new List<string>();
        public List<string> ReUsableTestCases = new List<string>();
        public List<string> ObsoleteTestCases = new List<string>();
        List<int> reservedTestCases = new List<int>();
        List<string> TestName = new List<string>();
        int[,] InitMatrix;


        public FilterTestCase(Compare CompareResult)
        {
            TestName = CompareResult.TestName;
            GetRetestable(CompareResult.Changed, CompareResult.TestName);
            GetReusable(CompareResult.SamePairs, CompareResult.TestName);
            GetObsolete(CompareResult.RemovedPairs, CompareResult.TestName);
        }
        private void GetObsolete(List<Pair> removedPairs, List<string> testName)
        {
            InitialMatrix(removedPairs);
            List<int> cols = GetColNumber(InitMatrix);
            cols = cols.Except(reservedTestCases).ToList();
            ObsoleteTestCases = GetTestCasesOf(cols);
        }
        private void GetReusable(List<Pair> samePairs, List<string> testName)
        {
            InitialMatrix(samePairs);
            List<int> cols = GetColNumber(InitMatrix);
            cols = cols.Except(reservedTestCases).ToList();
            ReUsableTestCases = GetTestCasesOf(cols);
            reservedTestCases = reservedTestCases.Union(cols).ToList();
        }
        public void GetRetestable(List<Pair> ChangedPairs, List<string> _testCases)
        {
            InitialMatrix(ChangedPairs);
            List<int> cols = reservedTestCases = GetColNumber(InitMatrix);
            Matrix = new int[ChangedPairs.Count, cols.Count];
            for (int i = 0; i < cols.Count; i++)
            {
                for (int j = 0; j < ChangedPairs.Count; j++)
                {
                    Matrix[j, i] = InitMatrix[j, cols[i]];
                }
            }

            RetestableTestCases = GetTestCasesOf(cols);
            
        }
        private List<int> GetColNumber(int[,] matrix)
        {
            List<int> colnumber = new List<int>();
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    if (matrix[row,col] >= 1)
                    {
                        colnumber.Add(col);
                        break;
                    }
                }
            }
            return colnumber;
        }
        private List<string> GetTestCasesOf(List<int> ColNumber)
        {
            List<string> testCases = new List<string>();
            for (int i = 0; i < ColNumber.Count; i++)
            {
                testCases.Add(TestName[ColNumber[i]]);
            }
            return testCases;
        }
        private void InitialMatrix(List<Pair> _pairs)
        {
            int pcount = _pairs.Count;
            int tcount = _pairs[0].TestCases.Count;

            InitMatrix = new int[pcount, tcount];
            for (int i = 0; i < pcount; i++)
            {
                for (int j = 0; j < tcount; j++)
                {
                    InitMatrix[i, j] = _pairs[i].TestCases[j];
                }
            }

        }
    }
}
