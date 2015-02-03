using UnityEngine;
using System.Collections;
using System;

public class KnightMovementScript : MonoBehaviour {

	float [,] theBoard;
	GameObject[] theGameObjects;

	// Use this for initialization
	void Start () {
		theBoard = new float [8,8];
		theGameObjects = GameObject.FindGameObjectsWithTag("Board Piece");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public bool MovementRuling(Vector3 Pos, Vector3 toMove)
	{
		float deltaX = Pos.x - toMove.x;
		float deltaY = Pos.y - toMove.y;
		bool CanMove = false;

		// Knight move in a L movement. distance is evaluated by a multiplication of both direction
		if((deltaX != 0 && deltaY != 0) && Mathf.Abs(deltaX*deltaY) == 2)
		{
			CanMove = true;
		}
		return CanMove;
	}

	public void HightlightPossibleMoves(bool highlight, Vector2 Pos)
	{
		Vector3 CuncurrentBoard;
		Array.Clear (theBoard, 0, theBoard.Length);

//This could be optimized and cleaner
		theBoard [(int)Pos.x, (int)Pos.y] = 1;
		for (int i = 0; i < 9; i ++) {
				for (int j = 0; j < 9; j++) {
						CuncurrentBoard = new Vector3 (i, j, 0);
						if (MovementRuling (new Vector3 (Pos.x, Pos.y, 0), CuncurrentBoard)) {
								for (int a = 0; a < theGameObjects.Length; a++) {
										//if(a==63)
										//{
										//int asss=0;
										//}
										Vector3 Bpos = new Vector2 ();
										Bpos.x = theGameObjects [a].transform.position.x;
										Bpos.y = theGameObjects [a].transform.position.y;
										Bpos.z = 0;
										if (CuncurrentBoard == Bpos) {
												theGameObjects [a].GetComponent<HighlightBox> ().HighLight (highlight, false);
										}
								}
						}
				}
		}
	}
}
