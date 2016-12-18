using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUITextScript : MonoBehaviour {

	private Text HPText;

	// Use this for initialization
	void Start () {
		HPText = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		HPText.text = MonsterManager.Instance.player.HP.ToString();
	}
}
