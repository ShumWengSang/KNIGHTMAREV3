using UnityEngine;
using System.Collections;

public class InputDetection : MonoBehaviour {

	// Use this for initialization
	public CameraMovement targetScript;
	public SliderScript targetScript2;


	void Start () 	
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			targetScript.PanToOption();
		}
		if (Input.GetKeyDown (KeyCode.S)) 
		{
			targetScript.PanToMain();	
		}
		if (Input.GetKeyDown (KeyCode.K)) 
		{
			targetScript2.currentValue--;	
		}
		if (Input.GetKeyDown (KeyCode.L)) 
		{
			targetScript2.currentValue++;	
		}

	}
}
