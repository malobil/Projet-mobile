using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI ;

public class LevelSelectManager : MonoBehaviour {

	public Text champiText  ;
	public GameObject recipeBook, shopLayout, missionLayout, recipeBookButton, shopLayoutButton ;
  public GameObject missionPrefab ;
	public List<Scriptable_level> levelList = new List<Scriptable_level>() ;

  //----Tuto Shop----///

  public Animator questAnimator, recipeBookAnimator, shopAnimator ;
  public string[] mamyDialogue ;
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
   			SceneManager.LoadScene (GameManager.Instance().sceneName[levelnumber]);
   		}
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

   	public void ChangeChampiText()
   	{
   		champiText.text = GameManager.Instance().ReturnChampiBank().ToString() + " Mush";
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
      }
      else if(thingTopop.activeInHierarchy)
      {
        thingTopop.SetActive(false) ;
      }
   	}

   	public void BackToSelect(GameObject thingToDepop)
   	{
   		thingToDepop.SetActive(false) ;
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
      mamyText.text = mamyDialogue[tutoState] ;
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
