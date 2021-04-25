using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        var position = new Position { X = rng.Next(0, 40), Y = rng.Next(0, 25) };

        maze[position.X, position.Y] |= WallState.VISITED;  // 0001 0000 1111
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUnvisitedNeighbours(current, maze);

            if (neighbours.Count > 0)
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

        if (p.Y < 24)
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

        if (p.X < 39)
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
        WallState[,] maze = new WallState[40, 25];
        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;
        for (int i = 0; i < 40; ++i)
        {
            for (int j = 0; j < 25; ++j)
            {
                maze[i, j] = initial;  // 1111
                if (i % 14 == 12) maze[i, j] |= WallState.RK;
                if (i % 14 == 13) maze[i, j] |= WallState.LK;

            }
            if (i % 13 != 8)
            {
                maze[i, 12] |= WallState.UK;
                maze[i, 13] |= WallState.DK;
            }
        }

        maze[12, 18] &= ~WallState.RK;
        maze[13, 18] &= ~WallState.LK;

        maze[26, 7] &= ~WallState.RK;
        maze[27, 7] &= ~WallState.LK;

        return ApplyRecursiveBacktracker(maze);
    }
}