using UnityEngine;
using System.Collections;

public class ThreatenBoxesPawn : MonoBehaviour {
	public int direction;
	public GameObject[] theGameObjects;
	public Vector3 vel;

	void Awake() {
		direction = Random.Range(1, 4); //1 = Heading up, 2 = down, 3 = left, 4 = right
		vel.Set (0, 1, 0);
	}

	void Start () {

		theGameObjects = GameObject.FindGameObjectsWithTag("Board Piece");
		if (direction == 2)
			this.transform.Rotate(0, 0, 180);
		else if (direction == 3)
			this.transform.Rotate(0, 0, 90);
		else if (direction == 4)
			this.transform.Rotate(0, 0, -90);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 roundPos = new Vector3 ();
		roundPos.x = Mathf.RoundToInt(this.transform.position.x);
		roundPos.y = Mathf.RoundToInt(this.transform.position.y);
		roundPos.z = Mathf.RoundToInt(this.transform.position.z);
		this.transform.position = roundPos;
	}

	void OnDestroy() {
		if (theGameObjects[0] != null)
		{
			for (int a = 0; a < theGameObjects.Length; a++) {
				Vector2 Bpos = new Vector2 ();
				Bpos.x = theGameObjects [a].transform.position.x;
				Bpos.y = theGameObjects [a].transform.position.y;
				if (MovementRuling(this.transform.position,Bpos)) {
					theGameObjects [a].GetComponent<HighlightBox> ().HighLight (false, false);
				}
			}
		}
	}

	public void Threathen(float x, float y, bool highlight)
	{
		for (int a = 0; a < theGameObjects.Length; a++) {
			Vector2 Bpos = new Vector2 ();
			Bpos.x = theGameObjects [a].transform.position.x;
			Bpos.y = theGameObjects [a].transform.position.y;
			if (MovementRuling(new Vector2(x,y),Bpos)) {
				theGameObjects [a].GetComponent<HighlightBox> ().HighLight (highlight, true);
			}
		}
	}
	/*
	public void MoveForward(Vector2 Pos, int ToMoveX, int ToMoveY)
	{
		for (int a = 0; a < theGameObjects.Length; a++) {
			Vector2 Bpos = new Vector2 ();
			Bpos.x = theGameObjects [a].transform.position.x;
			Bpos.y = theGameObjects [a].transform.position.y;
			if ((Pos.x - Bpos.x == ToMoveX) && (Pos.y - Bpos.y == ToMoveY)) {
				this.transform.Translate(theGameObjects [a].transform.position.x, theGameObjects [a].transform.position.y, 0);
				break;
			}
		}
	}
	*/

	public bool MovementRuling(Vector2 Pos, Vector2 ToMove)
	{
		float deltaX = Pos.x - ToMove.x;
		float deltaY = Pos.y - ToMove.y;
		bool CanMove = false;

		if (direction == 1) //up
		{
			if ((deltaY == -1 && deltaX == 1) || (deltaY == -1 && deltaX == -1)) {
				CanMove = true;
			}
		} 
		else if (direction == 2) //down
		{
			if ((deltaY == 1 && deltaX == 1) || (deltaY == 1 && deltaX == -1)) {
				CanMove = true;
			}
		} 
		else if (direction == 3) //left
		{
			if ((deltaY == 1 && deltaX == 1) || (deltaY == -1 && deltaX == 1)) {
				CanMove = true;
			}
		} 
		else if (direction == 4) //right
		{
			if ((deltaY == 1 && deltaX == -1) || (deltaY == -1 && deltaX == -1)) {
				CanMove = true;
			}

		}
		return CanMove;
	}
}
