using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public Camera OptionsPan;
	public Camera MainPan;
	private Vector3 Target;
	public float panSpeed=1;
	bool move;
	public DrawerMovement script;
	// Use this for initialization
	void Start () 
	{
		move = false;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (move) 
		{
			Vector3 pos=this.transform.position;
			if((pos-Target).sqrMagnitude>0.1)
			{
				this.transform.Translate((Target-pos).normalized*Time.deltaTime*panSpeed);
			}
			else
			{
				move=false;
			}
		}
	}
	public void PanToMain()
	{
		Target = MainPan.transform.position;
		script.MoveToStart ();
		move = true;
	}
	public void PanToOption()
	{
		Target = OptionsPan.transform.position;
		script.MoveToEnd ();
		move = true;
	}
}
