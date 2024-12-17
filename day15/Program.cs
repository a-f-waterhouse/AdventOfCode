using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Configuration;
using System.Text.RegularExpressions;
using System.ComponentModel.Design.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace day15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 50;
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
            //Grid newGrid = new Grid(input, size * 2, size);

            Coords robot = grid.CoordsOf('@');
            Console.WriteLine("start: ");
            grid.Display();
            foreach (char c in instructions)
            {
                Console.WriteLine("Instruction: " + c);
                char[,] stuff = grid.ReturnGRID();
                Grid newGrid = new Grid(stuff, size*2, size);
                if (RobotMove(c, robot, newGrid))
                {                    
                    grid.grid = newGrid.ReturnGRID();
                    robot = grid.CoordsOf('@');
                    //grid.Display();
                }
                else
                {
                    Console.WriteLine("no");
                    //newGrid.Display();
                }
                

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

        static bool RobotMove (char dir, Coords robot, Grid grid)
        {
            Console.WriteLine("RobotMove: " + robot.x + " " + robot.y);
            char next = ' ';
            grid.grid[robot.x, robot.y] = '.';
            bool r = false;
            switch (dir)
            {
                case '^':
                    next = grid.grid[robot.x, robot.y - 1];
                    if (next == ']')
                    {                        
                        robot.y--;                        
                        robot.x--;
                        r = BoxMove(dir, robot, grid, 0);
                        robot.x++;
                        grid.grid[robot.x, robot.y] = '@';
                    }
                    else if(next == '[')
                    {                        
                        robot.y--;                        
                        r =  BoxMove(dir, robot, grid, 0);
                        grid.grid[robot.x, robot.y] = '@';
                    }
                    else if(next == '.')
                    {
                        robot.y--;
                        grid.grid[robot.x, robot.y] = '@';
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case 'v':
                    next = grid.grid[robot.x, robot.y + 1];
                    if (next == ']')
                    {
                        robot.y++;
                        robot.x--;
                        r =  BoxMove(dir, robot, grid, 0);
                        robot.x++;
                        grid.grid[robot.x, robot.y] = '@';
                    }
                    else if (next == '[')
                    {
                        robot.y++;
                        r =  BoxMove(dir, robot, grid, 0);
                        grid.grid[robot.x, robot.y] = '@';
                    }
                    else if (next == '.')
                    {
                        robot.y++;
                        grid.grid[robot.x, robot.y] = '@';
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case '>':
                    next = grid.grid[robot.x + 1, robot.y];
                    switch (next)
                    {
                        case '[':
                            robot.x++;
                            grid.grid[robot.x, robot.y] = '@';
                            return BoxMove(dir, robot, grid, 0);

                        case '.':
                            robot.x++;
                            grid.grid[robot.x, robot.y] = '@';
                            return true;

                        case '#':
                            return false;
                    }
                    break;

                case '<':
                    next = grid.grid[robot.x - 1, robot.y];
                    switch (next)
                    {
                        case ']':
                            grid.grid[robot.x-1, robot.y] = '@';
                            robot.x-=2;                                                     
                            return BoxMove(dir, robot, grid, 0);

                        case '.':
                            robot.x--;
                            grid.grid[robot.x, robot.y] = '@';
                            return true;

                        case '#':
                            return false;
                    }
                    break;



            }
            return r;
        }

        static bool BoxMove(char dir, Coords box, Grid grid, int prev)
        {
            Console.WriteLine("BoxMove: " + box.x + " " + box.y + " " + prev);            

            switch (dir)
            {
                case '^':
                    string above = "";
                    for (int i = 0; i < 4; i++)
                    {
                        above += grid.grid[box.x -1 + i, box.y-1];
                    }
                    grid.grid[box.x, box.y-1] = '[';
                    grid.grid[box.x+1, box.y-1] = ']';
                    if (prev == 2)
                    {
                        grid.grid[box.x, box.y] = '.';
                        grid.grid[box.x+3, box.y] = '.';
                    }
                    else if(prev == 0)
                    {
                        grid.grid[box.x, box.y] = '.';
                        grid.grid[box.x+1, box.y] = '.';
                    }
                    if (prev == 3)
                    {
                        grid.grid[box.x, box.y] = '.';
                        grid.grid[box.x + 1, box.y] = '[';
                    }
                    else if (prev == 4)
                    {
                        grid.grid[box.x, box.y] = ']';
                        grid.grid[box.x + 1, box.y] = '.';
                    }
                    if (Regex.Match(above, ".\\[\\].").Success)
                    {          
                        box.y -= 1;
                        return BoxMove(dir, box, grid, 1);
                    }
                    else if(above == "[][]")
                    {
                        box.y--;
                        box.x -= 1;
                        Coords temp = new Coords();
                        temp.x = box.x + 2;
                        temp.y = box.y;
                        if(BoxMove(dir, box, grid, 2))
                        {                            
                            return BoxMove(dir, temp, grid, 1);
                        }                        
                        return false ;
                    }
                    else if (Regex.Match(above, ".\\.\\..").Success)
                    {
                        return true;
                    }
                    else if (Regex.Match(above, "\\[\\]\\..").Success)
                    {
                        box.y -= 1;
                        box.x -= 1;
                        return BoxMove(dir, box, grid, 3);
                    }
                    else if (Regex.Match(above, ".\\.\\[\\]").Success)
                    {
                        box.y -= 1;
                        box.x += 1;
                        return BoxMove(dir, box, grid, 4);
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case 'v':
                    string below = "";
                    for (int i = 0; i < 4; i++)
                    {
                        below += grid.grid[box.x - 1 + i, box.y + 1];
                    }
                    grid.grid[box.x, box.y + 1] = '[';
                    grid.grid[box.x + 1, box.y + 1] = ']';
                    if (prev == 2)
                    {
                        grid.grid[box.x, box.y] = '.';
                        grid.grid[box.x + 3, box.y] = '.';
                    }
                    else if (prev == 0)
                    {
                        grid.grid[box.x, box.y] = '.';
                        grid.grid[box.x + 1, box.y] = '.';
                    }
                    else if (prev == 3)
                    {
                        grid.grid[box.x, box.y] = '.';
                        grid.grid[box.x + 1, box.y] = '[';
                    }
                    else if (prev == 4)
                    {
                        grid.grid[box.x, box.y] = ']';
                        grid.grid[box.x + 1, box.y] = '.';
                    }
                    if (Regex.Match(below, ".\\[\\].").Success)
                    {
                        box.y += 1;
                        return BoxMove(dir, box, grid, 1);
                    }
                    else if (below == "[][]")
                    {
                        box.y++;
                        box.x -= 1;
                        Coords temp = new Coords();
                        temp.x = box.x + 2;
                        temp.y = box.y;
                        if (BoxMove(dir, box, grid, 2))
                        {
                            return BoxMove(dir, temp, grid, 1);
                        }
                        return false;
                    }
                    else if (Regex.Match(below, ".\\.\\..").Success)
                    {
                        return true;
                    }
                    else if (Regex.Match(below, "\\[\\]\\..").Success)
                    {
                        box.y += 1;
                        box.x -= 1;
                        return BoxMove(dir, box, grid, 3);
                    }
                    else if (Regex.Match(below, ".\\.\\[\\]").Success)
                    {
                        box.y += 1;
                        box.x += 1;
                        return BoxMove(dir, box, grid, 4);
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case '>':
                    char next = grid.grid[box.x + 2, box.y];
                    switch (next)
                    {
                        case '[':
                            if(prev == 0)
                            {
                                grid.grid[box.x, box.y] = '@';
                            }
                            grid.grid[box.x+1, box.y] = '[';
                            grid.grid[box.x+2, box.y] = ']';
                            box.x+=2;                            
                            return BoxMove(dir, box, grid, 1);

                        case '.':                            
                            if(prev == 0)
                            {
                                grid.grid[box.x, box.y] = '@';
                            }
                            grid.grid[box.x + 1, box.y] = '[';
                            grid.grid[box.x + 2, box.y] = ']';
                            return true;

                        case '#':
                            return false;
                    }
                    break;

                case '<':
                    char next2 = grid.grid[box.x - 1, box.y];
                    switch (next2)
                    {
                        case ']':
                            if (prev == 0)
                            {
                                grid.grid[box.x+1, box.y] = '@';
                            }
                            grid.grid[box.x - 1, box.y] = '[';
                            grid.grid[box.x, box.y] = ']';
                            box.x -=2;
                            return BoxMove(dir, box, grid, 1);

                        case '[':
                            grid.grid[box.x - 1, box.y] = '[';
                            grid.grid[box.x, box.y] = ']';
                            box.x -= 1;
                            return BoxMove(dir, box, grid, 1);

                        case '.':
                            if (prev == 0)
                            {
                                grid.grid[box.x+1, box.y] = '@';
                            }
                            grid.grid[box.x - 1, box.y] = '[';
                            grid.grid[box.x, box.y] = ']';
                            return true;

                        case '#':
                            return false;
                    }
                    break;
            }
            return false;
        }


    }



    
}
