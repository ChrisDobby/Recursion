﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Recursion_Solutions_CSharp {
    static class Program {

        /***************************
         ********** Basic **********
         ***************************/

        static int factorial(int n) {
            return n == 0
                ? 1
                : n * factorial(n - 1);
        }

        static int factorial2(int n) {
            if( n == 0 )
                return 1;
            else
                return n * factorial2(n - 1);
        }

        static int fibonacci(int n) {
            return n < 2
                ? 1
                : fibonacci(n-1) + fibonacci(n-2);
        }

        static int sum(IEnumerable<int> list) {
            return list.Count() == 0
                ? 0
                : list.ElementAt(0) + sum(list.Skip(1));
        }

        static int count(int item, IEnumerable<int> list) {
            return list.Count() == 0
                ? 0
                : (list.ElementAt(0) == item ? 0 : 1) + count(item, list.Skip(1));
        }

        static bool even(int num) { return num % 2 == 0; }

        static string print(this LinkedList<int> list) {
            return list.Aggregate( "", (acc, elem) => acc + "," + elem);
        }

        static LinkedList<int> evens(LinkedList<int> list) {
            if( list.Count() == 0 )
                return new LinkedList<int>();
            if (even(list.First.Value)) {
                var head = list.First.Value;
                list.RemoveFirst();
                var rest = evens(list);
                rest.AddFirst(head);
                return rest;
            }
            else {
                list.RemoveFirst();
                return evens(list);
            }
        }

        static bool isEven(int n) {
          return n == 0 
              ? true
              : isOdd(n - 1);
        }

        static bool isOdd(int n) {
            return n == 0
                ? false
                : isEven(n - 1);
        }

        /**********************************
         ********** Intermediate **********
         **********************************/

        static int ackermann(int m, int n) {
            if (m == 0)
                return n + 1;
            else if (n == 0)
                return ackermann(m - 1, 1);
            else
                return ackermann(m - 1, ackermann(m, n - 1));
        }

        static bool treesearch(string filename, string path) {
            var filesFolders = new DirectoryInfo(path).EnumerateFileSystemInfos();

            if (filesFolders.Where(info => info.Name == filename).Count() > 0)
                return true;

            return filesFolders
                .Where( info => info is DirectoryInfo )
                .Any( directory => treesearch(filename, directory.FullName ));
        }

        static int count_tail(int item, IEnumerable<int> list, int count) {
            return list.Count() == 0
                ? count
                : count_tail(item, list.Skip(1), count + (list.ElementAt(0) == item ? 0 : 1));
        }

        static void Main(string[] args) {

            Console.WriteLine("Basic\n");
            Console.WriteLine("Factorial 5: {0}", factorial(5));
            Console.WriteLine("Fibonacci 7: {0}", fibonacci(7));
            Console.WriteLine("Sum [1;2;3;4;5] = {0}", sum(new List<int> {1,2,3,4,5}));
            Console.WriteLine("Count 1 [1,2,1,3,4,1] = {0}", count(1, new List<int> { 1, 2, 1, 3, 4, 1 }));
            Console.WriteLine("Evens [1,2,3,4,5,6,7,8] = {0}", evens(new LinkedList<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 })).print());
            Console.WriteLine("99 even? {0} 78 even? {1} 21 odd? {2}", isEven(99), isEven(78), isOdd(21));

            Console.WriteLine("\nIntermediate\n");
            Console.WriteLine("Ackermann 3 10: {0}", ackermann(3,10));
            Console.WriteLine("treesearch c:temp {0}", treesearch("thefile.txt", @"c:\temp"));
            Console.WriteLine("Count 99 [1..100000] = {0}", count(99, Enumerable.Range(1,100000)));
        }
    }
}
