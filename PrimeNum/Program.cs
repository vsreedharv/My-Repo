using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNum
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number to check Prime");
            int val = int.Parse(Console.ReadLine());

            if (val == 1 || val == 2)
                Console.WriteLine(val + " is a Prime Number");

            if (val > 2)
            {
                for (int i = 2; i < val; i++)
                {
                    if (val % i == 0)
                    {
                        Console.WriteLine(val + " is NOT a Prime Number");
                        break;
                    }    
                    if (i==val-1)
                        Console.WriteLine(val + " is a Prime Number");
                }
            }
            
            Console.ReadLine();
        }
    }
}
