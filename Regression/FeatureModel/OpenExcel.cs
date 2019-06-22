﻿using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;



namespace FeatureClass
{
    public class OpenExcel
    {
        //property=============
        string Path;
        _Application ap = new _Excel.Application();
        Workbook wb;
        Worksheet ws;
        object misValue = System.Reflection.Missing.Value;

        public string[,] OldMatrix;
        public List<string> newPairs = new List<string>();
        public List<string> ChangedFeatureList = new List<string>();
        int Row;
        int Col;
        int FeatureSize;
        
         //=============
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(int handel, out int ProcessId);
        public OpenExcel(string path)
        {
           
            this.Path = path;
            wb = ap.Workbooks.Open(path);

            //open sheet1
            GetOldMatrix();


            //open sheet 2
            GetNewMatrix();

            //open sheet 3
            GetChangedFeature();
            

            //close  Excel
            wb.Close(true, misValue, misValue);
            ap.Quit();
            int prid;
            GetWindowThreadProcessId(ap.Hwnd, out prid);
            Process[] Allprocess = Process.GetProcessesByName("excel");
            foreach (var process in Allprocess)
            {
                if (process.Id == prid)
                {
                    process.Kill();
                }
            }
            Allprocess = null;
            
        }
        private void GetOldMatrix()
        {
            ws = wb.Worksheets[1];
            _Excel.Range r = ws.UsedRange;

            GetRowCol(r);
            OldMatrix = new string[Row, Col];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        j++;
                    }
                    if (r.Cells[i + 1, j + 1].Value2 != null)
                    {
                        if (Convert.ToString(r.Cells[i + 1, j + 1].Value2) == "")
                        {
                            continue;
                        }
                        try
                        {
                            OldMatrix[i, j] = r.Cells[i + 1, j + 1].Value2.ToString();

                        }
                        catch (Exception e)
                        {

                            throw new Exception(e.ToString());
                        }


                    }
                    else
                    {
                        //OldMatrix[i, j] = "";
                    }

                }
            }
        }
        private void GetNewMatrix()
        {
            ws = wb.Worksheets[2];
            _Excel.Range ra = ws.UsedRange;
            int row = ra.Rows.Count;
            
            
            for (int i = 1; i < row; i++)
            {
                
                    if (ra.Cells[i , 1].Value2 != null)
                    {
                        if (Convert.ToString(ra.Cells[i , 1].Value2) == "")
                        {
                            continue;
                        }
                        try
                        {
                            newPairs.Add( ra.Cells[i,1].Value2.ToString());

                        }
                        catch (Exception e)
                        {

                            throw new Exception(e.ToString());
                        }
                    }
                
            }
        }
        private void GetChangedFeature()
        {
            ws = wb.Worksheets[3];
            _Excel.Range ran = ws.UsedRange;
            this.FeatureSize = ran.Columns.Count;

            for (int i = 0; i < FeatureSize; i++)
            {
                if (ran.Cells[i + 1].Value2 != null)
                {
                    try
                    {
                        ChangedFeatureList.Add(ran.Cells[1, i + 1].Value2.ToString());

                    }
                    catch (Exception e)
                    {

                        throw new Exception(e.ToString());
                    }


                }
                else
                {
                    continue;
                }

            }
        }
        private void GetRowCol(Range r)
        {
            int[] rowcol = new int[2];
            int row = r.Rows.Count;
            int col = r.Columns.Count;
            for (int i = 2; i <= row; i++)
            {
                
                if (r.Cells[i,1].Value2 != null)
                {
                    rowcol[0] = i;
                }
            }
            for (int j = 2; j <= col; j++)
            {
                if (r.Cells[1,j].Value2 != null)
                {
                    rowcol[1] = j;
                }
            }
            this.Row = rowcol[0];
            this.Col = rowcol[1];
        }
    }
}

