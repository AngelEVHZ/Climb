using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
		//if(col.gameObject.tag != "Player")
		//SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		gameObject.GetComponent<CarController>().restart();	
	}
}
