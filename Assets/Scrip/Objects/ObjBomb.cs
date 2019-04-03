using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBomb : MonoBehaviour {

	// Use this for initialization
	
	GameController gc;
	Rigidbody2D rb;
	float aliveTime=0.1f;
    public void setGameController(GameController gameController_)
    {
        gc = gameController_;
    }
	
	
	void OnCollisionEnter2D(Collision2D col){
		if(
			col.gameObject.tag=="pipe" ||
			col.gameObject.tag=="hammer" ||
			col.gameObject.tag=="box" ||
			col.gameObject.tag=="ramp"
		)
		Destroy(col.gameObject);
		Destroy(gameObject);
		
	}

	void Update () {
		if(gc){
			if(gc.setRampOk){
				aliveTime-= Time.deltaTime;
				if(!rb){
					rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
				}

				if (aliveTime<=0f)
				{
					Destroy(gameObject);
				}
			}
		}
	}

	// Update is called once per frame

}
