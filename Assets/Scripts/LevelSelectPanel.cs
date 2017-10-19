using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class LevelSelectPanel : MonoBehaviour {

	public Text descriptionText,timeText, toxicText, recompText ;
	public Scriptable_level level ;

	// Use this for initialization
	void Start () 
	{
		descriptionText.text = level.description ;
		timeText.text = "Time : " + level.time ;
		toxicText.text = "Goal : " + level.toxicNecc ;
		recompText.text = "Award : " + level.recompense +"mush" ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
