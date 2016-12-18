using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class World : Singleton<World> {

	public GameObject wallPrefab;
	public GameObject floorPrefab;
	public float tileSize;
	
	public TextMesh AINodeMesh;
	
	public int mapSizeX, mapSizeY;
	
	public List<AINodeGameObject> AINodeList;
	
	private int label;

	public char [,] mainWorld;
	public Tile [,] mainWorldTiles;
	
	private string worldFilePath = "TestWorld3.txt";

	void Awake () {
		GameEventManager.GameSave += HandleGameSave;
		//This code currently only checks a single, pre-built world file.
		//TODO: Create world generation code and create the ability to read multiple generated worlds.
		AINodeList = new List<AINodeGameObject>();
		if(!File.Exists(worldFilePath)) {
			Debug.Log("Wellp, it can't find the file that is clearly there.");
		}

		//Read the world file.
		StreamReader sr = File.OpenText (worldFilePath);
		mapSizeX = System.Convert.ToInt32 (sr.ReadLine ());
		mapSizeY = System.Convert.ToInt32 (sr.ReadLine ());
		Debug.Log(mapSizeX + " " + mapSizeY);

		//Initialize the world
		mainWorld = new char[mapSizeX,mapSizeY];
		mainWorldTiles = new Tile[mapSizeX, mapSizeY];

		//Put blank walls in each tile.
		for(int i = 0; i < mapSizeX; i++) {
			for(int j = 0; j < mapSizeY; j++) {
				mainWorld[i, j] = 'W';
			}
		}

		//Read file into floor
		label = mapSizeY - 1;
		while(sr.Peek () != -1) {
			string rawr = sr.ReadLine ();
			string[] rawrarr = rawr.Split (' ');
			for(int i = 0; i < rawrarr.Length; i++) {
				if(rawrarr[i].Length == 1) {
					mainWorld[i, label] = rawrarr[i][0];
				}
			}
			label--;
		}

		//Convert text characters into tiles.
		for(int i = 0; i < mapSizeX; i++) {
			for (int j = 0; j < mapSizeY; j++) {
				if(mainWorld[i,j] == 'F') {
					Instantiate(floorPrefab, new Vector3 (i*tileSize, j*tileSize, 0), Quaternion.identity);
					mainWorldTiles[i,j] = new Tile();
					mainWorldTiles[i,j].Initiate(i, j, mainWorld[i,j]);
				}
				else if (mainWorld[i,j] == 'W') {
					Instantiate(wallPrefab, new Vector3 (i*tileSize, j*tileSize, 0), Quaternion.identity);
					mainWorldTiles[i,j] = new Tile();
					mainWorldTiles[i,j].Initiate(i, j, mainWorld[i,j]);
				}
			}
		}
	}

	void HandleGameSave () {
		//Do Something? I doubt it.
	}
	
	void Start() {
		GameEventManager.TriggerWorldBuilt();
		Debug.Log ("World: All Clear");
	}
}
