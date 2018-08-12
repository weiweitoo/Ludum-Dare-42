using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager{

	public static GameObject GetBlockSpace(){
		return GameObject.Find("BlockSpace");
	}

	public static GameObject GetCurrentPath(){
		return GameObject.Find("CurrentPath");	
	}

	public static GameObject GetUIManager(){
		return GameObject.Find("@UIManager");	
	}
}
