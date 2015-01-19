using UnityEngine;
using System.Collections;

public class ColourSetting : MonoBehaviour {

	public float red;
	public float blue;
	public float green;

	// Use this for initialization
	void Start () 
	{
		if (red > 1.0f) 
		{
			red = 1.0f;
		}
		else if (red < 0) 
		{
			red = 0.0f;
		}
		if (blue > 1.0f) 
		{
			blue = 1.0f;
		} 
		else if (blue < 0) 
		{
			blue = 0.0f;
		}
		if (green > 1.0f) 
		{
			green = 1.0f;
		} 
		else if (green < 0) 
		{
			green = 0.0f;
		}
		this.renderer.material.color= new Color(red,green,blue);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
