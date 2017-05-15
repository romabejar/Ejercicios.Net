using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers
{
    public class Algorithms
    {

        private static Algorithms instance;

        private Algorithms() { }

        public static Algorithms Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Algorithms();
                }
                return instance;
            }
        }

        /// <summary>
        /// This function represents the Sieve of Erathosthenes algorithm, it is a fast way
        /// to find all prime numbers from the first one to a specific one.
        /// </summary>
        /// <param name="n"> This is the number that will limit the sieve. </param>
        /// <returns>The result is a list with all the prime numbers from 2 to n, ordered from 
        /// least to greatest.</returns>
        public List<int> SieveOfErathosthenes(int n)
        {
            List<int> result = new List<int>();

            /* numbers is an array of bool, each value represent wether if the integer equal
             * to the value position is or is not a prime number:
             * if value is false, the number of the index of that position is prime.
             * if value is true, the number of the index of that position is not prime.
             * 
             * We add 1 to n because we want that the index of the array represents the integer
             * which prime status is represented by the value of that position  */
            bool[] numbers = new bool[n+1]; 
            // We set 0, and 1 numbers as not prime.
            numbers[0] = true;
            numbers[1] = true;

            for(int i = 2; i < n+1; ++i)
            {
                if (!numbers[i])
                {
                    result.Add(i);
                    for(int j = 2*i; j < n+1; j += i)
                    {
                        numbers[j] = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Converts a list of integers to a string.
        /// </summary>
        /// <param name="intList">The list to be converted to String.</param>
        /// <param name="separator">The string element that will separate the elements of the list.</param>
        /// <returns>A string containing all the elements of the intList list, in the same order, and 
        /// separated by separator.</returns>
        public string IntListToString(List<int> intList, string separator)
        {
            StringBuilder result = new StringBuilder();

            bool firstTime = true;
            foreach ( int integer in intList)
            {
                if (firstTime) firstTime = false;
                else result.Append(separator);
                result.Append(integer.ToString());
            }

            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="num"></param>
        /// <returns>Returns true if s is a valid argument</returns>
        public bool isAValidArgument(string s, out int num)
        {
            bool result = true;

            result = int.TryParse(s, out num);                  // Check if argument is an integer
            if (num < 2 || num > 1000000) result = false;       // Check if 2 <= argument <= 1000000

            return result;
        }
    }
}
