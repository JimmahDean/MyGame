using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*
*File created by Austin Locke
*Creation Date: April 21, 2014 9:25 AM
*/

//This file is used to hold the attributes of all sorts of monsters. Variables are public for manual editting of attributes in Unity.

public class Monster : MonoBehaviour {

	public float movementSpeed = 0.6f;
	public int hp;
	public float aggroDistance;
	public string name;
	public bool AI_Toggle = false; 

	public float minDistance;
	public float maxDistance;
	public float attackDistance;

	private List<AINode> movePath;
	private bool pathFound = false;
	private bool playerSeen = false;
	private int fsmState = 0;
	private int AIPlayerPathCalculationTime = 0;
	private int currentStep = 0;
	private int attackTime = 0;

	private enum EFsmState : int {IDLE, CHASE, ATTACK};

	public MonsterAttack AttackType;

	public int Location;

	//TODO: Make the following line of code include all summoned units, not just the player.
	//TODO: This edit can only take place after summons have been implemented.
	private Player player;

	void Start () {
		movePath = new List<AINode> ();
		player = MonsterManager.Instance.player;
		Debug.Log (Vector3.forward * 0.6f);
	}
	
	// Update is called once per frame

	//TODO: Move entire movement code block to FixedUpdate().
	void Update () {
		if(Idle ()) {
			if(Vector2.Distance(transform.position, player.transform.position) < minDistance) {
				fsmState = (int)EFsmState.CHASE;
			}
		}
		else if(Chase ()) {

			//Player is seen, no pathfinding needs to occur.
			if(playerSeen) {
				transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * movementSpeed);
			}
			//Player is not seen
			else {
				if(movePath != null) {
					transform.position = Vector2.MoveTowards (transform.position, movePath [currentStep].Position, Time.deltaTime * movementSpeed);
				}
			}
			//Idle if player is too far away.
			if(Vector2.Distance(transform.position, player.transform.position) > maxDistance) {
				fsmState = (int)EFsmState.IDLE;
			}
			//Attack if player is close enough.
			if(Vector2.Distance(transform.position, player.transform.position) < attackDistance) {
				fsmState = (int)EFsmState.ATTACK;
			}
		}
		else if(Attack ()) {
			if(Vector2.Distance(transform.position, player.transform.position) > attackDistance) {
				fsmState = (int)EFsmState.CHASE;
			}
			else if(attackTime <= 0) {
				//Okay. Here's how this works:
				//First, the attack is created at the monster's position.
				MonsterAttack rawr = Instantiate(AttackType, transform.position, Quaternion.identity) as MonsterAttack;

				//Second, the attack is given a power.
				rawr.Initiate(5);

				//Third, the attack is moved towards the player.
				rawr.transform.parent = transform;
				rawr.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1.4f);

				//Fourth, math is done to find the rotation of the attack.
				//This is done so if the monster has a narrow attack, such as a spear or arrow, the attack still looks like it originates from the monster.
				float diffX = (rawr.transform.position.x - player.transform.position.x) * (-1);
				float diffY = (rawr.transform.position.y - player.transform.position.y) * (-1);

				//Fifth, more math. (Same as fourth)
				float rotationRads = Mathf.Tan(diffX / diffY);
				float rotationDegs = rotationRads * Mathf.Rad2Deg;

				//Sixth, the rotation happens.
				rawr.transform.Rotate (new Vector3 (0, 0, -rotationDegs), Space.World);

				//Seventh, the attack timer is reset.
				attackTime = 100;
				Debug.Log ("An attack happened!");
			}
		}

		//If no health, monster ded.
		if (hp <= 0) {
			Destroy (gameObject);
		}
		//Decrement attack timer.
		attackTime--;
	}
	void TakeDamage(int damage) {
		hp -= damage;
	}

	bool Idle() {
		if(fsmState == (int)EFsmState.IDLE) {
			return true;
		}
		return false;
	}

	bool Chase() {
		if (fsmState == (int)EFsmState.CHASE) {
			return true;
		}
		return false;
	}

	bool Attack() {
		if(fsmState == (int)EFsmState.ATTACK) {
			return true;
		}
		return false;
	}

	void FixedUpdate() {
		AIPlayerPathCalculationTime++;

		if(AIPlayerPathCalculationTime > 5 && Chase ()) {

			//NEVER CHANGE THE FOLLOWING CODE.
			int layerMask = 1 << 9; //WALLS ARE LAYER LEVEL 9. THIS NUMBER CANNOT CHANGE. YOU WILL BREAK THE GAME.
			//NEVER CHANGE THE PREVIOUS CODE.

			//Check if player is in sight, no pathfinding is required if player is in sight.
			{
				Vector2 start = transform.position;
				Vector2 end = player.transform.position;

				RaycastHit2D hit = Physics2D.Linecast(start, end, layerMask);

				if(hit.collider != null) {
					playerSeen = false;
				}
				else {
					playerSeen = true;
				}
			}

			if(!playerSeen) {

				//TODO: LOW PRIORITY: Check if path to player already exists from closest node. If so, use said path.

				float distance = 999.0f;
				float distance2 = 999.0f;
				AINode closestNode = null;
				AINode closestNode2 = null;

				List<AINode> possibleNodes = new List<AINode> ();
				List<AINode> possiblePlayerNodes = new List<AINode> ();

				//MONSTER CLOSEST NODE
				for (int i = 0; i < World.Instance.AINodeList.Count; i++) {					
					Vector2 start = transform.position;
					Vector2 end = World.Instance.AINodeList[i].transform.position;
					
					RaycastHit2D hit = Physics2D.Linecast(start, end, layerMask);
						
					if (hit.collider != null) {
						//This is not a possible node.
						continue;
					}
					else {
						//This is a possible node.
						possibleNodes.Add(World.Instance.AINodeList[i].attachedAINode);
					}
				}
			
	            for(int i = 0; i < possibleNodes.Count; i++) {
					float tempDistance = Vector2.Distance(transform.position, possibleNodes[i].Position);
					if(tempDistance < distance) {
						distance = tempDistance;
						closestNode = possibleNodes[i];
					}
				}
				//END MONSTER CLOSEST NODE

				//PLAYER CLOSEST NODE
				for (int i = 0; i < World.Instance.AINodeList.Count; i++) {					
					Vector2 start = player.transform.position;
					Vector2 end = World.Instance.AINodeList[i].transform.position;
				
					start = Vector2.MoveTowards(start, end, 1f);
				
					RaycastHit2D hit = Physics2D.Linecast(start, end, layerMask);
				
					if (hit.collider != null) {
						//This is not a possible node.
						continue;
					}
					else {
						//This is a possible node.
						possiblePlayerNodes.Add(World.Instance.AINodeList[i].attachedAINode);
					}
				}
				
				for(int i = 0; i < possiblePlayerNodes.Count; i++) {
					float tempDistance = Vector2.Distance(player.transform.position, possiblePlayerNodes[i].Position);
					if(tempDistance < distance2) {
						distance2 = tempDistance;
						closestNode2 = possiblePlayerNodes[i];
					}
				}

				Debug.DrawLine(player.transform.position, closestNode2.Position, Color.green, 0.1f);
				for(int i = 0; i < possiblePlayerNodes.Count; i++) {
					Debug.DrawLine(player.transform.position, possiblePlayerNodes[i].Position, Color.red, 0.1f);
				}
				//END PLAYER CLOSEST NODE

				//Calculate path.
				movePath = AStarHelper.Calculate(closestNode, closestNode2);

				//If possible node is along path, start there instead.
				int pathStart = 0;

				for(int i = 0; i < possibleNodes.Count; i++) {
					for(int j = 0; j < movePath.Count; j++) {
						if(possibleNodes[i] == movePath[j] && j > pathStart) {
							pathStart = j;
						}
					}
				}
				currentStep = pathStart;
				AIPlayerPathCalculationTime = 0;
			}
		}
	}

	//When monster enters a node, monster then begins move to next node.
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "AINode" && currentStep < movePath.Count - 1) {
			currentStep++;
		}
	}
}