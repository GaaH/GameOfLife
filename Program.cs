using System;
using System.Linq;

namespace gameoflife
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new Grid(30, 10, 0.3);

            foreach (var step in Enumerable.Range(0, 3))
            {
                Console.WriteLine(world);
                world.NextStep();
                Console.WriteLine();
            }
        }
    }
}
