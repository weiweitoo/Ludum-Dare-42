using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	public string status = "Sperm";
	public GameObject player;

	private bool enable = false;
	private bool choice01Valid = true;
	private bool choice02Valid = true;
	private bool choice03Valid = true;

	void Start () {
		enable = true;
		UIManagerScript.SetChoice01("i am vallid man.",true);
		UIManagerScript.SetChoice02("this is invalid",false);
	}
	

	void Update () {
		if(enable == true){
			// Update input
			
			if (Input.GetKeyDown(KeyCode.UpArrow)){
	            print("forward key was pressed");
	        }
	        else if (Input.GetKeyDown(KeyCode.LeftArrow)){
	            print("left key was pressed");
	        }
	        else if (Input.GetKeyDown(KeyCode.RightArrow)){
	            print("right key was pressed");
	        }
			//Detect input
				// Player Turn
				// Save state into playerdata
				// some animation
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

	public void UpdateChoice(){
		// Fetch 3 choice by status
		// Check if the condition fulfill
		// Set choice using UIManagerScript.SetChoice01(newtext);
	}

}
