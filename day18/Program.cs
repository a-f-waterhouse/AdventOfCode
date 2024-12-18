using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day18
{
    internal class Program
    {
        static List<Coords> D(Grid map, ref int result)
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
            dirs[start] = 'E';
            prevs.Add(end, dummy);
            dirs.Add(end, '.');
            dists.Add(end, 1000000000);

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

            Console.WriteLine(dists[end]);
            result = dists[end];
            if (cur.x == -1)
            {
                return new List<Coords>();
            }

            return path;
        }
        static void Main(string[] args)
        {
            int SIZE = 71;
            int COUNT = 1024;



            List<Coords> falling = new List<Coords>();
            Grid g = new Grid(SIZE);
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    Coords c = new Coords();
                    string[] line = sr.ReadLine().Split(',');
                    c.x = int.Parse(line[0]);
                    c.y = int.Parse(line[1]);
                    falling.Add(c);
                }
            }

            g.grid[0, 0] = 'S';
            g.grid[SIZE-1, SIZE-1] = 'E';
            
            int i = 0;
            for (i = 0; i <COUNT; i++)
            {
                g.grid[falling[i].x, falling[i].y] = '#';
                //g.Display();
            }
            g.Display();
            i = COUNT;
            int r = 0;
            while (i < falling.Count && r < 1000000000)
            {
                g.grid[falling[i].x, falling[i].y] = '#';
                D(g, ref r);
                i++;
                Console.WriteLine(i);
                //Console.ReadKey();
            }
            
            
            Console.ReadKey();

        }
    }
}
