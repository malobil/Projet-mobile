using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public string MainScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void QuitMenuAccueil ()
   	{
        Application.Quit();
        Debug.Log("quit game");
    }

	public void PlayMenuAccueil ()
   	{
        SceneManager.LoadScene (MainScene);
        GameManager.Instance().LoadGame() ;
   	}

}
