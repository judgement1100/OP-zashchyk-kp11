using static System.Math;
using System.IO;
using System.Linq;

namespace Lab2
{
    class AlgorithmsClass
    {
        private double Dispersion1(int[] arr)
        {
            double totalSum1 = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                totalSum1 += Pow(arr[i], 2);
            }
            totalSum1 = totalSum1 / (arr.Length - 1);
            double totalSum2 = Pow(MatSpodiv(arr), 2);
            return totalSum1 - totalSum2;
        }
        private double MatSpodiv(int[] arr)
        {
            double sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }
            return sum / arr.Length;
        }
        private int[][] GetToothArr(int[,] arr)
        {
            int[][] curArr = new int[arr.GetLength(0)][];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                curArr[i] = new int[arr.GetLength(1)];
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    curArr[i][j] = arr[i, j];
                }
            }
            return curArr;
        }
        public void WriteInFile(string filePath, string text, bool append)
        {
            StreamWriter streamWriter = new StreamWriter(filePath, append);
            streamWriter.Write(text);
            streamWriter.Close();
        }

        public bool GetValuableArray(int[,] arr, string file1, string file2)
        {
            bool hasValuableLine = false;
            int valuableCounter = 0;
            int[][] toothArr = GetToothArr(arr);
            for (int i = 0; i < toothArr.Length; i++)
            {
                for (int j = 0; j < toothArr[i].Length; j++)
                {
                    int indexToRemove = j;
                    int[] tmpLine = toothArr[i].Where((source, index) => index != indexToRemove).ToArray();
                    double matSpod = MatSpodiv(tmpLine);
                    double disp = Dispersion1(tmpLine);
                    double standardDeviation = Sqrt(disp);
                    double t_p = Abs((toothArr[i][j] - matSpod) / (standardDeviation));
                    // double t_T = 1.813; 
                    double t_T = 2.5;
                    if (t_p > t_T)
                    {
                        toothArr[i] = toothArr[i].Where((source, index) => index != indexToRemove).ToArray();
                        valuableCounter++;
                    }
                }
                if (valuableCounter == 0)
                {
                    hasValuableLine = true;
                }
            }
            if (!hasValuableLine)
            {
                return false;
            }
            // output:
            string str1 = "";
            for (int i = 0; i < toothArr.Length; i++)
            {
                for (int j = 0; j < toothArr[i].Length; j++)
                {
                    str1 += $"{toothArr[i][j]}\t";
                }
                str1 += "\n";
            }
            WriteInFile(file1, str1, false);

            string str2 = "";
            for (int i = 0; i < toothArr.Length; i++)
            {
                str2 += $"Математичне сподівання №{i + 1} = {MatSpodiv(toothArr[i])}\n";
                str2 += $"Математична дисперсія №{i + 1} = {Dispersion1(toothArr[i])}\n\n";
            }
            WriteInFile(file2, str2, false);
            return true;
        }

        private int[][] GetMeaningToothArr(string source, int attempts)
        {
            StreamReader streamReader = new StreamReader(source);
            int counter = 0;
            int[][] res = new int[attempts][];
            while(!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                string[] lineArr = line.Split("\t");
                if(lineArr.Length == 13)
                {
                    res[counter] = new int[12];
                    for(int i = 0; i < 12; i ++)
                    {
                        res[counter][i] = int.Parse(lineArr[i]);
                    }
                    counter ++;
                }
            }
            streamReader.Close();
            return res;
        }

        private int GetNumOfValuableAttempts(string file)
        {
            int res = 0;
            StreamReader streamReader = new StreamReader(file);
            string[] text = streamReader.ReadToEnd().Split("\n");
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Length > 1)
                {
                    res++;
                }
            }
            streamReader.Close();
            return res;
        }

        private double Dispersion2(int[] arr)
        {
            double totalSum1 = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                totalSum1 += Pow(arr[i], 2);
            }
            totalSum1 = totalSum1 / arr.Length;
            double totalSum2 = Pow(MatSpodiv(arr), 2);
            return totalSum1 - totalSum2;
        }

        private double FindFisher(int[] arr1, int[] arr2)
        {
            double disp1 = Dispersion2(arr1);
            double disp2 = Dispersion2(arr2);
            double fisher = Max(Pow(disp1, 2), Pow(disp2, 2)) / Min(Pow(disp1, 2), Pow(disp2, 2));
            return fisher;
        }

        public bool AreSimilar()
        {
            int attempts1 = GetNumOfValuableAttempts("__Еталонні значення 1__.txt");
            int attempts2 = GetNumOfValuableAttempts("__Еталонні значення 2__.txt");
            int[][] toothArr1 = GetMeaningToothArr("__Еталонні значення 1__.txt", attempts1);
            int[][] toothArr2 = GetMeaningToothArr("__Еталонні значення 2__.txt", attempts2);
            bool a = true;
            double totalMyFisher = 0;
            double fisherCounter = 0;
            for (int i = 0; i < toothArr1.Length; i++)
            {
                if (toothArr1[i] != null)
                {
                    for (int j = 0; j < toothArr2.Length; j++)
                    {
                        if (toothArr2[j] != null)
                        {
                            double myFisher = FindFisher(toothArr1[i], toothArr2[j]);
                            double fisher = 2.79;
                            totalMyFisher += fisher;
                            fisherCounter++;
                            if (myFisher > fisher)
                            {
                                a = false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return a;
        }

        private double[] CheckAuthentication2(int[][] toothArr)
        {
            double[] mathArr = new double[toothArr.Length];
            for (int i = 0; i < toothArr.Length; i++)
            {
                if (toothArr[i] != null)
                {
                    mathArr[i] = MatSpodiv(toothArr[i]);
                }
            }
            return mathArr;
        }

        private double[] CheckAuthentication1(int[][] toothArr)
        {
            double[] dispArr = new double[toothArr.Length];
            for (int i = 0; i < toothArr.Length; i ++)
            {
                if (toothArr[i] != null)
                {
                    dispArr[i] = Dispersion2(toothArr[i]);
                }
            }
            return dispArr;
        }

        public double CheckAuthentication()
        {
            int attempts1 = GetNumOfValuableAttempts("__Еталонні значення 1__.txt");
            int attempts2 = GetNumOfValuableAttempts("__Еталонні значення 2__.txt");
            int[][] toothArr1 = GetMeaningToothArr("__Еталонні значення 1__.txt", attempts1);
            int[][] toothArr2 = GetMeaningToothArr("__Еталонні значення 2__.txt", attempts2);
            double[] dispArr1 = CheckAuthentication1(toothArr1);
            double[] dispArr2 = CheckAuthentication1(toothArr2);
            double[] mathArr1 = CheckAuthentication2(toothArr1);
            double[] mathArr2 = CheckAuthentication2(toothArr2);
            int len = 0;
            for (int i = 0; i < dispArr1.Length; i++)
            {
                if (dispArr1[i] != 0)
                {
                    len++;
                }
            }
            double[] arrP = new double[len];
            double dispersion = 0, t_p = 0;
            int r = 0;
            for (int i = 0; i < dispArr2.Length; i++)
            {
                if (dispArr2[i] != 0)
                {
                    for (int j = 0; j < dispArr1.Length; j++)
                    {
                        if (dispArr1[j] != 0)
                        {
                            dispersion = Pow(((Pow(dispArr1[j], 2) + Pow(dispArr2[i], 2)) * 11) / 23, 0.5);
                            t_p = Abs(mathArr1[j] - mathArr2[i]) / (dispersion * Sqrt(1.0 * 2 / 12));
                            // double t_T = 1.813; 
                            double t_T = 2.5;
                            if (t_p <= t_T)
                            {
                                r++;
                            }
                        }
                    }
                    arrP[i] = 1.0 * r / toothArr1.Length;
                    r = 0;
                }
            }
            double finaleP = 0;
            for (int i = 0; i < arrP.Length; i++)
            {
                finaleP += arrP[i];
            }
            return 1.0 * finaleP / arrP.Length;
        }

        private int GetNumOfValuableAttempts1(string file)
        {
            StreamReader read = new StreamReader(file);
            string[] text = read.ReadToEnd().Split("\n");
            int num = 0;
            for (int i = 0; i < text.Length; i++)
            {
                string[] line = text[i].Split("\t");
                if (line.Length == 13)
                {
                    num++;
                }
            }
            read.Close();
            return num;
        }

        public double Find_P1()
        {
            int valuableAttempts = GetNumOfValuableAttempts1("__Еталонні значення 1__.txt");
            int justAttempts = GetNumOfValuableAttempts("__Data 1__.txt");
            return 1.0 * (justAttempts - valuableAttempts) / justAttempts;
        }

        public double Find_P2()
        {
            int justAttempts = GetNumOfValuableAttempts("__Data 2__.txt");
            int valuableAttempts = GetNumOfValuableAttempts1("__Еталонні значення 2__.txt");
            return 1.0 * valuableAttempts / justAttempts;
        }
    }
}
