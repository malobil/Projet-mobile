using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI ;

public class LevelSelectManager : MonoBehaviour {

	public Text champiText, champiText2,champiText3  ;
	public GameObject recipeBook, shopLayout, missionLayout, recipeBookButton, shopLayoutButton ;
  public GameObject missionPrefab ;
	public List<Scriptable_level> levelList = new List<Scriptable_level>() ;
  ///----Loading----///

  public GameObject loadingScreen ;

  ///-----Sound----///

  public AudioSource globalAudioSource ;
  public AudioClip shopAudio ;
  public AudioClip clasicAudio ; 

  //----Tuto Shop----///

  public Animator questAnimator, recipeBookAnimator, shopAnimator ;
  public string[] mamyDialogue ;
  public string[] mamyDialogueFr ; 
  public GameObject mamyLayout, shopQuitButton, button1,button2,button3, arrowBuy2, arrowBuy3, arrowBuy4, arrowObtain;
  public Text mamyText ;
  public Scriptable_Recette_Achetable tutoAchetableList, normalAchetableList ;
  private int tutoState = -1 ;
  private bool mamyIsTalking ;
  private bool mamyAskingSomething = false ;

  private static LevelSelectManager instance ;
  public static LevelSelectManager Instance () 
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
		ChangeChampiText() ;
		PopLevel() ;
    Tuto() ;
	}
	
	// Update is called once per frame
	void Update () 
  {
		  if(mamyIsTalking && Input.GetMouseButtonDown(0) && !mamyAskingSomething)
      {
        TutoShop() ;
      }
	}

	public void LoadLevel (int levelnumber)
   	{
   		if (GameManager.Instance().lvlSuccess[levelnumber] == true)
   		{
        loadingScreen.SetActive(true) ;
   			StartCoroutine(WaitLoadLevel(levelnumber)) ; 
   		}
   	}

  IEnumerator WaitLoadLevel(int levelToLoad)
  {
        yield return new WaitForSeconds(3f) ;
       AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameManager.Instance().sceneName[levelToLoad]);
        
        /*while(!asyncLoad.isDone)
        {
          yield return null ;
        }*/
  }

   	public void PopLevel()
   	{
   		for(int i = 0 ; i < levelList.Count ; i++)
   		{
   			if(GameManager.Instance().lvlSuccess[i] == true)
   			{
   				//levelList[i].SetActive(true) ;
          GameObject temp = Instantiate(missionPrefab,missionLayout.transform) ;
          temp.GetComponent<LevelSelectPanel>().level = levelList[i] ;
   			}
   		}
   	}

    public void PopLevel3()
    {
          GameObject temp = Instantiate(missionPrefab,missionLayout.transform) ;
          temp.GetComponent<LevelSelectPanel>().level = levelList[3] ;
    }

   	public void ChangeChampiText()
   	{
   		champiText.text = GameManager.Instance().ReturnChampiBank().ToString() ;
      champiText2.text =  GameManager.Instance().ReturnChampiBank().ToString()  ;
      champiText3.text =  GameManager.Instance().ReturnChampiBank().ToString()  ;
   	}

   	public void PopMenu(GameObject thingTopop)
   	{ 
      if(!thingTopop.activeInHierarchy)
      {
   	   thingTopop.SetActive(true) ;

        if(thingTopop.name == "Panel" && GameManager.Instance().ReturnTutoState() == 0)
        {
          GameManager.Instance().AddTutoState() ;
          Tuto() ;
        }
        else if(thingTopop.name == "Panel" && GameManager.Instance().ReturnTutoState() == 1)
        {
          questAnimator.SetTrigger("UnBlink") ;
        }
        else if(thingTopop.name == "Boutique_Layout" && GameManager.Instance().ReturnTutoState() == 2)
        {
          GameManager.Instance().AddTutoState() ;
          Tuto() ;
        }

        if(thingTopop.name == "Boutique_Layout")
        {
          ChangeAudio(shopAudio) ;
        }
      }
      else if(thingTopop.activeInHierarchy)
      {
        thingTopop.SetActive(false) ;
      }
   	}

    public void ChangeAudio(AudioClip newClip)
    {
        globalAudioSource.clip = newClip ;
        globalAudioSource.Play() ;
    }

   	public void BackToSelect(GameObject thingToDepop)
   	{
   		thingToDepop.SetActive(false) ;

      if(globalAudioSource.clip == shopAudio)
      {
        ChangeAudio(clasicAudio) ;
      }
      
   	}

    public void GoToMainMenu()
    {
      SceneManager.LoadScene ("Accueil");
    }

  public void Tuto()
  {
      if(GameManager.Instance().ReturnTutoState() == 0)
      {
        questAnimator.SetTrigger("Blink") ;
      }
      else if(GameManager.Instance().ReturnTutoState() == 1)
      {
        questAnimator.SetTrigger("Blink") ;
      }
      else if(GameManager.Instance().ReturnTutoState() == 2)
      {
        questAnimator.SetTrigger("UnBlink") ;
        shopLayoutButton.SetActive(true) ;
        shopAnimator.SetTrigger("Blink") ;
      }
      else if(GameManager.Instance().ReturnTutoState() == 3)
      {
         shopAnimator.SetTrigger("UnBlink") ;
         TutoShop() ;
         shopLayout.SetActive(true) ;
         shopQuitButton.SetActive(false) ;
         mamyIsTalking = true ;
      }

      if(GameManager.Instance().ReturnTutoState() > 3)
      {
        shopLayoutButton.SetActive(true) ;
      }
  }

  public void TutoShop()
  {
    if(tutoState +1 < mamyDialogue.Length)
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
      button1.GetComponent<Button>().interactable = false ;
      button2.GetComponent<Button>().interactable = false ;
      button3.GetComponent<Button>().interactable = false ;
      Debug.Log("tuto state shop " + tutoState) ;
    }
    else if(tutoState +1 >= mamyDialogue.Length)
    {
      Debug.Log("End tuto shop") ;
      mamyIsTalking = false ;
      mamyLayout.SetActive(false) ;
      shopQuitButton.SetActive(true) ;
      button1.GetComponent<Button>().interactable = true ;
      button2.GetComponent<Button>().interactable = true ;
      button3.GetComponent<Button>().interactable = true ;
      arrowBuy2.SetActive(false) ;
      arrowBuy3.SetActive(false) ;
      arrowBuy4.SetActive(false) ;
      arrowObtain.SetActive(false) ;
      GameManager.Instance().AddTutoState() ;
      GameManager.Instance().UnlockLvl2() ;
      PopLevel3() ;
      GameManager.Instance().SaveGame() ;
    }
    
    if(tutoState == 2)
    {
        GameManager.Instance().ChampiBank(100f) ;
        arrowBuy2.SetActive(true) ;
        mamyAskingSomething = true ;
        ShopManager.Instance().ChangeAchetableList(tutoAchetableList) ;
        button1.GetComponent<Button>().interactable = true ;
    }
    else if(tutoState == 3)
    {
      arrowBuy2.SetActive(false) ;
      mamyAskingSomething = false ;
      button1.GetComponent<Button>().interactable = false ;
      arrowObtain.SetActive(true) ;
    }
    else if(tutoState == 4)
    {
        arrowObtain.SetActive(false) ;
        arrowBuy3.SetActive(true) ;
    }
    else if(tutoState == 5)
    {
      arrowBuy3.SetActive(false) ;
      arrowBuy4.SetActive(true) ;
      ShopManager.Instance().ChangeAchetableList(normalAchetableList) ;
    }
  }

  public int TutoStateValorReturn()
  {
    return tutoState ;
  }
}
