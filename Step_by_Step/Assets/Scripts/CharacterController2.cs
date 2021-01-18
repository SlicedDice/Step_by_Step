using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterController2 : MonoBehaviour
{
    //Variables
    public GameObject mainBody; //Object Reference to control the body
    private Rigidbody mainRB; //Rigidbody of the main body. Will be initialized in Start()
    public float balanceThrust; //Variable affecting how strong the character balances
    public float rotation = 30f; // How much the character rotates when pressing A and D (degree per secound)

    public GameObject rightFoot; //Object Refernece to the stilt's right foot
    public GameObject leftFoot; //Object Refernece to the stilt's left foot
    public float feetMassBase; //The base weight of the feet, changes how strong the character is held on the ground
    public float footWeightWhileMoving; //Exactly as the variable name describes, this is the variable effecting how heavy the foot is while moving a stilt

    public GameObject stiltLeft;
    public GameObject stiltRight; //Two Variables referencing the stilts

    private Vector2 lastMousePos = new Vector2(); //The last position of the mouse; to calculate the mouse movement


    public bool foundCollectible = false; //Test Collectible Item, until all actual collectible items are decided on


    private Vector3 respawnLocation; //Location when respawning with (R)
    
    // Start is called before the first frame update
    void Start()
    {
        mainRB = mainBody.GetComponent<Rigidbody>();
        lastMousePos = Input.mousePosition;
        leftFoot.GetComponent<Rigidbody>().mass = feetMassBase;
        rightFoot.GetComponent<Rigidbody>().mass = feetMassBase;

        respawnLocation = new Vector3(-1.27999997f, -1.88810349f, 0.159999996f);
    }

    // Update is called once per frame
    void Update()
    {

        balanceControls();
        walkingControls();

        generalControls();

        lastMousePos = Input.mousePosition;
    }


    void generalControls()
    {
        if (Input.GetKey("r"))
        {
            Time.timeScale = 0;

            resetCharacterPos();
            transform.localPosition = respawnLocation;
            Time.timeScale = 1;
        }
    }
    void resetCharacterPos()
    {
        mainBody.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        mainBody.transform.localPosition = new Vector3(0f, 0f, 0f);
        mainBody.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);

        stiltLeft.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        stiltLeft.transform.localPosition = new Vector3(0.310000002f, -0.800000012f, 0f);
        stiltLeft.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);

        stiltRight.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        stiltRight.transform.localPosition = new Vector3(-0.310000002f, -0.800000012f, 0f);
        stiltRight.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);

        leftFoot.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f); 
        leftFoot.transform.localPosition = new Vector3(0.310000002f, -2.30100012f, 0f);
        leftFoot.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);

        rightFoot.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        rightFoot.transform.localPosition = new Vector3(-0.310000002f, -2.30100012f, 0f);
        rightFoot.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
    }

    //Function for balancing the character
    void balanceControls()
    {
        if (Input.GetKey("w"))
        {
            mainRB.AddForce(-transform.forward * balanceThrust);
        }
        else if (Input.GetKey("s"))
        {
            mainRB.AddForce(transform.forward * balanceThrust);
        }
        else if (Input.GetKey("a"))
        {
            mainRB.AddForce(transform.right * balanceThrust);
        }
        else if (Input.GetKey("d"))
        {
            mainRB.AddForce(-transform.right * balanceThrust);
        }
    }

    //Function for controlling the individual stilts
    void walkingControls()
    {
        CharacterJoint charJointLeft = stiltLeft.GetComponent<CharacterJoint>();
        Vector3 tmpL = charJointLeft.connectedAnchor;

        CharacterJoint charJointRight = stiltRight.GetComponent<CharacterJoint>();
        Vector3 tmpR = charJointRight.connectedAnchor;

        float rot = rotation * Time.deltaTime;

        if (Input.GetMouseButton(0))
        {

            Vector2 deltaMouseMov = mouseMov() * -1f;
            Rigidbody leftFootRB = leftFoot.GetComponent<Rigidbody>();
            //   Rigidbody rightFootRB = leftFoot.GetComponent<Rigidbody>();
            //   Rigidbody rightStiltRB = stiltRight.GetComponent<Rigidbody>();

            charJointLeft.connectedAnchor = new Vector3(tmpL.x, -0f, tmpL.z);

            leftFootRB.mass = footWeightWhileMoving;
            leftFootRB.AddForce(mainBody.transform.forward * -deltaMouseMov.y * 2);
            leftFootRB.AddForce(mainBody.transform.right * -deltaMouseMov.x * 2);

            if (Input.GetKey("a"))
            {
                mainBody.transform.Rotate(0f, -rot, 0f, Space.World);
                stiltRight.transform.Rotate(0f, -rot, 0f, Space.World);
                rightFoot.transform.Rotate(0f, -rot, 0f, Space.World);

                //   mainRB.AddTorque(mainRB.transform.up * -rotation,ForceMode.VelocityChange);
                //   rightFootRB.AddTorque(mainRB.transform.up * -rotation, ForceMode.VelocityChange);
                //   rightStiltRB.AddTorque(mainRB.transform.up * -rotation, ForceMode.VelocityChange);

            }
            else if (Input.GetKey("d"))
            {
                mainBody.transform.Rotate(0f, rot, 0f, Space.World);
                stiltRight.transform.Rotate(0f, rot, 0f, Space.World);
                rightFoot.transform.Rotate(0f, rot, 0f, Space.World);
            }


        }
        else if (Input.GetMouseButton(1))
        {
            Vector2 deltaMouseMov = mouseMov() * -1f;
            Rigidbody rightFootRB = rightFoot.GetComponent<Rigidbody>();

            charJointRight.connectedAnchor = new Vector3(tmpR.x, -0f, tmpR.z);

            rightFootRB.mass = footWeightWhileMoving;
            rightFootRB.AddForce(mainBody.transform.forward * -deltaMouseMov.y * 2);
            rightFootRB.AddForce(mainBody.transform.right * -deltaMouseMov.x * 2);

            if (Input.GetKey("a"))
            {
                mainBody.transform.Rotate(0f, -rot, 0f, Space.World);
                stiltLeft.transform.Rotate(0f, -rot, 0f, Space.World);
                leftFoot.transform.Rotate(0f, -rot, 0f, Space.World);
            }
            else if (Input.GetKey("d"))
            {
                mainBody.transform.Rotate(0f, rot, 0f, Space.World);
                stiltLeft.transform.Rotate(0f, rot, 0f, Space.World);
                leftFoot.transform.Rotate(0f, rot, 0f, Space.World);
            }

        }
        else
        {
            leftFoot.GetComponent<Rigidbody>().mass = feetMassBase;
            rightFoot.GetComponent<Rigidbody>().mass = feetMassBase;

            charJointLeft.connectedAnchor = new Vector3(tmpL.x, -0.25f, tmpL.z);
            charJointRight.connectedAnchor = new Vector3(tmpR.x, -0.25f, tmpR.z);
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
}
