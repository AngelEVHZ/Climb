using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
        //if(col.gameObject.tag != "Player")
        //SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);


        if (col.gameObject.tag == "endFlag")
        {
            gameObject.GetComponent<CarController>().gameOver = true;

        }
        else if (col.gameObject.tag == "box")
        {
		
			if(transform.position.y > col.transform.position.y){
				 gameObject.GetComponent<CarController>().gameOver = true;
			}

        }
        else if (col.gameObject.tag == "hammer")
        {
			 gameObject.GetComponent<CarController>().gameOver = true;
        }
        else
        {
            gameObject.GetComponent<CarController>().gameOver = true;

        }
	}
}
