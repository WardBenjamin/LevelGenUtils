using System;

public static class Example
{
    public static void Main(String[] args)
    {
        CellularAutomata automata = new CellularAutomata(this.size);
        automata.RandomSeed = this.LevelSeed;
        automata.InitializeMap();
        for (int i = 0; i < 6; i++)
            automata.DoStep();
    }
    public static void printMap(CellularAutomata automata) 
    {
    #if UNITY
        int maxX = automata.size.x, maxY = automata.size.y;
#else
        int maxX = automata.length, maxY = automata.width;
#endif
        for (int x = 0; x < maxX; x++) 
        {
            for (int y = 0; y < maxY - 1; y++) 
            {
                Console.Write(Convert.ToString(automata.map[x,y]) + ", ");
            }
            Console.WriteLine(Convert.ToString(automata.map[x, maxY - 1]));
        }
        Console.WriteLine();
    }
}
