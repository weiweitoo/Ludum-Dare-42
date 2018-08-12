using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager{

	public static int gold;
	public static int happiness;
	public static int skill;
	public static int socialize;

	public static List<string> responsibility;
	public static List<string> effect;
	public static List<string> category;
	public static string nextGroup;

	public static void clearGroup(){
		nextGroup = "";
	}
}
