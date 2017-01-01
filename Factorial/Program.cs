using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number to find its Factorial: ");
            int n = int.Parse(Console.ReadLine());
            if(n>0)
            {
                for(int i=n-1; i>0; i--)
                {
                    n = n*i;
                }
            }

            Console.WriteLine("The Factorial of is " + n);
            Console.ReadLine();

        }
    }
}
