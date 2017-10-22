using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class LevelSelectPanel : MonoBehaviour {

	public Text descriptionText,timeText, toxicText, recompText, number, questName ;
	
	public Scriptable_level level ;
	private int lvlNumber ;

	// Use this for initialization
	void Start () 
	{
		lvlNumber = level.levelNum ;
		descriptionText.text = level.description ;
		timeText.text = "Time : " + level.time ;
		toxicText.text = "Goal : " + level.toxicNecc ;
		recompText.text = "Award : " + level.recompense +"mush" ;
		number.text = lvlNumber.ToString("") ; ;
		questName.text = level.name ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void LoadLevel()
	{
		LevelSelectManager.Instance().LoadLevel(lvlNumber) ;
	}
}
