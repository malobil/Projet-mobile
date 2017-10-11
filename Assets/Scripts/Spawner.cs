using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class Spawner : MonoBehaviour 
{

	public Scriptable_IngredientList associateList ;

	public Image ingredientImage ;

	private int randomNumber ;


	// Use this for initialization
	void Start () 
	{
		PopIngredient() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void PopIngredient()
	{
		randomNumber = Random.Range(0, associateList.itemList.Length) ;
		ingredientImage.sprite = associateList.itemList[randomNumber].ingredientImage ;
	}
}
