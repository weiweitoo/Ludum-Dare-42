using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour {
	public GameObject panel;
	public List<float> block = new List<float>();

	// Debug variable
	public int absorbCounter = 0;

	// Use this for initialization
	void Start () {
		AddBlock(10);
		AddBlock(30);
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
		block.Add(pixel);
		GameObject timeBlock = Instantiate(panel) as GameObject;
		RectTransform rectTransform = timeBlock.GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2 (rectTransform.rect.width,pixel);
		timeBlock.GetComponent<Image>().color = Random.ColorHSV(0f,0.3f);
		timeBlock.transform.parent = GlobalManager.GetBlockSpace().transform;
	}

	IEnumerator Absorb(){
		var blockSpace = GlobalManager.GetBlockSpace().transform;
		int count = blockSpace.childCount;
		while(true)
	    {
	        absorbCounter += 1;
	        for(int i = 0;i < count;i++){
	        	// blockSpace.GetChild(i).GetComponent<RectTransform>().localPosition = new Vector2();;
	        }
	        yield return new WaitForSeconds(1);
	    }
	}
}
