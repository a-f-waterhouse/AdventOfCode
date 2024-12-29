using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace day21
{
    internal class Program
    {
        static void Main(string[] args)
        {          
            string[] inputs = { "869A", "180A", "596A", "965A", "973A" };
            //string[] inputs = { "029A", "980A", "179A", "456A", "379A" };
            
            char[,] numKeyPad = { { '7', '8', '9' }, { '4', '5', '6' }, { '1', '2', '3' }, { 'X', '0', 'A' } };
            char[,] dirkeyPad = { { 'X', '^', 'A' }, { '<', 'v', '>' } };

            Console.WriteLine(numKeyPad.Length + " " + dirkeyPad.Length);

            string path = "";
            long total = 0;
            foreach (string s in inputs)
            {
                //Console.WriteLine(s);
                Console.WriteLine("Number Keypad: " + shortestPath('A',s, numKeyPad, ref path));
                Console.WriteLine(path); //possible paths = split at each A, with each piece can be any arrangement of parts <3
                
                string temp = "";


                Dictionary<string, long> parent = new Dictionary<string, long>();
                foreach(char c in dirkeyPad)
                {
                    foreach(char c2 in dirkeyPad)
                    {
                        //Console.WriteLine(c + " " + c2);
                        temp = "";
                        long cost = shortestPath(c, c2.ToString(), dirkeyPad, ref temp);
                        parent.Add((c + " " + c2), cost);
                            
                        //Console.WriteLine(temp);
                        //Console.WriteLine("Cost between: " + c + " " + c2 + " = " + cost);
                    }
                }


                for (long i = 0; i < 24; i++)
                {
                    Console.WriteLine("Keypad: " + (i+2));
                    Console.WriteLine(parent["< ^"]);
                    Dictionary<string, long> child = new Dictionary<string, long>();

                    foreach (char c in dirkeyPad)
                    {
                        foreach (char c2 in dirkeyPad)
                        {
                            if( c!= 'X' && c2 != 'X')
                            {
                                temp = "A";
                                shortestPath(c, c2.ToString(), dirkeyPad, ref temp);
                                long cost = 0;                                
                                for (int j = 0; j < temp.Length - 1; j++)
                                {
                                    string test = (temp[j] + " " + temp[j + 1]);
                                    //Console.WriteLine("Test: " + test);
                                    cost += ((parent[test]));// + 1));
                                }
                                //cost += 1;
                                child.Add((c + " " + c2), cost);
                                //if(c2 == 'A')
                                {
                                    //Console.WriteLine(temp);
                                    //Console.WriteLine("Cost between: " + c + " " + c2 + " = " + cost);
                                }
                                

                            }                           

                        }
                    }

                    parent = child;
                }

                long count = 0;

                path = "A" + path;
                for (int i = 0; i < path.Length-1; i++)
                {
                    count += parent[(path[i] + " " + path[i + 1])];// +1;
                }
                //count++;

                path = "";
                long a = (long.Parse(s.Substring(0, s.Length - 1)) * count);
                Console.WriteLine(count);               
                total += a;
                Console.ReadKey();
            }

            Console.WriteLine(total);
            Console.ReadKey();
        }

        static long shortestPath(char start, string input, char[,] keyPad, ref string path)
        {
            for (int j = 0; j < input.Length; j++)
            {                
                long a1 = 0, b1 = 0, a2 = 0, b2 = 0;
                getCoordsNum(ref a1, ref b1, start, keyPad);
                getCoordsNum(ref a2, ref b2, input[j], keyPad);
                string tmpPath = "";

                bool upFirst = false;

                if (keyPad.Length == 12 && a1 == 3 && b2 == 0)
                {
                    upFirst = true;
                }
                else if (keyPad.Length == 12 && a2 == 0 && b1 == 3)
                {
                    upFirst = false;
                }
                if (keyPad[a1, b1] == '<')
                {
                    upFirst = false;
                }
                else if (keyPad[a2, b2] == '<')
                {
                    upFirst = true;
                }
                else if (b2 - b1 >= 0)
                {
                    upFirst = true;
                }

                if (upFirst)
                {
                    for (long i = 0; i < Math.Abs(a1 - a2); i++)
                    {
                        if (a1 - a2 > 0)
                        {
                            tmpPath += '^';
                        }
                        else
                        {
                            tmpPath += 'v';
                        }
                    }
                    for (long i = 0; i < Math.Abs(b1 - b2); i++)
                    {
                        if (b1 - b2 > 0)
                        {
                            tmpPath += '<';
                        }
                        else
                        {
                            tmpPath += '>';
                        }
                    }
                }
                else
                {
                    for (long i = 0; i < Math.Abs(b1 - b2); i++)
                    {
                        if (b1 - b2 > 0)
                        {
                            tmpPath += '<';
                        }
                        else
                        {
                            tmpPath += '>';
                        }
                    }
                    for (long i = 0; i < Math.Abs(a1 - a2); i++)
                    {
                        if (a1 - a2 > 0)
                        {
                            tmpPath += '^';
                        }
                        else
                        {
                            tmpPath += 'v';
                        }
                    }
                }

                path += tmpPath;
                path += 'A';
                start = input[j];
            }

            return path.Length;
        }

        static long aaashortestPath(char start, string input, char[,] keyPad, ref string path)
        {
            if(input.Length == 0)
            {
                return 0;
            }            
            long a1 = 0, b1 = 0, a2 = 0, b2 = 0, Xa = 0, Xb = 0;
            getCoordsNum(ref a1, ref b1, start, keyPad);
            getCoordsNum(ref a2, ref b2, input[0], keyPad);
            getCoordsNum(ref Xa, ref Xb, 'X', keyPad);           
            string tmpPath = "";

            bool upFirst = false;

            if(keyPad.Length == 12 && a1 == 3 && b2 == 0)
            {
                upFirst = true;
            }
            else if(keyPad.Length == 12 && a2 == 0 && b1 == 3)
            {
                upFirst = false;
            }
            if (keyPad[a1,b1] == '<')
            {
                upFirst = false;
            }
            else if (keyPad[a2, b2] == '<')
            {
                upFirst = true;
            }
            else if(b2 - b1 >= 0)
            {
                upFirst = true;
            }

            if(upFirst)
            {
                for (long i = 0; i < Math.Abs(a1 - a2); i++)
                {
                    if (a1 - a2 > 0)
                    {
                        tmpPath += '^';
                    }
                    else
                    {
                        tmpPath += 'v';
                    }
                }
                for (long i = 0; i < Math.Abs(b1 - b2); i++)
                {
                    if (b1 - b2 > 0)
                    {
                        tmpPath += '<';
                    }
                    else
                    {
                        tmpPath += '>';
                    }
                }
            }
            else
            {
                for (long i = 0; i < Math.Abs(b1 - b2); i++)
                {
                    if (b1 - b2 > 0)
                    {
                        tmpPath += '<';
                    }
                    else
                    {
                        tmpPath += '>';
                    }
                }
                for (long i = 0; i < Math.Abs(a1 - a2); i++)
                {
                    if (a1 - a2 > 0)
                    {
                        tmpPath += '^';
                    }
                    else
                    {
                        tmpPath += 'v';
                    }
                }
            }
            
            path += tmpPath;
            path += 'A';
            return 1 + (Math.Abs(a1-a2) + Math.Abs(b1-b2)) + shortestPath(input[0], input.Substring(1), keyPad, ref path);
        }

        static long shortestPath(char startDigit, string input)
        {

            return 0;
        }

        static bool getCoordsNum(ref long a, ref long b, char c, char[,] grid)
        {
            for (long i = 0; i < 3; i++)
            {
                for (long j = 0; j < grid.Length /3; j++)
                {
                    if (grid[j,i] == c)
                    {
                        a = j;
                        b = i;
                        return true;
                    }
                }
            }
            a = -1;
            b = -1;
            return false;
        }
    }
}
