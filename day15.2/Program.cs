using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace day15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 7;
            string[] input = new string[size];
            string instructions = "";
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                int count = 0;
                while (!sr.EndOfStream)
                {
                    if (count < size)
                    {
                        string line = sr.ReadLine();
                        string newLine = "";
                        foreach (char c in line)
                        {
                            switch (c)
                            {
                                case ('#'):
                                    newLine += "##";
                                    break;
                                case ('.'):
                                    newLine += "..";
                                    break;
                                case ('@'):
                                    newLine += "@.";
                                    break;
                                case ('O'):
                                    newLine += "[]";
                                    break;


                            }

                        }

                        input[count] = newLine;
                    }
                    else if (count > size)
                    {
                        instructions += sr.ReadLine();
                    }
                    count++;
                }
            }
            Grid grid = new Grid(input, size * 2, size);
            Grid newGrid = new Grid(input, size * 2, size);
            Coords robot = grid.CoordsOf('@');

            grid.Display();
            foreach (char c in instructions)
            {
                if (CheckValidMove(c, robot, grid))
                {
                    Move(c, ref robot, ref grid);
                }
                Console.WriteLine(c);

                grid.Display();
            }

            List<Coords> boxes = new List<Coords>();
            boxes = grid.CoordsOf('[', true);
            int total = 0;
            foreach (Coords c in boxes)
            {
                total += (100 * c.y + c.x);
            }
            Console.WriteLine(total);

            Console.ReadKey();
        }

        static bool CheckValidMove(char dir, Coords box, Grid g)
        {
            bool wall = false;
            int count = 0;

            switch (dir)
            {
                case '^':
                    for (int i = box.y - 1; i >= 0; i--)
                    {
                        if (g.grid[box.x, i] == '.' && g.grid[box.x + 1, i] == '.' && !wall)
                        {
                            return true;
                        }
                        else if (g.grid[box.x, i] == '#' || g.grid[box.x + 1, i] == '#')
                        {
                            wall = true;
                        }
                    }
                    break;
                case 'v':
                    for (int i = box.y + 1; i < g.size; i++)
                    {
                        if (g.grid[box.x, i] == '.' && g.grid[box.x + 1, i] == '.' && !wall)
                        {
                            return true;
                        }
                        else if (g.grid[box.x, i] == '#' || g.grid[box.x + 1, i] == '#')
                        {
                            wall = true;
                        }
                    }
                    break;
                case '<':
                    for (int i = box.x - 1; i >= 0; i--)
                    {
                        if (g.grid[i, box.y] == '.' && !wall)
                        {
                            return true;

                        }
                        else if (g.grid[i, box.y] == '#')
                        {
                            wall = true;
                        }
                    }
                    break;
                case '>':
                    for (int i = box.x + 1; i < g.size - 1; i++)
                    {
                        if (g.grid[i + 1, box.y] == '.' && !wall)
                        {
                            return true;
                        }
                        else if (g.grid[i + 1, box.y] == '#')
                        {
                            wall = true;
                        }
                    }
                    break;



            }
            return false;

        }

        static void PushBox(char dir, Coords box, ref Grid g, bool b)
        {
            //g.Display();
            if (!b)
            {
                g.grid[box.x, box.y] = '.';
                g.grid[box.x + 1, box.y] = '.';
            }
            switch (dir)
            {
                case '^':
                    if (g.grid[box.x, box.y - 1] == '[' && g.grid[box.x + 1, box.y - 1] == ']')
                    {
                        box.y--;
                        PushBox(dir, box, ref g, true);
                    }
                    else if (g.grid[box.x, box.y - 1] == ']' && g.grid[box.x + 1, box.y - 1] == '[')
                    {
                        Coords one = new Coords();
                        one.x = box.x - 1;
                        one.x = box.y - 1;
                        PushBox(dir, one, ref g, true);
                        one.x += 2;
                        PushBox(dir, one, ref g, true);

                    }
                    g.grid[box.x, box.y - 1] = '[';
                    g.grid[box.x + 1, box.y - 1] = ']';
                    break;

                case 'v':
                    if (g.grid[box.x, box.y + 1] == '[' && g.grid[box.x + 1, box.y + 1] == ']')
                    {
                        box.y++;
                        PushBox(dir, box, ref g, true);
                    }
                    else if (g.grid[box.x, box.y + 1] == ']' && g.grid[box.x + 1, box.y + 1] == '[')
                    {
                        Coords one = new Coords();
                        one.x = box.x - 1;
                        one.x = box.y + 1;
                        PushBox(dir, one, ref g, true);
                        one.x -= 2;
                        PushBox(dir, one, ref g, true);

                    }
                    g.grid[box.x, box.y + 1] = '[';
                    g.grid[box.x + 1, box.y + 1] = ']';
                    break;

                case '<':
                    if (g.grid[box.x - 1, box.y] == ']')
                    {
                        box.x -= 2;
                        PushBox(dir, box, ref g, true);
                        box.x += 2;
                    }
                    g.grid[box.x - 1, box.y] = '[';
                    g.grid[box.x, box.y] = ']';
                    break;
                case '>':
                    if (g.grid[box.x + 1, box.y] == '[')
                    {
                        box.x++;
                        PushBox(dir, box, ref g, true);
                    }
                    break;
            }

        }

        static bool PushBox(char dir, Coords box, Grid g, bool b)
        {

            bool r = false;


            if (!b)
            {
                g.grid[box.x, box.y] = '.';
                g.grid[box.x + 1, box.y] = '.';
            }
            switch (dir)
            {
                case '^':
                    if (g.grid[box.x, box.y - 1] == '[' && g.grid[box.x + 1, box.y - 1] == ']')
                    {
                        box.y--;
                        r = PushBox(dir, box, g, true);
                    }
                    else if (g.grid[box.x, box.y - 1] == ']' && g.grid[box.x + 1, box.y - 1] == '[')
                    {
                        Coords one = new Coords();
                        one.x = box.x - 1;
                        one.x = box.y - 1;
                        Coords two = new Coords();
                        one.x = box.x + 1;
                        one.x = box.y - 1;
                        r = (PushBox(dir, one, g, true) && PushBox(dir, two, g, true));
                    }
                    else if(g.grid[box.x, box.y - 1] == '#' || g.grid[box.x + 1, box.y - 1] == '#')
                    {
                        return false;
                    }
                    g.grid[box.x, box.y - 1] = '[';
                    g.grid[box.x + 1, box.y - 1] = ']';
                    break;

                case 'v':
                    if (g.grid[box.x, box.y + 1] == '[' && g.grid[box.x + 1, box.y + 1] == ']')
                    {
                        box.y++;
                        r = PushBox(dir, box, g, true);
                    }
                    else if (g.grid[box.x, box.y + 1] == ']' && g.grid[box.x + 1, box.y + 1] == '[')
                    {
                        Coords one = new Coords();
                        one.x = box.x - 1;
                        one.x = box.y + 1;
                        if (PushBox(dir, one, g, true))
                        {
                            r = true;
                        }
                        one.x -= 2;
                        if (!PushBox(dir, one, g, true))
                        {
                            r = false;
                        }

                    }
                    g.grid[box.x, box.y + 1] = '[';
                    g.grid[box.x + 1, box.y + 1] = ']';
                    break;

                case '<':
                    if (g.grid[box.x - 1, box.y] == ']')
                    {
                        box.x -= 2;
                        r = PushBox(dir, box, g, true);
                        box.x += 2;
                    }
                    g.grid[box.x - 1, box.y] = '[';
                    g.grid[box.x, box.y] = ']';
                    break;
                case '>':
                    if (g.grid[box.x + 1, box.y] == '[')
                    {
                        box.x++;
                        r = PushBox(dir, box, g, true);
                    }
                    break;
            }
            return r;


        }

        static void Move(char dir, ref Coords robot, ref Grid grid)
        {
            grid.grid[robot.x, robot.y] = '.';
            int y = robot.y;
            int x = robot.x;
            char[] edges = { '[', ']' };
            char c = ' ';

            switch (dir)
            {
                case '^':
                    robot.y--;
                    y = robot.y;  //new Rpos
                    c = grid.grid[x, y];
                    if (c == '[')
                    {
                        if (PushBox(dir, robot, grid, false))
                        {

                            PushBox(dir, robot, ref grid, false);
                        }
                        else
                        {

                        }
                    }
                    else if (c == ']')
                    {
                        grid.Display();
                        robot.x--;
                        char[,] stuff = new char[grid.sizeX, grid.sizeY];
                        for (int i = 0; i < grid.sizeY; i++)
                        {
                            for (int j = 0; j < grid.sizeX; j++)
                            {
                                stuff[j, i] = grid.grid[j, i];
                            }
                        }
                        Grid g = new Grid(stuff, grid.sizeX, grid.sizeY);

                        if (PushBox(dir, robot, g, false))
                        {
                            PushBox(dir, robot, ref grid, false);
                        }
                        else
                        {
                            robot.y++;
                            Console.WriteLine("a");
                            grid.Display();
                        }
                        robot.x++;
                    }


                    break;

                case 'v':

                    robot.y++;
                    y = robot.y;
                    c = grid.grid[x, y];
                    if (c == '[')
                    {
                        PushBox(dir, robot, ref grid, false);
                    }
                    else if (c == ']')
                    {
                        robot.x--;
                        PushBox(dir, robot, ref grid, false);
                        robot.x++;
                    }

                    break;

                case '<':

                    robot.x--;
                    x = robot.x;
                    c = grid.grid[x, y];
                    if (c == ']')
                    {
                        robot.x--;
                        PushBox(dir, robot, ref grid, false);
                        robot.x++;
                    }
                    break;

                case '>':

                    robot.x++;
                    x = robot.x;
                    c = grid.grid[x, y];
                    if (c == '[')
                    {
                        PushBox(dir, robot, ref grid, false);
                    }
                    break;

            }
            grid.grid[robot.x, robot.y] = '@';
        }
    }
}
