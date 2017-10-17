using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public List <bool> lvlSuccess = new List <bool>();
	public List <string> sceneName = new List <string>();

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
		
	}

	public void HaveSuccessLevel (int lvlnumber)
	{
		lvlSuccess[lvlnumber] = true;
	}

   	public void ChampiBank (float champiObtained)
   	{
   		champiBank += champiObtained ;
   	}

   	public float ReturnChampiBank()
   	{
   		return champiBank ;
   	}

}