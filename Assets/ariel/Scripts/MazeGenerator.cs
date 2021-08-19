using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

[Flags]
public enum WallState
{
    // 0000 -> NO WALLS
    // 1111 -> LEFT,RIGHT,UP,DOWN
    LEFT = 1, // 0001
    RIGHT = 2, // 0010
    UP = 4, // 0100
    DOWN = 8, // 1000

    LK = 16, // 0001 0000
    RK = 32, // 0010 0000
    UK = 64, // 0100 0000
    DK = 128, // 1000 0000

    VISITED = 256, // 0001 0000 0000
    NOWAY = 512, // 0010 0000 0000
    ENTERED = 1024 // 0100 0000 0000
}

public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbour
{
    public Position Position;
    public WallState SharedWall;
}

public static class MazeGenerator
{
    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT;
        }
    }

    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze)
    {
        // here we make changes
        var rng = new System.Random(/*seed*/);
        var positionStack = new Stack<Position>();
        // var position = new Position { X = rng.Next(0, 20), Y = rng.Next(0, 20) };
        var position = new Position { X = 0, Y = 0 };


        maze[position.X, position.Y] |= WallState.VISITED;
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUnvisitedNeighbours(current, maze);

            if (neighbours.Count == 0 && !maze[current.X, current.Y].HasFlag(WallState.ENTERED))
            {
                maze[current.X, current.Y] |= WallState.NOWAY;
            }

            else if (neighbours.Count > 0)
            {
                positionStack.Push(current);

                var randIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var nPosition = randomNeighbour.Position;
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);
                maze[nPosition.X, nPosition.Y] |= WallState.VISITED;

                positionStack.Push(nPosition);
            }
            maze[current.X, current.Y] |= WallState.ENTERED;
        }



        return maze;
    }

    private static List<Neighbour> GetUnvisitedNeighbours(Position p, WallState[,] maze)
    {
        var list = new List<Neighbour>();

        if (p.X > 0)
        {  // left
            if (!maze[p.X - 1, p.Y].HasFlag(WallState.VISITED) && !maze[p.X, p.Y].HasFlag(WallState.LK))
            {
                list.Add(new Neighbour
                {
                    Position = new Position { X = p.X - 1, Y = p.Y },
                    SharedWall = WallState.LEFT
                });
            }
        }

        if (p.Y > 0)
        {  // DOWN
            if (!maze[p.X, p.Y - 1].HasFlag(WallState.VISITED) && !maze[p.X, p.Y].HasFlag(WallState.DK))
            {
                list.Add(new Neighbour
                {
                    Position = new Position { X = p.X, Y = p.Y - 1 },
                    SharedWall = WallState.DOWN
                });
            }
        }

        if (p.Y < 19)
        {  // UP
            if (!maze[p.X, p.Y + 1].HasFlag(WallState.VISITED) && !maze[p.X, p.Y].HasFlag(WallState.UK))
            {
                list.Add(new Neighbour
                {
                    Position = new Position { X = p.X, Y = p.Y + 1 },
                    SharedWall = WallState.UP
                });
            }
        }

        if (p.X < 19)
        {  // RIGHT
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.VISITED) && !maze[p.X, p.Y].HasFlag(WallState.RK))
            {
                list.Add(new Neighbour
                {
                    Position = new Position { X = p.X + 1, Y = p.Y },
                    SharedWall = WallState.RIGHT
                });
            }
        }
        return list;
    }

    public static WallState[,] Generate()
    {
        WallState[,] maze = new WallState[20, 20];

        string path = @"Assets/maze3.csv";
        // Get the file's text.
        string whole_file = System.IO.File.ReadAllText(path);

        // Split into lines.
        whole_file = whole_file.Replace('\n', '\r');
        string[] lines = whole_file.Split(new char[] { '\r' },
            StringSplitOptions.RemoveEmptyEntries);

        // See how many rows and columns there are.
        int num_rows = lines.Length;
        int num_cols = lines[0].Split(',').Length;

        // Allocate the data array.
        string[,] values = new string[num_rows, num_cols];

        // Load the array.
        for (int r = 0; r < num_rows; r++)
        {
            string[] line_r = lines[r].Split(',');
            for (int c = 0; c < num_cols; c++)
            {
                maze[r, c] = (WallState)Int32.Parse(line_r[c]);
            }
        }


        // // Use a StringBuilder to accumulate your output
        // StringBuilder sb = new StringBuilder();
        // for (int i = 0; i <= maze.GetUpperBound(0); i++)
        // {
        //     for (int j = 0; j <= maze.GetUpperBound(1); j++)
        //     {
        //         sb.Append((j == 0 ? "" : ",") + maze[i, j].GetHashCode());
        //     }
        //     sb.AppendLine();
        // }

        // string path = @"/maze.csv";
        // Debug.Log(path);

        // // Write everything with a single command 
        // File.WriteAllText(path, sb.ToString());

        return ApplyRecursiveBacktracker(maze);

    }

}