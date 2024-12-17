using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Data;

namespace day16
{
    internal class Program
    {
        static void D(Grid g, Coords start, Coords end)
        {
            List<Coords> nodes = new List<Coords>();
            nodes = g.CoordsOf('.', true);
            Dictionary<Coords, int> distances = new Dictionary<Coords, int>();
            Dictionary<Coords, Coords> prevs = new Dictionary<Coords, Coords>();
            Dictionary<Coords, char> dirs = new Dictionary<Coords, char>();
            List<Coords> Q = new List<Coords>();

            foreach(Coords c in nodes)
            {
                distances.Add(c, 1000000);
                Q.Add(c);
                prevs.Add(c, c);
                dirs.Add(c, ' ');
            }
            Q.Add(start);
            Q.Add(end);
            distances.Add(end, 1000000);
            dirs.Add(end, ' ');
            dirs.Add(start, 'E');
            distances[start] = 0;

            while (Q.Count != 0)
            {                

                Coords u = new Coords();
                int min = 100000;
                foreach(Coords c in Q)
                {                    
                    if(distances[c] < min)
                    {
                        min = distances[c];
                        u = c;
                    }
                }
                Console.WriteLine(Q.Count);
                Console.WriteLine(u.x + " " + u.y  + " " + distances[u]);
                Q.Remove(u);

                Coords v = new Coords();
                v.x = u.x - 1;
                v.y = u.y;
                if(Q.Contains(v)) //west
                {
                    int newDist = distances[u]+1;
                    if (dirs[u] == 'S' || dirs[u] == 'N')
                    {
                        newDist += 1000;
                    }                    
                    distances[v] = newDist;
                    prevs[v] = u;
                    dirs[v] = 'W';
                }
                v.x = u.x + 1;
                v.y = u.y;
                if (Q.Contains(v))
                {
                    int newDist = distances[u] + 1;
                    if (dirs[u] == 'S' || dirs[u] == 'N')
                    {
                        newDist += 1000;
                    }
                    distances[v] = newDist;
                    prevs[v] = u;
                    dirs[v] = 'E';
                }

                v.x = u.x;
                v.y = u.y +1;
                if (Q.Contains(v))
                {
                    int newDist = distances[u] + 1;
                    if (dirs[u] == 'E' || dirs[u] == 'W')
                    {
                        newDist += 1000;
                    }
                    distances[v] = newDist;
                    prevs[v] = u;
                    dirs[v] = 'S';
                }

                v.x = u.x;
                v.y = u.y - 1;
                if (Q.Contains(v))
                {
                    int newDist = distances[u] + 1;
                    if (dirs[u] == 'W' || dirs[u] == 'E')
                    {
                        newDist += 1000;
                    }
                    distances[v] = newDist;
                    prevs[v] = u;
                    dirs[v] = 'N';
                }                

            }
            Coords test = new Coords();
            test.x = 1;
            test.y = 11;
            Console.WriteLine(distances[end]);
            Console.ReadKey();

            Coords c2 = new Coords();
            c2.x = end.x;
            c2.y = end.y;
            int count = 0;

            while(c2.x != start.x || c2.y!= start.y)
            {
                Console.WriteLine(c2.x + " " + c2.y);
                c2 = prevs[c2];
                count++;
            }
            Console.WriteLine(count);



        }

        


        static void Main(string[] args)
        {
            Grid map = new Grid(File.ReadAllLines("input.txt"));
            map.Display();
            Coords start = map.CoordsOf('S');
            Coords end = map.CoordsOf('E');
            int total = 0;
            char dir = 'E';
            //D(map, start, end);
            
            
            Console.WriteLine(FindShortestPath(start, end, map, ref dir));
            Console.WriteLine("----------------------------------------------");
            /*Coords c = new Coords();
            c.x = 1;
            c.y = 9;*/

            dir = 'E';
            /*int a = FindShortestPath(start, c, map, ref dir);
            Console.ReadKey();
            int b = FindShortestPath(c, end, map, ref dir);
            Console.WriteLine("a: " + a + " b: " + b);*/

            List<Coords> spaces = new List<Coords>();
            //spaces = map.CoordsOf('.', true);
            
            /*int goal = FindShortestPath(start, end, map, ref dir);
            Console.WriteLine("Goal: " + goal);
            foreach(Coords c in spaces)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Testing: " + c.x + " " + c.y);
                dir = 'E';
                int a = FindShortestPath(start, c, map, ref dir);
                Console.WriteLine("A: " + a);
                Console.WriteLine("Dir: " + dir);
                if (a < goal)
                {
                    //dir = 'E';
                    int b = FindShortestPath(c, end, map, ref dir);
                    Console.WriteLine("B: " + b);
                    if(a+b == goal)
                    {
                        total++;
                        //Console.ForegroundColor = ConsoleColor.Green;
                        //Console.WriteLine("Success");
                    }
                    else
                    {
                        //Console.ForegroundColor = ConsoleColor.Red;
                        //Console.WriteLine("Fail");
                    }
                }
                else
                {
                    //Console.ForegroundColor = ConsoleColor.Red;
                    //Console.WriteLine("Fail");
                }
                

            }*/
            
            Console.WriteLine("yippeeeee");
            Console.WriteLine(total + 2);
            Console.ReadLine();
        }

        static int FindShortestPath(Coords start, Coords end, Grid map, ref char Idir)
        {
            Queue<Coords> queue = new Queue<Coords>();
            Queue<char> dirs = new Queue<char>();
            Queue<Coords> prevs = new Queue<Coords>();

            List<Coords> visited = new List<Coords> ();
            List<int> paths = new List<int> ();
            List<char> directions = new List<char>(); 

            dirs.Enqueue(Idir);
            //visited.Add(start);
            queue.Enqueue(start);
            while(queue.Count > 0)
            {
                Coords p = prevs.Dequeue();
                Coords c = queue.Dequeue();
                char dir = dirs.Dequeue();
                if (c.x == end.x && c.y == end.y)
                {
                    Console.WriteLine(c.dist);
                    directions.Add(dir);
                    paths.Add(c.dist);           
                    if(c.dist == 7036)
                    {
                        Console.WriteLine(visited.Count);                        
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
                        c.dist += 1;
                        c.x--;
                        if (map.Element(c) != '#') //W
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
                            prevs.Enqueue(original);
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
                            prevs.Enqueue(original);
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
                            prevs.Enqueue(original);
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
                            prevs.Enqueue(original);
                            dirs.Enqueue('S');
                        }
                    }
                    

                }

            }
            Idir = directions[(paths.IndexOf(paths.Min()))];
            return paths.Min();
        }


        static int FindPath(Coords start, Coords end, Grid map, ref char Idir, int goal)
        {
            Queue<Coords> points = new Queue<Coords>();

            return 0;
        }
    }
}
