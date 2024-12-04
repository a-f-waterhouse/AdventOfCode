using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> search = new List<string>();
            int total = 0;
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    search.Add( line);
                    total += Regex.Matches(line, "SAMX").Count;
                    total += Regex.Matches(line, "XMAS").Count;
                }

            }
            for (int i = 0; i < search.Count; i++)
            {
                
                string line = search[i];
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == 'X' && j < line.Length - 3 && i < search.Count-3)
                    {
                        if (search[i + 1][j+1] == 'M' && search[i + 2][j + 2] == 'A' &&  search[i + 3][j + 3] == 'S')
                        {
                            total++;
                            Console.WriteLine("A");
                        }
                    }
                    if (line[j] == 'X' && j >= 3 && i >= 3)
                    {
                        if (search[i - 1][j - 1] == 'M' && search[i - 2][j - 2] == 'A' && search[i - 3][j - 3] == 'S')
                        {
                            total++;
                            Console.WriteLine("B");
                        }
                        
                    }
                    if (line[j] == 'X' && j < line.Length - 3 && i  >=3)
                    {
                        if (search[i - 1][j + 1] == 'M' && search[i - 2][j + 2] == 'A' && search[i - 3][j + 3] == 'S')
                        {
                            total++;
                            Console.WriteLine("A");
                        }
                    }
                    if (line[j] == 'X' && j >= 3 && i < search.Count-3)
                    {
                        if (search[i + 1][j - 1] == 'M' && search[i + 2][j - 2] == 'A' && search[i + 3][j - 3] == 'S')
                        {
                            total++;
                            Console.WriteLine("B");
                        }

                    }
                    if (line[j] == 'X' && i >= 3)
                    {
                        if (search[i -1][j] == 'M' && search[i - 2][j] == 'A' && search[i - 3][j] == 'S')
                        {
                            total++;
                            Console.WriteLine("C");
                        }
                        
                    }
                    if (line[j] == 'X' && i < search.Count-3)
                    {
                        if (search[i + 1][j] == 'M' && search[i + 2][j] == 'A' && search[i + 3][j] == 'S')
                        {
                            total++;
                            Console.WriteLine("D");
                        }
                        
                    }
                }
            }
            Console.WriteLine(total);
            total = 0;

            for (int i = 0; i < search.Count-2; i++)
            {
                string l = search[i];
                for (int j = 0; j < l.Length-2; j++)
                {
                    if(Regex.Match(search[i + 1].Substring(j, 3), "[A-Z]A[A-Z]").Success)
                    {
                        if (Regex.Match(search[i].Substring(j, 3), "M[A-Z]M").Success && Regex.Match(search[i+2].Substring(j, 3), "S[A-Z]S").Success)
                        {
                            Console.WriteLine(1);
                            total++;
                        }
                        if (Regex.Match(search[i].Substring(j, 3), "S[A-Z]S").Success && Regex.Match(search[i + 2].Substring(j, 3), "M[A-Z]M").Success)
                        {
                            total++;
                            Console.WriteLine(2);
                        }
                        if (Regex.Match(search[i].Substring(j, 3), "S[A-Z]M").Success && Regex.Match(search[i + 2].Substring(j, 3), "S[A-Z]M").Success)
                        {
                            total++;
                            Console.WriteLine(3);
                        }
                        if (Regex.Match(search[i].Substring(j, 3), "M[A-Z]S").Success && Regex.Match(search[i + 2].Substring(j, 3), "M[A-Z]S").Success)
                        {
                            total++;
                            Console.WriteLine(4);
                        }

                    }




                }
            }

            Console.WriteLine(total);

            Console.ReadKey();


        }

    }
}
