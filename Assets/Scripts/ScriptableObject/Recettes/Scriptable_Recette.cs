using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Recette")]
public class Scriptable_Recette : ScriptableObject 
{
	public List<Scriptable_Forme> recetteForme = new  List<Scriptable_Forme>() ;
	public float recetteBonus ;

}
