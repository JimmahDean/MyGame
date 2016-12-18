using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;

/*
*Originally created by Austin Locke.
*Creation Date: April 20, 2014, 7:57 PM
*/

//This class is used for handling world data and spawning world entities.
/*
public class BackupWorld : Singleton<BackupWorld> {

	public char[,] mainWorld;
	public List<AINode> AINodeList;
	public int mapSizeX;
	public int mapSizeY;
	public bool rerollWorld;
	public bool emptyWorld;
	public bool randomWorld;

	public TextMesh TextPrefab;
	public GameObject wallPrefab;
	public GameObject floorPrefab;

	private bool allClear = true;
	
	void Awake() {
		AINodeList = new List<AINode> ();
		string worldFilePath = "TestWorld.txt";
		//Read map from file. If no file exists, create a random one.
		if(!File.Exists(worldFilePath) || rerollWorld) {
			mainWorld = new char[mapSizeX,mapSizeY];
			rerollWorld = false;
			if(randomWorld) {
				for(int i = 0; i < mapSizeX; i++) {
					for(int j = 0; j < mapSizeY; j++) {
						int k = UnityEngine.Random.Range(0, 2);
						if(k == 0) {
							mainWorld[i,j] = 'F';
						}
						else if(k == 1) {
							mainWorld[i,j] = 'W';
						}
					}
				}
			}
			else if(emptyWorld) {
				for(int i = 0; i < mapSizeX; i++) {
					for(int j = 0; j < mapSizeY; j++) {
						if(i == 0 || i == mapSizeX - 1 || j == 0 || j == mapSizeY - 1) {
							mainWorld[i,j] = 'W';
						}
						else {
							mainWorld[i,j] = 'F';
						}
					}
				}
			}
			else {
				Debug.Log ("No Worldtype Selected. No new map created.");
				allClear = false;
			}
			if(allClear) {
				using (StreamWriter sw = File.CreateText(worldFilePath)) {
					sw.WriteLine(mapSizeX);
					sw.WriteLine(mapSizeY);
					for(int i = 0; i < mapSizeX; i++) {
						for(int j = 0; j < mapSizeY; j++) {
								sw.Write(mainWorld[i,j]);
						}
						sw.WriteLine();
					}
				}
			}
		}
		else {
			using(StreamReader sr = File.OpenText(worldFilePath)) {
				mapSizeX = Convert.ToInt32(sr.ReadLine());
				mapSizeY = Convert.ToInt32(sr.ReadLine());
				mainWorld = new char[mapSizeX,mapSizeY];
				Debug.Log (mapSizeX + ", " + mapSizeY);
				string inputString = "";
				for(int i = mapSizeY - 1; i >= 0; i--) {
					inputString = sr.ReadLine();
					for (int j = 0; j < mapSizeX; j++) {
						mainWorld[j,i] = inputString[j];
					}
				}
				sr.ReadLine ();
				for(int i = mapSizeY - 1; i >= 0; i--) {
					inputString = sr.ReadLine();
					bool tripped = false;
					int twoLengthNodes = 0;
					for(int j = 0; j < inputString.Length - 1; j++) {
						if(inputString[j] != '0') {
							AINode newNode = new AINode();
							string newString = "";
							newString += inputString[j];
							if(inputString[j + 1] != '0') {
								newString = newString + inputString[j + 1];
								tripped = true;
							}
							newNode.Initiate(new Vector2((j - twoLengthNodes) * 1.28f, i * 1.28f), newString);
							Debug.Log ("Node: " + newString + " " + twoLengthNodes);
							AINodeList.Add(newNode);
							if (tripped) {
								j++;
								twoLengthNodes++;
								tripped = false;
							}
						}
					}
				}
				Debug.Log (AINodeList.Count);
				sr.ReadLine ();
				while(sr.Peek () != -1) {
					/*string Label1 = "";
					Label1 += (char)sr.Read(); 
					char possibleChar = (char)sr.Read ();
					if(possibleChar != ' ') {
						Label1 += possibleChar;
						sr.Read ();
					}
					string Label2 = "";
					Label2 += (char)sr.Read ();
					possibleChar = (char)sr.Read ();
					if(possibleChar != '\n') {
						Label2 += possibleChar;
						sr.Read ();
					}*/
/*
					string Label1, Label2;

					string rawr = sr.ReadLine();
					string[] rawrarr = rawr.Split();

					Label1 = rawrarr[0];
					Label2 = rawrarr[1];

					AINode ActiveNode = null;
					AINode ConnectingNode = null;
					for( int i = 0; i < AINodeList.Count; i++) {
						if(ActiveNode == null) {
							if(AINodeList[i].Label == Label1) {
								ActiveNode = AINodeList[i];
							}
						}
						if(ConnectingNode == null) {
							if(AINodeList[i].Label == Label2) {
								ConnectingNode = AINodeList[i];
							}
						}
						if(ConnectingNode != null && ActiveNode != null) {
							ActiveNode.AddConnection(ConnectingNode);
							Debug.Log ("Connection Added. There should be about 50 of these. " + ActiveNode.Label + " + " + ConnectingNode.Label);
							break;
						}
						else if(i == AINodeList.Count - 1) {
							Debug.Log ("Connection Failed. This SHOULD NOT HAPPEN.");
						}
					}
				}
			}
		}
		for(int i = 0; i < mapSizeX; i++) {
			for (int j = 0; j < mapSizeY; j++) {
				if(mainWorld[i,j] == 'F') {
					Instantiate(floorPrefab, new Vector3 (i*1.28f, j*1.28f, 0), Quaternion.identity);
				}
				else if (mainWorld[i,j] == 'W') {
					Instantiate(wallPrefab, new Vector3 (i*1.28f, j*1.28f, 0), Quaternion.identity);
				}
			}
		}

		for(int i = 0; i < AINodeList.Count; i++) {
			TextMesh rawr = Instantiate (TextPrefab, AINodeList[i].Position, Quaternion.identity) as TextMesh;
			rawr.text = AINodeList[i].Label;
		}
	}
	
}
*/