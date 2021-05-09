﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeRenderer : MonoBehaviour
{
    [Header("Build Maze")]
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

    [Header("Prefarbs to locate")]
    [SerializeField]
    private GameObject Player = null;
    
    [SerializeField]
    private GameObject Dog = null;

    [SerializeField]
    private GameObject Dictionary = null;

    [SerializeField]
    private GameObject Medical = null;

    [SerializeField]
    private GameObject HearingAid = null;

    [SerializeField]
    private GameObject Glasses = null;

    [SerializeField]
    private GameObject Hendle = null;

    bool[] located = {false, false, false, false, false, false};

    //public NavMeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGenerator.Generate();
        Draw(maze);

        //surface.BuildNavMesh();
    }

    private void Draw(WallState[,] maze)
    {
        //var size1 = size - WallThickness;
        //var floor = Instantiate(floorPrefab, transform);
        //floor.localScale = new Vector3(30 * size, 0.1f, 20 * size);
        //floor.position = new Vector3(0, -WallHeight / 2, 0);

        Player.transform.position = new Vector3(-14.5f  * size, 0, -9.5f * size);


        for (int i = 0; i < 30; ++i)
        {
            for (int j = 0; j < 20; ++j)
            {
                var cell = maze[i, j];
                var position = new Vector3((-14.5f + i) * size, 0, (-9.5f + j) * size);

                if (cell.HasFlag(WallState.NOWAY))
                {
                    if(i>=5 && i<=9 && j>=0 && j<=4 && !located[0])
                    {
                        located[0] = true;
                        var dog = Instantiate(Dog, transform);
                        dog.transform.position = position;
                    }
                    else if (i >= 0 && i <= 4 && j >= 5 && j <= 9 && !located[1])
                    {
                        located[1] = true;
                        var dic = Instantiate(Dictionary, transform);
                        dic.transform.position = position;
                    }
                    else if (i >= 10 && i <= 14 && j >= 10 && j <= 14 && !located[2])
                    {
                        located[2] = true;
                        var med = Instantiate(Medical, transform);
                        med.transform.position = position;
                    }
                    else if (i >= 10 && i <= 15 && j >= 0 && j <= 5 && !located[3])
                    {
                        located[3] = true;
                        var hear = Instantiate(HearingAid, transform);
                        hear.transform.position = position;
                    }
                    else if (i >= 20 && i <= 24 && j >= 5 && j <= 9 && !located[4])
                    {
                        located[4] = true;
                        var gls = Instantiate(Glasses, transform);
                        gls.transform.position = position;
                    }
                    else if (i >= 25 && i <= 29 && j >= 0 && j <= 4 && !located[5])
                    {
                        located[5] = true;
                        var hnd = Instantiate(Hendle, transform);
                        hnd.transform.position = position;
                    }
                    else
                    {
                        var floor = Instantiate(floorPrefab, transform);
                        //floor.localScale = new Vector3(30 * size, 0.1f, 20 * size);
                        floor.position = position;
                        floor.GetComponent<Renderer>().material.color = Color.blue;
                    }

                }

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

                if (i == 30 - 1)
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