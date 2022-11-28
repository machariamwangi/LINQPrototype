using System;
using System.Collections.Generic;
using System.Linq;

namespace CompleteQueryOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Specify the data source.
            int[] scores = { 97, 92, 81, 60 };

            // Define the query expression.
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                select score;

            // Execute the query.
            foreach (int i in scoreQuery)
            {
                Console.Write(i + " ");
            }

            int[] numbers = new int[7] {0, 1, 2,3,4,5,6 };

            var numQuery = from num in numbers where  (num % 2) == 0 select num;

            foreach (int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
      

        }
    }
}
