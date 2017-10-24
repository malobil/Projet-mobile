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
   			PopSomething(recipeArrow) ;
   			//PopSomething(dragArrow) ;
   			UnPopSomething(recipeListArrow) ;
   			if(!isWaiting)
   			{
   				StartCoroutine(Wait()) ;
   			}
   			LevelManager.Instance().ShowAnimation() ;
   			demandeSomething = true ;
   		}
   		else if(tutoState == 3)
   		{
   			UnPopSomething(recipeArrow) ;
   		}
   		else if(tutoState == 4)
   		{
   			demandeSomething = true ;
   			PopSomething(recipeArrow2) ;
   			LevelManager.Instance().EnableForm(0) ;
   		}
   		else if(tutoState == 5)
   		{
   			UnPopSomething(recipeArrow2) ;
   			demandeSomething = true ;
   			PopSomething(validArrow) ;
   		}
   		else if(tutoState == 6)
   		{
   			UnPopSomething(validArrow) ;
   			demandeSomething = false ;
   		}

	}

	IEnumerator Wait()
	{
		isWaiting = true ;
		Debug.Log("Wait") ;
		yield return new  WaitForSeconds(1.6f) ;
		LevelManager.Instance().DisableForm(0) ;
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
