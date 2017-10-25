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

	public Button button2, button3,button4 ;
	public GameObject text2,text3,text4 ;

	public AudioSource shopAudio ;

	private GameObject obtainLayoutObject  ;

	private int numberRecetteKnowWith2Ingr = 0 ;
	private int numberRecetteKnowWith3Ingr = 0 ;
	private int numberRecetteKnowWith4Ingr = 0 ;

	private static ShopManager instance ;
    public static ShopManager Instance () 
    {
        return instance;
    }

void Awake ()
    {
        if (instance != null)
        {
            Destroy (gameObject);
        }

        else 

        {
            instance = this;
        }
    }

	// Use this for initialization
	void Start () 
	{
		SetUpText() ;
		CheckIfAllBuyRecipe2() ;
		CheckIfAllBuyRecipe3() ;
		CheckIfAllBuyRecipe4() ;

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void SetUpText()
	{
		priceText01.text = price01.ToString("") ;
		priceText02.text = price02.ToString("");
		priceText03.text = price03.ToString("") ;
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
				
			if(LevelSelectManager.Instance().TutoStateValorReturn() == 2)
			{
				LevelSelectManager.Instance().TutoShop() ;
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
					shopAudio.Play() ;
					GameManager.Instance().SaveGame() ;
				}
				else
				{
					BuyRecipe2Ing() ;
				}
			}
			else
			{
				CheckIfAllBuyRecipe2() ;
			}
		}
	}

	void CheckIfAllBuyRecipe2()
	{
		if(numberRecetteKnowWith2Ingr >= listOfRecipe.recette2Ingredient.Count)
		{
			button2.interactable = false ;
			text2.SetActive(true) ;
		}
	}

	void CheckIfAllBuyRecipe3()
	{
		if(numberRecetteKnowWith3Ingr >= listOfRecipe.recette3Ingredient.Count)
		{
			button3.interactable = false ;
			text3.SetActive(true) ;
		}
	}

	void CheckIfAllBuyRecipe4()
	{
		if(numberRecetteKnowWith4Ingr >= listOfRecipe.recette4Ingredient.Count)
		{
			button4.interactable = false ;
			text4.SetActive(true) ;
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
					shopAudio.Play() ;
					GameManager.Instance().SaveGame() ;
				}
				else
				{
					BuyRecipe3Ing() ;
				}
			}
			else
			{
				CheckIfAllBuyRecipe3() ;
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
					shopAudio.Play() ;
					GameManager.Instance().SaveGame() ;
				}
				else
				{
					BuyRecipe4Ing() ;
				}
			}
			else
			{
				CheckIfAllBuyRecipe4() ;
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

	public void ChangeAchetableList(Scriptable_Recette_Achetable newAchetable)
	{
		listOfRecipe = newAchetable ;
	}

}
