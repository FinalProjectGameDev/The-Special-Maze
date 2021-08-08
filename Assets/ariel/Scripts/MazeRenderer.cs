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

    //[SerializeField]
    //private Transform floorPrefab = null;

    [Header("Prefarbs to locate")]
   // [SerializeField]
    //private GameObject Player = null;
    
    [SerializeField]
    private GameObject Dog = null;

    [SerializeField]
    private GameObject Light = null;

    [SerializeField]
    private GameObject Dictionary = null;
    [SerializeField]
    private GameObject DictManeger = null;

    [SerializeField]
    private GameObject Medical = null;

    [SerializeField]
    private GameObject HearingAid = null;

    [SerializeField]
    private GameObject Glasses = null;

    [SerializeField]
    private GameObject Hendle = null;

    bool[] located = {false, false, false, false, false, false};

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
        //var size1 = size - WallThickness;
        //var floor = Instantiate(floorPrefab, transform);
        //floor.localScale = new Vector3(30 * size, 0.1f, 20 * size);
        //floor.position = new Vector3(0, -WallHeight / 2, 0);

       // Player.transform.position = new Vector3(-14.5f  * size, 0, -9.5f * size);


        for (int i = 0; i < 30; ++i)
        {
            for (int j = 0; j < 20; ++j)
            {
                var cell = maze[i, j];
                var position = new Vector3((-14.5f + i) * size, 0, (-9.5f + j) * size);

                if  (i % 2 == 1 && j % 2 == 0)
                {
                    var light = Instantiate(Light, transform);
                    light.transform.position = position;
                }
                if (i % 2 == 0 && j % 2 == 1) 
                {
                    var light = Instantiate(Light, transform);
                    light.transform.position = position;
                    light.transform.eulerAngles = new Vector3(0, 90, 0);
                }

                var corner1 = Instantiate(wallPrefab, transform) as Transform;
                corner1.position = position + new Vector3(size / 2, 0, size / 2);
                corner1.localScale = new Vector3(WallThickness, WallHeight, WallThickness);

                if(i == 0)
                {
                    var corner2 = Instantiate(wallPrefab, transform) as Transform;
                    corner2.position = position - new Vector3(size / 2, 0, size / 2);
                    corner2.localScale = new Vector3(WallThickness, WallHeight, WallThickness);
                    //corner2.GetComponent<Renderer>().material.color = Color.red;
                }
                if (j == 0)
                {
                    var corner3 = Instantiate(wallPrefab, transform) as Transform;
                    corner3.position = position + new Vector3(size / 2, 0, -size / 2);
                    corner3.localScale = new Vector3(WallThickness, WallHeight, WallThickness);
                }


                if (cell.HasFlag(WallState.NOWAY))
                {
                    if(i>=5 && i<=9 && j>=0 && j<=4 && !located[0])
                    {
                        located[0] = true;
                        //var dog = Instantiate(Dog, transform);
                        Dog.transform.position = position - new Vector3(0, 1.45f, 0);
                        if(!cell.HasFlag(WallState.RIGHT)) Dog.transform.eulerAngles = new Vector3(0, 90, 0);
                        else if(!cell.HasFlag(WallState.DOWN)) Dog.transform.eulerAngles = new Vector3(0, 180, 0);
                        else if(!cell.HasFlag(WallState.LEFT)) Dog.transform.eulerAngles = new Vector3(0, 270, 0);
                        //if(cell.HasFlag(WallState.UP)) dog.transform.eulerAngles = new Vector3(0, 90, 0);

                        Dog.GetComponent<DogController>().player = GameObject.FindGameObjectWithTag("Player").transform;
                    }
                    else if (i >= 0 && i <= 4 && j >= 5 && j <= 9 && !located[1])
                    {
                        located[1] = true;
                        //var dic = Instantiate(Dictionary, transform);
                        Dictionary.transform.position = position - new Vector3(0, 1.45f, 0);
                        DictManeger.transform.position = position - new Vector3(0, 1.45f, 0);
                    }
                    // else if (i >= 10 && i <= 14 && j >= 10 && j <= 14 && !located[2])
                    // {
                    //     located[2] = true;
                    //     var med = Instantiate(Medical, transform);
                    //     med.transform.position = position;
                    // }
                    else if (i >= 6 && i <= 9 && j >= 10 && j <= 14 && !located[3])
                    {
                        located[3] = true;
                        // var hear = Instantiate(HearingAid, transform);
                        // hear.transform.position = position;
                        HearingAid.transform.position = position - new Vector3(0, 1.45f, 0);;
                    }
                    else if (i >= 10 && i <= 14 && j >= 15 && j <= 19 && !located[4])
                    {
                        located[4] = true;
                        Glasses.transform.position = position - new Vector3(0, 1.35f, 0);
                    }
                    else if (i >= 15 && i <= 19 && j >= 10 && j <= 14 && !located[5])
                    {
                        located[5] = true;
                        //var hnd = Instantiate(Hendle, transform);
                        //hnd.transform.position = position - new Vector3(0, 1.45f, 0);
                        Hendle.transform.position = position - new Vector3(0, 1.45f, 0);
                    }
                    //else
                    //{
                    //    var floor = Instantiate(floorPrefab, transform);
                        //floor.localScale = new Vector3(30 * size, 0.1f, 20 * size);
                    //    floor.position = position;
                    //    floor.GetComponent<Renderer>().material.color = Color.blue;
                   // }

                }

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size / 2);
                    topWall.localScale = new Vector3(size - WallThickness, WallHeight, WallThickness);
                    //if (cell.HasFlag(WallState.UK))
                    //{
                    //    topWall.GetComponent<Renderer>().material.color = Color.red;
                    //}
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size - WallThickness, WallHeight, WallThickness);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                    //if (cell.HasFlag(WallState.LK))
                    //{
                    //    leftWall.GetComponent<Renderer>().material.color = Color.red;
                    //}
                }

                if (i == 30 - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size - WallThickness, WallHeight, WallThickness);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -size / 2);
                        bottomWall.localScale = new Vector3(size - WallThickness, WallHeight, WallThickness);
                    }
                }
            }
        }
    }
}