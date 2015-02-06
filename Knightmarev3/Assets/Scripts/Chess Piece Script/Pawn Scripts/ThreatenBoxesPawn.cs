using UnityEngine;
using System.Collections;

public class ThreatenBoxesPawn : MonoBehaviour {
	public int direction;
	public GameObject[] theBoardObjects;
	public GameObject[] Pieces;
	public Vector3 vel;

	public Vector3 destination;

	public bool moving = false;

	float smoothness = 5.0f;

	void Awake() {
		//direction = 2;
		direction = Random.Range(1, 4); //1 = Heading up, 2 = down, 3 = left, 4 = right
		if (direction == 1)
			vel.Set (0, 1, 0);
		else if (direction == 2)
			vel.Set (0, -1, 0);
		else if (direction == 3)
			vel.Set (-1, 0, 0);
		else if (direction == 4)
			vel.Set (1, 0, 0);
	}

	void Start () {

		theBoardObjects = GameObject.FindGameObjectsWithTag("Board Piece");
		if (direction == 2)
			this.transform.Rotate(0, 0, 180);
		else if (direction == 3)
			this.transform.Rotate(0, 0, 90);
		else if (direction == 4)
			this.transform.Rotate(0, 0, -90);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnDestroy() {
		if (theBoardObjects[0] != null)
		{
			for (int a = 0; a < theBoardObjects.Length; a++) {
				Vector2 Bpos = new Vector2 ();
				Bpos.x = theBoardObjects [a].transform.position.x;
				Bpos.y = theBoardObjects [a].transform.position.y;
				if (MovementRuling(this.transform.position,Bpos)) {
					theBoardObjects [a].GetComponent<HighlightBox> ().HighLight (false, false);
				}
			}
		}
	}

	public void Threathen(float x, float y, bool highlight)
	{
		for (int a = 0; a < theBoardObjects.Length; a++) {
			Vector2 Bpos = new Vector2 ();
			Bpos.x = theBoardObjects [a].transform.position.x;
			Bpos.y = theBoardObjects [a].transform.position.y;
			if (MovementRuling(new Vector2(x,y),Bpos)) {
				theBoardObjects [a].GetComponent<HighlightBox> ().HighLight (highlight, true);
			}
		}
	}

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

	public bool StartMoving() //Calculates where the pawn needs to go
	{
		moving = true;
		Threathen(this.transform.position.x, this.transform.position.y, false);
		Pieces = GameObject.FindGameObjectsWithTag("Chess Piece");
		if (direction == 1) {
			if (this.transform.position.y + 1 <= 7) {
				for (int i = 0; i < Pieces.Length; i++) {
					if (Pieces[i].transform.position.y == this.transform.position.y + 1 && Pieces[i].transform.position.x == this.transform.position.x)
						return false;
				}
				destination = this.transform.position + vel;
				return true;
			}
		}
		else if (direction == 2) {
			if (this.transform.position.y - 1 >= 0) {
				for (int i = 0; i < Pieces.Length; i++) {
					if (Pieces[i].transform.position.y == this.transform.position.y - 1 && Pieces[i].transform.position.x == this.transform.position.x)
						return false;
				}
				destination = this.transform.position + vel;
				return true;
			}
		}
		else if (direction == 3) {
			if (this.transform.position.x - 1 >= 0) {
				for (int i = 0; i < Pieces.Length; i++) {
					if (Pieces[i].transform.position.x == this.transform.position.x - 1 && Pieces[i].transform.position.y == this.transform.position.y)
						return false;
				}
				destination = this.transform.position + vel;
				return true;
			}
		}
		else if (direction == 4) {
			if (this.transform.position.x + 1 <= 7) {
				for (int i = 0; i < Pieces.Length; i++) {
					if (Pieces[i].transform.position.x == this.transform.position.x + 1 && Pieces[i].transform.position.y == this.transform.position.y)
						return false;
				}
				destination = this.transform.position + vel;
				return true;
			}
		}
		return false;
	}

	public void MoveForward() //Function called to move the pawn
	{
		while (destination.x == 0 && destination.y == 0 && destination.z == 0) {
			destination = this.transform.position + vel;
		}
		this.transform.position =  Vector3.Lerp(this.transform.position, destination, Time.deltaTime * smoothness);
		if (Mathf.Abs(this.transform.position.x - destination.x) < 0.01 //Lerp will keep gradually incrementing so this cuts it short
		    && Mathf.Abs(this.transform.position.y - destination.y) < 0.01) {
			StopMoving ();
			//return false;
		}
		//return true;
	}


	public bool StopMoving()
	{
		moving = false;
		Vector3 roundPos = new Vector3 ();
		roundPos.x = Mathf.RoundToInt(this.transform.position.x);
		roundPos.y = Mathf.RoundToInt(this.transform.position.y);
		roundPos.z = Mathf.RoundToInt(this.transform.position.z);
		this.transform.position = roundPos;


		switch (direction) 
		{
		case 1:
			if (this.transform.position.y == 7)
				Promote ();
			break;
		case 2:
			if (this.transform.position.y == 0)
				Promote ();
			break;
		case 3:
			if (this.transform.position.x == 7)
				Promote ();
			break;
		case 4:
			if (this.transform.position.x == 0)
				Promote ();
			break;
		}


		return true;
		//playerTurn = true;
	}

	public void Promote()
	{
		//moving = false;
		//Destroy (this);
	}
}
