using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindrome
{
    class Program
    {
        public static void Main(string[] args)
        {
            String val;
            Console.WriteLine("Enter the word to check if it's a Palindrome");
            val = Console.ReadLine();
            char[] a = val.ToCharArray();
            int max = val.Length - 1;
            for(int min=0;min<=max;)
            {
                Char x = a[min];
                Char y = a[max];
                if (x==y)
                {
                    min++;
                    max--;
                }
                if (min > max)
                {
                    Console.WriteLine(val + " This is a Palindrome");
                }
                if (x!=y)
                {
                    Console.WriteLine(val + " This is not a Palindrome");
                    break;
                }
           } 
            Console.ReadLine();
        }
    }
}