using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{

    //CONTROLLER
    private GameControllerTouchControl touchController;
	public CamaraController cameraController;
    
    //CANVAS
    public GameObject DriveControl;
    public GameObject EditModeCanvas;
    public GameObject EditModeRampCanvas;

    public Text playButtonText;
    
    //PLAYER
    public GameObject car;
	public GameObject player;
    
    
    //EDIT MODE
    public GameObject[] rampPrefab;
    public GameObject buttonSetRamp;
    public Sprite[] rampSprites;
    public int spriteIndex = 0;
    public GameObject imgObjSelected;
    private bool setRampMode;
    public bool setRampOk = false;
    public bool editMode = true;
    public bool setRamp = false;
    public bool mouseRelease = false;
    private GameObject newRamp;




    public float boardH = 6;
    public float boardW = 2;




    // CAMERAS
    public Camera[] cameras;
    public int cameraIndex = 1;
    private int cameraNumber;



    //GAME RULES
	public GameObject startFlag;
	public GameObject endFlag;
	public GameObject endRoomPoint;


    void Start()
    {

        if(cameraIndex==0){
            cameras[0].gameObject.SetActive(true);
            cameras[1].gameObject.SetActive(false);
        }else{
            cameras[1].gameObject.SetActive(true);
            cameras[0].gameObject.SetActive(false);
        }


        cameraNumber = cameras.Length;
		player = Instantiate(car,startFlag.transform.position,  Quaternion.identity);
		player.GetComponent<CarController>().initializationCar(gameObject.GetComponent<GameController>(),startFlag,endRoomPoint);
		cameraController.setTarget(player);
        touchController = gameObject.GetComponent<GameControllerTouchControl>();
        setTouchController();

    }

    void setTouchController(){
        touchController.setCarController(player.GetComponent<CarController>());
    }

    public void destroyPlayer(){
        if(player)
            Destroy(player);
    }
    public void restart(){
        destroyPlayer();
        player = Instantiate(car,startFlag.transform.position,  Quaternion.identity);
		player.GetComponent<CarController>().initializationCar(gameObject.GetComponent<GameController>(),startFlag,endRoomPoint);
		cameraController.setTarget(player);
        setTouchController();
    }
    public void activeEditMode(){
        cameraIndex = 0;
        nextCamera();
    }

    public void setSetRampOk()
    {
        setRampOk = true;
    }
    public void setRampModeOn()
    {
        setRampMode = true;
    }

    public void nextSprite()
    {
        spriteIndex++;
        if (spriteIndex >= rampSprites.Length)
            spriteIndex = 0;

    }
    public void previewSprite()
    {
        spriteIndex--;
        if (spriteIndex < 0)
            spriteIndex = (rampSprites.Length - 1);

    }
    Vector3 getNewPos()
    {
        Vector3 mp = cameras[1].ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        float newX = (mp.x);
        float newY = (mp.y);
        return new Vector3(newX, newY, 0f);
    }



    void rampEditor()

    {
      
        if (editMode)
        {

            if (!setRampMode)
            {
                EditModeRampCanvas.SetActive(false);
                EditModeCanvas.SetActive(true);
                imgObjSelected.GetComponent<Image>().sprite = rampSprites[spriteIndex];
                imgObjSelected.GetComponent<Image>().SetNativeSize();
            }
            else
            {
                EditModeCanvas.SetActive(false);
                if (Input.GetMouseButtonDown(1))
                {
                    if (setRamp)
                    {

                        newRamp.transform.localScale = new Vector3(newRamp.transform.localScale.x * -1f, 1f, 1f);
                    }
                }

                if (!setRamp)
                {
                    setRamp = true;
                    Vector3 newPos = getNewPos();
                    newRamp = Instantiate(rampPrefab[spriteIndex], newPos, Quaternion.identity);
                    //  newRamp.GetComponent<ObjController> ().setSprite(rampSprites[spriteIndex]);

                }

                if (Input.GetMouseButton(0))
                {
                      screenCamaraFolow();

                    if (!checkMousePos())
                    {
                        EditModeRampCanvas.SetActive(false);
                        newRamp.transform.position = getNewPos();
                    }
                }
                else
                {
                    EditModeRampCanvas.SetActive(true);
                }


                if (setRampOk)
                {
                    setRampOk = false;
                    setRampMode = false;
                    setRamp = false;
                }

            }
        }
        else
        {
            EditModeRampCanvas.SetActive(false);
            EditModeCanvas.SetActive(false);
            if (setRamp)
            {
                setRamp = false;
                setRampOk = false;
                setRampMode = false;
                Destroy(newRamp);
            }
        }

    }

    void screenCamaraFolow(){
        float xPart = Screen.width / 10;
        if(Input.mousePosition.x <= xPart ){
            cameras[1].transform.position = new Vector3(cameras[1].transform.position.x - 0.1f,cameras[1].transform.position.y,cameras[1].transform.position.z);
        }else if(Input.mousePosition.x >= (Screen.width - xPart) ){
             cameras[1].transform.position = new Vector3(cameras[1].transform.position.x + 0.1f,cameras[1].transform.position.y,cameras[1].transform.position.z);
        }
    }

    bool checkMousePos()
    {
        
        float x = buttonSetRamp.GetComponent<RectTransform>().rect.width;
        float y = buttonSetRamp.GetComponent<RectTransform>().rect.height;
        Vector2 position = new Vector2(buttonSetRamp.transform.position.x - (x / 2f), buttonSetRamp.transform.position.y + (y / 2f));
        Vector2 position2 = new Vector2(buttonSetRamp.transform.position.x + (x / 2f), buttonSetRamp.transform.position.y - (y / 2f));
        if ((Input.mousePosition.x >= position.x && Input.mousePosition.x <= position2.x) && (Input.mousePosition.y <= position.y && Input.mousePosition.y >= position2.y))
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Z))
        {
            nextCamera();
        }

        if (cameraIndex == 1)
        {
            destroyPlayer();
            editMode = true;
            DriveControl.SetActive(false);

        }
        else
        {
           if(!player)restart();
            editMode = false;
            DriveControl.SetActive(true);
        }

        rampEditor();
    }


    public void nextCamera()
    {
        cameras[cameraIndex].gameObject.SetActive(false);
        if(cameraIndex == 0){
            cameraIndex=1;
            cameras[cameraIndex].gameObject.SetActive(true);
            playButtonText.text = "Jugar";
        }else {
            cameraIndex=0;
            cameras[cameraIndex].gameObject.SetActive(true);
             playButtonText.text = "Terminar";
        }

/*
        cameraIndex++;
        if (cameraIndex >= cameraNumber)
            cameraIndex = 0;
        for (int i = 0; i < cameraNumber; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        cameras[cameraIndex].gameObject.SetActive(true);
*/
    }
}


