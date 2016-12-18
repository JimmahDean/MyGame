using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//I DON'T KNOW WHAT THIS CLASS IS. COMMENTING UNTIL FURTHER NOTICE.
/*public class DungeonObject : MonoBehaviour {

	public int m_label;
	
	public int Label {
		get {
			return m_label;
		}
		set {
			m_label = value;
		}
	}
	
	public List<AINode> AINodes = new List<AINode>();
	
	void Awake() {
		GameEventManager.WorldBuilt += HandleWorldBuilt;
		Debug.Log("WorldBuilt Event Added");
	}
	
	/*void OnCollisionEnter2D(Collision2D col) {
		Debug.Log("Does This Ever Happen?");
		if(col.gameObject.tag == "Floor") {
			DungeonObject rawr = col.gameObject.GetComponent<DungeonObject>();
			for(int i = 0; i < rawr.AINodes.Count; i++) {
				if(collider.bounds.Contains(rawr.AINodes[i].Position)) {
					for(int j = 0; j < AINodes.Count; j++) {
						AINodes[j].AddConnection(rawr.AINodes[i]);
						rawr.AINodes[i].AddConnection(AINodes[j]);
						Debug.Log("Okay, so it actually happened. Here's the rundown: " + AINodes[j].Label + " " + rawr.AINodes[i].Label);
					}
				}
			}
		}
	}*//*
	void HandleWorldBuilt() {
		for(int i = 0; i < World.Instance.AINodeList.Count; i++) {
			Vector3 extents = GetComponent<BoxCollider2D>().size * 0.5f;
			Vector3 negExtents = -extents;
			extents += transform.position;
			negExtents += transform.position;
			if(contains(extents, negExtents, World.Instance.AINodeList[i].Position)) {
				for(int j = 0; j < AINodes.Count; j++) {
					AINodes[j].AddConnection(World.Instance.AINodeList[i]);
					World.Instance.AINodeList[i].AddConnection(AINodes[j]);
				}
			}
		}
	}

	bool contains(Vector3 minimum, Vector3 maximum, Vector2 point) {
		if(point.x > maximum.x && point.x < minimum.x) {
			if(point.y > maximum.y && point.y < minimum.y) {
				return true;
			}
		}
		return false;
	}
}*/
