using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LevelManager : MonoBehaviour {

	public int numberlvl;

	public Image potionState ;

	public float scoreNeed ;

	public float timeIngredientShow ;
	public float timeHide ;

	public Text textEndLevel;

	public Text timerText;
	public float timerMinutes;
	public float timerSecondes;

	private float scoreToxic = 0f;

	////----Timers (hide)---///

	private float currentTimeHide ;
	private float currentTimeShow ;

	////----Statuts---///

	private bool isHide = true ;
	private bool levelIsEnd = false ;
	private bool pointAdded = false ;

	////----End game---///

	public GameObject endLayout ;
	public Text stateText, timeLeftText, scoreEndText, recipeFoundText, mushGainText ;

	////----Champi Value By Level---///

	public float levelChampiValue ;
	private float difference ; 

	////----Spawner and object list---///

	public List<GameObject> listOfobjectPresent = new List<GameObject>() ;
	public List<GameObject> spawnerList = new List<GameObject>() ;


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
		TimerShow() ;
		TimerHide() ;
		TimerGeneral() ;

		if(scoreToxic >= scoreNeed && !pointAdded)
		{
			EndLevel() ;
		}
	}

	void TimerGeneral()
	{
		if(!pointAdded)
		{
			timerSecondes-=Time.deltaTime;
		}

			if(timerSecondes < 0)
        	{
            	timerSecondes = 0;
        	}

        	if(timerMinutes < 0)
        	{
            	timerMinutes = 0;
        	}

        	if(timerMinutes == 0 && timerSecondes == 0 && !levelIsEnd)
        	{
        		EndLevel() ;
        	}

        	if (timerMinutes >= 1 && timerSecondes <= 0 && !levelIsEnd)
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
		endLayout.SetActive(true) ;
		UnpopObject() ;

		if(scoreToxic >= scoreNeed && !pointAdded)
		{	
			difference = (scoreToxic - scoreNeed) * numberlvl ;

			if(difference > 0)
			{
			   levelChampiValue += difference ;
			}

			if(GameManager.Instance() != null)
			{
				GameManager.Instance().ChampiBank(levelChampiValue) ;
				LvlSucces() ;
				pointAdded = true ;
			}

		}

		levelIsEnd = true ;
		EndText() ;
		GameManager.Instance().SaveGame() ;
	
	}

	void EndText()
	{
		endLayout.SetActive(true) ;
		if(scoreToxic >= scoreNeed)
		{	
			stateText.color = Color.green ;
			stateText.text = "VICTORY" ;
			timeLeftText.text = "Time left : " + "<b>" + timerMinutes.ToString("f0") + "</b>" + " min " + "<b>" + timerSecondes.ToString("f0")+ "</b>" + " sec" ;
			scoreEndText.text = "Score : " + "<b>" + scoreToxic.ToString("") +  "</b>" +  " / " + "<b>" + scoreNeed.ToString("") + "</b>" ;
			mushGainText.text = "Mushroom gain : " + "<b>" + levelChampiValue.ToString("") + "</b>" ;
		}
		else if(scoreToxic < scoreNeed)
		{
			stateText.color = Color.red ;
			stateText.text = "DEFEAT" ;
			timeLeftText.text = "<b>No time left !</b>" ;
			scoreEndText.text = "Score : " + "<b>" + scoreToxic.ToString("") + "</b>" + " / " + "<b>" + scoreNeed.ToString("") + "</b>";
			mushGainText.text = "Mushroom gain : " +  "<b>0</b>" ;
		}

		
	}

	void TimerShow()
	{
		if(currentTimeHide > 0 && isHide && !levelIsEnd)
		{
			//Debug.Log("time show ") ;
			currentTimeHide -= Time.deltaTime ;
		}
		else if(currentTimeHide < 0 && isHide)
		{
			
			//Debug.Log("time bef show < 0") ;
			PopObject() ;
			isHide = false ;
			currentTimeHide = 0 ;
			currentTimeShow = timeIngredientShow ;
		}
	}

	void TimerHide()
	{
		if(currentTimeShow > 0 && !isHide && !levelIsEnd)
		{
			//Debug.Log("time hide ") ;
			currentTimeShow -= Time.deltaTime ;
		}
		else if(currentTimeShow < 0 && !isHide)
		{
			//Debug.Log("time bef hide < 0") ;
			UnpopObject() ;
			isHide = true ;
			currentTimeShow = 0 ;
			currentTimeHide = timeHide ;
		}
	}

	public void ScoreUpdate (float toxicAdded) 
	{
		scoreToxic += toxicAdded;
		PoisonAdded() ;
		Debug.Log(toxicAdded);
	}

	public void AddObjectToList(GameObject gOPop)
	{
		listOfobjectPresent.Add(gOPop) ;
	}

	public void RetireObjectToList(GameObject gORetire)
	{
		for(int u = 0 ; u < listOfobjectPresent.Count ; u++)
		{
			if(listOfobjectPresent[u] == gORetire)
			{
				listOfobjectPresent.Remove(gORetire) ;
			}
		}
	}

	void PopObject()
	{	
		for(int y = 0 ; y < spawnerList.Count ; y++)
		{	
			Debug.Log("Pop") ;
			spawnerList[y].GetComponent<Spawner>().PopFormAndIngredient() ;
		}
	}

	void UnpopObject()
	{
		if(listOfobjectPresent.Count != 0)
			{
				for(int i = 0 ; i < listOfobjectPresent.Count ; i++)
				{
					if(listOfobjectPresent[i] != null && listOfobjectPresent[i].GetComponent<Formes_Et_Ingredients>().ReturnIsDrag())
					{
						listOfobjectPresent[i].GetComponent<Formes_Et_Ingredients>().AddPoint() ;
					}
				}
				
			}

		for(int y = 0 ; y < listOfobjectPresent.Count ; y++)
		{
			Destroy(listOfobjectPresent[y].gameObject) ;
		}

		listOfobjectPresent.Clear() ;
	}

	void PoisonAdded ()
	{
		potionState.fillAmount = scoreToxic / scoreNeed ;
	}

	void LvlSucces ()
	{
		GameManager.Instance().HaveSuccessLevel(numberlvl);
	}

	public void QuitLevel (string levelSelec)
	{
		 SceneManager.LoadScene(levelSelec) ;
	}
}
