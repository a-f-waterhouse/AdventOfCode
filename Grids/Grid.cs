using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grids
{
    public struct Coords
    {
        public int x;
        public int y;
    }

    public class Grid
    {
        int size;
        public char[,] grid;

        public Grid(string[] filesLines) //square
        {
            
            size = filesLines.Length;
            grid = new char[size, size];
            for (int y = 0; y < size; y++)
            {
                for(int x = 0; x < size; x++)
                {
                    
                    grid[x,y] = filesLines[y][x];
                }
            }
        }

        public void Display()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(grid[j,i]);
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
                for(int j = 0; j < size; j++)
                {
                    if (grid[i,j] == c)
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
