using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMeg : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Show()
	{
		LevelManager.Instance().PopObject() ;
	}

	public void Hide()
	{
		LevelManager.Instance().UnpopObject() ;
	}
}
