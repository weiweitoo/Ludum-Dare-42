using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float m_movingSpeed = 1;
	public float x = 0;
	public float z = 0;
	public GameObject path;

	private CharacterController m_CharacterController;
	private Vector3 m_MoveDir = Vector3.zero;
	public bool enable = false;

	//For audio
	private AudioSource m_AudioSource;
	[SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.

	// Debug
	public int right = 0;
	public int left = 0;

	void Start () {
		m_CharacterController = GetComponent<CharacterController>();
		m_AudioSource = GetComponent<AudioSource>();
		SetChild();
		StartCoroutine(FootStepAudio());
	}
	
	void FixedUpdate () {
		if(enable == true){
			m_MoveDir = new Vector3(x,0,z) * m_movingSpeed;
			m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);
		}
	}

	void Update(){
		if(right == 1){
			TurnRight();
		}
		else if(left == 1){
			TurnLeft();
		}
	}

	public IEnumerator FootStepAudio(){
		while(true){
			PlayFootStepAudio();
			yield return new WaitForSeconds(0.2f + Mathf.Clamp(1-(m_movingSpeed/10), 0.3f, 2f));
		}
	}

	private void PlayFootStepAudio(){
        int n = Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }

	public void Enable(){
		enable = true;
	}

	public void Disable(){
		enable = false;
	}

	public void TurnRight(){
		//fuck it
		if(x == 0 && z == 1){
			x = 1;
			z = 0;
		}
		else if (x == 1 && z == 0){
			x = 0;
			z = -1;
		}
		else if(x == -1 && z == 0){
			x = 0;
			z = 1;
		}
		else if(x == 0 && z == -1){
			x = -1;
			z = 0;
		}
		right = 0;

		// spawn road	
		// TODO mencorrectkan the pivot point
		// make it transition effect
		GameObject currentPath = GlobalManager.GetCurrentPath();
		float height = currentPath.transform.position.y;
		Quaternion newRotation = currentPath.transform.rotation * Quaternion.Euler(0, 90, 0);
		Vector3 newPosition = new Vector3(transform.position.x,height,transform.position.z);

		GameObject newPath = Instantiate(path,newPosition,newRotation) as GameObject;
		if(GameObject.Find("PrevPath")){
			Destroy(GameObject.Find("PrevPath"));
		}
		currentPath.name = "PrevPath";
		newPath.name = "CurrentPath";
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
