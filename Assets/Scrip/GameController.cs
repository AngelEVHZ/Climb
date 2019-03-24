using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {


	public Sprite[] rampSprites;
	public int spriteIndex = 0;
	public Image imgObjSelected;
	public GameObject EditModeCanvas;


	public float boardH = 6;
	public float boardW = 2;


	public GameObject rampa;
	private GameObject newRamp;

	public bool editMode = false;
	public bool setRamp = false;

	// Use this for initialization
	public Camera[] cameras;
	public int cameraIndex = 0;
	private int cameraNumber;
	void Start () {
		cameraNumber = cameras.Length;
	}

	void nextSprite(){
		spriteIndex++;
		if (spriteIndex >= rampSprites.Length)
			spriteIndex = 0;

	}

	Vector3 getNewPos(){
		Vector3 mp = cameras [1].ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0f));
		float newX = Mathf.Floor (mp.x ) ;
		float newY = Mathf.Floor (mp.y ) ;
		return new Vector3 (newX, newY, 0f);
	}

	void rampEditor(){
		if (editMode) {
			EditModeCanvas.SetActive(true);
			if (Input.GetKeyDown (KeyCode.X)) {
				nextSprite ();
				if (setRamp) {
					newRamp.GetComponent<ObjController> ().setSprite(rampSprites[spriteIndex]);
					newRamp.GetComponent<ObjController> ().updateCollider();
				}
			}
			if (Input.GetMouseButtonDown (1)) {
				if (setRamp) {
					newRamp.transform.localScale = new Vector3 (newRamp.transform.localScale.x * -1f,1f,1f);
				}
			}

			if (!setRamp) {
				setRamp = true;
				Vector3 newPos = getNewPos ();
				newRamp = Instantiate (rampa, newPos, Quaternion.identity);
				newRamp.GetComponent<ObjController> ().setSprite(rampSprites[spriteIndex]);
				
				
			} else {
				newRamp.transform.position = getNewPos();
			}
			if (Input.GetMouseButtonDown (0)) {
				setRamp = false;
			}
		} else {
			EditModeCanvas.SetActive(false);
			if (setRamp) {
				setRamp = false;
				Destroy (newRamp);
			}
		}
	
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			nextCamera ();
		}

		if (cameraIndex == 1) {
			editMode = true;
		} else {
			editMode = false;
		}

		rampEditor ();
	}

	void nextCamera(){
		cameraIndex++;
		if (cameraIndex >= cameraNumber)
			cameraIndex = 0;
		for (int i = 0; i < cameraNumber; i++) {
			cameras [i].gameObject.SetActive (false);
		}
		cameras [cameraIndex].gameObject.SetActive (true);
		
	}
}


