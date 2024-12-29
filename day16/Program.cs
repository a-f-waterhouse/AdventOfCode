using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Data;

namespace day16

    ///WORKING D
{
    internal class Program
    {
        static void D(Grid map)
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
                    if (newDist < dists[test])
                    {
                        prevs[test].Clear();
                    }
                    if (newDist <= dists[test])
                    {
                        dists[test] = newDist;
                        prevs[test].Add(current);
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
                        prevs[test].Add(current);
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
                        prevs[test].Add(current);
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
                        prevs[test].Add(current);
                        dirs[test] = 'N';
                        queue.Enqueue(test);
                    }
                }
            }

            List<Coords> onPath = new List<Coords>();
            backtrack(end, prevs, ref onPath);
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
            Grid map = new Grid(File.ReadAllLines("input.txt"));
            map.Display();
            Coords start = map.CoordsOf('S');
            Coords end = map.CoordsOf('E');
            Console.WriteLine(FindShortestPath(start, end, map));

            Console.ReadLine();
        }

        static int FindShortestPath(Coords start, Coords end, Grid map)
        {
            Queue<Coords> queue = new Queue<Coords>();
            Queue<char> dirs = new Queue<char>();
            Dictionary<Coords, List<Coords>> prevs = new Dictionary<Coords, List<Coords>>();
            Dictionary<Coords, int> distances = new Dictionary<Coords, int>();

            List <Coords> visited = new List<Coords> ();
            List<int> paths = new List<int> ();
            List<char> directions = new List<char>(); 

            dirs.Enqueue('E');
            queue.Enqueue(start);
            foreach (Coords c in map.CoordsOf('.', true))
            {
                prevs.Add(c, new List<Coords>());
            }
            prevs.Add(end, new List<Coords>());

            bool finish = false;
            while(queue.Count > 0 || !finish)
            {
                Coords c = queue.Dequeue();
                char dir = dirs.Dequeue();
                if (c.x == end.x && c.y == end.y)
                {
                    Console.WriteLine(c.dist);
                    directions.Add(dir);
                    paths.Add(c.dist);           
                    if(c.dist > 7036)
                    {
                        finish = true;              
                    }
                }
                else
                {                    
                    bool ADD = true;
                    foreach(Coords search in visited)
                    {                        
                        if (c.x == search.x && c.y == search.y)
                        {
                            ADD = false;
                            if(c.dist <= search.dist)
                            {
                                ADD = true;                                
                            }
                        }
                    }    
                    if(ADD || c.dist == 0) //IF C IS NOT IN VISITED OR DISTANCE IS LOWER THAN IN VISITED
                    {
                        //Console.WriteLine(c.x + " " + c.y + " " + c.dist + " " + dir);
                        Coords original = c;
                        visited.Add(c);
                        distances[c] += 1;
                        c.x--;
                        if (map.Element(c) != '#') //W
                        {
                            
                            if (dir == 'N' || dir == 'S')
                            {
                                distances[c] += 1000;
                                queue.Enqueue(c);
                                c.dist -= 1000;
                            }
                            else
                            {
                                queue.Enqueue(c);
                            }
                            prevs[c].Add(original);
                            dirs.Enqueue('W');
                        }
                        c.x += 2;
                        if (map.Element(c) != '#' && !visited.Contains(c)) //E
                        {
                            
                            if (dir == 'N' || dir == 'S')
                            {
                                c.dist += 1000;
                                queue.Enqueue(c);
                                c.dist -= 1000;
                            }
                            else
                            {
                                queue.Enqueue(c);
                            }
                            prevs[c].Add(original);
                            dirs.Enqueue('E');
                        }
                        c.y--;
                        c.x--;
                        if (map.Element(c) != '#') //N
                        {                            
                            if (dir == 'W' || dir == 'E')
                            {
                                c.dist += 1000;
                                queue.Enqueue(c);
                             
                                c.dist -= 1000;
                            }
                            else
                            {
                                queue.Enqueue(c);
                          
                            }
                            prevs[c].Add(original);
                            dirs.Enqueue('N');
                        }
                        c.y += 2;
                        if (map.Element(c) != '#' && !visited.Contains(c)) //S
                        {
                            
                            if (dir == 'W' || dir == 'E')
                            {
                                c.dist += 1000;
                                queue.Enqueue(c);
                 
                                c.dist -= 1000;
                            }
                            else
                            {
                                queue.Enqueue(c);
                     
                            }
                            prevs[c].Add(original);
                            dirs.Enqueue('S');
                        }
                    }
                    

                }

            }
            return paths.Min();
        }


        static int FindPath(Coords start, Coords end, Grid map, ref char Idir, int goal)
        {
            Queue<Coords> points = new Queue<Coords>();

            return 0;
        }
    }
}
