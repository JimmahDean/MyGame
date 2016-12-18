using UnityEngine;
using System.Collections;

/*
*Originally created by Austin Locke.
*Creation Date: April 20, 2014, 7:35 PM
*/

//This is the class which controls the player's movement and actions.
//This file should only contain keyboard input, and should be the only file that does.

public class PlayerControl : MonoBehaviour {

	public float movementSpeed;
	public Attack melee;
		
	void Start () {
	
	}
	
	void FixedUpdate () {
		if(Input.GetKey(KeyCode.A)) {
			transform.Translate(-movementSpeed, 0, 0);
		}
		if(Input.GetKey(KeyCode.D)) {
			transform.Translate(movementSpeed, 0, 0);
		}
		if(Input.GetKey(KeyCode.W)) {
			transform.Translate(0, movementSpeed, 0);
		}
		if(Input.GetKey(KeyCode.S)) {
			transform.Translate(0, -movementSpeed, 0);
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			Attack rawr = Instantiate(melee, new Vector3(transform.position.x - 0.6f, transform.position.y, 0), Quaternion.identity) as Attack;
			rawr.transform.parent = transform;
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)) {
			Attack rawr = Instantiate(melee, new Vector3(transform.position.x + 0.6f, transform.position.y, 0), Quaternion.identity) as Attack;
			rawr.transform.parent = transform;
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			Attack rawr = Instantiate(melee, new Vector3(transform.position.x, transform.position.y + 0.6f, 0), Quaternion.identity) as Attack;
			rawr.transform.parent = transform;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			Attack rawr = Instantiate(melee, new Vector3(transform.position.x, transform.position.y - 0.6f, 0), Quaternion.identity) as Attack;
			rawr.transform.parent = transform;
		}
		if(Input.GetKeyDown(KeyCode.LeftControl)) {
			GameEventManager.TriggerGameSave();
		}
	}
	
	/*void OnCollisionEnter() {
		Debug.Log("Collision! :D");
	}*/
}
