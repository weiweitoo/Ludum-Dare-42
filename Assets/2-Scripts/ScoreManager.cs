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

	public static List<string> responsibility = new List<string>();
	public static List<string> effect = new List<string>();
	public static List<string> category = new List<string>();
	public static string nextGroup = "";

	public static void clearGroup(){
		nextGroup = "";
	}

	void update(){
		coinUI.GetComponent<Text>().text = gold.ToString();
		happinessUI.GetComponent<Text>().text = happiness.ToString();
		skillUI.GetComponent<Text>().text = skill.ToString();
		socializeUI.GetComponent<Text>().text = socialize.ToString();
		ageUI.GetComponent<Text>().text = age.ToString();
	}
}
