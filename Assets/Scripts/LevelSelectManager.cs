using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI ;

public class LevelSelectManager : MonoBehaviour {

	public Text champiText  ;
	public GameObject recipeBook, shopLayout ;
	public List<GameObject> levelList = new List<GameObject>() ;

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
   				levelList[i].SetActive(true) ;
   			}
   		}
   	}

   	public void ChangeChampiText()
   	{
   		champiText.text = GameManager.Instance().ReturnChampiBank().ToString() + " Mush";
   	}

   	public void PopMenu(GameObject thingTopop)
   	{
   	   thingTopop.SetActive(true) ;
   	}

   	public void BackToSelect(GameObject thingToDepop)
   	{
   		thingToDepop.SetActive(false) ;
   	}

    public void GoToMainMenu()
    {
      SceneManager.LoadScene ("Accueil");
    }

}
