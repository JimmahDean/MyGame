using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
* File created before November 9, 2014
* File created by Austin Locke
*/

public class AIManager : Singleton<AIManager> {

	public AINodeGameObject AIPrefab;

	private float tileSize;
	private List<AINodeGameObject> AINodeList;
	private int AILabel = 0;
	
	void Start () {
		tileSize = World.Instance.tileSize;
		AINodeList = new List<AINodeGameObject> ();

		for(int i = 1; i < World.Instance.mapSizeY - 1; i++) {
			for(int j = 1; j < World.Instance.mapSizeX - 1; j++) {

				//Do not link walls to anything, they are impassable
				if(World.Instance.mainWorldTiles[j,i].tileType == 'W') {
					continue;
				}
				//Check if current tile is a floor.
				else if(World.Instance.mainWorldTiles[j,i].tileType == 'F') {

					bool ignoreNorth = false;
					bool ignoreSouth = false;
					bool ignoreEast = false;
					bool ignoreWest = false;

					/*Check adjacent sides for possible direction changes.
					 *If adjacent side is a wall, check if a corner, then ignore perpendicular sides.
					 *If adjacent side has adjacent walls, place node.
					 */

					//TODO: LOW PRIORITY: Fix algorithm so dead ends are not AI nodes, they serve no purpose.

					if(World.Instance.mainWorldTiles[j-1,i].tileType == 'W') {
						if(World.Instance.mainWorldTiles[j+1,i+1].tileType == 'W') {
							if(World.Instance.mainWorldTiles[j,i-1].tileType == 'W') {
								CreateAINode(j,i);
								continue;
							}
						}
						if(World.Instance.mainWorldTiles[j+1,i-1].tileType == 'W') {
							if(World.Instance.mainWorldTiles[j,i+1].tileType == 'W') {
								CreateAINode(j,i);
								continue;
							}
						}
						ignoreEast = true;
						ignoreWest = true;
					}
					if(World.Instance.mainWorldTiles[j+1,i].tileType == 'W') {
						if(World.Instance.mainWorldTiles[j-1,i+1].tileType == 'W') {
							if(World.Instance.mainWorldTiles[j,i-1].tileType == 'W') {
								CreateAINode(j,i);
								continue;
							}
						}
						if(World.Instance.mainWorldTiles[j-1,i-1].tileType == 'W') {
							if(World.Instance.mainWorldTiles[j,i+1].tileType == 'W') {
								CreateAINode(j,i);
								continue;
							}
						}
						ignoreEast = true;
						ignoreWest = true;
					}
					if(World.Instance.mainWorldTiles[j,i-1].tileType == 'W') {
						if(World.Instance.mainWorldTiles[j+1,i].tileType == 'W') {
							if(World.Instance.mainWorldTiles[j-1,i+1].tileType == 'W') {
								CreateAINode(j,i);
								continue;
							}
						}
						if(World.Instance.mainWorldTiles[j-1,i].tileType == 'W') {
							if(World.Instance.mainWorldTiles[j+1,i+1].tileType == 'W') {
								CreateAINode(j,i);
								continue;
							}
						}
						ignoreNorth = true;
						ignoreSouth = true;
					}
					if(World.Instance.mainWorldTiles[j,i+1].tileType == 'W') {
						if(World.Instance.mainWorldTiles[j-1,i].tileType == 'W') {
							if(World.Instance.mainWorldTiles[j+1,i-1].tileType == 'W') {
								CreateAINode(j,i);
								continue;
							}
						}
						if(World.Instance.mainWorldTiles[j+1,i].tileType == 'W') {
							if(World.Instance.mainWorldTiles[j-1,i-1].tileType == 'W') {
								CreateAINode(j,i);
								continue;
							}
						}
						ignoreNorth = true;
						ignoreSouth = true;
					}
					//The above directions are a guess. If they are wrong, the code should still work.

					if(World.Instance.mainWorldTiles[j-1,i].tileType == 'F' && !ignoreNorth) {
						if(World.Instance.mainWorldTiles[j-1,i+1].tileType == 'W') {
							CreateAINode(j,i);
							continue;
						}
						if(World.Instance.mainWorldTiles[j-1,i-1].tileType == 'W') {
							CreateAINode(j,i);
							continue;
						}
					}
					if(World.Instance.mainWorldTiles[j+1,i].tileType == 'F' && !ignoreSouth) {
						if(World.Instance.mainWorldTiles[j+1,i+1].tileType == 'W') {
							CreateAINode(j,i);
							continue;
						}
						if(World.Instance.mainWorldTiles[j+1,i-1].tileType == 'W') {
							CreateAINode(j,i);
							continue;
						}

					}
					if(World.Instance.mainWorldTiles[j,i-1].tileType == 'F' && !ignoreEast) {
						if(World.Instance.mainWorldTiles[j-1,i-1].tileType == 'W') {
							CreateAINode(j,i);
							continue;
						}
						if(World.Instance.mainWorldTiles[j+1,i-1].tileType == 'W') {
							CreateAINode(j,i);
							continue;
						}
					}
					if(World.Instance.mainWorldTiles[j,i+1].tileType == 'F' && !ignoreWest) {
						if(World.Instance.mainWorldTiles[j-1,i+1].tileType == 'W') {
							CreateAINode(j,i);
							continue;
						}
						if(World.Instance.mainWorldTiles[j+1,i+1].tileType == 'W') {
							CreateAINode(j,i);
							continue;
						}
					}
				}	
			}
		}

		//Check node connections and connect nodes that can see each other.

		Debug.Log (AINodeList [1].transform.position);

		for (int i = 0; i < AINodeList.Count; i++) {
			for(int j = 0; j < AINodeList.Count; j++) {

				//If checking the node against itself, do not.
				if(i == j) {
					continue;
				}

				Vector2 start = AINodeList[i].transform.position;
				Vector2 end = AINodeList[j].transform.position;

				int layerMask = 1 << 9;

				RaycastHit2D hit = Physics2D.Linecast(start, end, layerMask);

				if (hit.collider != null && hit.collider != AINodeList[i].GetComponent<Collider>()) {
					if(hit.collider.tag == "Wall") {
						//Debug.Log ("WALL!");
						continue;
					}
				}
				else {
					//Debug.DrawLine(start, hit.transform.position, Color.white, 20);
						bool connectionExists = false;

						//Check that the connection does not already exist.
						if(AINodeList[i].attachedAINode.Connections.Count > 0) {
							for(int k = 0; k < AINodeList[i].attachedAINode.Connections.Count; k++) {
								if(AINodeList[i].attachedAINode.Connections[k].Label == AINodeList[j].attachedAINode.Label) {
									connectionExists = true;
								}
							}
						}

						//Add connection to the node if connection does not already exist.
						if(!connectionExists) {
							AINodeList[i].attachedAINode.AddConnection(AINodeList[j].attachedAINode);
							AINodeList[j].attachedAINode.AddConnection(AINodeList[i].attachedAINode);
							Debug.DrawLine(AINodeList[i].transform.position, AINodeList[j].transform.position, Color.white, 20);
						}
						else continue;
					}
				}
			//Debug.Log (i);
		}
		World.Instance.AINodeList = AINodeList;
	}

	// Create the gameobject with collider along with the accompanied AINode variables.
	void CreateAINode(int j, int i) {
		//Create the node game object
		AINodeGameObject newAINode;
		newAINode = Instantiate (AIPrefab, new Vector3 (j * tileSize, i * tileSize, 0), Quaternion.identity) as AINodeGameObject;

		//Initiate the node's pathfinding variables
		AINode newAINodeTwo = new AINode();
		newAINodeTwo.Initiate (new Vector2 (j * tileSize, i * tileSize), AILabel);

		//Connect node's object with variables.
		newAINode.Initiate(AILabel, newAINodeTwo);

		//Increment label number and add node to the list of nodes.
		AILabel++;
		AINodeList.Add (newAINode);
	}
}
