using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerTouchControl : MonoBehaviour {

	// Use this for initialization
	public TouchControl touchControl;
	CarController carController;
	

	public void setCarController(CarController carController_){
		carController=carController_;
		touchControl.setCarController(carController_);
	}
}
