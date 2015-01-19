using UnityEngine;
using System.Collections;

public class SliderScript : MonoBehaviour {
	public float maxValue=100;
	public float minValue=0;
	public float currentValue=0;
	public GameObject sliderKnob;
	public Vector3 minPos;
	public Vector3 maxPos;
	public float length;
	// Use this for initialization
	void Start () {
		minPos = this.transform.position;
		minPos.x=this.transform.position.x - this.transform.localScale.x * 0.5f;
		maxPos = this.transform.position ;
		maxPos.x = this.transform.position.x + this.transform.localScale.x * 0.5f;
		length = this.transform.localScale.x;
		minPos.z = sliderKnob.transform.position.z;
		maxPos.z = sliderKnob.transform.position.z;
		//sliderKnob.transform.position.Set (minPos.x, minPos.y, minPos.z);
		sliderKnob.transform.position = new Vector3 (minPos.x, minPos.y, minPos.z);
	}
	
	// Update is called once per frame
	void Update () {
		/*if(Input.GetKeyDown(KeyCode.K))
		{
			currentValue+=(maxValue-minValue)*0.01f;
		}
		if(Input.GetKeyDown(KeyCode.L))
		{
			currentValue-=(maxValue-minValue)*0.01f;
		}*/
		//float posRatio = 100;
		float posRatio = currentValue / (maxValue - minValue);
		sliderKnob.transform.position = new Vector3 (minPos.x + length * posRatio, sliderKnob.transform.position.y, sliderKnob.transform.position.z);

	}
}
