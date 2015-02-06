using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour {
	public static int combo;
	
	Text text;
	
	// Use this for initialization
	void Awake () {
		text = GetComponent<Text> ();
		combo = 1;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Combo: " + combo;
	}
}
