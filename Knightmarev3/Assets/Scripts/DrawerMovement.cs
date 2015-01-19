using UnityEngine;
using System.Collections;

public class DrawerMovement : MonoBehaviour {
	public Vector3 endPoint;
	Vector3 startPoint;
	public float panSpeed;
	bool move;
	Vector3 target;
	// Use this for initialization
	void Start () {
		startPoint = this.transform.position;
		endPoint = startPoint + endPoint;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (move) 
		{
			Vector3 pos=this.transform.position;
			if((pos-target).sqrMagnitude>0.1)
			{
				this.transform.Translate((target-pos).normalized*Time.deltaTime*panSpeed);
			}
			else
			{
				move=false;
			}
		}
	}
	public void MoveToStart()
	{
		target = startPoint;
		move = true;
	}
	public void MoveToEnd()
	{
		target = endPoint;
		move = true;
	}

}
