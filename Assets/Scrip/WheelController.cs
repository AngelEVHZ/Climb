using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour {
	public int wheelNumber= 1;

	private GameObject car;
	private CarController carController;
		

	void Start () {
		car = transform.parent.gameObject;
		carController = car.GetComponent<CarController> ();
	}

	void OnCollisionStay2D(Collision2D obj){
		if (obj.gameObject.tag == "ground") {
			carController.onGround (wheelNumber,true);
		}
	}


	void OnCollisionExit2D(Collision2D obj){
		if (obj.gameObject.tag == "ground") {
			carController.onGround (wheelNumber,false);
		}

	}

}
