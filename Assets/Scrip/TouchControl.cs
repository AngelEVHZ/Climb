using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl :MonoBehaviour {
	public CarController car;
	// Use this for initializatio

	public void runOn(){
		car.setTouchMovement(-1);
	}
	public void runOff(){
		car.setTouchMovement(0);
	}

	
	public void breakOn(){
		car.setTouchMovement(1);
	}
	public void breakOff(){
		car.setTouchMovement(0);
	}


	public void jump(){
		car.setTouchJump(true);
	}
}
