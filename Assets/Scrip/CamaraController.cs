using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour {
	public GameObject target;
	private CarController carController;
	// Use this for initialization

	void Start () {
		
	}

	public void setTarget(GameObject target_){
		target = target_;
		carController = target.GetComponent<CarController> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(target){
			if (!carController.gameOver) {
				Vector3 newPosition =  new Vector3(target.transform.position.x,transform.position.y,-10f);
				transform.position = newPosition;
			}
		}
	}
}
