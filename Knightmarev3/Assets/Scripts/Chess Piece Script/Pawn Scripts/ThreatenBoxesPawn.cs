using UnityEngine;
using System.Collections;

public class ThreatenBoxesPawn : MonoBehaviour {

	GameObject[] theGameObjects;
	// Use this for initialization
	void Start () {
		theGameObjects = GameObject.FindGameObjectsWithTag("Board Piece");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Threathen(float x, float y, bool highlight)
	{
		for (int a = 0; a < theGameObjects.Length; a++) {
			Vector2 Bpos = new Vector2 ();
			Bpos.x = theGameObjects [a].transform.position.x;
			Bpos.y = theGameObjects [a].transform.position.y;
			if (MovementRuling(new Vector2(x,y),Bpos)) {
				theGameObjects [a].GetComponent<HighlightBox> ().HighLight (highlight);
			}
		}
	}
	
	public bool MovementRuling(Vector2 Pos, Vector2 ToMove)
	{
		float deltaX = Pos.x - ToMove.x;
		float deltaY = Pos.y - ToMove.y;
		bool CanMove = false;
		
		if(deltaY == 1)
		{
			//Debug.Log ("_deltaY");
			CanMove = true;
		}
		return CanMove;
	}
}
