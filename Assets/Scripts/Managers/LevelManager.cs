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

	////----Quit (hide)---///

	public GameObject quitButton ;

	////----Champi Value By Level---///

	public float levelChampiValue ;

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

        	if(timerMinutes == 0 && timerSecondes == 0 && !pointAdded)
        	{
        		EndLevel() ;
        		UnpopObject() ;
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
		textEndLevel.gameObject.SetActive(true) ;
		if(scoreToxic >= scoreNeed && !pointAdded)
		{	
			textEndLevel.color = Color.green ;
			textEndLevel.text = "VICTOIRE ! =')" ;
			quitButton.SetActive(true);

			if(GameManager.Instance() != null)
			{
				GameManager.Instance().ChampiBank(levelChampiValue) ;
				LvlSucces() ;
				pointAdded = true ;
			}

		}
		else
		{
			textEndLevel.color = Color.red ;
			textEndLevel.text = "DEFAITE ! ='( " ;
			quitButton.SetActive(true);
		}

		levelIsEnd = true ;
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
