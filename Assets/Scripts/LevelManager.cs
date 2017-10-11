using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


	public List<GameObject> buttonList = new List<GameObject>() ;

	public float timeIngredientShow ;
	public float timeHide ;

	private float scoreToxic = 0f;

	private float currentTimeHide ;
	private float currentTimeShow ;

	private bool isHide = true ;

	private static LevelManager instance ;
    public static LevelManager Instance () 
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
		currentTimeHide = timeHide ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown("s"))
		{
			EnableObject() ;
		}

		TimerShow() ;
		TimerHide() ;
		
	}

	void TimerShow()
	{
		if(currentTimeHide > 0 && isHide)
		{
			currentTimeHide -= Time.deltaTime ;
		}
		else if(currentTimeHide < 0 && isHide)
		{
			EnableObject() ;
			Debug.Log("time bef show < 0") ;
			currentTimeHide = 0 ;
			currentTimeShow = timeIngredientShow ;
		}
	}

	void TimerHide()
	{
		if(currentTimeShow > 0 && !isHide)
		{
			currentTimeShow -= Time.deltaTime ;
		}
		else if(currentTimeShow < 0 && !isHide)
		{
			DisableObject() ;
			Debug.Log("time bef hide < 0") ;
			currentTimeShow = 0 ;
			currentTimeHide = timeHide ;
		}
	}

	public void ScoreUpdate (float toxicAdded) 
	{
		scoreToxic += toxicAdded;
		Debug.Log(scoreToxic);
	}

	public void DisableObject()
	{
		for(int i = 0 ; i < buttonList.Count ; i++)
		{
			buttonList[i].SetActive(false) ;
		}

		currentTimeHide = timeHide ;
		isHide = true ;
	}

	public void EnableObject()
	{
		for(int i = 0 ; i < buttonList.Count ; i++)
		{
			buttonList[i].SetActive(true) ;
		}

		isHide = false ;
	}


}
