using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI ;

public class LevelSelectManager : MonoBehaviour {

	public Text champiText  ;
	public GameObject recipeBook, shopLayout, missionLayout ;
  public GameObject missionPrefab ;
	public List<Scriptable_level> levelList = new List<Scriptable_level>() ;

  //----Tuto----///

  public Animator questAnimator, recipeBookAnimator, shopAnimator ;

  private int tutoState = 0 ;

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
    if(GameManager.Instance().ReturnTuto())
    {
       Tuto() ;
    }
	}
	
	// Update is called once per frame
	void Update () {
		
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

        if(thingTopop.name == "Panel" && tutoState==0 && GameManager.Instance().ReturnTuto())
        {
          tutoState++ ;
          Tuto() ;
          Debug.Log(tutoState) ;
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
      if(tutoState == 0)
      {
        questAnimator.SetTrigger("Blink") ;
      }
      if(tutoState == 1)
      {
        questAnimator.SetTrigger("UnBlink") ;
      }
  }

  public int ReturnState()
  {
    return tutoState ;
  }

}
