using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    //Variables
    public GameObject mainBody; //Object Reference to control the body
    private Rigidbody mainRB; //Rigidbody of the main body. Will be initialized in Start()
    public float balanceThrust; //Variable affecting how strong the character balances
    public float rotation; // How much the character rotates when pressing A and D (degree per secound)


    public GameObject rightFoot; //Object Refernece to the stilt's right foot
    public GameObject leftFoot; //Object Refernece to the stilt's left foot
    public float feetMassBase; //The base weight of the feet, changes how strong the character is held on the ground
    public float footWeightWhileMoving; //Exactly as the variable name describes, this is the variable effecting how heavy the foot is while moving a stilt

    public GameObject stiltLeft;
    public GameObject stiltRight; //Two Variables referencing the stilts

    public GameObject rotationObj;

    private Vector2 lastMousePos = new Vector2(); //The last position of the mouse; to calculate the mouse movement


    public bool foundBeanstalkCollectible = false; //Checks to see if the collectible items were picked up
    public bool foundShipCollectible = false;
    public bool foundRuinCollectible = false;


    public bool movementByCamera = true;
    public bool invertedControls = true;

    private Vector3 respawnLocation; //Location when respawning with (R)
    private Quaternion respawnRotation;

    private bool dead = false;

    private GameController gameController;
    private CameraController mainCam;

    private float resetCooldown = 1f;

    public float legRaise;
    
    // Start is called before the first frame update
    void Start()
    {
        
        mainRB = mainBody.GetComponent<Rigidbody>();
        lastMousePos = Input.mousePosition;
        leftFoot.GetComponent<Rigidbody>().mass = feetMassBase;
        rightFoot.GetComponent<Rigidbody>().mass = feetMassBase;

        

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        mainCam.playerCharacter = mainBody;

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        respawnLocation = transform.position;
        respawnRotation = transform.rotation;

        setResetLocation(respawnLocation, respawnRotation);
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {
            balanceControls();

            if (movementByCamera) walkingControlsByCamera();
            else walkingControlsByCharacter();


            lastMousePos = Input.mousePosition;
        }
        
        generalControls();

        resetCooldown -= Time.deltaTime;
    }

    public void loadPlayer(PlayerData data)
    {

        respawnLocation = new Vector3(data.respawnLocation[0], data.respawnLocation[1], data.respawnLocation[2]);
        respawnRotation = new Quaternion(data.respawnRotation[0], data.respawnRotation[1], data.respawnRotation[2], data.respawnRotation[3]);

        foundBeanstalkCollectible = data.beanstalkCollectible;
        foundShipCollectible = data.shipwreckCollectible;
        foundRuinCollectible = data.ruinCollectible;
    }

    public void death(bool pDead)
    {
        dead = pDead;
    }

    void generalControls()
    {
        if (Input.GetKey("r") && resetCooldown <= 0f)
        {
            resetCooldown = 1f;

            gameController.dead = false;
            gameController.reset();
            death(false);
        }
    }

    public void setResetLocation(Vector3 resetLocation, Quaternion resetRotation)
    {
        gameController.respawnLocation = resetLocation;
        gameController.respawnRotation = resetRotation;

        respawnLocation = resetLocation;
        respawnRotation = resetRotation;
    }


    //Function for balancing the character
    void balanceControls()
    {
        if (Input.GetKey("w"))
        {
            mainRB.AddForce(mainRB.transform.forward * balanceThrust);
        }
        else if (Input.GetKey("s"))
        {
            mainRB.AddForce(-mainRB.transform.forward * balanceThrust);
        }
        else if (Input.GetKey("a"))
        {
            mainRB.AddForce(-mainRB.transform.right * balanceThrust);
        }
        else if (Input.GetKey("d"))
        {
            mainRB.AddForce(mainRB.transform.right * balanceThrust);
        }
    }

    void rotateCharacterRightStilt()
    {
        float rot = rotation * Time.deltaTime;

        if (Input.GetKey("q"))
        {
            //mainBody.transform.Rotate(0f, -rot, 0f, Space.World);
            stiltRight.transform.Rotate(0f, -rot, 0f, Space.World);
            rightFoot.transform.Rotate(0f, -rot, 0f, Space.World);
        }
        else if (Input.GetKey("e"))
        {
            //mainBody.transform.Rotate(0f, rot, 0f, Space.World);
            stiltRight.transform.Rotate(0f, rot, 0f, Space.World);
            rightFoot.transform.Rotate(0f, rot, 0f, Space.World); 

        }
    }
    void rotateCharacterLeftStilt()
    {
        float rot = rotation * Time.deltaTime;

        if (Input.GetKey("q"))
        {
            //mainBody.transform.Rotate(0f, -rot, 0f, Space.World);
            stiltLeft.transform.Rotate(0f, -rot, 0f, Space.World);
            leftFoot.transform.Rotate(0f, -rot, 0f, Space.World);
        }
        else if (Input.GetKey("e"))
        {
            //mainBody.transform.Rotate(0f, rot, 0f, Space.World);
            stiltLeft.transform.Rotate(0f, rot, 0f, Space.World);
            leftFoot.transform.Rotate(0f, rot, 0f, Space.World);
        }
    }

    //Function for controlling the individual stilts
    void walkingControlsByCamera()
    {
        CharacterJoint charJointLeft = stiltLeft.GetComponent<CharacterJoint>();
        Vector3 tmpL = charJointLeft.connectedAnchor;

        CharacterJoint charJointRight = stiltRight.GetComponent<CharacterJoint>();
        Vector3 tmpR = charJointRight.connectedAnchor;

        Quaternion quat = mainCam.gameObject.transform.rotation;

        Vector3 t = quat.eulerAngles;
        if (t.y == 180f || t.y == -180f || t.y == -70f || t.y == 290f) t *= -1f;
        Vector3 tmp1 = new Vector3(t.x / 180f, 0.0f, t.z / 180f);
        Vector3 tmp2 = new Vector3(-t.z / 180f, 0.0f, t.x / 180f);
        

        if (Input.GetMouseButton(0))
        {
            Vector2 deltaMouseMov = mouseMov();
            if(!invertedControls) deltaMouseMov *= -1f;

            Rigidbody leftFootRB = leftFoot.GetComponent<Rigidbody>();

            charJointLeft.connectedAnchor = new Vector3(tmpL.x, legRaise, tmpL.z);
            rotateCharacterRightStilt();
            leftFootRB.mass = footWeightWhileMoving;
            leftFootRB.AddForce(tmp1 * deltaMouseMov.y * 2);
            leftFootRB.AddForce(tmp2 * -deltaMouseMov.x * 2);
        }
        else if (Input.GetMouseButton(1))
        {
            Vector2 deltaMouseMov = mouseMov();
            if (!invertedControls) deltaMouseMov *= -1f;

            Rigidbody rightFootRB = rightFoot.GetComponent<Rigidbody>();

            charJointRight.connectedAnchor = new Vector3(tmpR.x, legRaise, tmpR.z);
            rotateCharacterLeftStilt();
            rightFootRB.mass = footWeightWhileMoving;
            rightFootRB.AddForce(tmp1 * deltaMouseMov.y *2);
            rightFootRB.AddForce(tmp2 * -deltaMouseMov.x *2 );
        }
        else
        {
            leftFoot.GetComponent<Rigidbody>().mass = feetMassBase;
            rightFoot.GetComponent<Rigidbody>().mass = feetMassBase;

            charJointLeft.connectedAnchor = new Vector3(tmpL.x, 0.03166672f, tmpL.z);
            charJointRight.connectedAnchor = new Vector3(tmpR.x, 0.05666729f, tmpR.z);
        }

    }

    void walkingControlsByCharacter()
    {
        CharacterJoint charJointLeft = stiltLeft.GetComponent<CharacterJoint>();
        Vector3 tmpL = charJointLeft.connectedAnchor;

        CharacterJoint charJointRight = stiltRight.GetComponent<CharacterJoint>();
        Vector3 tmpR = charJointRight.connectedAnchor;

        

        if (Input.GetMouseButton(0))
        {
            Vector2 deltaMouseMov = mouseMov();
            if (invertedControls) deltaMouseMov *= -1f;

            Rigidbody leftFootRB = leftFoot.GetComponent<Rigidbody>();

            charJointLeft.connectedAnchor = new Vector3(tmpL.x, legRaise, tmpL.z);
            rotateCharacterRightStilt();
            leftFootRB.mass = footWeightWhileMoving;
            leftFootRB.AddForce(mainBody.transform.forward * -deltaMouseMov.y * 0.25f);
            leftFootRB.AddForce(mainBody.transform.right * -deltaMouseMov.x * 0.25f);

        }
        else if (Input.GetMouseButton(1))
        {
            Vector2 deltaMouseMov = mouseMov();
            if (invertedControls) deltaMouseMov *= -1f;

            Rigidbody rightFootRB = rightFoot.GetComponent<Rigidbody>();

            charJointRight.connectedAnchor = new Vector3(tmpR.x, legRaise, tmpR.z);
            rotateCharacterLeftStilt();
            rightFootRB.mass = footWeightWhileMoving;
            rightFootRB.AddForce(mainBody.transform.forward * -deltaMouseMov.y * 0.25f);
            rightFootRB.AddForce(mainBody.transform.right * -deltaMouseMov.x * 0.25f);

        }
        else
        {
            leftFoot.GetComponent<Rigidbody>().mass = feetMassBase;
            rightFoot.GetComponent<Rigidbody>().mass = feetMassBase;

            charJointLeft.connectedAnchor = new Vector3(tmpL.x, 0.03166672f, tmpL.z);
            charJointRight.connectedAnchor = new Vector3(tmpR.x, 0.05666729f, tmpR.z);
        }

    }

    //Function to calculate and return the movement of the mouse relative to the last update
    Vector2 mouseMov()
    {
        Vector2 currMousePos = Input.mousePosition;
        Vector2 deltaMousePos = lastMousePos - currMousePos;
        lastMousePos = currMousePos;

        return deltaMousePos;
    }


    
    public Quaternion returnRotation()
    {
        return respawnRotation;
    }
    public Vector3 returnLocation()
    {
        return respawnLocation;
    }
}
