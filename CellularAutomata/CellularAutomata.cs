//#define UNITY

#if UNITY
using UnityEngine;
#else
using System;
#endif
using System.Collections;

public class CellularAutomata
{
    public bool[,] map;
#if UNITY
    public Vector2 size;
#else
    public int length, width;
#endif

    public float aliveChance = 0.45f; // Chance for any specific node to start as alive.

#if UNITY
    public int RandomSeed = 0;
#endif

    public int deathLimit = 3;
    public int birthLimit = 4;

    public CellularAutomata(int length, int width)
    {
        map = new bool[length, width];
#if UNITY
        size = new Vector2(length, width);
#else
        this.length = length;
        this.width = width;
#endif
    }
#if UNITY
    public CellularAutomata(Vector2 size)
    {
        this.size = size;
        this.map = new bool[(int)size.x, (int)size.y];
    }
#endif

    public void InitializeMap()
    {
#if UNITY
        Random.seed = this.RandomSeed;
        int maxX = (int)this.size.x, maxY = (int)this.size.y;
#else
        int maxX = this.length, maxY = this.width;
#endif
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
#if UNITY
                float randomNum = Random.value;
#else
                Random random = new Random();
                float randomNum = (float)random.NextDouble();
#endif
                if (randomNum < aliveChance)
                {
                    map[x, y] = true;
                }
            }
        }
    }
    public void DoStep()
    {
#if UNITY
        int maxX = (int)this.size.x, maxY = (int)this.size.y;
#else
        int maxX = this.length, maxY = this.width;
#endif
        bool[,] nMap = new bool[maxX, maxY];

        //Loop over each row and column of the map
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                int neighbors = AliveNeighbours(x, y);
                // If a cell is alive and has too few neighbors, kill it.
                if (this.map[x, y])
                {
                    if (neighbors < deathLimit)
                    {
                        nMap[x, y] = false;
                    }
                    else
                    {
                        nMap[x, y] = true;
                    }
                }
                // If the cell is dead and it has enough neighbors to be born, create it.
                else
                {
                    if (neighbors > birthLimit)
                    {
                        nMap[x, y] = true;
                    }
                    else
                    {
                        nMap[x, y] = false;
                    }
                }
            }
        }
        this.map = nMap;
    }

    //Returns the number of cells in a ring around (x,y) that are alive.
    public int AliveNeighbours(int x, int y)
    {
#if UNITY
        int maxX = (int)size.x, maxY = (int)size.y;
#else
        int maxX = this.length, maxY = this.width;
#endif
        int count = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int curX = x + i;
                int curY = y + j;

                // Skip invalid points (center and out of bounds)
                if ((i == 0 && j == 0) || curX < 0 || curY < 0 || curX >= maxX || curY >= maxY)
                {
                    // Do nothing
                }

                // Check cell if it is valid
                else if (map[curX, curY])
                {
                    count = count + 1;
                }
            }
        }
        return count;
    }
}