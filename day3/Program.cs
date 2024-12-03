using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int total = 0;
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string l = "";
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    l += line;
                    
                }
                MatchCollection m = Regex.Matches(l, "mul\\([0-9]+,[0-9]+\\)");
                foreach ( Match match in m )
                {
                    string s = l.Substring(match.Index, match.Length);
                    string[] x = s.Split(',');
                    total += (int.Parse(x[0].Substring(4)) * int.Parse(x[1].Substring(0, x[1].Length - 1)));
                }
            }
            Console.WriteLine(total);
            total = 0;
            
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string l = "";
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    l += line;

                }                

                MatchCollection mul = Regex.Matches(l, "mul\\([0-9]+,[0-9]+\\)");
                foreach (Match match in mul)
                {
                    MatchCollection d = Regex.Matches(l.Substring(0,match.Index), "do\\(\\)");
                    MatchCollection dont = Regex.Matches(l.Substring(0, match.Index), "don't\\(\\)");
                    Console.WriteLine(d.Count + " " + dont.Count);
                    if (d.Count == 0 && dont.Count == 0)
                    {
                        string s = l.Substring(match.Index, match.Length);
                        string[] x = s.Split(',');
                        total += (int.Parse(x[0].Substring(4)) * int.Parse(x[1].Substring(0, x[1].Length - 1)));
                    }
                    else if(dont.Count == 0)
                    {
                        string s = l.Substring(match.Index, match.Length);
                        string[] x = s.Split(',');
                        total += (int.Parse(x[0].Substring(4)) * int.Parse(x[1].Substring(0, x[1].Length - 1)));
                    }
                    else if(d.Count == 0)
                    {

                    }
                    else if (d[d.Count - 1].Index > dont[dont.Count - 1].Index)
                    {
                        
                        string s = l.Substring(match.Index, match.Length);
                        string[] x = s.Split(',');
                        total += (int.Parse(x[0].Substring(4)) * int.Parse(x[1].Substring(0, x[1].Length - 1)));
                    }



                }

            }

            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
