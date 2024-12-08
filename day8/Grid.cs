using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day8
{
    public struct Coords
    {
        public int x;
        public int y;
    }

    public class Grid
    {
        public int size;
        public char[,] grid;

        public Grid(string[] filesLines) //square
        {

            size = filesLines.Length;
            grid = new char[size, size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {

                    grid[x, y] = filesLines[y][x];
                }
            }
        }


        public bool withinBounds(Coords c)
        {
            int x = c.x;
            int y = c.y;
            if(x >= 0 && x < size && y >= 0 && y < size)
            {
                return true;
            }
            return false;
        }

        public void Display()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(grid[j, i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void Display(List<Coords> highlight)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    foreach (Coords c in highlight)
                    {
                        if(c.x == j && c.y == i)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        }
                    }
                    Console.Write(grid[j, i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }



        public Coords CoordsOf(char c) //first instance
        {
            Coords coords = new Coords();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (grid[i, j] == c)
                    {
                        coords.x = i;
                        coords.y = j;
                        return coords;
                    }
                }
            }
            coords.x = -1;
            coords.y = -1;
            return coords;
        }

        public List<Coords> CoordsOf(char c, bool x)
        {
            List<Coords> list = new List<Coords>();
            Coords coords = new Coords();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (grid[i, j] == c)
                    {
                        coords.x = i;
                        coords.y = j;
                        list.Add(coords);
                    }
                }
            }
            coords.x = -1;
            coords.y = -1;
            return list;
        }

    }
}
