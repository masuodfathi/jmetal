using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JMetalRunners.NSGAII;
using FeatureClass;
using System.IO;


namespace Regression
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private List<string> GetPairsList(List<Pair> pairs)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < pairs.Count; i++)
            {
                list.Add(pairs[i].Feature1 + "," + pairs[i].Feature2);
            }
            return list;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Open Matrix File";
            openFileDialog1.DefaultExt = "xlsx";
            openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = openFileDialog1.FileName;

            }
            
            OpenExcel a = new OpenExcel(label1.Text);
            featureModel fmO = new featureModel(a.OldMatrix);
            PaireSet newPairs = new PaireSet(a.newPairs);
            Compare c = new Compare(fmO, newPairs, a.ChangedFeatureList);
            FilterTestCase f = new FilterTestCase(c);
            for (int i = 0; i < c.NewPairs.Count; i++)
            {
                listBox1.Items.Add(c.NewPairs[i].Feature1 + "," + c.NewPairs[i].Feature2);
            }
            listBox2.DataSource = f.RetestableTestCases;
            listBox3.DataSource = f.ReUsableTestCases;
            listBox4.DataSource = f.ObsoleteTestCases;
            listBox5.DataSource = GetPairsList(c.Changed);
            listBox6.DataSource = GetPairsList(c.SamePairs);
            listBox7.DataSource = GetPairsList(c.RemovedPairs);
            int[] rowcol = new int[2];
            string[] arg = new string[1];
            arg[0] = "Regression";
            rowcol[0] = f.Matrix.GetLength(0);
            rowcol[1] = f.Matrix.GetLength(1);
            NSGAII.Matrix = f.Matrix;
            NSGAII.RowCol = rowcol;
            NSGAII.Main(arg);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //label3.Text = "extracting matrix from"+openFileDialog1.FileName.ToString();
            
            
            
            //int[,] mrx;
            //int[] rowcol;
            //string[] arg = new string[1];
            //arg[0] = "Regression";

            //OpenExcel matrix = new OpenExcel(label1.Text);
            //mrx = matrix.getMatrix();
            //rowcol = matrix.getRowCol();
            
            //NSGAII.Matrix = mrx;
            //NSGAII.RowCol = rowcol;
            //label4.Text ="Row: " + rowcol[0].ToString() + " - Col: " + rowcol[1].ToString();
            //label4.Refresh();
            //label3.Text = "Algorithm is running";
            //label3.Refresh();

            //NSGAII.Main(arg);
            //long tick = NSGAII.estimatedTime / 1000;
            //label3.Text = "Finished in: "+tick.ToString() +" ms";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
