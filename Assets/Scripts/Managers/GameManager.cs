using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System ;

public class GameManager : MonoBehaviour {

	public List <bool> lvlSuccess = new List <bool>();
	public List <string> sceneName = new List <string>();

	////----Recette system---///

	public Scriptable_RecetteByLevel recetteList ;

	public List<Scriptable_Recette> recetteKnow = new List<Scriptable_Recette>() ;

	////----Champi system---///

	public float champiBank ;

	////----Instance System---///

	private static GameManager instance ;
    public static GameManager Instance () 
    {
        return instance;
    }

	void Awake ()
    {
    	DontDestroyOnLoad(gameObject);
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

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown("s"))
		{
			SaveGame() ;
		}

		if(Input.GetKeyDown("l"))
		{
			LoadGame() ;
		}

		if(Input.GetKeyDown("d"))
		{
			DeleteSave() ;
		}
	}

	public void HaveSuccessLevel (int lvlnumber)
	{
		lvlSuccess[lvlnumber] = true;
		SaveGame() ;
	}

   	public void ChampiBank (float champiObtained)
   	{
   		champiBank += champiObtained ;
   	}

   	public float ReturnChampiBank()
   	{
   		return champiBank ;
   	}


   	/////-------------Recette Manager ----------------////////

    public void AddRecetteKnow(Scriptable_Recette recetteToAdd)
    {
    	if(!recetteKnow.Contains(recetteToAdd))
    	{
    		recetteKnow.Add(recetteToAdd) ;
    	}
    }


    /////---------SAVE----------//////

    public void SaveGame()
    {
    	Debug.Log("SAVE") ;
    	PlayerPrefs.SetFloat("PlayerScore", champiBank) ;

    	for(int i = 0 ; i < lvlSuccess.Count ; i++)
    	{
    		PlayerPrefs.SetInt("LvlSuccesList" + i, Convert.ToInt32(lvlSuccess[i])) ;
    	}
    }

    public void LoadGame()
    {
    	Debug.Log("LOAD") ;
    	champiBank = PlayerPrefs.GetFloat("PlayerScore") ;

    	for(int y = 1 ; y < lvlSuccess.Count ; y++)
    	{
    		lvlSuccess[y] = Convert.ToBoolean(PlayerPrefs.GetInt("LvlSuccesList" + y)) ;
    	}
    }

    public void DeleteSave()
    {
    	Debug.Log("DELETE SAVE") ;

    	for(int p = 1 ; p < lvlSuccess.Count ; p++)
    	{
    		PlayerPrefs.DeleteKey("LvlSuccesList" + p) ;
    	}
    }

}