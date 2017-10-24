using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems ;
using UnityEngine.SceneManagement ;

public class DropFormes_Et_Indredients : MonoBehaviour, IDropHandler {

	public void OnDrop(PointerEventData eventData)
	{	
		if(eventData.pointerDrag.gameObject.GetComponent<Formes_Et_Ingredients>() != null)
		{
			Debug.Log(eventData.pointerDrag.name) ;
			if(TutoRecipeManager.Instance() != null && TutoRecipeManager.Instance().ReturnDemand())
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
			else
			{
				LevelManager.Instance().DestroyAObject(eventData.pointerDrag.gameObject) ;
			}
		}

		if(SceneManager.GetActiveScene().name == "Level_Tuto" && TutoManager.Instance().ReturnDemand())
		{
			//TutoManager.Instance().RequestDone() ;
			if(TutoManager.Instance().ReturnState() == 3)
				{
					//LevelManager.Instance().DestroyAObject(eventData.pointerDrag.gameObject) ;
					//LevelManager.Instance().UnpopObject() ;
					Debug.Log("Tuto destroy") ;
					LevelManager.Instance().DestroyAObject(eventData.pointerDrag.gameObject) ;
					LevelManager.Instance().PopParticularObject(1) ;
					//Destroy(eventData.pointerDrag.gameObject) ;
				}
				else if(TutoManager.Instance().ReturnState() == 5)
				{
					Debug.Log("Tuto destroy") ;
					LevelManager.Instance().DestroyAObject(eventData.pointerDrag.gameObject) ;
					LevelManager.Instance().PopParticularObject(0) ;
				}
		}
	}
}
