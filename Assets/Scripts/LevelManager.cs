using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 

public class LevelManager : MonoBehaviour {

	public float scoreNeed ;

	public List<GameObject> buttonList = new List<GameObject>() ;

	public float timeIngredientShow ;
	public float timeHide ;

	public Text textEndLevel, textToxicNeed ;

	public Text timerText;
	public float timerMinutes;
	public float timerSecondes;

	private float scoreToxic = 0f;

	private float currentTimeHide ;
	private float currentTimeShow ;

	private bool isHide = true ;
	private bool levelIsEnd = false ;

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
		textToxicNeed.text = "Score necessaire : " + scoreNeed.ToString("") ;
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
		TimerGeneral() ;
		
	}

	void TimerGeneral()
	{
		timerSecondes-=Time.deltaTime;
		

			if(timerSecondes < 0)
        	{
            	timerSecondes = 0;
        	}

        	if(timerMinutes < 0)
        	{
            	timerMinutes = 0;
        	}

        	if(timerMinutes == 0 && timerSecondes == 0)
        	{
        		EndLevel() ;
        	}

        	if (timerMinutes >= 1 && timerSecondes <= 0)
        	{
            	timerMinutes--;
            	timerSecondes = 60.0f;
        	}


        	if ( timerSecondes <= 59)
        	{
				timerText.text=timerMinutes.ToString ("") + ":" + timerSecondes.ToString ("00");
        	}
	}

	void EndLevel()
	{
		DisableObject() ;
		textEndLevel.gameObject.SetActive(true) ;
		if(scoreToxic >= scoreNeed)
		{	
			textEndLevel.color = Color.green ;
			textEndLevel.text = "VICTOIRE ! =')" ;
		}
		else
		{
			textEndLevel.color = Color.red ;
			textEndLevel.text = "DEFAITE ! ='( " ;
		}

		levelIsEnd = true ;
	}

	void TimerShow()
	{
		if(currentTimeHide > 0 && isHide && !levelIsEnd)
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
		if(currentTimeShow > 0 && !isHide && !levelIsEnd)
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
