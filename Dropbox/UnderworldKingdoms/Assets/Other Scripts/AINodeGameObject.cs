using UnityEngine;
using System.Collections;

public class AINodeGameObject : MonoBehaviour {

	private AINode m_AINode;
	public int Label = 15;

	public AINode attachedAINode
	{
		get { return m_AINode; }
	}
	

	void Start() {
		//Do nothing.
	}

	public void Initiate(int label, AINode node) {
		transform.GetComponent<TextMesh>().text = label.ToString();
		Label = label;
		m_AINode = node;
	}
}
