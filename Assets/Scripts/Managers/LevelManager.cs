﻿using System.Collections;
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

	////----Animation----////

	public Animator littleMeg ;

	///----Feedback visu----//
  
  	public ParticleSystem splash, goodFeedBack, badFeedback ;

  	///----Feedback audio---///

  	public AudioSource marmitteAudio ;
  	public AudioClip splashSound ;

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

	////----Score---///

	public float levelChampiValue ;
	public float[] palierScore ;
	private float scoreToxic = 0f;
	public float[] bonusMushByPalier ;
	private float mushBonus = 0f ;

	////----Spawner and object list---///

	public List<GameObject> listOfobjectPresent = new List<GameObject>() ;
	public List<GameObject> spawnerList = new List<GameObject>() ;

	////---- Is tuto ----///

	private bool isTuto = false ;

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

		if(SceneManager.GetActiveScene().name == "Level_Tuto" || SceneManager.GetActiveScene().name == "Level_Tuto_Recipe")
		{
			isTuto = true ;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isTuto)
		{
			TimerShow() ;
			TimerHide() ;
			TimerGeneral() ;

			if(scoreToxic >= scoreNeed && !pointAdded)
			{
				EndLevel() ;
			}
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
		currentTimeShow = -1 ;

		if(scoreToxic >= scoreNeed && !pointAdded)
		{	

			if( palierScore.Length > 0 && scoreToxic >= palierScore[0] && scoreToxic < palierScore[1])
			{
				mushBonus = bonusMushByPalier[0] ;
			  	//levelChampiValue += bonusMushByPalier[0] ;
			  	Debug.Log("Palier1") ;
			   
			}
			else if(palierScore.Length >= 1 && scoreToxic >= palierScore[1] && scoreToxic < palierScore[2])
			{
				//levelChampiValue += bonusMushByPalier[1] ;
				mushBonus = bonusMushByPalier[1] ;
				Debug.Log("Palier2") ;
			}
			else if(palierScore.Length >= 2 && scoreToxic >= palierScore[2])
			{
				//levelChampiValue += bonusMushByPalier[2] ;
				mushBonus = bonusMushByPalier[2] ;
				Debug.Log("Palier3") ;
			}

			if(GameManager.Instance() != null)
			{
				GameManager.Instance().ChampiBank(levelChampiValue + mushBonus) ;
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
			if(GameManager.Instance().ReturnLanguage())
			{
				stateText.color = Color.green ;
				stateText.text = "VICTORY" ;
				timeLeftText.text = "Time left : " + "<b>" + timerMinutes.ToString("f0") + "</b>" + " min " + "<b>" + timerSecondes.ToString("f0")+ "</b>" + " sec" ;
				scoreEndText.text = "Score : " + "<b>" + scoreToxic.ToString("") +  "</b>" +  " / " + "<b>" + scoreNeed.ToString("") + "</b>" ;
				mushGainText.text = "Mush gain : " + "<b>" + levelChampiValue.ToString("") + "</b>" + " + " + "<b>" +  mushBonus.ToString("") + "</b>" ;
			}
			else if(!GameManager.Instance().ReturnLanguage())
			{
				stateText.color = Color.green ;
				stateText.text = "VICTOIRE" ;
				timeLeftText.text = "Temps restant : " + "<b>" + timerMinutes.ToString("f0") + "</b>" + " min " + "<b>" + timerSecondes.ToString("f0")+ "</b>" + " sec" ;
				scoreEndText.text = "Score : " + "<b>" + scoreToxic.ToString("") +  "</b>" +  " / " + "<b>" + scoreNeed.ToString("") + "</b>" ;
				mushGainText.text = "Mush gagnés : " + "<b>" + levelChampiValue.ToString("") + "</b>" + " + " + "<b>" +  mushBonus.ToString("") + "</b>" ;
			}	
		}
		else if(scoreToxic < scoreNeed)
		{
			if(GameManager.Instance().ReturnLanguage())
			{
				stateText.color = Color.red ;
				stateText.text = "DEFEAT" ;
				timeLeftText.text = "<b>No time left !</b>" ;
				scoreEndText.text = "Score : " + "<b>" + scoreToxic.ToString("") + "</b>" + " / " + "<b>" + scoreNeed.ToString("") + "</b>";
				mushGainText.text = "Mushroom gain : " +  "<b>"+ mushBonus + "</b>" ;
			}
			else if(!GameManager.Instance().ReturnLanguage())
			{
				stateText.color = Color.red ;
				stateText.text = "DEFAITE" ;
				timeLeftText.text = "<b>Pas de temps restant !</b>" ;
				scoreEndText.text = "Score : " + "<b>" + scoreToxic.ToString("") + "</b>" + " / " + "<b>" + scoreNeed.ToString("") + "</b>";
				mushGainText.text = "Mush gagnés : " +  "<b>"+ mushBonus + "</b>" ;
			}
			
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
			ShowAnimation() ;
			//PopObject() ;
			isHide = false ;
			currentTimeHide = 0 ;
			currentTimeShow = timeIngredientShow ;
		}
	}

	public void ShowAnimation()
	{
			//Debug.Log("time bef show < 0") ;
			if(spawnerList.Count <= 2)
			{
				littleMeg.SetTrigger("Show") ;
			}
			else if(spawnerList.Count >= 3 && spawnerList.Count <= 4)
			{
				littleMeg.SetTrigger("Show4") ;
			}
			else if(spawnerList.Count >= 5)
			{
				littleMeg.SetTrigger("Show6") ;
			}
	}

	public void HideAnimation()
	{
		littleMeg.SetTrigger("Hide") ;
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
			HideAnimation() ;
			//UnpopObject() ;
			isHide = true ;
			currentTimeShow = 0 ;
			currentTimeHide = timeHide ;
		}
	}

	public void ScoreUpdate (float toxicAdded) 
	{
		scoreToxic += toxicAdded;
		PoisonAdded() ;
		splash.Play() ;
		marmitteAudio.clip = splashSound ;
		marmitteAudio.Play() ;
		Debug.Log(toxicAdded);

		if(toxicAdded < 0)
		{
			badFeedback.Play() ;
		}
		else if(toxicAdded > 0)
		{
			goodFeedBack.Play() ;
		}

		if(scoreToxic < 0)
		{
			scoreToxic = 0 ;
		}
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

	public void PopObject()
	{	
		for(int y = 0 ; y < spawnerList.Count ; y++)
		{	
			Debug.Log("Pop") ;
			spawnerList[y].GetComponent<Spawner>().PopFormAndIngredient() ;
		}
	}

	public void UnpopObject()
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

	public bool ReturnLevelEnd()
	{
		return levelIsEnd ;
	}

	public void EndTuto()
	{
		//HideAnimation() ;
		HideAnimation() ;
		//isHide = false ;
		isTuto = false ;
		if(SceneManager.GetActiveScene().name == "Level_Tuto" )
		{
			GameManager.Instance().DoneTuto() ;
		}
		else
		{
			if(GameManager.Instance() != null)
			{
				GameManager.Instance().AddTutoState() ;
			}
		}
	}

	public void DisableForm(int formIdx)
	{
		listOfobjectPresent[formIdx].GetComponent<Image>().raycastTarget = false ;
	}

	public void EnableForm(int formIdx)
	{
		listOfobjectPresent[formIdx].GetComponent<Image>().raycastTarget = true ;
	}

	public void ChangeSpawnerIngredientList(Scriptable_IngredientList newIngredientList)
	{
		for(int y = 0 ; y < spawnerList.Count ; y++)
		{
			spawnerList[y].GetComponent<Spawner>().associateList = newIngredientList ;
		}
	}

	public void ChangeSpawnerFormList(Scriptable_FormeByLevel newFormeList)
	{
		for(int y = 0 ; y < spawnerList.Count ; y++)
		{
			spawnerList[y].GetComponent<Spawner>().associateFormList = newFormeList ;
		}
	}

	public void PopParticularObject(int idx)
	{
		spawnerList[idx].GetComponent<Spawner>().PopFormAndIngredient() ;
	}

	public void DestroyAObject(GameObject go)
	{
		Destroy(go) ;
		RetireObjectToList(go) ;
	}
}
