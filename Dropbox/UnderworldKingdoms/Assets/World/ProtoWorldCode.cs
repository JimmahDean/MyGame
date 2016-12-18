using UnityEngine;
using System.Collections;
using System.IO;

public class ProtoWorldCode : Singleton<ProtoWorldCode> {

	public Transform CorridorPrefab;
	public Transform RoomPrefab;

	char [,] mainWorld;
	
	string worldFilePath = "TestWorld2.txt";

	void Awake () {
		if(!File.Exists(worldFilePath)) {
			Debug.Log("Wellp, it can't find the file that is clearly there.");
		}
		StreamReader sr = File.OpenText (worldFilePath);
		int mapSizeX = System.Convert.ToInt32 (sr.ReadLine ());
		int mapSizeY = System.Convert.ToInt32 (sr.ReadLine ());
		Debug.Log(mapSizeX + " " + mapSizeY);
		sr.ReadLine();
		mainWorld = new char[mapSizeX,mapSizeY];
		for(int i = 0; i < mapSizeX; i++) {
			for(int j = 0; j < mapSizeY; j++) {
				mainWorld[j, i] = 'W';
			}
		}
		while(sr.Peek () != -1) {
			string rawr = sr.ReadLine ();
			Debug.Log(rawr);
			string[] rawrarr = rawr.Split ();
			int type = System.Convert.ToInt32 (rawrarr[0]);
			int coordX = System.Convert.ToInt32 (rawrarr[1]);
			int coordY = System.Convert.ToInt32 (rawrarr[2]);
			char direction = (char)rawrarr[3][0];
			int sizeX = System.Convert.ToInt32 (rawrarr[4]);
			if(type == 1) {
				int sizeY = System.Convert.ToInt32 (rawrarr[5]);
				CreateRoom (direction, sizeX, sizeY, coordX, coordY);
			}
			else if (type == 0) {
				CreateCorridor(direction, sizeX, coordX, coordY);
			}

		}
	}

	void CreateCorridor(char direction, int sizeX, int coordX, int coordY) {
		for(int i = 0; i < sizeX; i++) {
			switch(direction) {
			case 'n':
				mainWorld[coordX,coordY + i] = 'F';
				break;
			case 's':
				mainWorld[coordX, coordY - i] = 'F';
				break;
			case 'e':
				mainWorld[coordX + i, coordY] = 'F';
				break;
			case 'w':
				mainWorld[coordX - i, coordY] = 'F';
				break;
			}
		}
		
		Vector2 newSize = new Vector2(1.28f, 1.28f);
		Vector2 newPosition = new Vector2(0, 0);

		switch(direction) {
		case 'n':
			newSize = new Vector2(1.28f, sizeX * 1.28f);
			newPosition = new Vector2(coordX * 1.28f, (coordY * 1.28f) + ((sizeX / 2) * 1.28f));
			break;
		case 's':
			newSize = new Vector2(1.28f, sizeX * 1.28f);
			newPosition = new Vector2(coordX * 1.28f, (coordY * 1.28f) - (sizeX / 2) * 1.28f);
			break;
		case 'w':
			newSize = new Vector2(sizeX * 1.28f, 1.28f);
			newPosition = new Vector2((coordX * 1.28f) + (sizeX / 2) * 1.28f, coordY * 1.28f);
			break;
		case 'e':
			newSize = new Vector2(sizeX * 1.28f, 1.28f);
			newPosition = new Vector2((coordX * 1.28f) - (sizeX / 2) * 1.28f, coordY * 1.28f);
			break;
		}
		Transform newCorridor = Instantiate(CorridorPrefab, new Vector2(coordX * 1.28f, coordY * 1.28f), Quaternion.identity) as Transform;
		newCorridor.localScale = newSize;
		newCorridor.position = newPosition;
	}

	void CreateRoom(char direction, int sizeX, int sizeY, int coordX, int coordY) {
		
	}
}
