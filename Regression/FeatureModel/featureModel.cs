using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureClass
{
    public class featureModel
    {
        public string[,] Matrix;
        public List<Pair> Pairs = new List<Pair>();
        public List<string> testcases = new List<string>();
        public List<string> TestName = new List<string>();
        private int ColumeSize;
        private int RowSize;
        List<Feature> Features = new List<Feature>();
        
        
        public featureModel(string[,] _matrix)
        {
            Matrix = _matrix;
            ColumeSize=Matrix.GetLength(1);
            RowSize = Matrix.GetLength(0);
            List<string> FeaturesString = GetFeaturesString();
            for (int i = 0; i < FeaturesString.Count; i++)
            {
                Feature f = new Feature(FeaturesString[i]);
                Features.Add(f);
            }
            List<string> pairString = GetPairsString();
            for (int i = 0; i < pairString.Count; i++)
            {
                Pair p = new Pair(pairString[i], GetTestCasesOfPair(i + 1));
                Pairs.Add(p);
            }
            GetTestCases();
            GetTestName();
        }
        private List<string> GetFeaturesString()
        {
            List<string> pair = GetPairsString();
            List<string> featureString = new List<string>();
            //List<string> features = new List<string>();
            for (int i = 0; i < pair.Count; i++)
            {
                string[] f = pair[i].Split(',');
                for (int j = 0; j < f.Length; j++)
                {
                    featureString.Add(f[j]);
                }

            }
            featureString = featureString.Distinct().ToList();
            return featureString;
        }
        private List<string> GetPairsString()
        {
            List<string> pairs = new List<string>();
            for (int i = 1; i < RowSize; i++)
            {
                pairs.Add(Matrix[i, 0]);
            }
            return pairs;
        }
        private void GetTestCases()
        {
            
            for (int i = 0; i < ColumeSize - 1; i++)
            {
                testcases.Add( Matrix[0, i + 1]);
            }
            
        }
        private List<int> GetTestCasesOfPair(int index)
        {
            List<int> tests = new List<int>();
            for (int i = 1; i < Matrix.GetLength(1); i++)
            {
                try
                {
                    tests.Add(int.Parse(Matrix[index, i]));
                }
                catch
                {

                }
            }
            return tests;
        }
        public void AddPair(Pair _pair)
        {
            Pairs.Add(_pair);
        }
        private void GetTestName()
        {
            for (int i = 1; i < Matrix.GetLength(1); i++)
            {
                TestName.Add(Matrix[0, i]);
            }
        }
    }
}
