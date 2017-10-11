using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_ManagerFinal : MonoBehaviour {

	private float totalCount = 0f ; // Barre_Score
	public float addCount; // Score ajouté au Score quand on appuie sur Bouton_Prod
	public Text countText ; // Texte de Barre_Score 
	public Text countPerMinute ; // Texte de Barre_Score/min
	public float cdTime ; // Temps en seconde du décompte de Barre_Score/min avant d'ajouter sa valeur à Barre_Score
	private float actualCD ; // Variable qui va servir de cooldown
	public float countAddPerMinute ; // Variable du score qui va s'ajouter chaque minutes à Barre_Score
	public Text scoreAdvancement; // Texte de Barre_Objectif
	public float scoreAdvancing; // Variable de Barre_Objectif correspondant au Score total
	public float scoreToAdvance; // Variable de Barre_Objectif correspondant à l'objectif à atteindre
	public bool lvlUp = false; // Est-ce que le bouton +2 est actif ou non 
	public float priceMultiplier; // Multiplieur de prix à chaque amélioration
	public float scoreMultiplier; // Multiplieur de score à chaque amélioration
	public Text plusButton ; // Texte de Bouton_Augmentation
	private float secretCD = 0f ; // Variable d'augmentation de Bouton_Augmentation (+2 +3 +4)
	public float secretCDInterval ; // Variable requise pour augmenter le Bouton_Augmentationp
	public GameObject shop ; // Shop
	public GameObject shopButton ; // Bouton_Shop
	public GameObject power ;
	private bool buybuy = true;
   	private bool coolingDown = false;
    public float waitTime;
    private float waitTimeActual;
	public Button buttonBoost;
    public float boostMultiplier;
    public Image imageBoost;
    public GameObject saveMenu;
    public GameObject quitMenu;
    public GameObject shopQuit;
    private bool boostOn = false;
    public GameObject pepelBoost;
    public Button pepelButton;
    public Button pepelBoostButton;
    private bool buyPepel = true;
    private bool pepelBoostActive = false;
    public float pepelBoostMultiplier;


	// Use this for initialization
	void Start () {
		actualCD = cdTime;
		TotalChange();
		SPMChange();
		waitTimeActual = waitTime;
		pepelBoostButton.interactable = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CoolDown();
		ScoreAdvance();
		SPMPlusButton();
		PlusButtonChange();
		ShopButton() ;
		CoolDownBoost();
		UnlockBooster();
		SPMChange();

	}
	// CLick de Bouton_Prod
	public void OnClick () {
		totalCount = totalCount + addCount ;
		TotalChange();
	}
	// Cooldown pour Bouton_Score/min
	void CoolDown () 
	{
		 actualCD -= Time.deltaTime;
         if(actualCD < 0)
         {
			totalCount = totalCount + (countAddPerMinute / 60f) ;
			Debug.Log(totalCount) ;
			actualCD = cdTime;
			TotalChange();
         }
	}
	// Recharger texte de Barre_Score
	void TotalChange () {
		countText.text = totalCount.ToString("F0");
	}
	// Recharger texte de Barre_Score/min
	void SPMChange () {
		countPerMinute.text = countAddPerMinute.ToString("F0") + " / m";
	}
	// Recharger texte de Barre_Objectif
	void SAChange () {
		scoreAdvancement.text = scoreAdvancing.ToString("F0") + " / " + Mathf.FloorToInt(scoreToAdvance).ToString("F0");
	}
	// Recharger texte de Bouton_Augmantation
	void PlusButtonChange () {
		plusButton.text = "+ " + scoreMultiplier.ToString();
	}
	// Code de Barre_Objectif
	void ScoreAdvance () {
		scoreAdvancing = totalCount;
		scoreToAdvance = Mathf.FloorToInt(scoreToAdvance);
		if (Mathf.FloorToInt(scoreAdvancing) >= Mathf.FloorToInt(scoreToAdvance))
			{
				scoreAdvancing = Mathf.FloorToInt(scoreToAdvance);
				lvlUp = true;
			}
		else if (scoreAdvancing <= Mathf.FloorToInt(scoreToAdvance))
			{
				lvlUp = false;
			}
			SAChange();
	}
	// Code de Barre_Score/min
	public void SPMAdvance () {
		if (lvlUp == true)
			{
				totalCount -= Mathf.FloorToInt(scoreToAdvance) ;
				scoreToAdvance *= priceMultiplier ;
				if (coolingDown == true)
				{
					if (pepelBoostActive == true)
					{
					countAddPerMinute += scoreMultiplier * boostMultiplier * pepelBoostMultiplier;
					addCount += scoreMultiplier * boostMultiplier;
					}
					if (pepelBoostActive == false)
					{
					countAddPerMinute += scoreMultiplier * boostMultiplier;
					addCount += scoreMultiplier * boostMultiplier;
					}

				}
				else if (coolingDown == false)
				{
					countAddPerMinute += scoreMultiplier;
					addCount += scoreMultiplier;
				}
				secretCD += 1;
				lvlUp = false;
				TotalChange();
				SPMChange();
			}
	}
	// Code de Bouton_Augmentation
	void SPMPlusButton () {
		if (secretCD == secretCDInterval) 
			{
				scoreMultiplier += 1;
				secretCD = 0;
			}
	}
	// Code de Layout_Boutique
	void ShopButton () 
	{
				shopButton.SetActive(true) ;
	
	}

	public void ShopOn ()
	{
		shop.SetActive(true);
		shopQuit.SetActive(true);



	}
	public void ShopOff ()
	{
		shop.SetActive(false);
		shopQuit.SetActive(false);
	}
	
	public void OnClickPepel ()
	{
		if(totalCount >= 500 && buyPepel == true) 
			{
				pepelBoost.SetActive(true) ;	
				totalCount -= 500;
				buyPepel = false;
				boostOn = true;
				TotalChange();
				ShopOff();
				pepelButton.interactable = false;
			}
	}

	public void OnClickBoostPepel ()
	{
		pepelBoost.SetActive(false);
		pepelBoostActive = true;
		countAddPerMinute *= pepelBoostMultiplier;
	}


	void UnlockBooster ()
	{
		if (buybuy == true)
		{
			if(totalCount >= 5000) 
			{
				power.SetActive(true);
				TotalChange();
				buybuy = false;
			}
		}
	}

	void CoolDownBoost	()
	{		

				if (coolingDown == true)
			       	{
			            imageBoost.fillAmount += (1.0f / waitTime) * Time.deltaTime;
			            waitTimeActual -= Time.deltaTime;
			            buttonBoost.interactable = false;
					}
				if (imageBoost.fillAmount == 1f && coolingDown == true && pepelBoostActive == true)
					{
					 	coolingDown = false;
					 	countAddPerMinute /= boostMultiplier * pepelBoostMultiplier;
					 	SPMChange();
					 	waitTimeActual = waitTime;
					 	buttonBoost.interactable = true;
					 	pepelButton.interactable = true;
					 	pepelBoostActive = false;
					 	buyPepel = true;
					 	pepelBoostButton.interactable = false;
					}

				if (imageBoost.fillAmount == 1f && coolingDown == true && pepelBoostActive == false)
					{
					 	coolingDown = false;
					 	countAddPerMinute /= boostMultiplier;
					 	SPMChange();
					 	waitTimeActual = waitTime;
					 	pepelButton.interactable = true;
					 	buttonBoost.interactable = true;
					 	buyPepel = true;
					 	pepelBoostButton.interactable = false;
					}

	}	
	public void BoostActive ()
	{
		if (totalCount >= 50)
		{
			pepelBoostButton.interactable = true;
			countAddPerMinute *=  boostMultiplier;
			SPMChange();
			totalCount -= 50f;
			coolingDown = true;
			TotalChange ();
			imageBoost.fillAmount = 0;
		}
	}
public void SaveLoadOn (){
		saveMenu.SetActive(true);
	}

	public void SaveLoadOff (){
		saveMenu.SetActive(false);
	}

	public void QuitMenuOn (){
		quitMenu.SetActive(true);
	}

	public void QuitMenuOff (){
		quitMenu.SetActive(false);
	}

	public void Quit (){
		Application.Quit();
	}
		public void Save ()
	{
		PlayerPrefs.SetFloat("totalCount",totalCount);
		PlayerPrefs.SetFloat("countAddPerMinute",countAddPerMinute);
		PlayerPrefs.SetFloat("scoreToAdvance",scoreToAdvance);
		PlayerPrefs.SetFloat("priceMultiplier",priceMultiplier);
		PlayerPrefs.SetFloat("scoreMultiplier",scoreMultiplier);
		PlayerPrefs.SetFloat("secretCD",secretCD);
		PlayerPrefs.SetFloat("secretCDInterval",secretCDInterval);
		PlayerPrefs.SetInt("buybuy",buybuy ? 1 : 0);
		PlayerPrefs.SetInt("coolingDown",coolingDown ? 1 : 0);
		PlayerPrefs.SetFloat("waitTime",waitTime);
		PlayerPrefs.SetFloat("waitTimeActual",waitTimeActual);
		PlayerPrefs.SetFloat("boostMultiplier",boostMultiplier);
		PlayerPrefs.SetFloat("imageBoost",imageBoost.fillAmount);
		PlayerPrefs.SetInt("boostOn",boostOn ? 1 : 0);
		PlayerPrefs.SetInt("buyPepel",buyPepel ? 1 : 0);
		PlayerPrefs.SetInt("pepelBoostActive",pepelBoostActive ? 1 : 0);
		PlayerPrefs.SetFloat("pepelBoostMultiplier",pepelBoostMultiplier);
		/* PlayerPrefs.SetString("countText",countText);
		PlayerPrefs.SetString("countPerMinute",countPerMinute);
		PlayerPrefs.SetString("scoreAdvancement",scoreAdvancement);
		PlayerPrefs.SetString("plusButton",plusButton);
		PlayerPrefs.SetString("buyText",buyText); */
		PlayerPrefs.Save();
		saveMenu.SetActive(false);
	}
	public void Load ()
	{
		totalCount = PlayerPrefs.GetFloat("totalCount");
		countAddPerMinute = PlayerPrefs.GetFloat("countAddPerMinute");
		scoreToAdvance = PlayerPrefs.GetFloat("scoreToAdvance");
		priceMultiplier = PlayerPrefs.GetFloat("priceMultiplier");
		scoreMultiplier = PlayerPrefs.GetFloat("scoreMultiplier");
		secretCD = PlayerPrefs.GetFloat("secretCD");
		secretCDInterval = PlayerPrefs.GetFloat("secretCDInterval");
		buybuy = Convert.ToBoolean(PlayerPrefs.GetInt("buybuy"));
		coolingDown = Convert.ToBoolean(PlayerPrefs.GetInt("coolingDown"));
		waitTime = PlayerPrefs.GetFloat("waitTime");
		waitTimeActual = PlayerPrefs.GetFloat("waitTimeActual");
		boostMultiplier = PlayerPrefs.GetFloat("boostMultiplier");
		imageBoost.fillAmount = PlayerPrefs.GetFloat("imageBoost");
		boostOn = Convert.ToBoolean(PlayerPrefs.GetInt("boostOn"));
		buyPepel = Convert.ToBoolean(PlayerPrefs.GetInt("buyPepel"));
		pepelBoostActive = Convert.ToBoolean(PlayerPrefs.GetInt("pepelBoostActive"));
		pepelBoostMultiplier = PlayerPrefs.GetFloat("pepelBoostMultiplier");
		TotalChange();
		SPMChange();
		SAChange();
		PlusButtonChange();
		saveMenu.SetActive(false);
		if(boostOn == true)
		{
			power.SetActive(true);
		}

		/* if (buybuyToInt == 0f)
			{
				buybuy = false;
			}
		else if (buybuyToInt == 1f)
			{
				buybuy = true;
			}
		if (coolingDownToInt == 0f)
			{
				buybuy = false;
			}
		if (coolingDownToInt == 1f)
			{
				buybuy = false;
			}
		if (coolingDownSheepToInt == 0f)
			{
				buybuy = false;
			}
		if (coolingDownSheepToInt == 1f)
			{
				buybuy = false;
			} */
		/* countText = PlayerPrefs.GetString("countText");
		countPerMinute = PlayerPrefs.GetString("countPerMinute");
		scoreAdvancement = PlayerPrefs.GetString("scoreAdvancement");
		plusButton = PlayerPrefs.GetString("plusButton");
		buyText = PlayerPrefs.GetString("buyText"); */
	}
}