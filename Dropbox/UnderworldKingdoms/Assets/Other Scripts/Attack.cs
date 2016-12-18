using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	public int damage;
	public int attackTime;

	private bool hit = false;

	void FixedUpdate() {
		attackTime--;
		if(attackTime <= 0) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log ("Collision");
		if(collision.gameObject.tag == "Monster" && !hit) {
			collision.gameObject.SendMessage("TakeDamage", damage);
			hit = true;
		}
	}
}
