using UnityEngine;
using System.Collections;

/*
*Originally created by Austin Locke.
*Creation Date: April 20, 2014, 7:51 PM
*/

//This class is designed to hold all variables that define the player.
//Definitions include inventory, attributes, levels, etc.
//This class should only be used as information, and should contain no methods aside from Start() and update, and collisions, apparently.

public class Player : MonoBehaviour {
	
	private double m_hp = 10;

	public double HP {
		get { return m_hp; }
		set { m_hp = value; }
	}
	
	//This variable is stored for the sole purpose of possibly needing it in the future.
	//It may be removed.
	private PlayerControl controller;
	
	void Start() {
		controller = gameObject.GetComponent<PlayerControl>();
	}
	
	void Update() {
		if(m_hp <= 0) {
			GameObject.Destroy(gameObject);
			GameEventManager.TriggerGameOver();
		}
	}

	//Player takes damage.
	void TakeDamage(double dam) {
		m_hp -= dam;
	}

}
