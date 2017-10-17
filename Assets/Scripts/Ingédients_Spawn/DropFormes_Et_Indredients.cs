using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems ;

public class DropFormes_Et_Indredients : MonoBehaviour, IDropHandler {

	public void OnDrop(PointerEventData eventData)
	{	
		if(eventData.pointerDrag.gameObject.GetComponent<Formes_Et_Ingredients>() != null)
		{
			Debug.Log(eventData.pointerDrag.name) ;
			eventData.pointerDrag.gameObject.GetComponent<Formes_Et_Ingredients>().AddPoint() ;
		}
	}
}
