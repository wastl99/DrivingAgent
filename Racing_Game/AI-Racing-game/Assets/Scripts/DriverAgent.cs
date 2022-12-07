using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class DriverAgent : Agent
{
    public GameObject vehicle;
    private forwardView forwardView;
    private VehicleControl control;
    private Vector3 lastPosition = new Vector3(201.9f, 26.02f, 555.88f);

    public GameObject FinishTrigger;

    float laptime = 0;
    float distance = 0;

    // hi
    private bool onGras = false;
    private float offRoadTime = 0;

    public RayPerceptionSensorComponent3D raySensorMiddleLine;

    private void Start()
    {
        if(vehicle != null)
        {
            forwardView = vehicle.GetComponent<forwardView>();
            control = vehicle.GetComponent<VehicleControl>();
        }
    }

    public void FixedUpdate()
    {
        laptime += Time.fixedDeltaTime;

       if (onGras)
       {
            offRoadTime += Time.fixedDeltaTime;
       }

    }


    

    public override void OnEpisodeBegin()
    {


        int x = Random.Range(0, 2);
        var rb = this.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        laptime = 0f;
        distance = 0f;
        onGras = false;
        offRoadTime = 0f;


        //this.transform.localPosition = new Vector3(115.62f, 0.4f, 268.8f);
        //this.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));

        this.transform.localPosition = new Vector3(585.17f, 0.75f, 76f);
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        /*
        if (x == 0)
        {
            this.transform.localPosition = new Vector3(585.17f, 0.4f, 76f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        if (x == 1)
        {
            this.transform.localPosition = new Vector3(719f, 0.75f, 230.2f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
        }*/



        /*
        if (x == 0)
        {
            this.transform.localPosition = new Vector3(115.62f, 0.75f, 577.22f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        if (x == 1)
        {
            this.transform.localPosition = new Vector3(192.3f, 0.75f, 840.7f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 68.71f, 0f));
        }
        if (x == 2)
        {
            this.transform.localPosition = new Vector3(362.1f, 0.75f, 671.4f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 179.618f, 0f));
        }*/

    }



    public override void OnActionReceived(ActionBuffers actions)
    {
        float acc = Mathf.Clamp(actions.ContinuousActions[0], 0f, 1f);
        float steer = Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);
        int brake = actions.DiscreteActions[0];

        Debug.Log(acc);

        //forward to vehicle;
        control.AgentAcc = acc;
        control.AgentSteer = steer;
        control.agentbrake = brake;



        if (forwardView.OnRoad == false)
        {

            AddReward(distance / laptime);

            EndEpisode();
            /*            onGras = true;



                        // checks if the cart is on the road and let it drive 

                        if(offRoadTime > 1f)
                        {
                            float punishmentGras = distance * offRoadTime / laptime;                    

                            AddReward((distance / laptime) - punishmentGras);

                            EndEpisode();
                        }*/
        }
        else
        {
/*            if(offRoadTime > 0f)
            {
                float punishmentGras = -distance * offRoadTime / laptime;
                AddReward(punishmentGras);
            }

            onGras = false;
            offRoadTime = 0;*/
        }

        Vector3 currposition = this.transform.position;
        if(Vector3.Distance(currposition, lastPosition) > 1)
        {
            int hits = getHits();
            
            distance += 1f;
            lastPosition = currposition;
            AddReward(0.5f * control.speed * hits);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {       
    }

    //private Vector3 oldTrigger = new Vector3(115.62f, 0.4f, 268.8f);
    private Vector3 oldTrigger = new Vector3(585.17f, 0.4f, 76f);
    private Vector3 newTrigger = new Vector3(0f, 0f, 0f);

    private void OnTriggerEnter(Collider other)
    {

        

        if(other.gameObject.tag == "end")
        {
            //episode complete
            AddReward(distance / laptime);
        }

        if(other.gameObject.tag == "guide")
        {

            newTrigger = other.gameObject.transform.localPosition;

            float distance = Vector3.Distance(newTrigger, oldTrigger);

            float reward = distance * control.speed;

            AddReward(reward);


        }

        if(other.gameObject.tag == "punish")
        {
            AddReward(-200f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        oldTrigger = other.gameObject.transform.localPosition; 
    }

    private int getHits()
    {
        // Accessing ray perception sensors 
        var input = raySensorMiddleLine.GetRayPerceptionInput();
        var output = RayPerceptionSensor.Perceive(input);

        int hits = 0;

        for (int i = 0; i < output.RayOutputs.Length; i++)
        {
            if (output.RayOutputs[i].HasHit)
            {
                hits++;
            }
        }

        return hits;
    }

}
