using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;

namespace day17
{
    internal class Program
    {

        static string Part1(int[] program, ref long x)
        {
            long A = x;
            long B = 0;
            long C = 0;
            string output = "";
            for (int i = 0; i < program.Length; i += 2)
            {
                int operand = program[i + 1];
                switch (program[i])
                {
                    case 0:
                        adv(ref A, operand, B, C);
                        break;
                    case 1:
                        bxl(ref B, operand);
                        break;
                    case 2:
                        bst(ref B, operand, A, C);
                        break;

                    case 3:
                        if (A != 0)
                        {
                            i = operand - 2;
                        }
                        break;
                    case 4:
                        bxc(ref B, C);
                        break;
                    case 5:
                        output += ((getComboOperand(operand, A, B, C) % 8) + ",").ToString();
                        break;
                    case 6:
                        bdv(A, operand, ref B, C);
                        break;
                    case 7:
                        cdv(A, operand, B, ref C);
                        break;
                }

            }
            return output;
        }

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            string goal = input[4].Split(' ')[1] + ",";
            int[] program = goal.Substring(0, goal.Length - 1).Split(',').Select(int.Parse).ToArray();

            long x = Convert.ToInt64("3000000000000000", 8);
            FindPoss("3000000000000000", program, 14);

        }

        static void FindPoss(string number, int[] program, int digit)
        {             
            for (int i = 0; i < 8; i++)
            {                
                char[] numberA = number.ToCharArray();
                numberA[15-digit] = i.ToString()[0];
                number = new string(numberA);
                long X = Convert.ToInt64(number, 8);
                string result = Part1(program, ref X);
                

                if (int.Parse(result.Substring(digit*2, 1)) == program[digit] && digit!= 0 && result.Length == 32)
                {
                    FindPoss(number, program, digit - 1);                    
                }
                else if (digit == 0 && int.Parse(result.Substring(digit * 2, 1)) == program[digit] && result.Length == 32)
                {
                    Console.WriteLine(result + " " + Convert.ToInt64(number, 8));
                }
                numberA[15-digit] = '0';
                number = new string(numberA);
                
            }
            

        }

        static void adv(ref long A, int operand, long B, long C)
        {            
            A = A / (long)(Math.Pow(2, getComboOperand(operand, A, B, C)));
            
        }
        static void bxl(ref long B, int operand)
        {
            B = B ^ operand;
        }
        static void bst(ref long B, int operand, long A, long C)
        {
            B = getComboOperand(operand, A, B, C) % 8;
        }

        //jnz if A ! = 0, jump to literal op

        static void bxc(ref long B, long C)
        {
            B = B ^ C;
        }


        static void bdv(long A, int operand, ref long B, long C)
        {
            B = A / (long)(Math.Pow(2, getComboOperand(operand, A, B, C)));
        }

        static void cdv(long A, int operand, long B, ref long C)
        {
            C = A / (long)(Math.Pow(2 , getComboOperand(operand, A, B, C)));
        }



        static long getComboOperand(int operand, long A, long B, long C)
        {
            switch (operand)
            {
                case 0:
                    return 0;
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
                case 4:
                    return A;
                case 5:
                    return B;
                case 6:
                    return C;
                default:
                    Console.WriteLine("Operand Not Recognised");
                    return -1;

            }

        }
    }
}
