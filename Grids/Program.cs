using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grids
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Grid g = new Grid(File.ReadAllLines("input.txt"));
            g.Display();
            Console.ReadKey();
        }
    }
}
