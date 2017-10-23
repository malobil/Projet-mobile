using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class Spawner : MonoBehaviour
{

	public Scriptable_IngredientList associateList ;

	public Scriptable_FormeByLevel associateFormList ;

	public GameObject prefab ;

	private Scriptable_Ingredient choosenItem ;
	private GameObject objectPop ;

	private int randomNumberIngredient ;
	private int randomNumberForm ;

	public float decalage ;
	public float decalageStar ;

	

	// Use this for initialization
	/*void Start () 
	{
		PopFormAndIngredient() ;
	}*/
	
	// Update is called once per frame
	void Update () 
	{
		/*if(Input.GetKeyDown("p"))
		{
			PopFormAndIngredient() ;
		}*/
	}

	public void PopFormAndIngredient()
	{
		randomNumberForm = Random.Range(0, associateFormList.formList.Length) ; // choose a random form
		randomNumberIngredient = Random.Range(0, associateList.itemList.Length) ; // choose a random ingredient

		objectPop = Instantiate(prefab, transform.position,Quaternion.identity,transform.parent) ; // pop and stock the form pop
		choosenItem = associateList.itemList[randomNumberIngredient] ; // stock the item choose
		
		objectPop.GetComponent<Image>().sprite = associateFormList.formList[randomNumberForm].formeImage ; // Change form pop sprite
		objectPop.transform.GetChild(0).GetComponent<Image>().sprite = choosenItem.ingredientImage ; // change ingredient pop sprite
		
		if(randomNumberForm == 1)
		{
			objectPop.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition += new Vector2(0,-decalage) ;
		}
		else if(randomNumberForm == 3)
		{
			objectPop.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition += new Vector2(0,-decalageStar) ;
		}

		objectPop.GetComponent<Formes_Et_Ingredients>().SetToxicValueAndform(choosenItem.toxicValor,associateFormList.formList[randomNumberForm]) ;

		LevelManager.Instance().AddObjectToList(objectPop) ;
	}
	
}
