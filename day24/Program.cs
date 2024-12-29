using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace day24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> wireValues = new Dictionary<string, int>();
            List<string> gates = new List<string>();
            
            using(StreamReader sr = new StreamReader("input.txt"))
            {
                bool s = false;
                while(!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if(line== "")
                    {
                        s = true;
                    }

                    if(!s)
                    {
                        wireValues.Add(line.Substring(0,3), int.Parse(line.Substring(5)));
                        Console.WriteLine("Adding: " + line.Substring(0, 3) + " " + int.Parse(line.Substring(5)));
                    }
                    else if(line != "")
                    {
                        gates.Add(line);
                    }
                }
            }

            foreach (string line in gates) //vgh OR dhk -> kfp
            {
                string[] gate = line.Split(' '); //0,2 inputs, 1 gate, 4 output

                for (int i = 0; i < gate.Length; i++)
                {
                    if (i != 3 && i!= 1 && !wireValues.ContainsKey(gate[i]))
                    {
                        wireValues.Add(gate[i], -1);
                        Console.WriteLine("Adding: " + gate[i] + " -1");
                    }
                }
            }

            int[] Xvalues = new int[47];
            for (int i = 0; i < Xvalues.Length; i++)
            {
                Xvalues[i] = 3;
            }

            gates.Sort();
            foreach (string gate in gates)
            {
                Console.WriteLine(gate);
            }

            Console.ReadKey();
            pt1(gates,wireValues);
            Console.WriteLine();
            Console.WriteLine(23854477729455 + 22066456577055);
            Console.ReadKey();
        }


        static void pt1(List<string> gates, Dictionary<string, int> wireValues)
        {
            bool end = false;

            while (!end)
            {
                end = true;
                foreach (string line in gates) //vgh OR dhk -> kfp
                {

                    string[] gate = line.Split(' '); //0,2 inputs, 1 gate, 4 output

                    //Console.WriteLine("inputs: " + wireValues[gate[0]] + " " + wireValues[gate[2]]);
                    //Console.WriteLine(gate[4] + " becomes " + applyGate(gate[1], wireValues[gate[0]], wireValues[gate[2]]));
                    //Console.ReadKey();


                    wireValues[gate[4]] = applyGate(gate[1], wireValues[gate[0]], wireValues[gate[2]]);
                    if (wireValues[gate[4]] == -1)
                    {
                        //Console.WriteLine(gate[4]);
                        end = false;
                    }
                }
            }

            int[] Zvalues = new int[47];
            for (int i = 0; i < Zvalues.Length; i++)
            {
                Zvalues[i] = 3;
            }

            foreach (string key in wireValues.Keys)
            {
                if (key.StartsWith("z"))
                {
                    Zvalues[int.Parse(key.Substring(1, 2))] = wireValues[key];
                }
            }
            foreach (int i in Zvalues)
            {
                Console.Write(i);
            }
            Console.ReadKey();
        }



        static int applyGate(string gate, int a, int b)
        {
            //Console.WriteLine(a + gate + b);
            if(a >= 0 && b >= 0)
            {
                switch (gate)
                {
                    case "AND":
                        return a & b;
                    case "OR":
                        return a | b;
                    case "XOR":
                        return a ^ b;

                }
            }
            return - 1;

        }
    }


}
