﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AffichageBookRecette : MonoBehaviour {

	public int recetteNumberToShow ;

	public GameObject layout ;
	public GameObject prefabRecetteObject ;
	public GameObject textNoRecipe ;

	public GameObject buttonUp ;
	public GameObject buttonDown ;

	private List<GameObject> listRecetteShow = new List<GameObject>() ;

	private int depValue ;
	private int endValue ;


	void Start()
	{
		buttonDown.SetActive(false) ;
	}
	
	// Use this for initialization
	void OnEnable () 
	{
		endValue = recetteNumberToShow ;
		depValue = endValue - recetteNumberToShow ;
		ShowRecette() ;

		if(GameManager.Instance().recetteKnow.Count > endValue)
		{
			buttonUp.SetActive(true) ;
		}

		if(GameManager.Instance().recetteKnow.Count == 0)
		{
			textNoRecipe.SetActive(true) ;
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void ShowRecette()
	{
		for(int p = 0 ; p <listRecetteShow.Count ; p++)
		{
			Destroy(listRecetteShow[p]) ;
		}

		if(GameManager.Instance().recetteKnow.Count != 0)
		{
			textNoRecipe.SetActive(false) ;
			for(int i = depValue ; depValue <= i && i < endValue ; i++)
			{
				if(i < GameManager.Instance().recetteKnow.Count)
				{
					if(GameManager.Instance().recetteKnow[i] != null)
					{
						GameObject temp = Instantiate(prefabRecetteObject, layout.transform) ;
						temp.GetComponent<AffichageRecetteScript>().Change(GameManager.Instance().recetteKnow[i]) ;
						listRecetteShow.Add(temp) ;
					}
				}
				
			}
		}
	}

	public void ChangePageUp()
	{
		if(GameManager.Instance().recetteKnow.Count > endValue)
		{
			Debug.Log("Next") ;
			endValue += recetteNumberToShow ;
			depValue = endValue - recetteNumberToShow ;

			for(int o = 0 ; o < listRecetteShow.Count ; o++)
			{
				Destroy(listRecetteShow[o]) ;
			}

			listRecetteShow.Clear() ;
			ShowRecette() ;

			buttonDown.SetActive(true) ;

			if(GameManager.Instance().recetteKnow.Count <= endValue)
			{
				buttonUp.SetActive(false) ;
			}
		}
	}

	public void ChangePageDown()
	{
		if(endValue > recetteNumberToShow)
		{
			Debug.Log("Next") ;
			endValue -= recetteNumberToShow ;
			depValue = endValue - recetteNumberToShow ;

			for(int o = 0 ; o < listRecetteShow.Count ; o++)
			{
				Destroy(listRecetteShow[o]) ;
			}

			listRecetteShow.Clear() ;
			ShowRecette() ;

			if(endValue == recetteNumberToShow)
			{
				buttonDown.SetActive(false) ;
			}

			buttonUp.SetActive(true) ;
		}
	}
}
