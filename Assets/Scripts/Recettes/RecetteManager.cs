using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecetteManager : MonoBehaviour {

	public int maximumIngredient = 2 ;
	public List<GameObject> listOfIngredient = new List<GameObject>() ;

	private static RecetteManager instance ;
    public static RecetteManager Instance () 
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
}
