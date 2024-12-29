using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace day19
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            long count = 0;
            List<string> newRules = new List<string>();


            string[] avaliableTowels = input[0].Split(',');
            for (long i = 0; i < avaliableTowels.Length; i++)
            {
                avaliableTowels[i] = avaliableTowels[i].Trim(' ');
            }
            

            Console.WriteLine("---------------------------");
            Console.ReadKey();

            for (long i = 2; i < input.Length; i++) 
            {
                Console.WriteLine(i);
                string towelCombo = input[i];
               
                if(Possible(towelCombo, avaliableTowels.ToArray(), new List<string>()))
                {
                    long combos = Combos(towelCombo, avaliableTowels.ToArray(), new Dictionary<string, long>());
                    count += combos;
                    Console.WriteLine("Final: " + towelCombo + " " + combos);

                }
            }
            
            Console.WriteLine(count + " yay");
            Console.ReadKey();

        }
        static long Combos(string towel, string[] avaliableTowels, Dictionary<string, long> cache)
        {
            if(towel.Length == 0)
            {
                return 1;
            }
            //Console.WriteLine("Testing: " + towel);
            //Console.ReadKey();
            if(cache.ContainsKey(towel))
            {
                //Console.WriteLine("Returned (A): " + cache[towel]);
                return cache[towel];
            }
            else
            {
                long total = 0;
                foreach(string t in avaliableTowels)
                {                    
                    if(towel.StartsWith(t))
                    {
                        //long i = towel.IndexOf(t);
                        //string[] split = { t, towel.Substring(0, i), towel.Substring(i + t.Length) };

                        total += Combos(towel.Substring(t.Length), avaliableTowels, cache);
                    }
                }
                cache.Add(towel, total);
                //Console.WriteLine("Returned (B): " + total + " for " + towel);
                
                return total;
                
            }
        }


        static bool Possible(string towel, string[] avaliableTowels, List<string> rejected)
        {
            if(avaliableTowels.Contains(towel))// || (!towel.Contains('u')))
            {
                return true;
            }
            else if(towel.Length == 1 || rejected.Contains(towel))
            {
                return false;
            }
            else
            {
                for (int i = towel.Length-1; i > 0; i--)
                {
                    if (avaliableTowels.Contains(towel.Substring(0, i)))
                    {
                        if(Possible(towel.Substring(i), avaliableTowels, rejected))
                        {
                            return true;
                        }
                        else
                        {
                            rejected.Add(towel.Substring(i));
                        }
                    }
                }
                return false;
            }
        }
    }
}
