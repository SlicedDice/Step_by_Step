using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    //Variables
    public GameObject mainBody; //Object Reference to control the body
    private Rigidbody mainRB; //Rigidbody of the main body. Will be initialized in Start()
    public float balanceThrust = 1f; //Variable affecting how strong the character balances

    public GameObject rightFoot; //Object Refernece to the stilt's right foot
    public GameObject leftFoot; //Object Refernece to the stilt's left foot
    public float feetMassBase; //The base weight of the feet, changes how strong the character is held on the ground
    public float footWeightWhileMoving; //Exactly as the variable name describes, this is the variable effecting how heavy the foot is while moving a stilt

    public GameObject stiltLeft;
    public GameObject stiltRight; //Two Variables referencing the stilts

    private Vector2 lastMousePos = new Vector2(); //The last position of the mouse; to calculate the mouse movement

    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        mainRB = mainBody.GetComponent<Rigidbody>();
        lastMousePos = Input.mousePosition;
        leftFoot.GetComponent<Rigidbody>().mass = feetMassBase;
        rightFoot.GetComponent<Rigidbody>().mass = feetMassBase;

    }

    // Update is called once per frame
    void Update()
    {
        
        balanceControls();
        walkingControls();
        lastMousePos = Input.mousePosition;
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

        //Vector3 fwd = Vector3.Scale(transform.forward, camera.transform.forward);
        //Vector3 rght = Vector3.Scale(transform.right, camera.transform.right);
        Vector3 tmp1 = new Vector3(0.875f, 0.0f, 1.125f);
        Vector3 tmp2 = new Vector3(-1.125f, 0.0f, 0.875f);
        if (Input.GetMouseButton(0))
        {
            
            Vector2 deltaMouseMov = mouseMov(); 
            Rigidbody leftFootRB = leftFoot.GetComponent<Rigidbody>();
    
            charJointLeft.connectedAnchor = new Vector3(tmpL.x, -0.1f, tmpL.z);

            leftFootRB.mass = footWeightWhileMoving;
            
            leftFootRB.AddForce(tmp1 * deltaMouseMov.y * 2);
            leftFootRB.AddForce(tmp2 * -deltaMouseMov.x * 2);
        }
        else if (Input.GetMouseButton(1))
        {
            Vector2 deltaMouseMov = mouseMov();
            Rigidbody rightFootRB = rightFoot.GetComponent<Rigidbody>();

            charJointRight.connectedAnchor = new Vector3(tmpR.x, -0.1f, tmpR.z);

            rightFootRB.mass = footWeightWhileMoving;
            rightFootRB.AddForce(tmp1 * deltaMouseMov.y * 2);
            rightFootRB.AddForce(tmp2 * -deltaMouseMov.x * 2);
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

        //Vector2 finalMousePos = new Vector2(deltaMousePos.x * 0.875f, deltaMousePos.y * 1.125f);
        return deltaMousePos;
    }
}
