using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems ;

public class Formes_Et_Ingredients : MonoBehaviour, IDragHandler, IEndDragHandler,  IPointerClickHandler {

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

	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log("OnBeginDrag") ;

		transform.position = eventData.position ;
		isDrag = true ;	
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("Drop") ;
		isDrag = false ;
		AddPoint() ;
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
