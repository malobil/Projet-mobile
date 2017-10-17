using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class AffichageRecetteScript : MonoBehaviour {

	//public GameObject plusImageGO ;

	private int loopNumber = 0 ;

	public GameObject standardImage ;
	public GameObject standardPlusImage ;

	public Sprite plusSprite ;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Change(Scriptable_Recette recetteAffiche)
	{

		for(int i = 0 ; i < (recetteAffiche.recetteForme.Count + recetteAffiche.recetteForme.Count - 1 );i++)
		{
		
			if(loopNumber < recetteAffiche.recetteForme.Count && i%2 == 0)
			{
				GameObject temps = Instantiate(standardImage, this.transform) ;
				temps.GetComponent<Image>().sprite = recetteAffiche.recetteForme[loopNumber].formeImage ;
				temps.SetActive(true) ;
				Debug.Log("change") ;
				loopNumber++ ;
			}
			else if(i%2 == 1)
			{
				GameObject plusTemp = Instantiate(standardPlusImage, this.transform) ;
				plusTemp.GetComponent<Image>().sprite =  plusSprite ;
				plusTemp.SetActive(true) ;
		
			}
		}
	}
}
