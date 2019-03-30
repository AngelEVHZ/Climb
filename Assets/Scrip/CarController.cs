using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CarController : MonoBehaviour {
	

	public GameController gameController;
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
	private float speed = 0;
	public float maxSpeed=2500f;
	private float movement = 0f;
	private float touchMovement=0f;
	private bool touchJump=false;
	private float rotationSpeed = 0f;
	public float jumpForce = 100f;
	private bool jumpTime = false;
	private int jumpCount = 0;

	private float jump_timer =0;
	private float jump_timer_seconds=1f;

	public float groundRotationSpeed = 15f;
	public float airRotationSpeed = 15f;
	public float deceleration=300f;
	public float aceleration=300f;
	private bool wheelOngroundB=false;
	private bool wheelOngroundF=false;
	private bool carOnGround = false;

	private float lastMovement = 0f;
	private bool isDeceleration = false;
	private bool isDecelerationRelease = false;
	private float maxRbSpeed = 7.4f;
	// Use this for initialization
	public void initializationCar(GameController gameController_ ,GameObject startPosition_,GameObject endPoint_){
		gameController =gameController_;
		startPosition=startPosition_;
		endPoint=endPoint_;
	}

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	public void restart(){
		gameController.activeEditMode();

	
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
				gameOverTimer = 1.5f;
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
			rotationSpeed = groundRotationSpeed;
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
		if (Input.GetKeyDown (KeyCode.Space) || touchJump ) {
			touchJump = false;
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


	void setGravity(float gravity){
		rb.gravityScale = gravity;
		rbWheelB.gravityScale = gravity;
		rbWheelF.gravityScale = gravity;
	}

	public void setTouchMovement(float movement_){
		touchMovement = movement_;
	}
	public void setTouchJump(bool jump_){
		touchJump = jump_;
	}

	void Update () {

		if (!isGameOver ()) {

			
			movement = -Input.GetAxisRaw ("Vertical");
			if(movement==0f)movement = touchMovement;
		
		
			checkOnGround ();
			jump ();

		}

		
	}


	void FixedUpdate(){
		if(carOnGround)
			Run();
		else
			Rotate();
	}

    void setMotor(float speed_)
    { 
		JointMotor2D motor = new JointMotor2D { motorSpeed = speed_, maxMotorTorque = wheelB.motor.maxMotorTorque };
        wheelB.motor = motor;
        wheelF.motor = motor;

    }
    void setUseMotor(bool use)
    {
        wheelB.useMotor = use;
        wheelF.useMotor = use;

    }
	void Rotate(){
		setUseMotor(false);
		rb.AddTorque(-movement * rotationSpeed * Time.fixedDeltaTime);
		
	}




    void Run()
    {
        if (movement != 0f)
        {
            setUseMotor(true);
            if (lastMovement != movement)
            {
                if (rb.velocity.x > -1 && rb.velocity.x < 1) speed = 0;
                speed -= deceleration;
                if (speed <= 0)
                {
                    speed = 0;
                    lastMovement = movement;
                }
                setMotor(lastMovement * speed);
            }
            else
            {
                speed += aceleration;
                if (speed >= maxSpeed)
                    speed = maxSpeed;

				 setMotor(movement * speed);
            }
        }
        else
        {
			speed = ( Mathf.Abs( rb.velocity.x) * maxSpeed) / maxRbSpeed;
            setUseMotor(false);
        }

    }
	


}
