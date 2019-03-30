using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl :MonoBehaviour {
	public CarController car;
	// Use this for initializatio
	public void setCarController(CarController car_){
		car =car_;
	}
	public void runOn(){
		if(car)
		car.setTouchMovement(-1);
	}
	public void runOff(){
		if(car)
		car.setTouchMovement(0);
	}

	
	public void breakOn(){
		if(car)
		car.setTouchMovement(1);
	}
	public void breakOff(){
		if(car)
		car.setTouchMovement(0);
	}


	public void jump(){
		if(car)
		car.setTouchJump(true);
	}
}
