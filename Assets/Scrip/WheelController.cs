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
		if (obj.gameObject.tag == "ground" || 
			obj.gameObject.tag == "box" ||
			obj.gameObject.tag == "pipe" ||
			obj.gameObject.tag == "ramp"
		) {
			carController.onGround (wheelNumber,true);
		}
	}


	void OnCollisionExit2D(Collision2D obj){
		if (obj.gameObject.tag == "ground" || 
			obj.gameObject.tag == "box" ||
			obj.gameObject.tag == "pipe" ||
			obj.gameObject.tag == "ramp"

		) {
			carController.onGround (wheelNumber,false);
		}

	}

	void OnCollisionEnter2D(Collision2D obj){
		if (obj.gameObject.tag == "hammer" ||
			obj.gameObject.tag == "spike"  
			) {
			carController.gameOver = true;
			if(wheelNumber==1){
				Destroy(carController.wheelB);
				carController.wheelB = car.AddComponent<WheelJoint2D>() as WheelJoint2D;
			}else{
				Destroy(carController.wheelF);
				carController.wheelF = car.AddComponent<WheelJoint2D>() as WheelJoint2D;

			}
		}
		
	}

}
