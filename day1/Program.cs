using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int total = 0;

            List<int> first = new List<int>();
            List<int> second = new List<int>();

            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(' ');
                    first.Add(int.Parse(line[0]));
                    second.Add(int.Parse(line[3]));
                }
            }

            while(first.Count > 0)
            {
                total += (Math.Abs(first.Min() - second.Min()));

                first.Remove(first.Min());
                second.Remove(second.Min());
            }
            Console.WriteLine(total);
            total = 0;

            foreach (int i in first)
            {
                int a = 0;
                while (second.Contains(i))
                {
                    a++;
                    second.Remove(i);
                }
                total += i * a;
            }

            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
