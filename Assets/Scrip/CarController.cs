using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CarController : MonoBehaviour {
	
	public WheelJoint2D wheelB;
	public WheelJoint2D wheelF;

	public Rigidbody2D rb;
	public Rigidbody2D rbWheelB;
	public Rigidbody2D rbWheelF;

	public GameObject startPosition;
	public GameObject endPoint;
	public bool gameOver = false;
	public float gameOverTimer = 1.5f;

	public float groundGravity = 1f;
	public float airGravity = 0.5f;
	public float speed = 1500f;
	private float tempSpeed=0f;
	private float movement = 0f;
	private float rotation =0f;
	public float rotationSpeed = 15f;
	public float jumpForce = 100f;
	private bool jumpTime = false;
	private int jumpCount = 0;

	private float jump_timer =0;
	private float jump_timer_seconds=1f;

	private float rotationSpeedTemp = 15f;
	public float airRotationSpeed = 15f;
	public float deceleration=300f;
	public float aceleration=300f;
	private bool brake = false;
	private bool wheelOngroundB=false;
	private bool wheelOngroundF=false;
	private bool carOnGround = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		tempSpeed = speed;
		speed = 0;
		rotationSpeedTemp = rotationSpeed;

		motorTimer = motorTimerMax;

	}

	public void restart(){
		transform.position = startPosition.transform.position;
	}

	public void onGround(int wheelNumber, bool onGround){
		if (wheelNumber == 1) {
			wheelOngroundB= onGround;
		} else if (wheelNumber == 2) {
			wheelOngroundF= onGround;
		}
	}
	// Update is called once per frame


	bool isGameOver(){
		if(endPoint.transform.position.y > transform.position.y){
			gameOver = true;
		}
		if (gameOver) {
			gameOverTimer -= Time.deltaTime;
			if (gameOverTimer <= 0f) {
				restart ();
				gameOver = false;
			}
		}
		return gameOver;
	}

	void checkOnGround(){
		if (!wheelOngroundB && !wheelOngroundF) {
			carOnGround = false;
			setGravity (airGravity);
			rotationSpeed = airRotationSpeed;
		} else {
			carOnGround = true;
			setGravity (groundGravity);
			rotationSpeed = rotationSpeedTemp;
			if (jump_timer <= 0f) {
				jumpTime = false;
				jumpCount = 0;
			}
		}
	}

	void jump(){
		if (jump_timer >= 0f) {
			jump_timer -= Time.deltaTime;
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (!jumpTime ) {
				if(!carOnGround)
					jumpTime = true;
				jumpCount++;
				jump_timer = jump_timer_seconds;
				if(jumpCount==1)
					rb.AddForce (new Vector2 (0, jumpForce));
				else
					rb.AddForce (new Vector2 (0, (jumpForce/1.5f) ));
			}
		}
	}

	void test(){
		if (Input.GetKeyDown (KeyCode.P)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}

	}

	void setGravity(float gravity){
		rb.gravityScale = gravity;
		rbWheelB.gravityScale = gravity;
		rbWheelF.gravityScale = gravity;
	}



	void Update () {

		if (!isGameOver ()) {
			movement = -Input.GetAxisRaw ("Vertical");
			rotation = Input.GetAxisRaw ("Horizontal");
		
			checkOnGround ();
			jump ();


			//Debug.Log ("Movement "+ movement);
		}

		test ();
	}

	void breaking(){
		if (Input.GetKey (KeyCode.C)) {
			brake = true;
			speed = 0f;
		} else {
			brake = false;
			speed = tempSpeed;
		}
	}
		
	private bool motorBreak = false;
	private float motorTimer=0.5f;
	public float motorTimerMax = 0.5f;
	public float lastMovement = 0f;
	void FixedUpdate(){
		/*if (brake) {
			wheelB.useMotor = true;
			wheelF.useMotor = true;
			wheelB.breakTorque = brakeSpeed;
			wheelF.breakTorque = brakeSpeed;
		}*/

		if (movement != 0f) {
			wheelB.useMotor = true;
			wheelF.useMotor = true;
			if (lastMovement != movement ) {

				speed -= deceleration;
				if (speed <= 0) {
					speed = 0;
					lastMovement = movement;
				}
				JointMotor2D motor = new JointMotor2D{ motorSpeed = lastMovement * speed, maxMotorTorque = wheelB.motor.maxMotorTorque };	
				wheelB.motor = motor;
				wheelF.motor = motor;

			} else {
				speed += aceleration;
				if(speed>=tempSpeed)
					speed = tempSpeed;

				JointMotor2D motor = new JointMotor2D{ motorSpeed = movement * speed, maxMotorTorque = wheelB.motor.maxMotorTorque };	
				wheelB.motor = motor;
				wheelF.motor = motor;
			}
		
		} else {
			

			wheelB.useMotor = false;
			wheelF.useMotor = false;
		}

	
		rb.AddTorque (-rotation * rotationSpeed * Time.fixedDeltaTime);
	}


}
