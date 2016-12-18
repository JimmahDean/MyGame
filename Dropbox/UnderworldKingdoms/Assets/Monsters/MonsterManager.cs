using UnityEngine;
using System.Collections;
using System.IO;

/*
*File Created by Austin Locke
*Creation Date: April 21, 2014 9:36 AM
*/

//TODO: The following code is useless aside from very early testing. Rewrite as needed.
public class MonsterManager : Singleton<MonsterManager> {

	public Monster monsterPrefab;
	public Player player;
	public Monster activeMonster;
	public AINode PlayerClosestNode;
	public AINode MonsterClosestNode;

	private float tileSize;

	private bool initiated = false;

	// Use this for initialization
	void Awake () {
		GameEventManager.GameSave += HandleGameSave;
		tileSize = World.Instance.tileSize;
		string monsterDataPath = "monsters.raw";
		if(File.Exists(monsterDataPath)) {
			Debug.Log ("Monsters: All Clear.");
		}
		else {
			Debug.Log ("If this occurs, fix it before moving on.");
		}

		//TODO: Add code to create monster attributes from the .raw file.

	}

	void HandleGameSave () {
		
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!initiated) {
			int randomX = Random.Range (1, World.Instance.mapSizeX);
			int randomY = Random.Range (1, World.Instance.mapSizeY);
			while(World.Instance.mainWorld[randomX,randomY] == 'W') {
				randomX = Random.Range (1, World.Instance.mapSizeX);
				randomY = Random.Range (1, World.Instance.mapSizeY);
			}
			activeMonster = Instantiate (monsterPrefab,
			                             new Vector3 (randomX * tileSize, randomY * tileSize, 0),
			                             Quaternion.identity) as Monster;
			Debug.Log (activeMonster.name);
			initiated = true; 
		}
		//TODO: This code is useless and only exists for pre-prototype testing. Please remove when able.

	}
}