using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Schema;

namespace day13
{
    internal class Program
    {
        struct Arcade
        {
            public long aX;
            public long aY;
            public long bX,bY,goalX,goalY;

        }


        static void Main(string[] args)
        {
            ulong total = 0;
            string[] lines = File.ReadAllLines("input.txt");
            for (int i = 0; i < lines.Length; i+=4)
            {
                Arcade arc = new Arcade();
                arc.aX = long.Parse(lines[i].Substring(12,2)) ;
                arc.aY = long.Parse(lines[i].Substring(18, 2));

                arc.bX = long.Parse(lines[i+1].Substring(12, 2));
                arc.bY = long.Parse(lines[i+1].Substring(18, 2));

                string[] goal = lines[i + 2].Split(' ');

                arc.goalX = long.Parse(goal[1].Substring(2, goal[1].Length - 3)) + 10000000000000;
                arc.goalY = long.Parse(goal[2].Substring(2, goal[2].Length - 2)) + 10000000000000;

                Console.WriteLine(arc.aX + " " + arc.aY + " " + arc.bX + " " + arc.bY + " " + arc.goalX + " " + arc.goalY);

                long alpha = (-arc.goalX * arc.bY + arc.goalY*arc.bX)/(arc.bX*arc.aY - arc.bY*arc.aX);
                long beta = (arc.goalX * arc.aY - arc.goalY*arc.aX)/(arc.bX * arc.aY - arc.bY*arc.aX); 

                Console.WriteLine(alpha + " " + beta);
                Console.WriteLine((-arc.goalX * arc.bY + arc.goalY * arc.bX) % (arc.bX * arc.aY - arc.bY * arc.aX));
                if (alpha >= 0 && beta >= 0 && (-arc.goalX * arc.bY + arc.goalY * arc.bX) % (arc.bX * arc.aY - arc.bY * arc.aX) == 0 && (arc.goalX * arc.aY - arc.goalY * arc.aX) % (arc.bX * arc.aY - arc.bY * arc.aX)  == 0)
                {                     
                    total += (ulong)(alpha * 3 + beta);
                    
                    Console.WriteLine(total);
                }
                else
                {
                    
                }


            }
            Console.WriteLine(total + " ");

            Console.ReadKey();
        }
    }
}
