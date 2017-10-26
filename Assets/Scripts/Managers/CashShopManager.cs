using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashShopManager : MonoBehaviour {

	public AudioSource cashShopAudio ;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void BuyMush(int mushAdd)
	{
		GameManager.Instance().ChampiBank(mushAdd) ;
		LevelSelectManager.Instance().ChangeChampiText() ;
		cashShopAudio.Play() ;
	}
}
