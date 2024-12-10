using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Security.Cryptography;

namespace day10
{
    internal class Program
    {

        static int total = 0;
        static List<Coords> visited = new List<Coords>();
        static bool ListContains(List<Coords> l, Coords c)
        {
            foreach (Coords cc in l)
            {
                if (cc.x == c.x && cc.y == c.y)
                {
                    return true;
                }
            }
            return false;
        }
        static int nextSquare(Coords c, Grid map)
        {           
            string expected = (int.Parse(map.Element(c).ToString()) + 1).ToString();
            Console.WriteLine(c.x + " " + c.y + " " + total + " " + expected);
            //Console.ReadKey();
            if ( expected == "10") //&& !visited.Contains(c))
            {
                total += 1;
                visited.Add(c);
                //Console.WriteLine(total + "a");
                return 1;
            }
            Coords newC = new Coords();

            if (c.x > 0 && (map.grid[c.x-1,c.y].ToString() == expected))
            {
                newC.x = c.x-1;
                newC.y = c.y;
                nextSquare(newC, map);
            }
            if (c.y > 0 && (map.grid[c.x, c.y - 1].ToString() == expected))
            {
                newC.x = c.x;
                newC.y = c.y-1;
                nextSquare(newC, map);
            }
            if (c.x < map.size -1 && (map.grid[c.x + 1, c.y].ToString() == expected))
            {                
                newC.x = c.x+1;
                newC.y = c.y;
                nextSquare(newC, map);
            }
            if (c.y < map.size -1 && (map.grid[c.x, c.y + 1].ToString() == expected))
            {
                newC.x = c.x;
                newC.y = c.y+1;
                nextSquare(newC, map);
            }
            return 0;
        }


        static void Main(string[] args)
        {
            Grid map = new Grid(File.ReadAllLines("input.txt"));
            //int total = 0;
            List<Coords> coords = new List<Coords>();
            coords = map.CoordsOf('0', true);
            foreach (Coords c in coords)
            {
                nextSquare(c, map);
                Console.WriteLine(total);
                visited.Clear();
            }
            
            

            
            Console.ReadKey();
        }
    }
}
