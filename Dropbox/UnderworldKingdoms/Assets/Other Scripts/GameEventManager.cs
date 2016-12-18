using UnityEngine;

public static class GameEventManager {

	public delegate void GameEvent();
	
	public static event GameEvent WorldBuilt;
	public static event GameEvent GameOver;
	public static event GameEvent GameSave;
	
	public static void TriggerWorldBuilt() {
		Debug.Log("THE WORLD HAS THEORETICALLY BEEN BUILT.");
		if(WorldBuilt != null) {
			Debug.Log("THE WORLD HAS BEEN BUILT. gameeventmanagercall()");
			WorldBuilt();
		}
		else {
			Debug.Log(WorldBuilt);
		}
	}

	public static void TriggerGameOver() {
		Debug.Log ("GAME OVER.");
	}

	public static void TriggerGameSave() {
		if(GameSave != null) {
			GameSave();
		}
	}
}
