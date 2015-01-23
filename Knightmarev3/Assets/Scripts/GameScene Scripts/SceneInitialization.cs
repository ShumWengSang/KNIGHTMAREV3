using UnityEngine;
using System.Collections;

public class SceneInitialization : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Object CubeDark;
		CubeDark = Resources.Load ("Prefab/ChessBoard/CubeDark") as Object;
		Object CubeLight;
		CubeLight = Resources.Load ("Prefab/ChessBoard/CubeLight") as Object;
		Object Knight;
		Knight = Resources.Load ("Prefab/Pieces/Knight") as Object;
		Object Rook;
		Rook = Resources.Load("Prefab/Pieces/Rook") as Object;

		for (int i = 1; i < 8; i += 2) {
			for(int k = 0; k < 8; k +=2) {
				GameObject something = GameObject.Instantiate (CubeLight) as GameObject;
				something.transform.position = new Vector3 (i, k);
				something.tag = "Board Piece";
				}
			}
		for (int i = 0; i < 8; i += 2) {
			for(int k = 0; k < 8; k +=2) {
				GameObject something = GameObject.Instantiate (CubeDark) as GameObject;
				something.transform.position = new Vector3 (i, k);
				something.tag = "Board Piece";
				//something.transform.parent = this.transform;
			}
		}

		//				}
		for (int i = 0; i < 8; i += 2) {
			for(int k = 1; k < 8; k +=2) {
				GameObject something = GameObject.Instantiate (CubeLight) as GameObject;
				something.transform.position = new Vector3 (i, k);
				something.tag = "Board Piece";
				//something.transform.parent = this.transform;
			}
		}
		for (int i = 1; i < 8; i += 2) {
			for(int k = 1; k < 8; k +=2) {
				GameObject something = GameObject.Instantiate (CubeDark) as GameObject;
				something.transform.position = new Vector3 (i, k);
				something.tag = "Board Piece";
				//something.transform.parent = this.transform;
			}
		}

		GameObject KnightObject = GameObject.Instantiate (Knight) as GameObject;
		int a = Random.Range(0,8);
		int b = Random.Range(0,8);
		KnightObject.transform.position = new Vector3 (a, b, -5);
		KnightObject.tag = "Knight";

		GameObject RookObject = GameObject.Instantiate (Rook) as GameObject;
		int c = 0;
		int d = 1;
		RookObject.transform.position = new Vector3 (c, d, -5);
		RookObject.tag = "Chess Piece";
	}
	
	// Update is called once per frame
	void Update () {
		GameObject Rook = GameObject.FindGameObjectWithTag("Chess Piece");
		Rook.GetComponent<ThreatenBoxesRook>().Threathen(Rook.transform.position.x,Rook.transform.position.y,true);
	}
}
