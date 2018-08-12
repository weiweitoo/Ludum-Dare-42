using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour {
	public GameObject panel;
	public List<float> block = new List<float>();
	public float absorbSpeed = 0.3f;

	// Use this for initialization
	void Start () {
		StartCoroutine(Absorb());
	}
	
	// Update is called once per frame
	void Update () {
	}

	public float GetRemainingSpace(){
		float sum = 0;
		for(int i = 0;i< block.Count;i++){
			sum+= block[i];
		}
		return 350 - sum;
	}

	public void AddBlock(float pixel){
		block.Insert(0,pixel);
		GameObject timeBlock = Instantiate(panel) as GameObject;
		RectTransform rectTransform = timeBlock.GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2 (rectTransform.rect.width,pixel);
		timeBlock.GetComponent<Image>().color = new Color(Random.Range(0.4f, 1.0f),Random.Range(0.4f, 1.0f),Random.Range(0.4f, 1.0f));
		timeBlock.transform.parent = GlobalManager.GetBlockSpace().transform;
		timeBlock.transform.SetSiblingIndex(0);
	}

	IEnumerator Absorb(){
		var blockSpace = GlobalManager.GetBlockSpace().transform;
		while(true && blockSpace.childCount != 0){
	        var lastIndex = blockSpace.childCount - 1;
	        var rectTransform = blockSpace.GetChild(lastIndex).GetComponent<RectTransform>();
	        rectTransform.sizeDelta = new Vector2 (rectTransform.rect.width,--block[lastIndex]);

	        if(rectTransform.sizeDelta.y == 0){
	        	block.Remove(lastIndex);
	        	Destroy (blockSpace.GetChild(lastIndex).gameObject);
	        	Debug.Log("Absorbed");
	        }

	        yield return new WaitForSeconds(absorbSpeed);
	    }
	}

	//////////////////////////////////////////////////////////////////////////
	//// Status Bar
	//////////////////////////////////////////////////////////////////////////

	//TODO update status bar
	public static void UpdateStatusBar(){
		// do something here lol
	}


	//////////////////////////////////////////////////////////////////////////
	//// Choice
	//////////////////////////////////////////////////////////////////////////
	public static void SetChoice01(string newText,bool valid){
		Text textComponent = GameObject.Find("Choice01Text").GetComponent<Text>();
		textComponent.text = newText;
		if(valid == false){
			textComponent.color = new Color(0.3f,0.3f,0.3f);
		}
		else{
			textComponent.color = new Color(0,0,0);
		}
	}

	public static void SetChoice02(string newText,bool valid){
		Text textComponent = GameObject.Find("Choice02Text").GetComponent<Text>();
		textComponent.text = newText;
		if(valid == false){
			textComponent.color = new Color(0.3f,0.3f,0.3f);
		}
		else{
			textComponent.color = new Color(0,0,0);
		}
	}

	public static void SetChoice03(string newText,bool valid){
		Text textComponent = GameObject.Find("Choice03Text").GetComponent<Text>();
		textComponent.text = newText;
		if(valid == false){
			textComponent.color = new Color(0.3f,0.3f,0.3f);
		}
		else{
			textComponent.color = new Color(0,0,0);
		}
	}

	public static string GetChoice01(){
		return GameObject.Find("Choice01Text").GetComponent<Text>().text;
	}

	public static string SetChoice02(){
		return GameObject.Find("Choice02Text").GetComponent<Text>().text;
	}

	public static string SetChoice03(){
		return GameObject.Find("Choice03Text").GetComponent<Text>().text;
	}
}
