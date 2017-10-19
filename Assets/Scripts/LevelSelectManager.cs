using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI ;

public class LevelSelectManager : MonoBehaviour {

	public Text champiText  ;
	public GameObject recipeBook, shopLayout ;

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
}
