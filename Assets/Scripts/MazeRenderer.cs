using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private float WallThickness = 0.1f;

    [SerializeField]
    private float WallHeight = 0.1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    public NavMeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGenerator.Generate();
        Draw(maze);

        surface.BuildNavMesh();
    }

    private void Draw(WallState[,] maze)
    {
        var size1 = size - WallThickness;
        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(40 * size, 0.1f, 25 * size);
        floor.position = new Vector3(0, -WallHeight / 2, 0);

        for (int i = 0; i < 40; ++i)
        {
            for (int j = 0; j < 25; ++j)
            {
                var cell = maze[i, j];
                var position = new Vector3((-19.5f + i) * size, 0, (-12 + j) * size);

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size / 2);
                    topWall.localScale = new Vector3(size + WallThickness, WallHeight, WallThickness);
                    if (cell.HasFlag(WallState.UK))
                    {
                        topWall.GetComponent<Renderer>().material.color = Color.red;
                    }
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size + WallThickness, WallHeight, WallThickness);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                    if (cell.HasFlag(WallState.LK))
                    {
                        leftWall.GetComponent<Renderer>().material.color = Color.red;
                    }
                }

                if (i == 40 - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size + WallThickness, WallHeight, WallThickness);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -size / 2);
                        bottomWall.localScale = new Vector3(size + WallThickness, WallHeight, WallThickness);
                    }
                }
            }
        }
    }
}