﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    //Singleton
    public static LevelManager instance;

    //Level counter
    public static int CurrentLevel = 1;
    public static int TotalLevels = 10;
    public Object[] Levels;
    public bool levelChangeChecker = false; //Variable to check to not add the current level for calling the Win() function more than once;

    //Boxes list in order to check if there are allocated
    public List<Box> boxes = new List<Box>();

    //Debug
    public GameObject PrefabButtonLevel;
    public Transform UI;

    private void Awake()
    {
        //set up the instance
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public List<GameObject> gameObjects = new List<GameObject>();
    public Tilemap tilemap;


    // Start is called before the first frame update
    void Start()
    {
        Levels = Resources.LoadAll("GameLevels", typeof(TextAsset));
        LoadLevel(Levels[CurrentLevel-1].name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel (string levelName)
    {
        //load the json file to a leveldata
        TextAsset level = Resources.Load<TextAsset>("GameLevels/" + levelName);

        LevelData data = JsonUtility.FromJson<LevelData>(level.ToString());

        //clear the tilemap and boxes list
        tilemap.ClearAllTiles();
        boxes.Clear();

        //place the tiles
        for (int i = 0; i < data.tiles.Count; i++)
        {
            GameObject goToInstantiate = gameObjects.Find(t => t.name == data.tiles[i]);
            Vector3Int positionToInstantiate = new Vector3Int(data.poses_x[i], data.poses_y[i], 0);

            GameObject goInstantiated = Instantiate(goToInstantiate, positionToInstantiate, Quaternion.identity);

            if (goToInstantiate.name == "boxTile")
            {
                boxes.Add(goInstantiated.GetComponentInChildren<Box>());
            }
        }

        //debug
        Debug.Log("Level was loaded");
    }

    public void CheckIfWin()
    {
        Debug.Log("check if win");
        foreach (Box box in boxes)
        {
            if (box.isAllocated == false)
            {
                return;
            }
        }

        //If the loop arrives here, it means the level is won
        Win();

    }

    public void Win()
    {
        if (levelChangeChecker == false)
        {
            levelChangeChecker = true;
            Debug.Log("Win");
            CurrentLevel++;
            
            if (CurrentLevel <= TotalLevels)
            {
                ReloadScene();
            }else
            {
                CurrentLevel = 1;
                ReloadScene();
            }

        }
        
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
