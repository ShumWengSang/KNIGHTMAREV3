using UnityEngine;
using System.Collections;

public class HighlightBox : MonoBehaviour {
	bool highlight;
	// Use this for initialization
	void Start () {
		highlight = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HighLight(bool highlight)
	{
		this.highlight = highlight;
		this.renderer.material.color = Color.blue;
	}

}
