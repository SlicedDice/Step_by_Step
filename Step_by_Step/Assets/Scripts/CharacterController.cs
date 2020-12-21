using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Variables
    public GameObject mainBody; //Object Reference to control the body
    private Rigidbody mainRB; //Rigidbody of the main body. Will be initialized in Start()
    public float balanceThrust = 1f; //Variable affecting how strong the character balances

    // Start is called before the first frame update
    void Start()
    {
        mainRB = mainBody.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        balanceControls();
    }

    //Function for balancing the character
    void balanceControls()
    {
        if (Input.GetKey("w"))
        {
            mainRB.AddForce(transform.forward * balanceThrust);
        }
        else if (Input.GetKey("s"))
        {
            mainRB.AddForce(-transform.forward * balanceThrust);
        }
        else if (Input.GetKey("a"))
        {
            mainRB.AddForce(-transform.right * balanceThrust);
        }
        else if (Input.GetKey("d"))
        {
            mainRB.AddForce(transform.right * balanceThrust);
        }
   

    }
}
