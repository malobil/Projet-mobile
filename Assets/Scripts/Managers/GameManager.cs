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

	//------Save recette system ------///

	public List<int> recetteKnowIdx = new List<int>() ;
	private bool hadSaveARecipe = false ;

	////----Champi system---///

	public float champiBank ;

    ///----Tuto----///

    private bool isFirstTime = true ;

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
    		recetteKnowIdx.Add(recetteList.recetteList.IndexOf(recetteToAdd)) ;
    		hadSaveARecipe = true ;
    		//Debug.Log(recetteList.recetteList.IndexOf(recetteToAdd) + "recette Index") ;
    	}
    }


    /////---------SAVE----------//////

    public void SaveGame()
    {
    	Debug.Log("SAVE") ;
    	PlayerPrefs.SetFloat("PlayerScore", champiBank) ;

  		PlayerPrefs.SetInt("HaveSaveRecipe", Convert.ToInt32(hadSaveARecipe)) ;

        PlayerPrefs.SetInt("HaveDoneTuto", Convert.ToInt32(isFirstTime)) ;

    	for(int i = 0 ; i < lvlSuccess.Count ; i++)
    	{
    		PlayerPrefs.SetInt("LvlSuccesList" + i, Convert.ToInt32(lvlSuccess[i])) ;
    	}

    	for(int y = 0 ; y < recetteKnowIdx.Count ; y++)
    	{
    		PlayerPrefs.SetInt("RecipeDiscover" + y, recetteKnowIdx[y]) ;
    	}
    }

    public void LoadGame()
    {
    	Debug.Log("LOAD") ;

    	if(PlayerPrefs.HasKey("PlayerScore"))
    	{
    		champiBank = PlayerPrefs.GetFloat("PlayerScore") ;
    	}

    	if(PlayerPrefs.HasKey("HaveSaveRecipe"))
    	{
    		hadSaveARecipe = Convert.ToBoolean(PlayerPrefs.GetInt("HaveSaveRecipe")) ;
    	}

        if(PlayerPrefs.HasKey("HaveDoneTuto"))
        {
            isFirstTime = Convert.ToBoolean(PlayerPrefs.GetInt("HaveDoneTuto")) ;
        }

    	for(int y = 1 ; y < lvlSuccess.Count ; y++)
    	{
    		lvlSuccess[y] = Convert.ToBoolean(PlayerPrefs.GetInt("LvlSuccesList" + y)) ;
    	}

    	if(hadSaveARecipe)
    	{
	    	for(int z = 0 ; z < recetteList.recetteList.Count ; z++)
	    	{
	    		//Debug.Log("Boucle + " + z) ;
	    		if(!recetteKnow.Contains(recetteList.recetteList[PlayerPrefs.GetInt("RecipeDiscover" + z)]) && PlayerPrefs.HasKey("RecipeDiscover" + z))
	    		{
	    			//Debug.Log("Poping " + z) ;
	    			recetteKnow.Add(recetteList.recetteList[PlayerPrefs.GetInt("RecipeDiscover" + z)]) ;
	    			recetteKnowIdx.Add(PlayerPrefs.GetInt("RecipeDiscover"+z)) ;
	    		}
	    	}
	    }
    }

    public void DeleteSave()
    {
    	Debug.Log("DELETE SAVE") ;

    	/*for(int p = 1 ; p < lvlSuccess.Count ; p++)
    	{
    		PlayerPrefs.DeleteKey("LvlSuccesList" + p) ;
    	}

    	for(int g = 0 ; g < recetteKnow.Count ; g++)
    	{
    		PlayerPrefs.DeleteKey("RecipeDiscover" + g) ;
    	}

    	PlayerPrefs.DeleteKey("PlayerScore") ;
    	PlayerPrefs.DeleteKey("HaveSaveRecipe") ;*/

    	PlayerPrefs.DeleteAll() ;
    }

    public bool ReturnTuto()
    {
        return isFirstTime ;
    }

    public void DoneTuto()
    {
        isFirstTime = false ;
        SaveGame() ;
    }

}