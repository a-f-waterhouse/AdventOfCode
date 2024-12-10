using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Data.SqlTypes;
using System.Runtime.Remoting.Channels;

namespace day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long total = 0;
            string line = File.ReadAllText("input.txt");
            int size = 0;
            foreach (char c in line)
            {
                size += int.Parse(c.ToString());
            }
            int[] disk = new int[size];
            Dictionary<int, int> diskDict = new Dictionary<int, int>(); //fileID, length

            int pointer = 0;
            for (int i = 0; i < line.Length; i++)
            {
                for (int j = 0; j < int.Parse(line[i].ToString()); j++)
                {
                    if (i % 2 == 0)
                    {
                        disk[pointer] = i / 2;
                        if(diskDict.ContainsKey(i/2))
                        {
                            diskDict[i/2]++;
                        }
                        else
                        {
                            diskDict.Add(i/2, 1);
                        }
                    }
                    else
                    {
                        disk[pointer] = -1;
                        
                    }
                    
                    pointer++;
                }

            }

            Console.WriteLine("list created");


            for (int i = diskDict.Keys.Count - 1; i >= 0; i--)
            {
                int num = diskDict.Keys.ElementAt(i);
                int length = diskDict[diskDict.Keys.ElementAt(i)];

                for (int j = 0; j < disk.Length; j++)
                {
                    int s = 0;
                    while (j < disk.Length && disk[j] == -1)
                    {
                        s++;
                        j++;
                    }
                    
                    if(s >= length && j - s < Array.IndexOf(disk, num))
                    {
                        //Console.WriteLine(j - s + " " + Array.IndexOf(disk, num));
                        j -= s;
                        for (int k = 0; k < diskDict[num]; k++)
                        {
                            disk[Array.IndexOf(disk, num)] = -1;
                        }
                        for (int k = 0; k < diskDict[num]; k++)
                        {
                            disk[j + k] = num;
                        }
                        //Display(disk);

                        j += s;
                        break;
                    }
                }
            }

            /*for (int i = 0; i < disk.Length; i++)
            {
                Display(disk);
                int s = 0;
                while(i < disk.Length && disk[i] == -1)
                {
                    s++;
                    i++;
                }
                if(s > 0)
                {
                    int replacement = FindLowest(diskDict, s);
                    
                    Console.WriteLine(i + " " + s + " " + replacement);
                    if(replacement != -1)
                    {
                        i -= s;
                        for (int j = 0; j < diskDict[replacement]; j++)
                        {                            
                            disk[Array.IndexOf(disk, replacement)] = -1;
                        }
                        for (int j = 0; j < diskDict[replacement]; j++)
                        {
                            disk[i + j] = replacement;
                        }
                        Display(disk);

                        i += s;
                        diskDict.Remove(replacement);
                    }

                }                

            }*/

            /*while(! sorted(disk))
            {
                for (int i = 0; i < disk.Length; i++)
                {
                    if (disk[i] == -1)
                    {
                        disk[i] = disk[lastIndex(disk)];
                        disk[lastIndex(disk)] = -1;
                    }
                }
                Console.WriteLine("sorting");
            }*/

            for (int i = 0; i < disk.Length; i++)
            {
                Console.Write(disk[i]);
                if (disk[i] == -1)
                {
                    //break;
                }
                else
                {
                    total += i * disk[i];
                   // Console.WriteLine(total);
                }
                
            }
            Console.WriteLine();

            Console.WriteLine(total);

            Console.ReadKey();
        }


        static int FindLowest(Dictionary<int,int> d, int size)
        {
            for (int i = d.Keys.Count-1; i >= 0; i--)
            {
                if (d[d.Keys.ElementAt(i)] <= size)
                {
                    return d.Keys.ElementAt(i);
                }
            }
            return -1;
        }

        static void Display(int[] aa)
        {
            foreach(int i in aa)
            {
                if(i == -1)
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write(i);
                }
            }
            Console.WriteLine();
        }


        static int lastIndex(int[] ints)
        {
            for (int i = ints.Length-1; i > 0; i--)
            {
                if(ints[i] != -1)
                {
                    return i;
                }
            }
            return -1;
        }

        static bool sorted(int[] ints)
        {
            bool x = false;
            foreach(int i in ints)
            {
                if(i == -1)
                {
                    x = true;
                }
                else if (x)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
