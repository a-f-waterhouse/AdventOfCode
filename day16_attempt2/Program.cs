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
        static List<Coords> D(Grid map, ref int result, char startDir, ref char endDir)
        {
            Coords start = map.CoordsOf('S');
            Coords end = map.CoordsOf('E');
            List<Coords> points = new List<Coords>();
            Dictionary<Coords, Coords> prevs = new Dictionary<Coords, Coords>();
            Dictionary<Coords, char> dirs = new Dictionary<Coords, char>();
            Dictionary<Coords, int> dists = new Dictionary<Coords, int>();
            points = map.CoordsOf('.', true);
            Coords dummy = new Coords();
            dummy.x = -1;
            dummy.y = -1;
            for (int i = 0; i < points.Count; i++)
            {
                prevs.Add(points[i], dummy);
                dirs.Add(points[i], '.');
                dists.Add(points[i], 1000000000);
            }
            Queue<Coords> queue = new Queue<Coords>();
            queue.Enqueue(start);
            dists[start] = 0;
            dirs[start] = startDir;
            prevs.Add(end, dummy);
            dirs.Add(end, '.');
            dists.Add(end, 1000000000);

            while (queue.Count > 0)
            {
                Coords current = queue.Dequeue();
                Coords test = current;
                //Console.WriteLine(queue.Count);

                test.x--;
                if (map.Element(test) != '#') //
                {
                    int newDist = dists[current] + 1;
                    if (dirs[current] == 'N' || dirs[current] == 'S')
                    {
                        newDist += 1000;
                    }

                    if (newDist < dists[test])
                    {
                        dists[test] = newDist;
                        prevs[test] = current;
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
                        dists[test] = newDist;
                        prevs[test] = current;
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
                        dists[test] = newDist;
                        prevs[test] = current;
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
                        dists[test] = newDist;
                        prevs[test] = current;
                        dirs[test] = 'N';
                        queue.Enqueue(test);
                    }
                }
            }
            Coords cur = end;
            List<Coords> path = new List<Coords>();

            while ((cur.x != start.x || cur.y != start.y) && (cur.x != -1))
            {
                //Console.WriteLine(prevs[cur].x + " " + prevs[cur].y);
                cur = prevs[cur];
                path.Add(cur);
            }

            Console.WriteLine(dists[end]);
            result = dists[end];
            endDir = dirs[end];
            if (cur.x == -1)
            {
                return new List<Coords>();
            }

            return path;
        }


        static void Main(string[] args)
        {
            Grid map = new Grid(File.ReadAllLines("input.txt"));
            Coords start = map.CoordsOf('S');
            Coords end = map.CoordsOf('E');

            int goal = 0;
            char aaaa = ' ';
            D(map, ref goal, 'E', ref aaaa);
            Console.WriteLine("GOAL: " + goal);
            List<Coords> allPoints = new List<Coords>();
            allPoints = map.CoordsOf('.', true);
            int count = 0;

            foreach (Coords c in allPoints)
            {
                Console.WriteLine("Testing: " + c.x + " " + c.y);
                map.grid[c.x, c.y] = 'E';
                map.grid[start.x, start.y] = 'S';
                map.grid[end.x, end.y] = '.';
                int distToC = 0;
                char endDir = ' ';
                D(map, ref distToC, 'E', ref endDir);
                map.grid[c.x, c.y] = 'S';
                map.grid[end.x, end.y] = 'E';
                map.grid[start.x, start.y] = '.';
                int distFromC = 0;
                char dummy = ' ';
                D(map, ref distFromC, endDir, ref dummy);
                map.grid[c.x, c.y] = '.';
                Console.WriteLine(distToC + " " + distFromC);
                if(distToC + distFromC == goal)
                {
                    count++;
                }
            }
            Console.WriteLine(count);






           /* for (int i = 0; i < path.Count; i++)
            {
                seats.Add(path[i]);
            }
            foreach (Coords c in path)
            {
                if (!(c.x == start.x && c.y == start.y))
                {
                    //explore(c, map, ref seats, r, start);
                }
            }*/
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
