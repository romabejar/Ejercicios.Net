using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int X;
            if ( args.Length != 1 || !Algorithms.Instance.isAValidArgument( args[0], out X ) )
            {
                System.Console.WriteLine("El parámetro debe ser un número válido.");
                System.Console.ReadKey();
                return;
            }
            X = int.Parse(args[0]);
            List<int> primeNumbers = Algorithms.Instance.SieveOfErathosthenes(X);
            System.Console.WriteLine(Algorithms.Instance.IntListToString(primeNumbers, " "));
            System.Console.ReadKey();
            return;
        }

        /* This is not part of the solution, but I was thinking that if we only want to give prime
         * numbers from 2 to 1000000, a valid solution would be to previously store them in a file
         * and return them from it, but I've found that Sieve of Erathosthenes is fast enough. */
        private static void generatePrimeNumbersFile()
        {
            List<int> primeNumbers = Algorithms.Instance.SieveOfErathosthenes(1000000);

            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter("prime_numbers.txt");

                foreach (int primeNumber in primeNumbers)
                {
                    sw.WriteLine(primeNumber);
                }
                sw.Flush();
                sw.Close();

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Data);
            }

        }
    }
}
