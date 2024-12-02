using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace day2
{
    internal class Program
    {

        static bool safe(int[] line)
        {
            bool d = false;
            if (line[0] - line[1] > 0)
            {
                d = true;
            }
            for (int i = 0; i < line.Length-1; i++)
            {
                if (Math.Abs(line[i] - line[i+1]) > 3|| Math.Abs(line[i] - line[i + 1]) < 1)
                {
                    return false;
                }
                else if (d && line[i] - line[i + 1] < 0)
                {
                    return false;
                }
                else if (!d && line[i] - line[i + 1] > 0)
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            int total = 0;
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    int[] line = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
                    if(safe(line))
                    {
                        total++;
                    }
                    else
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            int[] newline = new int[line.Length-1];
                            for (int j = 0; j < line.Length; j++)
                            {
                                if(j< i)
                                {
                                    newline[j] = line[j];
                                }
                                else if(j>i)
                                {
                                    newline[j-1] = line[j];
                                }
                            }
                            if(safe(newline))
                            {
                                total++;
                                break;
                            }
                            
                        }
                    }

                }
            }

            Console.WriteLine(total);
            Console.ReadKey();

        }
    }
}
