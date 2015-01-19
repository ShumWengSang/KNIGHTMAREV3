using UnityEngine;
using System.Collections;

public class GameSceneInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
					if(hit.collider.gameObject.tag == "Board Piece")
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
				if(hit.collider.gameObject.tag == "Board Piece")
				{
					hit.collider.gameObject.GetComponent<HighlightBox>().HighLight(true);
				}
			}
		}
#endif
	}
}
