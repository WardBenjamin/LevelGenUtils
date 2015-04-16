using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        // Example usage:
        int length = 10, width = 10;
        bool[,] map = new bool[10, 10];
        map[2, 2] = true;
        map[2, 3] = true;
        map[3, 3] = true;
        map[4, 3] = true;
        map[4, 2] = true;
        map[4, 1] = true;
        map[3, 1] = true;
        map[2, 1] = true;
        // //Define filled/empty nodes here
        int count = FloodFill.GetSizeOfBlock(map, new Vector2(10, 10), new Vector2(2, 2));
        Console.WriteLine("Count = " + count);
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width - 1; j++)
            {
                Console.Write(map[i, j] + ", ");
            }
            Console.WriteLine(map[i, width - 1]);
        }
        Console.ReadKey();
        map = FloodFill.Fill(map, new Vector2(10, 10), new Vector2(0, 0));
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width - 1; j++)
            {
                Console.Write(map[i, j] + ", ");
            }
            Console.WriteLine(map[i, width - 1]);
        }
        Console.ReadKey();
    }
}