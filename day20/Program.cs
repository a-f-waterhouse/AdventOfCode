using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace day20
{
    internal class Program
    {
        static List<Coords> D(Grid map, ref int result, Coords start, Coords end)
        {
            //Coords start = map.CoordsOf('S');
            //Coords end = map.CoordsOf('E');
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
            //dirs[start] = 'E';
            //prevs.Add(end, dummy);
            //dirs.Add(end, '.');
            //dists.Add(end, 1000000000);

            while (queue.Count > 0)
            {
                Coords current = queue.Dequeue();
                Coords test = current;
                //Console.WriteLine(queue.Count);

                test.x--;
                if (map.withinBounds(test) && map.Element(test) != '#') //
                {
                    int newDist = dists[current] + 1;

                    if (newDist < dists[test])
                    {
                        dists[test] = newDist;
                        prevs[test] = current;
                        dirs[test] = 'W';
                        queue.Enqueue(test);
                    }
                }
                test.x += 2;
                if (map.withinBounds(test) && map.Element(test) != '#')
                {
                    int newDist = dists[current] + 1;
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
                if (map.withinBounds(test) && map.Element(test) != '#')
                {
                    int newDist = dists[current] + 1;
                    if (newDist < dists[test])
                    {
                        dists[test] = newDist;
                        prevs[test] = current;
                        dirs[test] = 'S';
                        queue.Enqueue(test);
                    }
                }
                test.y -= 2;
                if (map.withinBounds(test) && map.Element(test) != '#')
                {
                    int newDist = dists[current] + 1;

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

            //Console.WriteLine(dists[end]);
            result = dists[end];
            if (cur.x == -1)
            {
                return new List<Coords>();
            }

            return path;
        }
        static void Main(string[] args)
        {
            Grid grid = new Grid(File.ReadAllLines("input.txt"));
            int shortestPath = 0;
            Coords start = grid.CoordsOf('S');
            Coords end = grid.CoordsOf('E');            
            grid.grid[start.x, start.y] = '.';
            grid.grid[end.x, end.y] = '.';
            D(grid, ref shortestPath, start, end);
            List<Coords> dots = grid.CoordsOf('.', true);            

            int newShortestPath = shortestPath - 100;

            int count = 0;
            int i = 0;
            Dictionary<Coords, int> cache = new Dictionary<Coords, int>();

            foreach(Coords c in dots)
            {
                int a = 0;
                D(grid, ref a, start, c);
                foreach (Coords c2 in dots)
                {
                    int dx = c2.x - c.x;
                    int dy = c2.y - c.y;
                    if((Math.Abs(dx) + Math.Abs(dy)) <= 20 && (Math.Abs(dx) + Math.Abs(dy)) > 0)
                    {
                        int b = 0;
                        if(!cache.Keys.Contains(c2))
                        {
                            D(grid, ref b, c2, end);
                            cache.Add(c2, b);
                        }
                        else
                        {
                            b = cache[c2];
                        }                       

                        int distance = a + b + Math.Abs(dx) + Math.Abs(dy);
                        if(distance <= newShortestPath)
                        {
                            count++;
                            Console.WriteLine(a +  " " + b);
                            Console.WriteLine(c.x + " " + c.y + "  " + c2.x + " " + c2.y + "  " + distance + "  " + (Math.Abs(dx) + Math.Abs(dy)));
                        }                        
                    }
                }
            }

            //PART ONE
            /*foreach(Coords c in barriers)
            {
                Console.WriteLine(i + " " + c.x + " " + c.y);
                int newPath = 0;
                grid.grid[c.x, c.y] = '.';
                D(grid, ref newPath);
                grid.grid[c.x, c.y] = '#';
                if(newPath <= newShortestPath)
                {
                    Console.WriteLine(c.x + " " + c.y + newPath);
                    count++;
                }
                i++;

            }*/
            Console.WriteLine(count);

            Console.ReadKey();
        }
    }
}
