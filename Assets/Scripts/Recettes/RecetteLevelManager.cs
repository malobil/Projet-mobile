using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecetteLevelManager : MonoBehaviour {

	public int maximumIngredient = 2 ;
	public List<GameObject> listOfIngredient = new List<GameObject>() ;
	
	private Scriptable_RecetteByLevel globalList ;
	private int ingredientCorrect ;
	private Scriptable_Recette recetteAscomplish ;

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
		globalList = GameManager.Instance().recetteList ; 
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
		for(int i = 0 ; i < listOfIngredient.Count ; i++)
		{
			Destroy(listOfIngredient[i]) ;
		}

		ClearIngredientList() ;
		Debug.Log("Clear button press") ;
	}

	public void ConfirmList()
	{
		if(listOfIngredient.Count != 0)
		{
			for(int i = 0 ; i < globalList.recetteList.Count ; i++)
			{
				if(listOfIngredient.Count == globalList.recetteList[i].recetteForme.Count)
				{
					ingredientCorrect = 0 ;
					Debug.Log("Une recette a ce nombre d'ingredient") ;
					
					for(int y = 0 ; y < listOfIngredient.Count ; y++)
					{
						Debug.Log("La recette trouvé contient :" + globalList.recetteList[i].recetteForme[y].formeImage + "comme ingredient" + y) ;
						Debug.Log(listOfIngredient[y].GetComponent<Formes_Et_Ingredients>().formActual.formeImage) ;

						if(globalList.recetteList[i].recetteForme[y].formeImage == listOfIngredient[y].GetComponent<Formes_Et_Ingredients>().formActual.formeImage)
						{
							recetteAscomplish = globalList.recetteList[i] ;
							Debug.Log("J'ai trouvé un ingrédient au bon endroit !") ;
							ingredientCorrect++ ;

							if(ingredientCorrect == listOfIngredient.Count)
							{
								RecetteDone() ;
							}
						}
					}
					
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

		ClearIngredientList() ;
		Debug.Log("Vous avez effectué une recette !") ;
	}
}
