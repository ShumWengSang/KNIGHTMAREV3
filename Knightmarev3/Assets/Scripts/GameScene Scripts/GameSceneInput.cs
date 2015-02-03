using UnityEngine;
using System.Collections;

public class GameSceneInput : MonoBehaviour {
	bool PieceSelected;
	GameObject Knight;
	Object Pawn;
	Vector3 oldVector;
	GameObject[] list;
	
	void Start () {
		PieceSelected = false;
		Pawn = Resources.Load("Prefab/Pieces/Pawn") as Object;
		SpawnPawn ();
		SpawnPawn ();
	}

	void SpawnPawn() {
		GameObject PawnObject = GameObject.Instantiate (Pawn) as GameObject;

		int spawnX = 0;
		int spawnY = 0;

		if (PawnObject.GetComponent<ThreatenBoxesPawn>().direction == 1) {
			spawnX = Random.Range(0, 7);
			spawnY = 0;
		}
		else if (PawnObject.GetComponent<ThreatenBoxesPawn>().direction == 2) {
			spawnX = Random.Range(0, 7);
			spawnY = 7;
		}
		else if (PawnObject.GetComponent<ThreatenBoxesPawn>().direction == 3) {
			spawnX = 7;
			spawnY = Random.Range(0, 7);
		}
		else if (PawnObject.GetComponent<ThreatenBoxesPawn>().direction == 4)
		{
			spawnX = 0;
			spawnY = Random.Range(0, 7);
		}

		PawnObject.transform.position = new Vector3 (spawnX, spawnY, -5);
		PawnObject.tag = "Chess Piece";
	}

	void MovePieces() {
		list = GameObject.FindGameObjectsWithTag("Chess Piece");
		foreach (GameObject Piece in list) {
			/*
			foreach (list) {
				if (Pawn.GetComponent<ThreatenBoxesPawn>().transform.position + Pawn.GetComponent<ThreatenBoxesPawn>().vel)
			}
*/
			if (Piece.name == "Pawn(Clone)")
			{
				if (Piece.GetComponent<ThreatenBoxesPawn>().direction == 1) {
					if (Piece.transform.position.y + 1 <= 7)
						Piece.transform.Translate(0, 1, 0);
				}
				else if (Piece.GetComponent<ThreatenBoxesPawn>().direction == 2) {
					if (Piece.transform.position.y - 1 >= 0)
						Piece.transform.Translate(0, 1, 0);
				}
				else if (Piece.GetComponent<ThreatenBoxesPawn>().direction == 3) {
					if (Piece.transform.position.x - 1 >= 0)
						Piece.transform.Translate(0, 1, 0);
				}
				else if (Piece.GetComponent<ThreatenBoxesPawn>().direction == 4) {
					if (Piece.transform.position.x + 1 <= 7) 
						Piece.transform.Translate(0, 1, 0);
				}
			}


		}
	}

	// Update is called once per frame
	void Update () {
#if UNITY_ANDROID
		if (Input.touchCount > 0)
		{
			foreach (Touch touch in Input.touches)
			{
				Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
				RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPoint.x,worldPoint.y),Vector2.zero);
				if(hit != null)
				{
					if(hit.collider.gameObject.tag == "Chess Piece")
					{
						hit.collider.gameObject.GetComponent<HighlightBox>().HighLight(true, true);
					}
				}
			}
		}
#else
		if(Input.GetMouseButtonUp(0))
		{
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Ray ray = new Ray(new Vector3(worldPoint.x,worldPoint.y,-10f), new Vector3(0,0,1));
			RaycastHit hit;

			if(Physics.Raycast(ray,out hit))
			{
				if(PieceSelected == false)
				{
					//GET CHESS PIECE. HIGHLIGHT THE PIECE
					//AND HIGHTLIGHT THE PLACES IT CAN GO
					if(hit.collider.gameObject.tag == "Knight")
					{
						PieceSelected = true;
						Knight = hit.collider.gameObject;
						Knight.GetComponent<HighlightBox>().HighLight(true, false);
						Knight.GetComponent<KnightMovementScript>().HightlightPossibleMoves(true,Knight.transform.position);
						oldVector = new Vector3(Knight.transform.position.x,Knight.transform.position.y);
					}
				}
				else
				{
					//THIS MEANS MOVE THE KNIGHT TO THE board piece
					if(hit.collider.gameObject.tag == "Board Piece")
					{
						//check whether you can move there, than move. else cancel movement.
						Knight.GetComponent<HighlightBox>().HighLight(false, false);
						float x = Mathf.Round(worldPoint.x );
						float y = Mathf.Round(worldPoint.y);
						if(Knight.GetComponent<KnightMovementScript>().MovementRuling(Knight.transform.position,new Vector3(x,y,Knight.transform.position.z)))
						{
							Knight.transform.position = new Vector3(x,y,Knight.transform.position.z);
							MovePieces();
						}
						Knight.GetComponent<KnightMovementScript>().HightlightPossibleMoves(false,oldVector);
					}
					//There is a piece the player moves here and eat it.
					else if(hit.collider.gameObject.tag == "Chess Piece")
					{
						float x = Mathf.Round(worldPoint.x );
						float y = Mathf.Round(worldPoint.y);
						if(Knight.GetComponent<KnightMovementScript>().MovementRuling(Knight.transform.position,new Vector3(x,y,Knight.transform.position.z)))
						{
							Knight.transform.position = new Vector3(x,y,Knight.transform.position.z);
							Destroy(hit.collider.gameObject);
							SpawnPawn();
							Knight.GetComponent<HighlightBox>().HighLight(false, true);
						}

						Knight.GetComponent<KnightMovementScript>().HightlightPossibleMoves(false,oldVector);
					}
					//Player hits the piece again, so he doesn't want to move
					else if(hit.collider.gameObject.tag == "Knight")
					{
						Knight.GetComponent<HighlightBox>().HighLight(false, false);
						Knight.GetComponent<KnightMovementScript>().HightlightPossibleMoves(false,oldVector);
					}

					//Either ways, don't select kngight anymore.
					PieceSelected = false;
				}
			}
		}
#endif
	}
}
