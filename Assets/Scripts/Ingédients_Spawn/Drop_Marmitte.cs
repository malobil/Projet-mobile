using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems ;

public class Drop_Marmitte : MonoBehaviour,IDropHandler {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("Drop marmitte") ;
		eventData.pointerDrag.gameObject.GetComponent<Formes_Et_Ingredients>().AddPoint() ;
	}
}
