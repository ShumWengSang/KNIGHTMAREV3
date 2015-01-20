using UnityEngine;
using System.Collections;

public class GameSceneInput : MonoBehaviour {
	bool PieceSelected;
	GameObject Knight;
	Vector3 oldVector;
	// Use this for initialization
	void Start () {
		PieceSelected = false;
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
						hit.collider.gameObject.GetComponent<HighlightBox>().HighLight(true);
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
						Knight.GetComponent<HighlightBox>().HighLight(true);
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
						Knight.GetComponent<HighlightBox>().HighLight(false);
						float x = Mathf.Round(worldPoint.x );
						float y = Mathf.Round(worldPoint.y);
						if(Knight.GetComponent<KnightMovementScript>().MovementRuling(Knight.transform.position,new Vector3(x,y,Knight.transform.position.z)))
						{
							Knight.transform.position = new Vector3(x,y,Knight.transform.position.z);
						}
						Knight.GetComponent<KnightMovementScript>().HightlightPossibleMoves(false,oldVector);
					}
					//There is a piece the player moves here and eat it.
					else if(hit.collider.gameObject.tag == "Chess Piece")
					{
						Knight.GetComponent<HighlightBox>().HighLight(false);
					}
					//Player hits the piece again, so he doesn't want to move
					else if(hit.collider.gameObject.tag == "Knight")
					{
						Knight.GetComponent<HighlightBox>().HighLight(false);
					}

					//Either ways, don't select kngight anymore.
					PieceSelected = false;
				}
			}
		}
#endif
	}
}
