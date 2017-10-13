using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecetteLevelManager : MonoBehaviour {

	public int maximumIngredient = 2 ;
	public List<GameObject> listOfIngredient = new List<GameObject>() ;
	public List<Scriptable_RecetteByLevel> listOfRecette = new List<Scriptable_RecetteByLevel>() ;

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

	public void ClearList()
	{
		for(int i = 0 ; i < listOfIngredient.Count ; i++)
		{
			Destroy(listOfIngredient[i]) ;
		}

		listOfIngredient.Clear() ;
		Debug.Log("Clear") ;
	}

	public void ConfirmList()
	{
		for(int y = 0 ; y < listOfRecette.Count ; y++)
		{
			if(listOfRecette[y].recetteList.Count == listOfIngredient.Count)
			{
	
			}
		}

		Debug.Log("Confirm") ;
	}
}
