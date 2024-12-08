using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace day7
{
    internal class Program
    {

        static bool test(long target, long[] nums)
        {
            long t = 0;
            /*foreach (long num in nums)
            {
                t += num;
            }
            if (t > target)
            {
                return false;
            }
            if (t == target)
            {
                return true;
            }
            t = 1;
            foreach (long num in nums)
            {
                t *= num;
            }
            if (t < target)
            {
                return false;
            }
            if(t ==  target)
            {
                return true;
            }           */ 
            
            for(int i = 0; i < Math.Pow(3,nums.Length-1); i++) //1 2 3 4
            {
                
                //string binary = Convert.ToString(i, 3);
                string binary = ConvertToBase3(i);
                while(binary.Length < nums.Length-1)
                {
                    binary = "0" + binary;
                }
                t = nums[0];
                for (int j = 1; j < nums.Length; j++)
                {
                    if (binary[j-1] == '1')
                    {
                        t *= nums[j];
                    }
                    else if (binary[j - 1] == '2')
                    {
                        t = long.Parse(t.ToString() + (nums[j].ToString()));
                    }
                    else
                    {
                        t += nums[j];
                    }
                }
                //Console.WriteLine(binary + " " + t);
                if (t == target)
                {
                    //Console.WriteLine();
                    return true;
                }

            }
           // Console.WriteLine();
            return false;
        }

        static string ConvertToBase3(long input)
        {
            string result = "";
            while (input > 0)
            {
                result = (input % 3).ToString() + result;
                input /= 3;
            }
            
            return result;
            
        }

        static void Main(string[] args)
        {
            long total = 0;

            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(':');
                    long target = long.Parse(line[0]);
                    long[] nums = line[1].Substring(1).Split(' ').Select(long.Parse).ToArray();

                    if(test(target, nums))
                    {
                        total += target;
                        Console.WriteLine(total);
                    }
                    else
                    {
                        Console.WriteLine(line[0] + ":" + line[1]);
                    }


                }
            }

            Console.WriteLine(total);
            Console.ReadKey();

            total = 0;

            Console.WriteLine(total);
        }
    }
}
