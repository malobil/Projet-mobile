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

	public GameObject rigthArrow, leftArrow, timer ;

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
			mamyText.text = mamyDialogue[tutoState] ;
		}

		if(tutoState == 2 && !isWaiting && !demandeSomething)
		{
			LevelManager.Instance().ShowAnimation() ;
			demandeSomething = true ;
			//PopSomething(rigthArrow) ;
			StartCoroutine(Wait()) ;
			
		}
		else if(tutoState == 4)
		{			
			demandeSomething = true ;
			LevelManager.Instance().EnableForm(0) ;
			PopSomething(leftArrow) ;
		}
		else if(tutoState +1 >= mamyDialogue.Length)
		{
			Debug.Log("END") ;
			tutoIsEnd = true ;
			LevelManager.Instance().EndTuto() ;
			timer.SetActive(true) ;
			mamyLayout.SetActive(false) ;
		}
		else
		{
			UnPopSomething(leftArrow) ;
			UnPopSomething(rigthArrow) ;
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
		yield return new  WaitForSeconds(1.5f) ;
		PopSomething(rigthArrow) ;
		LevelManager.Instance().DisableForm(0) ;
		isWaiting = false ;
		StopCoroutine(Wait()) ;
	}
}
