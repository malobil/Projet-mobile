using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.SceneManagement; 

public class TutoManager : MonoBehaviour {

	////-----Tuto------////

	public GameObject mamyLayout ;
	public Text mamyText ;
	public string[] mamyDialogue ;
	public string[] mamyDialogueFr ;
	public Scriptable_IngredientList ingredientList ;

	public GameObject rigthArrow, leftArrow, timer,fioleArrow ;

	private bool tutoIsEnd = false ;
	private int tutoState = -1 ;

	private bool demandeSomething = false ;

	private bool isWaiting = false ;

	private static TutoManager instance ;
    public static TutoManager Instance () 
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
			
			if(GameManager.Instance().ReturnLanguage())
			{
				mamyText.text = mamyDialogue[tutoState] ;
			}
			else if(!GameManager.Instance().ReturnLanguage())
			{
				mamyText.text = mamyDialogueFr[tutoState] ;
			}
		}

		if(tutoState == 2)
		{
			LevelManager.Instance().ShowAnimation() ;
		}
		else if(tutoState == 3 && !isWaiting)
		{
			StartCoroutine(Wait()) ;
			demandeSomething = true ;
			mamyLayout.SetActive(false) ;
			//PopSomething(rigthArrow) ;
		}
		else if(tutoState == 4)
		{			
			mamyLayout.SetActive(true) ;
			UnPopSomething(rigthArrow) ;
		}
		else if(tutoState == 5)
		{
			PopSomething(leftArrow) ;
			mamyLayout.SetActive(false) ;
			LevelManager.Instance().EnableForm(0) ;
			demandeSomething = true ;
			//UnPopSomething(leftArrow) ;
			//PopSomething(fioleArrow) ;
		}
		else if(tutoState +1 >= mamyDialogue.Length)
		{
			Debug.Log("END") ;
			tutoIsEnd = true ;
			LevelManager.Instance().EndTuto() ;
			LevelManager.Instance().ChangeSpawnerIngredientList(ingredientList) ;
			//GameManager.Instance().AddTutoState() ;
			timer.SetActive(true) ;
			mamyLayout.SetActive(false) ;
		}
		else
		{
			UnPopSomething(leftArrow) ;
			UnPopSomething(rigthArrow) ;
			UnPopSomething(fioleArrow) ;
		}
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

	public int ReturnState()
	{
		return tutoState ;
	}

	public void RequestDone()
	{
		Debug.Log("done") ;
		demandeSomething = false ;
		Tutoriel() ;
	}

	IEnumerator Wait()
	{
		isWaiting = true ;
		Debug.Log("Wait") ;
		yield return new  WaitForSeconds(1.2f) ;
		PopSomething(rigthArrow) ;
		LevelManager.Instance().DisableForm(0) ;
		isWaiting = false ;
		StopCoroutine(Wait()) ;
	}
}
