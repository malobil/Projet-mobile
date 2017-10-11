using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float myWitchTimer;
	public Text timerText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		myWitchTimer-=Time.deltaTime;
		timerText.text=myWitchTimer.ToString ("f0");
	}
}
