using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public string MainScene;

	public GameObject loadingScreen, storyScreen ;
	public AudioSource clickAudioSource ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void QuitMenuAccueil ()
   	{
        Application.Quit();
        AudioClick() ;
        Debug.Log("quit game");
    }

	public void PlayMenuAccueil ()
   	{
   		GameManager.Instance().LoadGame() ;
   		AudioClick() ;

   		if(GameManager.Instance().ReturnTuto())
   		{
   			storyScreen.SetActive(true) ;
   			StartCoroutine(LoadMyScene(20f)) ;
   		}
   		else
   		{
   			loadingScreen.SetActive(true) ;
   			StartCoroutine(LoadMyScene(3f)) ;
   		}  
   	}

   	public void DeleteCurrentSave()
   	{
   		GameManager.Instance().DeleteSave() ;
   		AudioClick() ;
   	}

   	void AudioClick()
   	{
   		clickAudioSource.Play() ;
   	}

   	IEnumerator LoadMyScene(float waitingTime)
   	{
   		yield return new WaitForSeconds(waitingTime) ;
   		
   		 AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (MainScene);

   		  while(!asyncLoad.isDone)
   		  {
   		  	yield return null ;
   		  }
   	}

}
