using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour {
	public GameObject target;
	private CarController carController;
	// Use this for initialization
	void Start () {
		carController = target.GetComponent<CarController> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!carController.gameOver) {
			Vector3 newPosition = target.transform.position;
			newPosition.z = -10;
			transform.position = newPosition;
		}
	}
}
