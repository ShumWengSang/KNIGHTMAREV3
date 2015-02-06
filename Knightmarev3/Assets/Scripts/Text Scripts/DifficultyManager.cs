using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour {

	public static int movesLeft;
	public static int movesMade;
	public static int difficulty;

	Text text;
	
	// Use this for initialization
	void Awake () {
		text = GetComponent<Text> ();
		movesLeft = 10;
		movesMade = 0;
		difficulty = 1;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Moves left until spawn: " + movesLeft;
	}

	public bool MoveMade() {
		movesLeft--;
		movesMade++;
		if (movesLeft <= 0) {
			if (movesMade > difficulty * difficulty + 5)
				difficulty++;
			movesLeft = 10 - difficulty;
			if (movesLeft < 3)
				movesLeft = 3;
			return true;
		}
		return false;
	}
}
