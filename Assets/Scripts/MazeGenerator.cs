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
        var position = new Position { X = rng.Next(0, 30), Y = rng.Next(0, 20) };

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

        if (p.X < 29)
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
        WallState[,] maze = new WallState[30, 20];
        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;
        for (int i = 0; i < 30; ++i)
        {
            for (int j = 0; j < 20; ++j)
            {
                maze[i, j] = initial;  // 1111
                if (i % 10 == 9) maze[i, j] |= WallState.RK;
                if (i % 10 == 0) maze[i, j] |= WallState.LK;

            }
            if (i % 10 != 5)
            {
                maze[i, 9] |= WallState.UK;
                maze[i, 10] |= WallState.DK;
            }
        }

        maze[9, 15] &= ~WallState.RK;
        maze[10, 15] &= ~WallState.LK;

        maze[19, 5] &= ~WallState.RK;
        maze[20, 5] &= ~WallState.LK;

        maze[5, 10] |= WallState.RK;
        maze[4, 11] |= WallState.RK;
        maze[4, 12] |= WallState.RK;
        maze[4, 13] |= WallState.RK;
        maze[4, 14] |= WallState.RK;
        maze[4, 15] |= WallState.RK;
        maze[4, 16] |= WallState.RK;
        maze[4, 17] |= WallState.RK;
        maze[4, 18] |= WallState.RK;
        maze[5, 11] |= WallState.RK;
        maze[5, 12] |= WallState.RK;
        maze[5, 13] |= WallState.RK;

        maze[5, 11] |= WallState.LK;
        maze[5, 12] |= WallState.LK;
        maze[5, 13] |= WallState.LK;
        maze[5, 14] |= WallState.LK;
        maze[5, 15] |= WallState.LK;
        maze[5, 16] |= WallState.LK;
        maze[5, 17] |= WallState.LK;
        maze[5, 18] |= WallState.LK;
        maze[6, 10] |= WallState.LK;
        maze[6, 11] |= WallState.LK;
        maze[6, 12] |= WallState.LK;
        maze[6, 13] |= WallState.LK;

        maze[5, 10] |= WallState.UK;
        maze[5, 14] |= WallState.UK;
        maze[6, 14] |= WallState.UK;
        maze[6, 15] |= WallState.UK;
        maze[7, 14] |= WallState.UK;
        maze[7, 15] |= WallState.UK;
        maze[8, 14] |= WallState.UK;
        maze[8, 15] |= WallState.UK;
        maze[9, 15] |= WallState.UK;

        maze[5, 11] |= WallState.DK;
        maze[5, 15] |= WallState.DK;
        maze[6, 15] |= WallState.DK;
        maze[6, 16] |= WallState.DK;
        maze[7, 15] |= WallState.DK;
        maze[7, 16] |= WallState.DK;
        maze[8, 15] |= WallState.DK;
        maze[8, 16] |= WallState.DK;
        maze[9, 16] |= WallState.DK;

        maze[25, 10] |= WallState.RK;
        maze[24, 11] |= WallState.RK;
        maze[24, 12] |= WallState.RK;
        maze[24, 13] |= WallState.RK;
        maze[24, 14] |= WallState.RK;
        maze[24, 15] |= WallState.RK;
        maze[24, 16] |= WallState.RK;
        maze[24, 17] |= WallState.RK;
        maze[24, 18] |= WallState.RK;
        maze[25, 11] |= WallState.RK;
        maze[25, 12] |= WallState.RK;
        maze[25, 13] |= WallState.RK;

        maze[25, 11] |= WallState.LK;
        maze[25, 12] |= WallState.LK;
        maze[25, 13] |= WallState.LK;
        maze[25, 14] |= WallState.LK;
        maze[25, 15] |= WallState.LK;
        maze[25, 16] |= WallState.LK;
        maze[25, 17] |= WallState.LK;
        maze[25, 18] |= WallState.LK;
        maze[26, 10] |= WallState.LK;
        maze[26, 11] |= WallState.LK;
        maze[26, 12] |= WallState.LK;
        maze[26, 13] |= WallState.LK;

        maze[25, 10] |= WallState.UK;
        maze[25, 14] |= WallState.UK;
        maze[26, 14] |= WallState.UK;
        maze[26, 15] |= WallState.UK;
        maze[27, 14] |= WallState.UK;
        maze[27, 15] |= WallState.UK;
        maze[28, 14] |= WallState.UK;
        maze[28, 15] |= WallState.UK;
        maze[29, 15] |= WallState.UK;

        maze[25, 11] |= WallState.DK;
        maze[25, 15] |= WallState.DK;
        maze[26, 15] |= WallState.DK;
        maze[26, 16] |= WallState.DK;
        maze[27, 15] |= WallState.DK;
        maze[27, 16] |= WallState.DK;
        maze[28, 15] |= WallState.DK;
        maze[28, 16] |= WallState.DK;
        maze[29, 16] |= WallState.DK;

        maze[5, 10] &= ~WallState.UP;
        maze[25, 10] &= ~WallState.UP;
        maze[5, 11] &= ~WallState.DOWN;
        maze[25, 11] &= ~WallState.DOWN;

        maze[29, 15] &= ~WallState.RIGHT;

        return ApplyRecursiveBacktracker(maze);

        

    }
}