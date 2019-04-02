using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBox : MonoBehaviour {

	// Use this for initialization

	Rigidbody2D rb;
	GameController gc;
	Vector3 startPos;
	Quaternion startRot;

	public void setGameController(GameController gameController_){
		gc=gameController_;
	}
	void Start () {
	
	}


	void restart(){
		if(rb){
			Destroy(rb);
			rb=null;
			gameObject.transform.position = startPos;
			gameObject.transform.rotation = startRot;

		}
	}
	void startGame(){
		if(!rb){
			startPos = gameObject.transform.position;
			startRot =gameObject.transform.rotation;
			rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
			rb.mass = 1f;
		
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(gc){
			if(gc.playGame){
				startGame();				
			}else{
				restart();
			}
		}
	}
}
