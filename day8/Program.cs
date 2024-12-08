using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace day8
{
    internal class Program
    {
        static bool ListContains(List<Coords> l, Coords c)
        {
            foreach(Coords cc in l)
            {
                if(cc.x == c.x && cc.y == c.y)
                {
                    return true;
                }
            }
            return false;
        }

        static void PartA()
        {
            Grid map = new Grid(File.ReadAllLines("input.txt"));
            List<char> done = new List<char>();

            List<Coords> antinodes = new List<Coords>();

            for (int i = 0; i < map.size; i++)
            {
                for (int j = 0; j < map.size; j++)
                {
                    if (map.grid[j, i] != '.' && !done.Contains(map.grid[j, i]))
                    {
                        List<Coords> coords = new List<Coords>();
                        coords = map.CoordsOf(map.grid[j, i], true);
                        for (int k = 0; k < coords.Count; k++)
                        {
                            for (int l = k + 1; l < coords.Count; l++)
                            {
                                Coords dist = new Coords();
                                dist.x = coords[l].x - coords[k].x;
                                dist.y = coords[l].y - coords[k].y;

                                Coords antinode = new Coords();
                                antinode.x = coords[k].x - dist.x;
                                antinode.y = coords[k].y - dist.y;

                                if (!ListContains(antinodes, antinode) && map.withinBounds(antinode))
                                {
                                    antinodes.Add(antinode);
                                }

                                antinode.x = coords[l].x + dist.x;
                                antinode.y = coords[l].y + dist.y;

                                if (!ListContains(antinodes, antinode) && map.withinBounds(antinode))
                                {
                                    antinodes.Add(antinode);
                                }
                            }
                        }

                        done.Add(map.grid[j, i]);
                    }
                }
            }
        }

        static void PartB()
        {
            Grid map = new Grid(File.ReadAllLines("input.txt"));
            List<char> done = new List<char>();

            List<Coords> antinodes = new List<Coords>();

            for (int i = 0; i < map.size; i++)
            {
                for (int j = 0; j < map.size; j++)
                {
                    Coords ant = new Coords();
                    ant.x = j;
                    ant.y = i;
                    if (map.grid[j,i] != '.' && !ListContains(antinodes,ant))
                    {
                        antinodes.Add(ant);
                    }

                    if (map.grid[j, i] != '.' && !done.Contains(map.grid[j, i]))
                    {
                        List<Coords> coords = new List<Coords>();
                        coords = map.CoordsOf(map.grid[j, i], true);
                        for (int k = 0; k < coords.Count; k++)
                        {
                            for (int l = k + 1; l < coords.Count; l++)
                            {
                                Coords dist = new Coords();
                                dist.x = coords[l].x - coords[k].x;
                                dist.y = coords[l].y - coords[k].y;

                                Coords antinode = new Coords();
                                int count = 1;
                                
                                while(map.withinBounds(antinode))
                                {
                                    antinode.x = coords[k].x - count* dist.x;
                                    antinode.y = coords[k].y - count* dist.y;
                                    if (!ListContains(antinodes, antinode) && map.withinBounds(antinode))
                                    {
                                        antinodes.Add(antinode);
                                    }
                                    count++;
                                }
                                antinode.x = 0;
                                antinode.y = 0;
                                count = 1;
                                while (map.withinBounds(antinode))
                                {
                                    antinode.x = coords[l].x + count * dist.x;
                                    antinode.y = coords[l].y + count * dist.y;
                                    if (!ListContains(antinodes, antinode) && map.withinBounds(antinode))
                                    {
                                        antinodes.Add(antinode);
                                    }
                                    count++;
                                }
                            }
                        }

                        done.Add(map.grid[j, i]);
                    }
                }
            }
            map.Display(antinodes);
            Console.WriteLine(antinodes.Count);
        }


        static void Main(string[] args)
        {

            PartB();
            
            Console.ReadKey();

        }
    }
}
