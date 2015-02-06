using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSceneInput : MonoBehaviour {
	public static GameSceneInput Instance;

	bool PieceSelected;
	GameObject Knight;
	Object Pawn;
	Vector3 oldVector;
	GameObject[] list;

	//public DifficultyManager difficulty;

	public AudioClip[] combo_sounds;
	public AudioSource source;
	/*
	public int movesMade;
	public int difficulty;
	public static int movesLeft;
	Text movesLeftText;
*/
	public static bool playerTurn;

	void Start () {
		playerTurn = true;
		PieceSelected = false;
		Pawn = Resources.Load("Prefab/Pieces/Pawn") as Object;
		SpawnPawn ();
		SpawnPawn ();
		list = GameObject.FindGameObjectsWithTag("Chess Piece");
		ShowThreatenedSquares();

		//movesLeft = 10;
		//movesLeftText = GetComponent<Text>(); 
		//movesMadeText.text = "Moves Made: " + movesMade.ToString();

		//combo_sounds [0] = (AudioClip)Resources.Load ("combo_1", typeof(AudioClip));
	}

	void SetCountText()
	{
		//movesMade++;
		//movesLeft--;

		//movesMadeText.text = "Moves Made: " + movesMade.ToString();
		//movesMadeText.text = gameObject.GetComponent<Text>(); 
		//movesMadeText.text = "Moves Made: " + movesMade.ToString();
	}
	void MoveMade() {
		DifficultyManager.movesLeft--;
		DifficultyManager.movesMade++;
		if (DifficultyManager.movesLeft <= 0) {
			if (DifficultyManager.movesMade > DifficultyManager.difficulty * DifficultyManager.difficulty + 5)
				DifficultyManager.difficulty++;
			DifficultyManager.movesLeft = 10 - DifficultyManager.difficulty;
			if (DifficultyManager.movesLeft < 3)
				DifficultyManager.movesLeft = 3;
			SpawnPawn();
		}
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
			if (Piece.name == "Pawn(Clone)")
			{
				Piece.GetComponent<ThreatenBoxesPawn>().MoveForward();
			}
		}
	}

	bool PiecesMoving()
	{
		foreach (GameObject Piece in list) {
			if (Piece.name == "Pawn(Clone)")
			{
				if (Piece.GetComponent<ThreatenBoxesPawn>().moving == true)
					return true;
			}
		}
		return false;
	}

	public void ShowThreatenedSquares() {
		//list = GameObject.FindGameObjectsWithTag("Chess Piece");
		foreach (GameObject ChessPiece in list) {
			if (ChessPiece.name == "Pawn(Clone)")
				//if (ChessPiece == null)
				ChessPiece.GetComponent<ThreatenBoxesPawn>().Threathen(ChessPiece.transform.position.x,ChessPiece.transform.position.y,true);
		}
	}

	// Update is called once per frame
	void Update () {
		//movesLeftText.text = "Moves until next spawn: " + movesLeft;
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
		//movesMadeText.text = "Moves Made: " + movesMade;
	

		list = GameObject.FindGameObjectsWithTag("Chess Piece");

		if (!playerTurn)
		{
			MovePieces();
			if (PiecesMoving() == false)
			{
				playerTurn = true;
			}
		}

		ShowThreatenedSquares();

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
					if(hit.collider.gameObject.name == "Knight(Clone)")
					{
						if (playerTurn)
						{
							PieceSelected = true;
							Knight = hit.collider.gameObject;
							Knight.GetComponent<HighlightBox>().HighLight(true, false);
							Knight.GetComponent<KnightMovementScript>().HightlightPossibleMoves(true,Knight.transform.position);
							oldVector = new Vector3(Knight.transform.position.x,Knight.transform.position.y);
						}
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
							MoveMade();
							ComboManager.combo = 1;
							playerTurn = false;
							list = GameObject.FindGameObjectsWithTag("Chess Piece");

							foreach (GameObject Piece in list) {
								if (Piece.name == "Pawn(Clone)")
								{
									Piece.GetComponent<ThreatenBoxesPawn>().StartMoving();
									/*
									if (Piece.GetComponent<ThreatenBoxesPawn>().direction == 1)
										if (this.transform.position.y == 7)
											Destroy(Piece);
											*/
									/*
									switch (Piece.GetComponent<ThreatenBoxesPawn>().direction) 
									{
									case 1:
										if (this.transform.position.y == 7)
											Destroy(Piece);
										break;
									case 2:
										if (this.transform.position.y == 0)
											Destroy(Piece);
										break;
									case 3:
										if (this.transform.position.x == 7)
											Destroy(Piece);
										break;
									case 4:
										if (this.transform.position.x == 0)
											Destroy(Piece);
										break;
									}
									*/
								}




/*
								switch (Piece.GetComponent<ThreatenBoxesPawn>().direction) 
								{
								case 1:
									if (this.transform.position.y == 7)
										Destroy(Piece);
									break;
								case 2:
									if (this.transform.position.y == 0)
										Destroy(Piece);
									break;
								case 3:
									if (this.transform.position.x == 7)
										Destroy(Piece);
									break;
								case 4:
									if (this.transform.position.x == 0)
										Destroy(Piece);
									break;
								}
								*/
							}


							//MovePieces();
						}
						Knight.GetComponent<KnightMovementScript>().HightlightPossibleMoves(false,oldVector);
						//ShowThreatenedSquares();
					}
					//There is a piece the player moves here and eat it.
					else if(hit.collider.gameObject.tag == "Chess Piece" && hit.collider.gameObject.name != "Knight(Clone)")
					{
						float x = Mathf.Round(worldPoint.x );
						float y = Mathf.Round(worldPoint.y);
						if(Knight.GetComponent<KnightMovementScript>().MovementRuling(Knight.transform.position,new Vector3(x,y,Knight.transform.position.z)))
						{
							Knight.transform.position = new Vector3(x,y,Knight.transform.position.z);
							Destroy(hit.collider.gameObject);
							SpawnPawn();
							ScoreManager.score += 10 * ComboManager.combo;
							audio.clip = combo_sounds[ComboManager.combo - 1];
							audio.Play();
							ComboManager.combo++;

							//SpawnPawn();
							MoveMade();
							Knight.GetComponent<HighlightBox>().HighLight(false, true);
						}

						Knight.GetComponent<KnightMovementScript>().HightlightPossibleMoves(false,oldVector);
					}
					//Player hits the piece again, so he doesn't want to move
					else if(hit.collider.gameObject.name == "Knight(Clone)")
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
