using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI ;

public class MenuManager : MonoBehaviour {

	public string MainScene;

  public Text playText, delSaveText, quitText, storyText ;


	public GameObject loadingScreen, storyScreen, storyScreenFr ;
	public AudioSource clickAudioSource ;

	// Use this for initialization
	void Start () 
  {
		if(!GameManager.Instance().ReturnLanguage())
    	{
	      playText.text = "JOUER" ;
	      delSaveText.text = "Supr.Sauv" ;
	      quitText.text = "Quitter" ;
    	}
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
   			if(GameManager.Instance().ReturnLanguage())
   			{
   				storyScreen.SetActive(true) ;
   			}
   			else if(!GameManager.Instance().ReturnLanguage())
   			{
   				storyScreenFr.SetActive(true) ;
   			}
   			
   			StartCoroutine(LoadMyScene(25f)) ;
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
