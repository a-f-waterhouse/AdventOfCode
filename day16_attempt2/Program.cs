using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Collections;

namespace day16_attempt2
{
    internal class Program
    {
        static int D(Grid map)
        {
            Coords start = map.CoordsOf('S');
            Coords end = map.CoordsOf('E');
            List<Coords> points = new List<Coords>();
            Dictionary<Coords, List<Coords>> prevs = new Dictionary<Coords, List<Coords>>();
            Dictionary<Coords, char> dirs = new Dictionary<Coords, char>();
            Dictionary<Coords, int> dists = new Dictionary<Coords, int>();
            points = map.CoordsOf('.', true);

            for (int i = 0; i < points.Count; i++)
            {
                prevs.Add(points[i], new List<Coords>());
                dirs.Add(points[i], '.');
                dists.Add(points[i], 1000000000);
            }
            Queue<Coords> queue = new Queue<Coords>();

            queue.Enqueue(start);
            //prevs.Add(start, new List<Coords>());
            dists[start] = 0;
            dirs[start] = 'E';

            dirs.Add(end, '.');
            dists.Add(end, 1000000000);
            prevs.Add(end, new List<Coords>());
            while (queue.Count > 0)
            {                
                Coords current = queue.Dequeue();
                Coords test = current;

                Console.WriteLine(current.x + " " + current.y + " " + dists[current] + " " + prevs[current].Count);

                test.x--;
                if (map.Element(test) != '#') //
                {
                    int newDist = dists[current] + 1;
                    if (dirs[current] == 'N' || dirs[current] == 'S')
                    {
                        newDist += 1000;
                    }
                    if(newDist < dists[test])
                    {
                        prevs[test].Clear();
                    }
                    if (newDist <= dists[test])
                    {
                        dists[test] = newDist;
                        prevs[test].Add( current);
                        dirs[test] = 'W';
                        queue.Enqueue(test);
                    }
                }
                test.x += 2;
                if (map.Element(test) != '#')
                {
                    int newDist = dists[current] + 1;
                    if (dirs[current] == 'N' || dirs[current] == 'S')
                    {
                        newDist += 1000;
                    }
                    if (newDist < dists[test])
                    {
                        prevs[test].Clear();
                    }


                    if (newDist <= dists[test])
                    {
                        dists[test] = newDist;
                        prevs[test].Add( current);
                        dirs[test] = 'E';
                        queue.Enqueue(test);
                    }
                }
                test.x--;
                test.y++;
                if (map.Element(test) != '#')
                {
                    int newDist = dists[current] + 1;
                    if (dirs[current] == 'E' || dirs[current] == 'W')
                    {
                        newDist += 1000;
                    }
                    if (newDist < dists[test])
                    {
                        prevs[test].Clear();
                    }

                    if (newDist <= dists[test])
                    {
                        dists[test] = newDist;
                        prevs[test].Add( current);
                        dirs[test] = 'S';
                        queue.Enqueue(test);
                    }
                }
                test.y -= 2;
                if (map.Element(test) != '#')
                {
                    int newDist = dists[current] + 1;
                    if (dirs[current] == 'E' || dirs[current] == 'W')
                    {
                        newDist += 1000;
                    }
                    if (newDist < dists[test])
                    {
                        prevs[test].Clear();
                    }

                    if (newDist <= dists[test])
                    {
                        dists[test] = newDist;
                        prevs[test].Add( current);
                        dirs[test] = 'N';
                        queue.Enqueue(test);
                    }
                }
            }

            List<Coords> onPath = new List<Coords>();
            backtrack(end,prevs,ref onPath);
            Console.WriteLine(onPath.Count);
            return dists[end];
        }

        static List<Coords> backtrack(Coords end, Dictionary<Coords, List<Coords>> prevs, ref List<Coords> count)
        {
            Console.WriteLine("Backtracking from: " + end.x + " " + end.y);
            Console.WriteLine("Prevs count: " + prevs[end].Count);
            foreach (Coords c in prevs[end])
            {
                Console.Write(c.x + " " + c.y + "   ");

            }
            Console.WriteLine();
            foreach (Coords c in prevs[end])
            {                
                if (!count.Contains(c))
                {
                    count.Add(c);
                    backtrack(c, prevs, ref count);
                }

            }
            return count;
        }


        static void Main(string[] args)
        {
            Grid maze = new Grid(File.ReadAllLines("input.txt"));
            Console.WriteLine(D(maze));
            Console.ReadKey();
        }


        /*static void explore(Coords c, Grid map, ref List<Coords> seats, int r, Coords start)
        {
            Console.WriteLine("Removing: " + c.x + " " + c.y);

            if (!(c.x == start.x && c.y == start.y))
            {
                int a = 0;
                map.grid[c.x, c.y] = '#';
                List<Coords> poss = D(map, ref a);
                if (a == r)
                {
                    //map.Display(poss);
                    foreach (Coords x in poss)
                    {
                        Console.WriteLine("->");
                        explore(x, map, ref seats, r, start);
                        Console.WriteLine("<-");
                        //Console.WriteLine(x.x + " " + x.y);
                        if (!seats.Contains(x))
                        {
                            seats.Add(x);
                        }
                    }
                }
                map.grid[c.x, c.y] = '.';
            }
        }*/

    }
}
