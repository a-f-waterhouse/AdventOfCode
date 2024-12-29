using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List <int[]> keys = new List <int[]> ();
            List<int[]> locks = new List<int[]>();

            using (StreamReader sr = new StreamReader("input.txt"))
            {
                int i = 0;
                bool isKey = false;
                int[] key = new int[5];
                int[] Llock = new int[5];

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if(i == 8)
                    {
                        i = 0;
                        if(isKey)
                        {
                            keys.Add(key);
                        }
                        else
                        {
                            locks.Add(Llock);
                        }                        
                        
                        key = new int[5];
                        Llock = new int[5];
                    }    
                    if(i == 0)
                    {
                        if(line.Contains('#'))
                        {                            
                            isKey = false;
                        }
                        else
                        {
                            isKey = true;
                        }                        
                    }
                    
                    if(isKey && line!= "" && i < 6)
                    {                        
                        for (int j = 0; j < 5; j++)
                        {
                            if (line[j] == '#')
                            {
                                key[j]++;
                            }
                        }
                    }                    
                    else if(line!= "" && i > 0)
                    {
                        
                        for (int j = 0; j < 5; j++)
                        {
                            if (line[j] == '#')
                            {
                                Llock[j]++;
                            }
                        }
                    }

                    i++;
                }
                if (isKey)
                {
                    keys.Add(key);
                }
                else
                {
                    locks.Add(Llock);
                }
            }

            foreach (int[] key in keys)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    Console.Write(key[i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            foreach (int[] l in locks)
            {
                for (int i = 0; i < l.Length; i++)
                {
                    Console.Write(l[i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            int count = 0;
            foreach (int[] key in keys)
            {
                Console.Write("Key: ");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write(key[i]);
                }
                Console.WriteLine();
                foreach (int[] l in locks)
                {
                    /*for (int i = 0; i < 5; i++)
                    {
                        Console.Write(l[i]);
                    }
                    Console.WriteLine();*/
                    bool fits = true;
                    for (int i = 0; i < 5; i++)
                    {
                        if (l[i] + key[i] >= 6)
                        {
                            fits = false;
                        }
                    }
                    if(fits)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}
