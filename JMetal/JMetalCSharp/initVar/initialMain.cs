using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMetalCSharp.initVar
{
    public class initialMain
    {
        public static int[,] initialArray(int x, int y)
        {
            Random rnd = new Random();
            int[,] f = new int[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    f[i, j] = rnd.Next(0, 2);
                }
            }
            return f;
        }
    }
}
