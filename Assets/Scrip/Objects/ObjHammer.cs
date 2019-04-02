using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjHammer : MonoBehaviour {

	// Use this for initialization
	public float rotationSpeed=5f;
	public bool switchRotation=false;

	GameController gc;
    public void setGameController(GameController gameController_)
    {
        gc = gameController_;
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gc)
        {
            if (gc.playGame)
            {
                if (!switchRotation)
                    transform.Rotate(new Vector3(0f, 0f, rotationSpeed) * Time.deltaTime);
                else
                    transform.Rotate(new Vector3(0f, 0f, -rotationSpeed) * Time.deltaTime);
            }else{
				transform.rotation = Quaternion.identity;
			}
        }

    }
}
