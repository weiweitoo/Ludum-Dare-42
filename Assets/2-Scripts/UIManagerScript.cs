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
		AddBlock(10);
		AddBlock(30);
		AddBlock(22);
		AddBlock(55);
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
		//TODO change the color of the block to ligher and beautiful color
		timeBlock.GetComponent<Image>().color = Random.ColorHSV(0f,0.3f);
		timeBlock.transform.parent = GlobalManager.GetBlockSpace().transform;
		timeBlock.transform.SetSiblingIndex(0);
	}

	IEnumerator Absorb(){
		var blockSpace = GlobalManager.GetBlockSpace().transform;
		while(true){
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
}
