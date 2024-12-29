using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long max = 0;
            string maxS = "";

            long[] input = File.ReadAllLines("input.txt").Select(long.Parse).ToArray();

            Dictionary<string, int> values = new Dictionary<string, int>();
 
            for (int i = 0; i < input.Length; i++)
            {
                number(input[i],  ref values);
                Console.WriteLine(i);
            }
            foreach(var key in values.Keys)
            {
                //Console.WriteLine(key + " " + values[key]);
                if(max < (values[key]) && !key.StartsWith("000"))
                {
                    max = values[key];
                    maxS = key;
                }
            }
            Console.WriteLine(maxS);
            

            Console.WriteLine(max);
            Console.ReadKey();
        }

        static long nextNum(long num)
        {
            long toMix = num * 64;
            num = (toMix ^ (num)) % 16777216;
            toMix = num / 32;            
            num = ( toMix ^ num) % 16777216;
            toMix = num * 2048;
            num = (toMix ^ (num)) % 16777216;
            return num;
        }

        static int number(long startNum, int[] sequence)
        {
            int[] currentSequence = new int[4];
            for (int i = 0; i < 2000; i++)
            {
                long next = nextNum(startNum);
                int diff = (int.Parse(next.ToString()[next.ToString().Length - 1].ToString()) - int.Parse(startNum.ToString()[startNum.ToString().Length - 1].ToString()));
                startNum = next;
                currentSequence = shuffleUp(currentSequence, diff);
                if (Enumerable.SequenceEqual(sequence, currentSequence))
                {
                    return int.Parse(next.ToString()[next.ToString().Length-1].ToString());
                }
            }
            return 0;
        }

        static int number(long startNum, ref Dictionary<string, int> sequences)
        {
            List<string> done = new List<string>();
            int[] currentSequence = new int[4];
            for (int i = 0; i < 2000; i++)
            {
                long next = nextNum(startNum);
                int diff = (int.Parse(next.ToString()[next.ToString().Length - 1].ToString()) - int.Parse(startNum.ToString()[startNum.ToString().Length - 1].ToString()));
                startNum = next;
                currentSequence = shuffleUp(currentSequence, diff);
                string sCS = "";
                foreach(int j in currentSequence)
                {
                    sCS += j.ToString();
                }
                if (!done.Contains(sCS))
                {
                    if (!sequences.ContainsKey(sCS))
                    {
                        sequences.Add(sCS, int.Parse(next.ToString()[next.ToString().Length - 1].ToString()));
                    }
                    else
                    {
                        sequences[sCS] += int.Parse(next.ToString()[next.ToString().Length - 1].ToString());
                    }
                    done.Add(sCS);
                }

            }
            return 0;
        }

        static int[] shuffleUp(int[] a, int n)
        {
            for (int i = 0; i < a.Length-1; i++)
            {
                a[i] = a[i + 1];
            }
            a[a.Length - 1] = n;
            return a;
        }

    }
}
