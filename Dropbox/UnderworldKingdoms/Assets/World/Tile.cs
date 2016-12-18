using UnityEngine;
using System.Collections;

public class Tile {

	public int x, y;
	public char tileType;

	public AINode LinkedAINode;

	public void Initiate(int X, int Y, char TileType) {
		x = X;
		y = Y;
		tileType = TileType;
	}

	public void LinkAINode(AINode linking) {
		LinkedAINode = linking;
	}
}
