using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController : MonoBehaviour {


	GameController gameController;
	public int objNumber=0;

/*

1: large box
2: box
3: large ramp
4: smal ramp
5:hammer

*/
	public void setGameController(GameController gameController_){
		gameController=gameController_;
		switch(objNumber){
			case 2:
				GetComponent<ObjBox>().setGameController(gameController);
			break;
			case 5:
				GetComponent<ObjHammer>().setGameController(gameController);
			break;
			case 6:
				GetComponent<ObjBomb>().setGameController(gameController);
			break;

			default:
			break;
			;
		}

	}


}
