using UnityEngine;
using System.Collections;

public class HighlightBox : MonoBehaviour {
	public bool highlight;
	// Use this for initialization
	void Start () {
		highlight = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HighLight(bool INhighlight)
	{
		this.highlight = INhighlight;
		if(highlight)
		{
			this.renderer.material.color = Color.blue;
		}
		else
		{
			this.renderer.material.color = Color.white;
		}
	}

}
