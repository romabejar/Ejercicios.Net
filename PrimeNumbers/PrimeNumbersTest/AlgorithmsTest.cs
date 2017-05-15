using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PrimeNumbers;

namespace PrimeNumbersTest
{
    [TestClass]
    public class AlgorithmsTest
    {
        [TestMethod]
        public void SieveOfErathostenesShouldReturnAListWithTheProperPrimeNumbers()
        {
            // Arrange
            int input_a = 2;
            int input_b = 25;
            int input_c = 200;
            List<int> expected_output_a = new List<int> { 2 };
            List<int> expected_output_b = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19, 23 };
            List<int> expected_output_c = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29,
                                                 31, 37, 41, 43, 47, 53, 59, 61, 67, 71,
                                                 73, 79, 83, 89, 97, 101, 103, 107, 109,
                                                 113, 127, 131, 137, 139, 149, 151, 157,
                                                 163, 167, 173, 179, 181, 191, 193, 197, 199 };

            // Act

            List<int> output_a = Algorithms.Instance.SieveOfErathosthenes(input_a);
            List<int> output_b = Algorithms.Instance.SieveOfErathosthenes(input_b);
            List<int> output_c = Algorithms.Instance.SieveOfErathosthenes(input_c);

            // Assert

            CollectionAssert.AreEqual(output_a, expected_output_a);
            CollectionAssert.AreEqual(output_b, expected_output_b);
            CollectionAssert.AreEqual(output_c, expected_output_c);
        }

        [TestMethod]
        public void IntListToStringShouldReturnAProperlyFormatedString()
        {
            // Arragne
            List<int> input_a = new List<int> { 2 };
            List<int> input_b = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19, 23 };
            List<int> input_c = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29,
                                                 31, 37, 41, 43, 47, 53, 59, 61, 67, 71,
                                                 73, 79, 83, 89, 97, 101, 103, 107, 109,
                                                 113, 127, 131, 137, 139, 149, 151, 157,
                                                 163, 167, 173, 179, 181, 191, 193, 197, 199 };

            string expected_output_a = "2";
            string expected_output_b = "2 3 5 7 11 13 17 19 23";
            string expected_output_c = "2 3 5 7 11 13 17 19 23 29 31 37 41 43 47 53 59 61 67 71 73 79 83 89 97 101 103 107 109 113 127 131 137 139 149 151 157 163 167 173 179 181 191 193 197 199";

            // Act
            string output_a = Algorithms.Instance.IntListToString(input_a, " ");
            string output_b = Algorithms.Instance.IntListToString(input_b, " ");
            string output_c = Algorithms.Instance.IntListToString(input_c, " ");

            // Assert
            Assert.AreEqual(expected_output_a, output_a);
            Assert.AreEqual(expected_output_b, output_b);
            Assert.AreEqual(expected_output_c, output_c);

        }

        [TestMethod]
        public void isAValidArgumentDoesNotAcceptNonIntegers32()
        {
            // Arrange
            string input_a = "foo";
            string input_b = "213.123";
            string input_c = "9999,999";
            string input_d = "99999999999999999999999999999999999999";
            int n = 0;

            // Act
            bool output_a = Algorithms.Instance.isAValidArgument(input_a, out n);
            bool output_b = Algorithms.Instance.isAValidArgument(input_b, out n);
            bool output_c = Algorithms.Instance.isAValidArgument(input_c, out n);
            bool output_d = Algorithms.Instance.isAValidArgument(input_d, out n);

            // Assert

            Assert.IsFalse(output_a);
            Assert.IsFalse(output_b);
            Assert.IsFalse(output_c);
            Assert.IsFalse(output_d);
        }

        [TestMethod]
        public void isAValidArgumentDoesNotAcceptOutOfRangeIntegers()
        {
            // Arrange
            string input_a = "0";
            string input_b = "-10";
            string input_c = "1";
            string input_d = "1000001";
            int n = 0;

            // Act
            bool output_a = Algorithms.Instance.isAValidArgument(input_a, out n);
            bool output_b = Algorithms.Instance.isAValidArgument(input_b, out n);
            bool output_c = Algorithms.Instance.isAValidArgument(input_c, out n);
            bool output_d = Algorithms.Instance.isAValidArgument(input_d, out n);

            // Assert

            Assert.IsFalse(output_a);
            Assert.IsFalse(output_b);
            Assert.IsFalse(output_c);
            Assert.IsFalse(output_d);
        }

        [TestMethod]
        public void isAValidArgumentAcceptsInRangeIntegers()
        {
            // Arrange
            string input_a = "2";
            string input_b = "1000000";
            string input_c = "55555";
            int n = 0;

            // Act
            bool output_a = Algorithms.Instance.isAValidArgument(input_a, out n);
            bool output_b = Algorithms.Instance.isAValidArgument(input_b, out n);
            bool output_c = Algorithms.Instance.isAValidArgument(input_c, out n);

            // Assert

            Assert.IsTrue(output_a);
            Assert.IsTrue(output_b);
            Assert.IsTrue(output_c);
        }
    }
}
