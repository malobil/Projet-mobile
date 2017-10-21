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
	private int numberRecetteKnowWith4Ingr = 0 ;


	// Use this for initialization
	void Start () 
	{
		SetUpText() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void SetUpText()
	{
		priceText01.text = price01.ToString("") + " mush";
		priceText02.text = price02.ToString("") + " mush";
		priceText03.text = price03.ToString("") + " mush" ;
	}

	public void BuyRecipe2Ing()
	{
		if(GameManager.Instance().champiBank >= price01)
		{
			CheckRecette2Ing() ;
			Debug.Log(numberRecetteKnowWith2Ingr) ;

			if(obtainLayout != null)
				{
					Destroy(obtainLayoutObject) ;
				}

			if(numberRecetteKnowWith2Ingr < listOfRecipe.recette2Ingredient.Count)
			{
				int tempRandomValor = Random.Range(0,listOfRecipe.recette2Ingredient.Count) ;
				Debug.Log(tempRandomValor) ;
				
				if(!GameManager.Instance().recetteKnow.Contains(listOfRecipe.recette2Ingredient[tempRandomValor]))
				{
					GameObject tempRecipe = Instantiate(prefabRecetteObject, obtainLayout.transform) ;
					tempRecipe.GetComponent<AffichageRecetteScript>().Change(listOfRecipe.recette2Ingredient[tempRandomValor]) ;
					obtainLayoutObject = tempRecipe ;
					GameManager.Instance().AddRecetteKnow(listOfRecipe.recette2Ingredient[tempRandomValor]) ;
					GameManager.Instance().ChampiBank(-price01) ;
					LevelSelectManager.Instance().ChangeChampiText() ;
					GameManager.Instance().SaveGame() ;
				}
				else
				{
					BuyRecipe2Ing() ;
				}
			}
		}
	}

	public void BuyRecipe3Ing()
	{
		if(GameManager.Instance().champiBank >= price02)
		{
		
			CheckRecette3Ing() ;

			if(obtainLayout != null)
			{
				Destroy(obtainLayoutObject) ;
			}

			if(numberRecetteKnowWith3Ingr < listOfRecipe.recette3Ingredient.Count)
			{

				int tempRandomValor2 = Random.Range(0,listOfRecipe.recette3Ingredient.Count) ;
				
				if(!GameManager.Instance().recetteKnow.Contains(listOfRecipe.recette3Ingredient[tempRandomValor2]))
				{
					GameObject tempRecipe1 = Instantiate(prefabRecetteObject, obtainLayout.transform) ;
					tempRecipe1.GetComponent<AffichageRecetteScript>().Change(listOfRecipe.recette3Ingredient[tempRandomValor2]) ;
					obtainLayoutObject = tempRecipe1 ;
					GameManager.Instance().AddRecetteKnow(listOfRecipe.recette3Ingredient[tempRandomValor2]) ;
					GameManager.Instance().ChampiBank(-price02) ;
					LevelSelectManager.Instance().ChangeChampiText() ;
					GameManager.Instance().SaveGame() ;
				}
				else
				{
					BuyRecipe3Ing() ;
				}
			}
		}
	}

	public void BuyRecipe4Ing()
	{
		if(GameManager.Instance().champiBank >= price03)
		{
			CheckRecette4Ing() ;

			if(obtainLayout != null)
			{
				Destroy(obtainLayoutObject) ;
			}

			if(numberRecetteKnowWith4Ingr < listOfRecipe.recette4Ingredient.Count)
			{
				int tempRandomValor3 = Random.Range(0,listOfRecipe.recette4Ingredient.Count) ;
				
				if(!GameManager.Instance().recetteKnow.Contains(listOfRecipe.recette4Ingredient[tempRandomValor3]))
				{
					GameObject tempRecipe2 = Instantiate(prefabRecetteObject, obtainLayout.transform) ;
					tempRecipe2.GetComponent<AffichageRecetteScript>().Change(listOfRecipe.recette4Ingredient[tempRandomValor3]) ;
					obtainLayoutObject = tempRecipe2 ;
					GameManager.Instance().AddRecetteKnow(listOfRecipe.recette4Ingredient[tempRandomValor3]) ;
					GameManager.Instance().ChampiBank(-price03) ;
					LevelSelectManager.Instance().ChangeChampiText() ;
					GameManager.Instance().SaveGame() ;
				}
				else
				{
					BuyRecipe4Ing() ;
				}
			}
		}
	}

	void  CheckRecette4Ing()
	{
		numberRecetteKnowWith4Ingr = 0 ;

		for (int y = 0 ; y < listOfRecipe.recette4Ingredient.Count ; y++)
		{
			if(GameManager.Instance().recetteKnow.Contains(listOfRecipe.recette4Ingredient[y]))
			{
				numberRecetteKnowWith4Ingr++ ;
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
