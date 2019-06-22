using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureClass
{
    public class Compare
    {
        List<string> ChangedFeature = new List<string>();
        public List<string> TestName = new List<string>();
        public List<Pair> SamePairs = new List<Pair>();
        public List<Pair> NewPairs = new List<Pair>();
        public List<Pair> RemovedPairs = new List<Pair>();
        public List<Pair> Changed = new List<Pair>();

        public Compare(featureModel featureModel1, PaireSet _newPairs,List<string> _changedFeatures)
        {
            featureModel f1 = featureModel1;
            TestName = featureModel1.TestName;
            PaireSet nPairs = _newPairs;
            ChangedFeature = _changedFeatures;
            GetSamePairs(f1, nPairs);
            GetAddPairs(f1,nPairs,NewPairs);
            GetRemovePairs(nPairs,f1,RemovedPairs);
            GetChangedPairs(f1, _changedFeatures);
        }
        private void GetRemovePairs(PaireSet nPairs, featureModel f1, List<Pair> removedPairs)
        {
            for (int i = 0; i < f1.Pairs.Count; i++)
            {
                bool check = false;
                for (int j = 0; j < nPairs.PairsList.Count; j++)
                {
                    if (f1.Pairs[i].Feature1 == nPairs.PairsList[j].Feature1 && f1.Pairs[i].Feature2 == nPairs.PairsList[j].Feature2)
                    {
                        check = true;
                        break;
                    }
                    if (f1.Pairs[i].Feature2 == nPairs.PairsList[j].Feature1 && f1.Pairs[i].Feature1 == nPairs.PairsList[j].Feature2)
                    {
                        check = true;
                        break;
                    }
                }
                if (!check)
                {
                    removedPairs.Add(f1.Pairs[i]);
                }
            }
        }
        private void GetSamePairs(featureModel f1, PaireSet f2)
        {
            for (int i = 0; i < f1.Pairs.Count; i++)
            {
                for (int j = 0; j < f2.PairsList.Count; j++)
                {
                    if (f1.Pairs[i].Feature1 == f2.PairsList[j].Feature1 && f1.Pairs[i].Feature2 == f2.PairsList[j].Feature2)
                    {
                        SamePairs.Add(f1.Pairs[i]);
                        break;
                    }
                    if (f1.Pairs[i].Feature2 == f2.PairsList[j].Feature1 && f1.Pairs[i].Feature1 == f2.PairsList[j].Feature2)
                    {
                        SamePairs.Add(f1.Pairs[i]);
                        break;
                    }
                }
            }
            SamePairs = SamePairs.Except(Changed).ToList();
            RemoveChFeatureFromSame();
        }
        private void RemoveChFeatureFromSame()
        {
            for (int i = 0; i < SamePairs.Count; i++)
            {
                for (int j = 0; j < ChangedFeature.Count; j++)
                {
                    if (SamePairs[i].Feature1 == ChangedFeature[j] || SamePairs[i].Feature2 == ChangedFeature[j])
                    {
                        SamePairs.RemoveAt(i);
                        break;
                    }
                }
                
            }
        }
        private void GetAddPairs(featureModel f1, PaireSet f2,List<Pair> _list)
        {
            bool c=true;
            for (int i = 0; i < f2.PairsList.Count; i++)
            {
                for (int j = 0; j < f1.Pairs.Count; j++)
                {
                    c = true;
                    if (f1.Pairs[j].Feature1 == f2.PairsList[i].Feature1 && f1.Pairs[j].Feature2 == f2.PairsList[i].Feature2)
                    {
                        c = false;
                        break;
                    }
                    if (f1.Pairs[j].Feature2 == f2.PairsList[i].Feature1 && f1.Pairs[j].Feature1 == f2.PairsList[i].Feature2)
                    {
                        c = false;
                        break;
                    }
                    
                }
                if (c)
                {
                    _list.Add(f2.PairsList[i]);
                }
                
            }
        }
        private void GetChangedPairs(featureModel f,List<string> _changedFeatures)
        {
            for (int i = 0; i < f.Pairs.Count; i++)
            {
                for (int j = 0; j < _changedFeatures.Count; j++)
                {
                    if (f.Pairs[i].Feature1==_changedFeatures[j] || f.Pairs[i].Feature2 == _changedFeatures[j])
                    {
                        if (true)
                        {

                        }
                        Changed.Add(f.Pairs[i]);
                        break;
                    }
                }
            }
            for (int i = 0; i < Changed.Count; i++)
            {
                for (int j = 0; j < RemovedPairs.Count; j++)
                {
                    if (Changed[i] == RemovedPairs[j])
                    {
                        Changed.RemoveAt(i);
                    }
                }
            }
        }
    }
}
