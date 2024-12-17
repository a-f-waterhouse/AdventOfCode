using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace day12
{
    internal class Program
    {
        static int perim = 0;

        static List<Coords> Traverse(Coords startPoint, Grid plots, List<Coords> visited, char dir)
        {
            //Console.WriteLine(startPoint.x + " " + startPoint.y + " " + dir + " " + visited.Contains(startPoint));
            if (!visited.Contains(startPoint))
            {                
                visited.Add(startPoint);
                Coords next = new Coords();
                next = startPoint;

                next.x--;
                if (plots.withinBounds(next) && plots.Element(startPoint) == plots.Element(next))
                {
                    char newDir = 'W';
                    if(newDir!= dir)// && !visited.Contains(next))
                    {
                        perim++;
                    }
                    visited = Traverse(next, plots, visited, newDir);
                }
                next.x++;
                next.y--;
                if (plots.withinBounds(next) && plots.Element(startPoint) == plots.Element(next))
                {
                    char newDir = 'N';
                    if (newDir != dir)// && !visited.Contains(next))
                    {
                        perim++;
                    }
                    visited = Traverse(next, plots, visited, newDir);
                }
                next.y++;
                next.x++;
                if (plots.withinBounds(next) && plots.Element(startPoint) == plots.Element(next))
                {
                    char newDir = 'E';
                    if (newDir != dir)// && !visited.Contains(next))
                    {
                        perim++;
                    }
                    visited = Traverse(next, plots, visited, newDir);
                }
                next.x--;
                next.y++;
                if (plots.withinBounds(next) && plots.Element(startPoint) == plots.Element(next))
                {
                    char newDir = 'S';
                    if (newDir != dir)// && !visited.Contains(next))
                    {
                        perim++;
                    }
                    visited = Traverse(next, plots, visited, newDir);
                }
                
            }
            return visited;
        }


        static void Main(string[] args)
        {
            Grid plots = new Grid(File.ReadAllLines("input.txt"));
            int total = 0;
            /*Coords c = new Coords();
            c.x = 0;
            c.y = 0;
            
            visited = (Traverse(c, plots, visited));*/


            List<Coords> visited = new List<Coords>();

            for (int y = 0; y < plots.size; y++)
            {
                for (int x = 0; x < plots.size; x++)
                {
                    Coords currentPlot = new Coords();
                    currentPlot.x = x;
                    currentPlot.y = y;
                    if(!visited.Contains(currentPlot))
                    {
                        List<Coords> garden = new List<Coords>();
                        garden = Traverse(currentPlot, plots, garden, 'N');
                        foreach(Coords c in garden)
                        {
                            visited.Add(c);
                        }
                        total += (garden.Count * Perimeter2(garden, plots));
                        //Console.WriteLine(total + " " + perim);
                        perim = 0;
                    }
                }
            }
            Console.WriteLine(total);
            Console.ReadKey();
        }

        static int Perimeter2(List<Coords> c, Grid plots)
        {
            int total = 0;
            foreach(Coords plot in c)
            {        
                //external corners
                for (int i = -1; i < 2; i+=2) // -1, 1 //-1 1  
                {
                    for (int j = -1; j < 2; j+=2)
                    {
                        Coords test1 = new Coords();
                        test1.x = plot.x + i;
                        test1.y = plot.y;

                        Coords test2 = new Coords();
                        test2.x = plot.x;
                        test2.y = plot.y +j ;

                        Coords test3 = new Coords();
                        test3.x = plot.x +i;
                        test3.y = plot.y + j;

                        //Console.WriteLine(plot.x + " " + plot.y + " " + test1.x + " " + test1.y + " " + test2.x + " " + test2.y + " " + test3.x + " " + test3.y);

                        if (!c.Contains(test1) && !c.Contains(test2))
                        {
                            total++;
                            //Console.WriteLine(total + "i");
                                                      
                        }
                        else if(c.Contains(test1) && c.Contains(test2) && !c.Contains(test3))
                        {
                            total++;
                        }
                    }
                }   
                //internal corners
                //0, 0 -> 
                

                
            }
            Console.WriteLine(total + "t");
            
            return total;
            
        }




        static int Perimeter(List<Coords>c, Grid plots)
        {
            int total = 0;

            foreach(Coords plot in c)
            {
                int perimeterCont = 4;
                Coords next = new Coords();
                next.x = plot.x-1;
                next.y = plot.y;
                if (plots.withinBounds(next) && plots.Element(plot) == plots.Element(next))
                {                    
                    perimeterCont--;
                }
                next.x++;
                next.y--;
                if (plots.withinBounds(next) && plots.Element(plot) == plots.Element(next))
                {
                    perimeterCont--;
                }
                next.y++;
                next.x++;
                if (plots.withinBounds(next) && plots.Element(plot) == plots.Element(next))
                {
                    perimeterCont--;
                }
                next.x--;
                next.y++;
                if (plots.withinBounds(next) && plots.Element(plot) == plots.Element(next))
                {
                    perimeterCont--;
                }
                total += perimeterCont;
            }
            Console.WriteLine(total);


            return total;
        }
    }
}
