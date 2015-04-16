using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class FloodFill
{
    public static bool[,] Fill(bool[,] map, Vector2 size, Vector2 position, bool state)
    {
        if (!map[(int)position.x, (int)position.y] == state)
        {
            return FillRecursive(map, size, position, state);
        }
        return map;
    }

    public static bool[,] Fill(bool[,] map, Vector2 size, Vector2 position)
    {
        if (!map[(int)position.x, (int)position.y])
        {
            return FillRecursive(map, size, position);
        }
        return map;
    }


    private static bool[,] FillRecursive(bool[,] map, Vector2 size, Vector2 position, bool state)
    {
        if (map[(int)position.x, (int)position.y] == state)
            return map;
        map[(int)position.x, (int)position.y] = state;
        foreach (var canidate in Neighbors(position, size))
        {
            map = FillRecursive(map, size, canidate, state);
        }
        return map;
    }

    private static bool[,] FillRecursive(bool[,] map, Vector2 size, Vector2 position)
    {
        if (map[(int)position.x, (int)position.y])
            return map;
        map[(int)position.x, (int)position.y] = true;
        foreach (var canidate in Neighbors(position, size))
        {
            map = FillRecursive(map, size, canidate);
        }
        return map;
    }

    public static int GetSizeOfBlock(bool[,] map, Vector2 size, Vector2 position)
    {
        List<Vector2> openList = new List<Vector2>();
        List<Vector2> closedList = new List<Vector2>();

        openList.Add(position);

        int count = 0;
        while (openList.Count > 0)
        {
            // Get the current node from the openList and remove it from the openList
            Vector2 current = openList[openList.Count - 1];
            openList.Remove(current);
            Console.WriteLine("openList.Count: " + openList.Count);
            List<Vector2> neighbors = Neighbors(current, size);
            foreach (var canidate in neighbors) // Check all valid neighbors
            {
                // We have not touched this node before
                if (!closedList.Contains(canidate) && !openList.Contains(canidate))
                {

                    // If it is a valid node
                    if (map[(int)canidate.x, (int)canidate.y])
                    {
                        Console.WriteLine("Valid! " + canidate.x + ", " + canidate.y);
                        openList.Add(canidate);
                    }
                }
            }
            // Add the current node to the closedList and add one to the count
            closedList.Add(current);
            count++;
        }
        return count;
    }

    private static Vector2[] surrounding = new Vector2[]
        {                         
		    new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0,-1)
	    };

    private static List<Vector2> Neighbors(Vector2 pos, Vector2 size)
    {
        List<Vector2> neighbors = new List<Vector2>();
        foreach (var possible in surrounding)
        {
            Vector2 canidate = new Vector2(pos.x + possible.x, pos.y + possible.y);
            if (InBounds(canidate, size))
                neighbors.Add(canidate);
        }
        return neighbors;
    }

    private static bool InBounds(Vector2 pos, Vector2 size)
    {
        return (0 <= pos.x && pos.x < size.x && 0 <= pos.y && pos.y < size.y);
    }
}

public struct Vector2
{
    public float x, y;
    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}