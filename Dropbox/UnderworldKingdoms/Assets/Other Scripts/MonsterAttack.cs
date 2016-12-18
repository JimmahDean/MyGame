using UnityEngine;
using System.Collections;

public class MonsterAttack : MonoBehaviour {
	
	public int damage;
	public int attackTime = 20;
	
	private bool hit = false;
	
	void FixedUpdate() {
		attackTime--;
		if(attackTime <= 0) {
			Destroy (gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log ("Collision");
		if(collision.gameObject.tag == "Player" && !hit) {
			collision.gameObject.SendMessage("TakeDamage", damage);
			hit = true;
		}
	}

	public void Initiate(int dam) {
		damage = dam;
		//attackTime = time;
	}
}
