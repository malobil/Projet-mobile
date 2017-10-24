using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems ;
using UnityEngine.SceneManagement; 

public class Drop_Marmitte : MonoBehaviour,IDropHandler 
{

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrop(PointerEventData eventData)
	{
		if(eventData.pointerDrag.gameObject.GetComponent<Formes_Et_Ingredients>() != null)
		{
			if(SceneManager.GetActiveScene().name != "Level_Tuto_Recipe")
			{
				Debug.Log("Drop marmitte") ;
				eventData.pointerDrag.gameObject.GetComponent<Formes_Et_Ingredients>().AddPoint() ;
			}
			else if(SceneManager.GetActiveScene().name == "Level_Tuto_Recipe" && TutoRecipeManager.Instance().ReturnTutoState())
			{
				Debug.Log("Drop marmitte") ;
				eventData.pointerDrag.gameObject.GetComponent<Formes_Et_Ingredients>().AddPoint() ;
			}

			if(SceneManager.GetActiveScene().name == "Level_Tuto" && TutoManager.Instance().ReturnDemand())
			{
				//TutoManager.Instance().RequestDone() ;
				if(TutoManager.Instance().ReturnState() == 3)
				{
					TutoManager.Instance().RequestDone() ;
				}
				else if(TutoManager.Instance().ReturnState() == 5)
				{
					TutoManager.Instance().RequestDone() ;
				}
			}

			if(SceneManager.GetActiveScene().name == "Level_Tuto_Recipe" && !TutoRecipeManager.Instance().ReturnTutoState())
			{
				if(TutoRecipeManager.Instance().ReturnState() <= 4)
				{
					//LevelManager.Instance().DestroyAObject(eventData.pointerDrag.gameObject) ;
					//LevelManager.Instance().UnpopObject() ;
					Debug.Log("Tuto destroy") ;
					LevelManager.Instance().DestroyAObject(eventData.pointerDrag.gameObject) ;
					LevelManager.Instance().PopParticularObject(1) ;
					//Destroy(eventData.pointerDrag.gameObject) ;
				}
				else if(TutoRecipeManager.Instance().ReturnState() == 6)
				{
					Debug.Log("Tuto destroy") ;
					LevelManager.Instance().DestroyAObject(eventData.pointerDrag.gameObject) ;
					LevelManager.Instance().PopParticularObject(0) ;
				}
			}
		}
	}
}
