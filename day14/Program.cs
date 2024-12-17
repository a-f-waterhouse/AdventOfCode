using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.ComponentModel;

namespace day14
{
    internal class Program
    {
        struct Robot
        {
            public int pX, pY, vX, vY;
        
        }

        static void Move(ref Robot robot, int maxX, int maxY)
        {
            
            robot.pX += robot.vX;
            robot.pY+= robot.vY;

            if (robot.pX < 0)
            {
                robot.pX += maxX;
            }
            if (robot.pY < 0)
            {
                robot.pY += maxY;
            }

            robot.pX %= maxX;
            robot.pY %= maxY;

        }

        static bool Check(Robot[] robots)
        {
            for (int j = 0; j < robots.Length; j++)
            {
                for (int k = 0; k < robots.Length; k++)
                {
                    if (robots[j].pY == robots[k].pY && robots[j].pX == robots[k].pX && j != k)
                    {
                        return false;
                    }

                }
            }
            return true;
        }

        static void Display(Robot[] robots)
        {
            Console.Clear();
            foreach(Robot r in robots)
            {
                Console.CursorLeft = r.pX;
                Console.CursorTop = r.pY;
                Console.Write("x");
            }
        }

        static void Main(string[] args)
        {
            Console.BufferHeight = 120;
            Console.BufferWidth = 120;
            int maxX = 101, maxY = 103;
            int[] quads = new int[4]; //tL, tR, bL, bR

            string[] input = File.ReadAllLines("input.txt");
            Robot[] robots = new Robot[input.Length];
            int x = 0;
            foreach (string line in input)
            {
                Robot r = new Robot();
                string[] l = line.Split(' ');
                r.pX = int.Parse(l[0].Split(',')[0].Substring(2));
                r.pY = int.Parse(l[0].Split(',')[1]);

                r.vX = int.Parse(l[1].Split(',')[0].Substring(2));
                r.vY = int.Parse(l[1].Split(',')[1]);
                robots[x] = r;
                x++;
            }

            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < robots.Length; j++)
                {                    
                    Move(ref robots[j], maxX, maxY);                    
                }
                if(Check(robots))
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = 0;
                    Console.WriteLine(i);
                    Display(robots);
                    Console.ReadKey();
                }

              
                             
            }
            for (int j = 0; j < robots.Length; j++)
            {
                Robot r = robots[j];

                
                if (r.pX != maxX/2 && r.pY != maxY/2)
                {
                    Console.WriteLine(robots[j].pX + " " + robots[j].pY);
                    if (r.pX > maxX / 2 ) //right
                    {
                        if(r.pY > maxY/2) //bottom
                        {
                            quads[3]++;
                        }
                        else //top
                        {
                            quads[1]++;
                        }
                    }
                    else //left
                    {
                        if (r.pY > maxY / 2 ) //bottom
                        {
                            quads[2]++;
                        }
                        else //top
                        {
                            quads[0]++;
                        }
                    }
                    
                }
            }

            int total = 1;
            foreach(int i in quads)
            {
                total *= i;
            }
            Console.WriteLine(total);

            Console.ReadKey();
        }
    }
}
