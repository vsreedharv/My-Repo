using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseString
{
    class Program
    {
        static void Main(string[] args)
        {
            String value;
            Console.WriteLine("Please Enter a String: ");
            value = Console.ReadLine();
            char[] x = value.ToCharArray();
            char[] y = new char[x.Length];
            if (value!=null)
            {
                for(int i=0; i<x.Length;)
                {
                    for(int j = value.Length-1; j>=0; j--)
                    {
                        y[j] = x[i];
                        i++;
                    } 
                }
                Console.WriteLine("The reversed string is: ");
                for(int i=0; i<x.Length;i++)
                {
                    Console.Write(y[i]);
                }
            }
            Console.ReadLine();
        }
    }
}