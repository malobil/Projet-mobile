using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class LevelSelectPanel : MonoBehaviour {

	public Text descriptionText,timeText, toxicText, recompText, number, questName ;
	
	public Scriptable_level level ;

	public Animator playButton ;
	private int lvlNumber ;
	private bool isAnimate = false ;

	// Use this for initialization
	void Start () 
	{
		lvlNumber = level.levelNum ;
		descriptionText.text = level.description ;
		timeText.text = "Time : " + level.time ;
		toxicText.text = "Goal : " + level.toxicNecc ;
		recompText.text = "Award : " + level.recompense +"mush" ;
		number.text = lvlNumber.ToString("") ; ;
		questName.text = level.Levelname ;
	}

	void Update()
	{
		if(GameManager.Instance().ReturnTuto() && GameManager.Instance().ReturnTutoState() == 1 && !isAnimate)
		{
			playButton.SetTrigger("Blink") ;
			isAnimate = false ;
			GameManager.Instance().DoneTuto() ;
		}
	}
	

	public void LoadLevel()
	{
		LevelSelectManager.Instance().LoadLevel(lvlNumber) ;
	}


}
