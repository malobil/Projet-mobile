using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class ShopManager : MonoBehaviour 
{

	public Text priceText01,priceText02,priceText03 ;
	public int price01, price02, price03 ;
	public Scriptable_Recette_Achetable listOfRecipe ;
	public GameObject obtainLayout ;
	public GameObject prefabRecetteObject ;

	private GameObject obtainLayoutObject  ;

	private int numberRecetteKnowWith2Ingr = 0 ;
	private int numberRecetteKnowWith3Ingr = 0 ;


	// Use this for initialization
	void Start () 
	{
		//priceText01.text = price01.ToString("") + " mush";
		//priceText02.text = price02.ToString("") + " mush";
		//priceText03.text = price03.ToString("") + " mush" ;
		//BuyRecipe2Ing() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void BuyRecipe2Ing()
	{
		CheckRecette2Ing() ;
		//Debug.Log(numberRecetteKnowWith2Ingr) ;
		if(numberRecetteKnowWith2Ingr < listOfRecipe.recette2Ingredient.Count)
		{
			if(obtainLayout != null)
			{
				Destroy(obtainLayoutObject) ;
			}

			int tempRandomValor = Random.Range(0,listOfRecipe.recette2Ingredient.Count) ;
			
			if(!GameManager.Instance().recetteKnow.Contains(listOfRecipe.recette2Ingredient[tempRandomValor]))
			{
				GameObject tempRecipe = Instantiate(prefabRecetteObject, obtainLayout.transform) ;
				tempRecipe.GetComponent<AffichageRecetteScript>().Change(listOfRecipe.recette2Ingredient[tempRandomValor]) ;
				obtainLayoutObject = tempRecipe ;
				GameManager.Instance().AddRecetteKnow(listOfRecipe.recette2Ingredient[tempRandomValor]) ;
				GameManager.Instance().SaveGame() ;
			}
			else
			{
				BuyRecipe2Ing() ;
			}
		}
	}

	public void BuyRecipe3Ing()
	{
		CheckRecette2Ing() ;
		if(numberRecetteKnowWith2Ingr < listOfRecipe.recette3Ingredient.Count)
		{
			if(obtainLayout != null)
			{
				Destroy(obtainLayoutObject) ;
			}

			int tempRandomValor = Random.Range(0,listOfRecipe.recette3Ingredient.Count) ;
			
			if(!GameManager.Instance().recetteKnow.Contains(listOfRecipe.recette3Ingredient[tempRandomValor]))
			{
				GameObject tempRecipe = Instantiate(prefabRecetteObject, obtainLayout.transform) ;
				tempRecipe.GetComponent<AffichageRecetteScript>().Change(listOfRecipe.recette3Ingredient[tempRandomValor]) ;
				obtainLayoutObject = tempRecipe ;
				GameManager.Instance().AddRecetteKnow(listOfRecipe.recette3Ingredient[tempRandomValor]) ;
				GameManager.Instance().SaveGame() ;
			}
			else
			{
				BuyRecipe3Ing() ;
			}
		}
	}

	void CheckRecette2Ing()
	{	
		numberRecetteKnowWith2Ingr = 0 ;

		for (int i = 0 ; i < listOfRecipe.recette2Ingredient.Count ; i++)
		{
			if(GameManager.Instance().recetteKnow.Contains(listOfRecipe.recette2Ingredient[i]))
			{
				numberRecetteKnowWith2Ingr++ ;
			}
		}
		
	}

	void CheckRecette3Ing()
	{
		numberRecetteKnowWith3Ingr = 0 ;

		for (int y = 0 ; y < listOfRecipe.recette3Ingredient.Count ; y++)
		{
			if(GameManager.Instance().recetteKnow.Contains(listOfRecipe.recette3Ingredient[y]))
			{
				numberRecetteKnowWith3Ingr++ ;
			}
		}
	}

}
