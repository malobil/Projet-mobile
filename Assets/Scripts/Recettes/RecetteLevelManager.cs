﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement  ;

public class RecetteLevelManager : MonoBehaviour {

	public int maximumIngredient = 2 ;
	public List<GameObject> listOfIngredient = new List<GameObject>() ;
	
	private List<Scriptable_Recette> globalList = new List<Scriptable_Recette>() ;
	private int ingredientCorrect ;
	private Scriptable_Recette recetteAscomplish ;

	public GameObject recetteKnowPanel ;
	public GameObject prefabImage ;

	private static RecetteLevelManager instance ;
    public static RecetteLevelManager Instance () 
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
		if(GameManager.Instance() != null)
		{
			globalList = GameManager.Instance().recetteKnow ;
		}
		LoadRecetteAlreadyKnow() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void AddIngredient(GameObject ingredientToAdd)
	{
		if(listOfIngredient.Count < maximumIngredient)
		{
			listOfIngredient.Add(ingredientToAdd) ;
			LevelManager.Instance().RetireObjectToList(ingredientToAdd) ;
		}
		else
		{
			ingredientToAdd.GetComponent<Formes_Et_Ingredients>().AddPoint() ;
		}
	}

	void ClearIngredientList()
	{
		listOfIngredient.Clear() ;
		Debug.Log("Ingredient list clear") ;
	}

	public void ClearList()
	{
		if(!LevelManager.Instance().ReturnLevelEnd())
		{
			if(SceneManager.GetActiveScene().name == "Level_Tuto_Recipe" && !TutoRecipeManager.Instance().ReturnTutoState())
			{
			
			}
			else
			{
				for(int i = 0 ; i < listOfIngredient.Count ; i++)
				{
					Destroy(listOfIngredient[i]) ;
				}

				ClearIngredientList() ;
				Debug.Log("Clear button press") ;
			}
		}
	}

	public void ConfirmList()
	{
		if(!LevelManager.Instance().ReturnLevelEnd())
		{
			if(listOfIngredient.Count != 0)
			{
				for(int i = 0 ; i < globalList.Count ; i++)
				{
					if(listOfIngredient.Count == globalList[i].recetteForme.Count)
					{
						ingredientCorrect = 0 ;
						Debug.Log("Une recette a ce nombre d'ingredient") ;
						
						for(int y = 0 ; y < listOfIngredient.Count ; y++)
						{
							Debug.Log("La recette trouvé contient :" + globalList[i].recetteForme[y].formeImage + "comme ingredient" + y) ;
							Debug.Log(listOfIngredient[y].GetComponent<Formes_Et_Ingredients>().formActual.formeImage) ;

							if(globalList[i].recetteForme.Contains(listOfIngredient[y].GetComponent<Formes_Et_Ingredients>().formActual))
							{
								Debug.Log("TRUE RECETTE PAS DANS L'ORDRE PUTAIN DE MERDE ") ;

								recetteAscomplish = globalList[i] ;
								Debug.Log("J'ai trouvé un ingrédient au bon endroit !") ;
								ingredientCorrect++ ;

								if(ingredientCorrect == listOfIngredient.Count)
								{
									RecetteDone() ;
									Debug.Log("Recette faite : " + recetteAscomplish) ;
								}
							}

							/*if(globalList[i].recetteForme[y].formeImage == listOfIngredient[y].GetComponent<Formes_Et_Ingredients>().formActual.formeImage)
							{
								recetteAscomplish = globalList[i] ;
								Debug.Log("J'ai trouvé un ingrédient au bon endroit !") ;
								ingredientCorrect++ ;

								if(ingredientCorrect == listOfIngredient.Count)
								{
									RecetteDone() ;
								}
							}*/
						}
						
					}
				}
			}

			if(SceneManager.GetActiveScene().name == "Level_Tuto_Recipe" && TutoRecipeManager.Instance() != null)
			{
				if(TutoRecipeManager.Instance().ReturnDemand() && TutoRecipeManager.Instance().ReturnState() == 8)
				{
					TutoRecipeManager.Instance().RequestDone() ;
				}
			}
		}
	}

	void RecetteDone()
	{
		for(int p = 0 ; p < listOfIngredient.Count ; p++)
			{
				listOfIngredient[p].GetComponent<Formes_Et_Ingredients>().AddPoint() ;
			}

		LevelManager.Instance().ScoreUpdate(recetteAscomplish.recetteBonus) ;

		/*if(!GameManager.Instance().recetteKnow.Contains(recetteAscomplish))
		{
			UpdateRecetteKnowImage() ;
			GameManager.Instance().AddRecetteKnow(recetteAscomplish) ;
		}*/
		
		ClearIngredientList() ;
		Debug.Log("Vous avez effectué une recette !") ;
	}

	void UpdateRecetteKnowImage()
    {
    	GameObject tempsInstant = Instantiate(prefabImage, recetteKnowPanel.transform) ;
    	tempsInstant.GetComponent<AffichageRecetteScript>().Change(recetteAscomplish) ;
    }

    void LoadRecetteAlreadyKnow()
    {
    	if(GameManager.Instance() != null)
    	{
	    	for(int y = 0 ; y < GameManager.Instance().recetteKnow.Count ; y++)
	    	{
	    		if(GameManager.Instance().recetteKnow[y] != null)
	    		{
	    			GameObject popStartTemp = Instantiate(prefabImage, recetteKnowPanel.transform) ;
	    			popStartTemp.GetComponent<AffichageRecetteScript>().Change(GameManager.Instance().recetteKnow[y]) ;
	    		}
	    	}
	    }
    }
}
