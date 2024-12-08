using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Grids;

namespace day6
{
    internal class Program
    {
        static int size = 130;

        static bool NextSquare(ref int x, ref int y, ref char dir, char[,] map)
        {
            switch (dir)
            {
                case 'N':
                    if(y >0)
                    {
                        if (map[x, y-1] != '#')
                        {
                            y--;
                            
                        }
                        else
                        {
                            dir = 'E';
                        }
                        return true;
                    }
                    return false;
                    break;

                case 'W':
                    if(x > 0)
                    {
                        if (map[x-1,y] != '#')
                        {
                            x--;
                        }
                        else
                        {
                            dir = 'N';
                        }
                        return true;
                    }
                    return false;
                    break;

                case 'S':
                    if(y < size-1)
                    {
                        if (map[x,y+1] != '#')
                        {
                            y++;
                            
                        }
                        else
                        {
                            dir = 'W';
                        }
                        return true;
                    }
                    return false;
                    break;


                case 'E':
                    if (x < size-1)
                    {
                        if (map[x+1, y] != '#')
                        {
                            x++;

                        }
                        else
                        {
                            dir = 'S';
                        }
                        return true;
                    }
                    return false;
                    break;



            }
            return false;

        }

        class coords 
        {
            public int x;
            public int y;

            public coords(int a, int b)
            {
                x = a;
                y = b;
            }
        }


        static void Main(string[] args)
        {
            int total = 0;
            int guardX = 0;
            int guardY = 0;
            int startX = 0;
            int startY = 0;
               

            char guardDir = 'N';

            string[] input = File.ReadAllLines("input.txt");
            char[,] map = new char[size,input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[j, i] = input[i][j];
                    if (map[j,i] == '^')
                    {
                        guardX = j;
                        guardY = i;
                        startX = j;
                        startY = i;
                        
                    }
                }
            }
            List<coords> v = new List<coords>();
            while (NextSquare(ref guardX, ref guardY, ref guardDir, map))
            {
                //Console.WriteLine(guardX + " " + guardY);
                bool c = false;
                foreach (coords a in v)
                {
                    if (a.x == guardX && a.y == guardY)
                    {
                        c = true;
                    }

                }
                if (!c)
                {
                    v.Add(new coords(guardX, guardY));
                }


            }

            foreach(coords coo in v)
            {
                if (map[coo.x, coo.y] == '.')
                {
                    map[coo.x,coo.y] = '#';
                    List<coords> visited = new List<coords>();
                    int count = 0;
                    guardDir = 'N';
                    guardX = startX;
                    guardY = startY;
                    while (NextSquare(ref guardX, ref guardY, ref guardDir, map) && count < 1000)
                    {
                        //Console.WriteLine(guardX + " " + guardY);
                        bool c = false;
                        foreach (coords a in visited)
                        {
                            if (a.x == guardX && a.y == guardY)
                            {
                                c = true;
                                count++;
                            }

                        }
                        if (!c)
                        {
                            visited.Add(new coords(guardX, guardY));
                        }


                    }
                    Console.WriteLine(count);
                    if (count == 1000)
                    {
                        total++;
                    }
                    map[coo.x,coo.y] = '.';
                }
            }


                /*for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        bool v = false;
                        foreach (coords a in visited)
                        {
                            if (a.x == j && a.y == i)
                                
                                
                                
                            {
                                v = true;
                            }

                        }
                        if (v)
                        {
                            //Console.Write('X');
                        }
                        else
                        {
                            //Console.Write(map[j, i]);
                        }

                    }
                    //Console.WriteLine();
                }
                //Console.WriteLine();
                //Console.ReadKey();*/

            Console.WriteLine(total);
            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
