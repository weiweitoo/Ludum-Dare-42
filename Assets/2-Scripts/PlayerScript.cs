using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float m_movingSpeed = 1;
	public float x = 0;
	public float z = 0;

	private CharacterController m_CharacterController;
	private Vector3 m_MoveDir = Vector3.zero;

	public int right = 0;
	public int left = 0;

	void Start () {
		m_CharacterController = GetComponent<CharacterController>();
		SetChild();
	}
	
	void FixedUpdate () {
		m_MoveDir = new Vector3(x,0,z) * m_movingSpeed;
		m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);
	}

	void Update(){
		if(right == 1){
			TurnRight();
		}
		else if(left == 1){
			TurnLeft();
		}
	}

	public void TurnRight(){
		//fuck it
		if(x == 0 && z == m_movingSpeed){
			x = m_movingSpeed;
			z = 0;
		}
		else if (x == m_movingSpeed && z == 0){
			x = 0;
			z = -m_movingSpeed;
		}
		else if(x == -m_movingSpeed && z == 0){
			x = 0;
			z = m_movingSpeed;
		}
		else if(x == 0 && z == -m_movingSpeed){
			x = -m_movingSpeed;
			z = 0;
		}
		right = 0;
	}

	public void TurnLeft(){
		//fuck it
		if(x == 0 && z == m_movingSpeed){
			x = -m_movingSpeed;
			z = 0;
		}
		else if (x == m_movingSpeed && z == 0){
			x = 0;
			z = m_movingSpeed;
		}
		else if(x == -m_movingSpeed && z == 0){
			x = 0;
			z = -m_movingSpeed;
		}
		else if(x == 0 && z == -m_movingSpeed){
			x = m_movingSpeed;
			z = 0;
		}
		left = 0;
	}

	public void SetChild(){
		m_movingSpeed = 1;
		transform.position = new Vector3(0.0f, 2f, 0.0f);
	}

	//TODO shake camera
}
