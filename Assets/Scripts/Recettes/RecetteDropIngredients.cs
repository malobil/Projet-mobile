using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems ;

public class RecetteDropIngredients : MonoBehaviour, IDropHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrop(PointerEventData eventData)
	{	
		eventData.pointerDrag.gameObject.transform.SetParent(transform) ;
		RecetteManager.Instance().AddIngredient(eventData.pointerDrag.gameObject) ;
	}
}
