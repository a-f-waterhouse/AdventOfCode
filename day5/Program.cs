using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int total = 0;
            List<int>[] pagesAfter = new List<int>[100];
            for (int i = 0; i < 100; i++)
            {
                pagesAfter[i] = new List<int>();
            }
            bool a = false;

            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line == "aaa")
                    {
                        a = true;
                    }
                    if (!a)
                    {
                        int one = int.Parse(line.Split('|')[0]);
                        int two = int.Parse(line.Split('|')[1]);

                        pagesAfter[one].Add(two);
                    }
                }
            }
            
            a = false;
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line == "aaa")
                    {
                        a = true;
                    }
                    if (line != "aaa" && a)
                    {
                        int[] pages = line.Split(',').Select(int.Parse).ToArray();
                        bool correct = true;
                        for (int i = 0; i < pages.Length; i++)
                        {
                            for (int j = 0; j < i; j++)
                            {                                
                                if (pagesAfter[pages[i]].Count != 0)
                                {

                                    if (pagesAfter[pages[i]].Contains(pages[j]))
                                    {
                                        correct = false;
                                    }
                                }
                            }
                        }
                        if (!correct)
                        {
                            bool ordered = false;
                            while(!ordered)
                            {
                                for (int i = 0; i < pages.Length-1; i++)
                                {
                                    if (pagesAfter[pages[i+1]].Contains(pages[i]))
                                    {
                                        int temp = pages[i];
                                        pages[i] = pages[i + 1];
                                        pages[i+ 1] = temp;
                                    }
                                }

                                ordered = true;
                                for (int i = 0; i < pages.Length; i++)
                                {
                                    for (int j = 0; j < i; j++)
                                    {
                                        if (pagesAfter[pages[i]].Count != 0)
                                        {

                                            if (pagesAfter[pages[i]].Contains(pages[j]))
                                            {
                                                ordered = false;
                                            }
                                        }
                                    }
                                }
                            }
                            
                            Console.WriteLine(line);
                            total += pages[pages.Length / 2];
                        }

                    }
                }

                Console.WriteLine(total);
                total = 0;




                Console.WriteLine(total);
                Console.ReadKey();
            }


        }
    }
}
