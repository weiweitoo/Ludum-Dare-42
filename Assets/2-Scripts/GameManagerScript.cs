using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	public string status = "Sperm";
	public GameObject player;

	public bool enable = true;
	private bool choice01Valid = true;
	private bool choice02Valid = true;
	private bool choice03Valid = true;

	private Category currentCategory;

	void Start () {
		enable = true;
		StartCoroutine(Wait(2));
		StartCoroutine(RefreshTime());
	}

	IEnumerator Wait(int x){
		yield return new WaitForSeconds(x);
		UpdateChoice();
	}
	
	void Update () {
		if(enable == true){
			// Update input
			
			if (Input.GetKeyDown(KeyCode.UpArrow)){
	            UpdateStatus(currentCategory.choices[0].statusUpdates);
	            GlobalManager.GetUIManager().GetComponent<UIManagerScript>().AddBlock(currentCategory.choices[0].time);
	            UpdateChoice();
	        }
	        else if (Input.GetKeyDown(KeyCode.LeftArrow)){
	            UpdateStatus(currentCategory.choices[1].statusUpdates);
	            GlobalManager.GetUIManager().GetComponent<UIManagerScript>().AddBlock(currentCategory.choices[1].time);
	            player.GetComponent<PlayerScript>().TurnLeft();
	            UpdateChoice();
	        }
	        else if (Input.GetKeyDown(KeyCode.RightArrow)){
	            UpdateStatus(currentCategory.choices[2].statusUpdates);
	            GlobalManager.GetUIManager().GetComponent<UIManagerScript>().AddBlock(currentCategory.choices[2].time);
	            player.GetComponent<PlayerScript>().TurnRight();
	            UpdateChoice();
	        }
		}
	}

	public void StartGame(){
		enable = true;
		player.GetComponent<PlayerScript>().Enable();
	}

	public void PauseGame(){
		enable = false;
		player.GetComponent<PlayerScript>().Disable();
	}

	public void EndGame(){
		enable = false;
		player.GetComponent<PlayerScript>().Disable();
	}

	IEnumerator RefreshTime(){
		while(true){
			ScoreManager.age++;
			if(ScoreManager.age >= 10){
				StartCoroutine(ShowGameOver());
			}
			yield return new WaitForSeconds(5);
		}
	}

	private bool CheckChoice(int x){
		return GlobalManager.GetUIManager().GetComponent<UIManagerScript>().GetRemainingSpace() + x <= 350;

	}

	public void UpdateChoice(){
		// Fetch 3 choice by status
		currentCategory = GameObject.Find("BabyData").GetComponent<CSVReaderScript>().GetRandomGroup();
		UIManagerScript.SetChoice01(currentCategory.choices[0].message,currentCategory.choices[0].whyFail == "");
		UIManagerScript.SetChoice02(currentCategory.choices[1].message,currentCategory.choices[1].whyFail == "");
		UIManagerScript.SetChoice03(currentCategory.choices[2].message,currentCategory.choices[2].whyFail == "");
	}

	public void UpdateStatus(StatusUpdates x){
		ScoreManager.happiness += x.happy;
		ScoreManager.gold += x.gold;
		ScoreManager.skill += x.skill;
		ScoreManager.socialize += x.socialize;
	}

	public IEnumerator ShowGameOver(){
		UIManagerScript UIManagerComponent = GlobalManager.GetUIManager().GetComponent<UIManagerScript>();
		UIManagerComponent.sideBarUI.SetActive(false);
		UIManagerComponent.choiceUI.SetActive(false);
		UIManagerComponent.endingUI.SetActive(true);
		UIManagerComponent.analysisUI.SetActive(true);
		yield return new WaitForSeconds(6);
		UIManagerComponent.analysisUI.SetActive(false);
		yield return new WaitForSeconds(1);
		UIManagerComponent.commentUI.SetActive(true);
		yield return new WaitForSeconds(6);
		UIManagerComponent.commentUI.SetActive(false);
		yield return new WaitForSeconds(1);
		UIManagerComponent.gameOverUI.SetActive(true);
	}

}
