using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class TutoRecipeManager : MonoBehaviour {

////-----Tuto------////

	public GameObject mamyLayout ;
	public Text mamyText ;
	public string[] mamyDialogue ;
	public Scriptable_IngredientList ingredientList ;
	public Scriptable_FormeByLevel formLevel ;

	public GameObject recipeListArrow, recipeArrow, validArrow, recipeArrow2, timer ;

	private bool tutoIsEnd = false ;
	private int tutoState = -1 ;

	private bool demandeSomething = false ;

	private bool isWaiting = false ;

	private static TutoRecipeManager instance ;
    public static TutoRecipeManager Instance () 
    {
        return instance;
    }

void Awake ()
    {
        if (instance != null)
        {
            Destroy (gameObject);
        }

        else 

        {
            instance = this;
        }
    }

	// Use this for initialization
	void Start () 
	{
		Tutoriel() ;
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0) && !tutoIsEnd && !demandeSomething)
		{
			Tutoriel() ;
		}
	}
	void Tutoriel()
	{
		if(tutoState +1 < mamyDialogue.Length && !demandeSomething)
		{
			tutoState++ ;
			mamyLayout.SetActive(true) ;
			mamyText.text = mamyDialogue[tutoState] ;
		}
		else if(tutoState +1 >= mamyDialogue.Length)
    	{
    		Debug.Log("End") ;
    		LevelManager.Instance().ChangeSpawnerIngredientList(ingredientList) ;
			LevelManager.Instance().ChangeSpawnerFormList(formLevel) ;
     		UnPopSomething(mamyLayout) ;
     		LevelManager.Instance().EndTuto() ;
			//GameManager.Instance().AddTutoState() ;
			timer.SetActive(true) ;
			demandeSomething = false ;
			tutoIsEnd = true ;
			//mamyLayout.SetActive(false) ;
   		}

   		if(tutoState == 1)
   		{
   			PopSomething(recipeListArrow) ;
   		}
   		else if(tutoState == 2)
   		{
   			if(!isWaiting)
   			{
   				StartCoroutine(Wait()) ;
   			}
   			UnPopSomething(recipeListArrow) ;
   			LevelManager.Instance().ShowAnimation() ;
   			
   		}
   		else if(tutoState == 3)
   		{
   			PopSomething(recipeArrow) ;
   			//PopSomething(dragArrow) ;
   			
   			UnPopSomething(mamyLayout) ;
   			demandeSomething = true ;
   			//LevelManager.Instance().EnableForm(1) ;
   			//UnPopSomething(recipeArrow) ;
   		}
   		else if(tutoState == 4)
   		{
   			UnPopSomething(recipeArrow) ;
   		}
   		else if(tutoState == 6)
   		{
   			PopSomething(recipeArrow2) ;
   			LevelManager.Instance().EnableForm(0) ;
   			UnPopSomething(mamyLayout) ;
   			demandeSomething = true ;
   			//UnPopSomething(recipeArrow2) ;
   			//demandeSomething = true ;
   			//PopSomething(validArrow) ;
   		}
   		else if(tutoState == 7)
   		{
   			UnPopSomething(recipeArrow2) ;
   		}
   		else if(tutoState == 8)
   		{
   			demandeSomething = true ;
   			PopSomething(validArrow) ;
   			UnPopSomething(mamyLayout) ;
   		}
   		else if(tutoState == 9)
   		{
   			UnPopSomething(validArrow) ;
   		}

	}

	IEnumerator Wait()
	{
		isWaiting = true ;
		Debug.Log("Wait") ;
		yield return new  WaitForSeconds(1.2f) ;
		LevelManager.Instance().DisableForm(0) ;
		//LevelManager.Instance().DisableForm(1) ;
		isWaiting = false ;
		StopCoroutine(Wait()) ;
	}

	public void PopSomething(GameObject objectToPop)
	{	
		objectToPop.SetActive(true) ;
	}

	public void UnPopSomething(GameObject objectToUnPop)
	{
		objectToUnPop.SetActive(false) ;
	}

	public bool ReturnDemand()
	{
		return demandeSomething ;
	}

	public void RequestDone()
	{
		Debug.Log("done") ;
		demandeSomething = false ;
		Tutoriel() ;
	}

	public bool ReturnTutoState()
	{
		return tutoIsEnd ;
	}

	public int ReturnState()
	{
		return tutoState ;
	}
}
