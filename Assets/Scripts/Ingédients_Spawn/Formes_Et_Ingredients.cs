﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems ;
using UnityEngine.UI ;

public class Formes_Et_Ingredients : MonoBehaviour, IBeginDragHandler, IDragHandler,  IPointerClickHandler {

	private bool isDrag = false ;

	public float toxicValue ;
	public Scriptable_Forme formActual ;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		GetComponent<Image>().raycastTarget = false ;
		LevelManager.Instance().RetireObjectToList(gameObject) ;
	}

	public void OnDrag(PointerEventData eventData)
	{
		//Debug.Log("OnBeginDrag") ;

		transform.position = eventData.position ;
		isDrag = true ;	
	}


	public void OnPointerClick(PointerEventData eventData)
	{
		if(!isDrag)
		{
			Debug.Log("click") ;
			AddPoint() ;
		}
	}

	public void AddPoint()
	{
		LevelManager.Instance().ScoreUpdate(toxicValue) ;
		Destroy(gameObject) ;
	}

	public void SetToxicValueAndform(float valor, Scriptable_Forme form)
	{
		toxicValue = valor ;
		formActual = form ;
	}

	public bool ReturnIsDrag()
	{
		return isDrag ;
	}

}
