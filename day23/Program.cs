using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Data;

namespace day23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            List<string> trios = new List<string>(0);
            Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
            foreach (string line in input)
            {
                string[] a = line.Split('-');
                if (!connections.ContainsKey(a[0]))
                {
                    List<string> c = new List<string>();
                    c.Add(a[1]);
                    connections.Add(a[0], c);
                }
                else
                {
                    connections[a[0]].Add(a[1]);
                }
                if (!connections.ContainsKey(a[1]))
                {
                    List<string> c = new List<string>();
                    c.Add(a[0]);
                    connections.Add(a[1], c);
                }
                else
                {
                    connections[a[1]].Add(a[0]);
                }
            }

            string[] format = "pp-fd-bu-yu-hu-kj-nx-it-dj-as-ez-cp-xh".Split('-');
            Array.Sort(format);
            foreach(string s in format)
            {
                Console.Write(s + ',');
            }
            Console.WriteLine();
            Console.ReadKey();




            foreach (string line in input)
            {
                int num = 2;
                string[] a = line.Split('-');
                List<string> possibles = new List<string>();
                foreach(string s in connections[a[0]])
                {
                    if (connections[a[1]].Contains(s))
                    {
                        num++;
                        possibles.Add(s);
                    }
                }
                bool loop = true;
                foreach(string s in possibles)
                {
                    foreach(string s2 in possibles)
                    {
                        if (s!= s2 && !connections[s].Contains(s2))
                        {
                            loop = false;
                        }
                    }
                }
                if(loop && possibles.Count > 2)
                {
                    Console.Write(line);
                    foreach(string s in possibles)
                    {
                        Console.Write('-' + s);
                    }
                    Console.WriteLine();
                }
            }


            trios.Sort();
            foreach(string s in trios)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine(trios.Count());

            Console.ReadKey();
        }


        /*static void pt1()
        {
                        foreach(string line in input)
            {               
                string[] a = line.Split('-');
                
                foreach(string s in connections[a[0]])
                {
                    if (connections[a[1]].Contains(s))
                    {                         
                        string[] t = new string[3];
                        t[0] = a[0];
                        t[1] = a[1];
                        t[2] = s;
                        Array.Sort(t);
                        string equiv = t[0] + "-" + t[1] + "-" + t[2];
                        if (!trios.Contains(equiv) && (t[0][0] == 't' || t[1][0] == 't' || t[2][0] == 't'))
                        {
                            trios.Add(equiv);
                        }

                    }
                }
                
            }
        }*/
    }
}
