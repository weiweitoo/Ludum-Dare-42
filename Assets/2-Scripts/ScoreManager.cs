using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager{

	public static int gold = 0;
	public static int happiness = 0;
	public static int skill = 0;
	public static int socialize = 0;

	public static List<string> responsibility = new List<string>();
	public static List<string> effect = new List<string>();
	public static List<string> category = new List<string>();
	public static string nextGroup = "";

	public static void clearGroup(){
		nextGroup = "";
	}
}
