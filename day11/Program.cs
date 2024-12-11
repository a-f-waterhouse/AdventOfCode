using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.Eventing.Reader;

namespace day11
{
    internal class Program

        
    {
        static List<long> ApplyRules(long num)
        {
            List<long> l = new List<long>();
            if (num == 0) // 2024, //20 24
            {
                l.Add(1);
            }
            else if (num.ToString().Length % 2 == 0)
            {            
                string s = num.ToString();
                l.Add(long.Parse(s.Substring(0, s.Length / 2)));
                l.Add(long.Parse(s.Substring(s.Length / 2, s.Length / 2)));
            }
            else
            {

                l.Add(num * 2024);
            }
            return l;
        }

        static void Main(string[] args)
        {
            List<long> input = File.ReadAllText("input.txt").Split(' ').Select(long.Parse).ToList();
            //List<long> stones = new List<long>();

            Dictionary<long, long> initialStones = new Dictionary<long, long>();
            foreach(long stone in input)
            {
                initialStones.Add(stone, 1);
            }

            for (int i = 0; i < 75; i++)
            {
                Dictionary<long, long> stones = new Dictionary<long, long>();
                List<long> ToRemove = new List<long>();
                foreach(long stone in initialStones.Keys)
                {
                    
                    if (initialStones[stone] > 0)
                    {
                        stones.Add(stone, initialStones[stone]);
                    }
                    else
                    {
                        ToRemove.Add(stone);
                    }
                }
                foreach(long stone in ToRemove)
                {
                    initialStones.Remove(stone);
                }
                foreach (long stone in initialStones.Keys)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        //Console.WriteLine(stone + "s");
                        if (stone == 0)
                        {
                            //Console.WriteLine("a");
                            if (stones.ContainsKey(1))
                            {
                                stones[1] += initialStones[stone];
                            }
                            else
                            {
                                stones.Add(1, initialStones[stone]);
                            }
                            stones[0]-= initialStones[stone];
                        }
                        else if (stone.ToString().Length % 2 == 0)
                        {
                          //  Console.WriteLine("b");
                            string s = stone.ToString();
                            if (stones.ContainsKey(long.Parse(s.Substring(0, s.Length / 2))))
                            {
                                stones[(long.Parse(s.Substring(0, s.Length / 2)))] += initialStones[stone];
                            }
                            else
                            {
                                stones.Add((long.Parse(s.Substring(0, s.Length / 2))), initialStones[stone]) ;
                            }
                            if (stones.ContainsKey(long.Parse(s.Substring(s.Length / 2, s.Length / 2))))
                            {
                                stones[long.Parse(s.Substring(s.Length / 2, s.Length / 2))] += initialStones[stone];
                            }
                            else
                            {
                                stones.Add(long.Parse(s.Substring(s.Length / 2, s.Length / 2)), initialStones[stone]);
                            }
                            stones[stone]-= initialStones[stone];
                        }
                        else
                        {
                           // Console.WriteLine("c");
                            if (stones.ContainsKey(stone * 2024))
                            {
                                stones[stone * 2024]+= initialStones[stone];

                            }
                            else
                            {
                                stones.Add(stone * 2024, initialStones[stone]);
                            }
                            stones[stone] -= initialStones[stone];

                        }
                    }

                    
                }

                initialStones.Clear();
                //Display(stones);
                initialStones = stones;

                Console.WriteLine(initialStones.Count + " " + i + "x");
                Console.WriteLine();
                //Console.ReadKey();
            }

            long total = 0;
            foreach(long s in initialStones.Keys)
            {
                total += initialStones[s];
                //Console.WriteLine(s + " " + initialStones[s]);
            }
            Console.WriteLine( total);

            Console.WriteLine("done");
            
            Console.ReadKey();
        }

        static void Display(Dictionary<long,long> dict)
        {
            foreach(long l in dict.Keys)
            {
                Console.WriteLine(l + " " + dict[l]);
            }
            Console.WriteLine();
        }
    }
}
