﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI ;

public class LevelSelectManager : MonoBehaviour {

	public Text champiText  ;

	// Use this for initialization
	void Start () 
	{
		ChangeChampiText() ;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadLevel (int levelnumber)
   	{
   		if (GameManager.Instance().lvlSuccess[levelnumber] == true)
   		{
   			SceneManager.LoadScene (GameManager.Instance().sceneName[levelnumber]);
   		}
   	}

   	public void ChangeChampiText()
   	{
   		champiText.text = GameManager.Instance().ReturnChampiBank().ToString() ;
   	}
}