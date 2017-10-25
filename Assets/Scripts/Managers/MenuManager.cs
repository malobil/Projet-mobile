using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public string MainScene;

	public GameObject loadingScreen ;

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
   		loadingScreen.SetActive(true) ;
     	StartCoroutine(LoadMyScene()) ;
        GameManager.Instance().LoadGame() ;
   	}

   	public void DeleteCurrentSave()
   	{
   		GameManager.Instance().DeleteSave() ;
   	}

   	IEnumerator LoadMyScene()
   	{
   		yield return new WaitForSeconds(3f) ;
   		
   		  AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (MainScene);

   		  while(!asyncLoad.isDone)
   		  {
   		  	yield return null ;
   		  }
   	}

}
