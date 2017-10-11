using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	private float scoreToxic = 0f;

	private static LevelManager instance ;
    public static LevelManager Instance () 
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

	public void ScoreUpdate (float toxicAdded) 
	{
		scoreToxic += toxicAdded;
		Debug.Log(scoreToxic);
	}


}
