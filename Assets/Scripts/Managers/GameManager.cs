using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public List <bool> lvlSuccess = new List <bool>();
	public List <string> sceneName = new List <string>();

	public GameObject gameManager ;

	public Text textChampiScore ;

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
    	DontDestroyOnLoad(gameManager);
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
		textChampiScore.text = champiBank.ToString("") ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void HaveSuccessLevel (int lvlnumber)
	{
		lvlSuccess[lvlnumber] = true;
	}

	public void LoadLevel (int levelnumber)
   	{
   		if (lvlSuccess[levelnumber] == true)
   		{
   			SceneManager.LoadScene (sceneName[levelnumber]);
   		}
   	}

   	public void ChampiBank (float champiObtained)
   	{
   		champiBank += champiObtained ;
   	}

}