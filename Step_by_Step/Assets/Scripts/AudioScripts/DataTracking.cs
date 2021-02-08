using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataTracking : MonoBehaviour
{

    public GameObject rightFoot; //Object Refernece to the stilt's right foot
    public GameObject leftFoot; //Object Refernece to the stilt's left foot

    public static float timeBetweenStep;
    public static List<float> timeBetweenStepList = new List<float>();

    public static float stepDistance;
    public static List<float> stepDistanceList = new List<float>();

    public static float timeForStep;
    public static List<float> timeForStepList = new List<float>();


    // private float[] timeForStepList;


    // Start is called before the first frame update
    void Start()
    {
        timeBetweenStep = 0f;
        timeBetweenStepList.Add(0.6f);

        stepDistance = 0f;
        stepDistanceList.Add(1.2f);

        timeForStep = 0f;
        timeForStepList.Add(2.5f);

    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            timeBetweenStep += Time.deltaTime*0.5f;
        } else
        {
            timeForStep += Time.deltaTime*0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (timeBetweenStep != 0f) timeBetweenStepList.Add(timeBetweenStep);
             
            // float z = timeBetweenStepList.Average();
            // Debug.Log("StandTime " + timeBetweenStep.ToString() + " Average: " + z.ToString() +  "ListCount " + stepDistanceList.Count.ToString());
            
            timeBetweenStep = 0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Pressure Plate" || collision.gameObject.tag == "Swamp Ground" || collision.gameObject.tag == "Wet Ground" || collision.gameObject.tag == "Lilypads")
            {
                stepDistance = (rightFoot.transform.position - leftFoot.transform.position).magnitude;
                if (stepDistance != 0f) stepDistanceList.Add(stepDistance);
             
             //   float y = stepDistanceList.Average();
             //   Debug.Log("Distance " + stepDistance.ToString() + " Average: " + y.ToString() + "ListCount " + stepDistanceList.Count.ToString());

                if (timeForStep != 0f) timeForStepList.Add(timeForStep);
             
             //    float x = timeForStepList.Average();
             //    Debug.Log("StepTime " + timeForStep.ToString()+ " Average: " + x.ToString());
             
                timeForStep = 0f;
                

            }
        }
    }

    public void ResetData()
    {
        timeBetweenStep = 0f;
        timeBetweenStepList.Clear();
        timeBetweenStepList.Add(0.8f);

        stepDistance = 0f;
        stepDistanceList.Clear();
        stepDistanceList.Add(1.2f);

        timeForStep = 0f;
        timeForStepList.Clear();
        timeForStepList.Add(1f);
    }

    public float AverageStepTime()
    {
        return timeForStepList.Average();
    }

    public float AverageTimeBetweenSteps()
    {
        return timeBetweenStepList.Average();
    }

    public float AverageStepDistance()
    {
        return stepDistanceList.Average();
    }

}
