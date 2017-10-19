using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="RecetteAchetable")]
public class Scriptable_Recette_Achetable : ScriptableObject 
{
	public List<Scriptable_Recette> recette2Ingredient = new List<Scriptable_Recette>() ;
	public List<Scriptable_Recette> recette3Ingredient = new List<Scriptable_Recette>() ;
	public List<Scriptable_Recette> recette4Ingredient = new List<Scriptable_Recette>() ;
}
