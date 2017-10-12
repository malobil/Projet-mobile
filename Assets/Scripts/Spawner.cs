using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class Spawner : MonoBehaviour 
{

	public Scriptable_IngredientList associateList ;

	public Scriptable_FormeByLevel associateFormList ;

	public Image ingredientImage ;
	public Image formImage ;

	private Scriptable_Ingredient choosenItem ;
	private int randomNumberIngredient ;
	private int randomNumberForm ;


	// Use this for initialization
	/*void Start () 
	{
		PopIngredient() ;
		PopForm() ;
	}*/
	
	void OnEnable()
	{
		PopIngredient() ;
		PopForm() ;
		Debug.Log("Enable") ;
	}
	// Update is called once per frame
	void Update () 
	{
		/*if(Input.GetKeyDown("p"))
		{
			PopIngredient() ;
			PopForm() ;
		}*/
	}

	void PopForm()
	{
		randomNumberForm = Random.Range(0, associateFormList.formList.Length) ;
		formImage.sprite = associateFormList.formList[randomNumberForm].formeImage ;
	}

	void PopIngredient()
	{
		randomNumberIngredient = Random.Range(0, associateList.itemList.Length) ;
		choosenItem = associateList.itemList[randomNumberIngredient] ;
		ingredientImage.sprite = choosenItem.ingredientImage ;
	}

	public void OnClick()
	{
		LevelManager.Instance().ScoreUpdate(choosenItem.toxicValor) ;
		gameObject.SetActive(false) ;
		//Debug.Log("Click") ;
	}
}
