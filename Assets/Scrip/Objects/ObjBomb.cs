using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBomb : MonoBehaviour {

	// Use this for initialization
	
	GameController gc;

    public void setGameController(GameController gameController_)
    {
        gc = gameController_;
    }
	
	
	void OnTriggerStay2D(Collider2D col){
			Debug.Log(col.gameObject.tag);		
	}

		void OnCollisionStay2D(Collision2D col){
		Debug.Log(col.gameObject.tag);
		
	}

	// Update is called once per frame

}
