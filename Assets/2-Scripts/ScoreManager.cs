using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour{

	public static int gold = 0;
	public static int happiness = 0;
	public static int skill = 0;
	public static int socialize = 0;
	public static int age = 0;
	public GameObject ageUI;
	public GameObject coinUI;
	public GameObject socializeUI;
	public GameObject skillUI;
	public GameObject happinessUI;
	public GameObject respUI1, respUI2, respUI3;

	public static List<string> responsibility = new List<string>();
	public static List<string> effect = new List<string>();
	public static List<string> category = new List<string>();
	public static string nextGroup = "";

	public void clearGroup(){
		nextGroup = "";
	}

    void Start(){
		Debug.Log("Started");
	}
	void Update(){
		gold++;
		coinUI.GetComponent<Text>().text = gold.ToString();
		happinessUI.GetComponent<Text>().text = happiness.ToString();
		skillUI.GetComponent<Text>().text = skill.ToString();
		socializeUI.GetComponent<Text>().text = socialize.ToString();
		ageUI.GetComponent<Text>().text = age.ToString();
		for (int i = 0; i < responsibility.Count; i++){
			switch (i){
				case 0:
					respUI1.GetComponent<Text>().text = responsibility[i];
					break;
				case 1:
					respUI2.GetComponent<Text>().text = responsibility[i];
					break;
				case 2:
					respUI3.GetComponent<Text>().text = responsibility[i];
					break;
			}
		}
	}
}
