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


    private bool onGras = false;
    private float offRoadTime = 0;

    private int countStepps = 0;

    public bool isTraining = false;

    private float configuration = 0;
    EnvironmentParameters environmentParameters;

    public RayPerceptionSensorComponent3D RaySensorMiddleLine;
    public RayPerceptionSensorComponent3D RaySensorCurve;

    private void Start()
    {
        if(vehicle != null)
        {
            forwardView = vehicle.GetComponent<forwardView>();
            control = vehicle.GetComponent<VehicleControl>();
        }

        environmentParameters = Academy.Instance.EnvironmentParameters;
    }

    public void FixedUpdate()
    {
        laptime += Time.fixedDeltaTime;

       if (onGras)
       {
            offRoadTime += Time.fixedDeltaTime;
       }

    }



    public int countEpisods = -1;
    public override void OnEpisodeBegin()
    {
        configuration = environmentParameters.GetWithDefault("ciriconf", 1.0f);
        configAgent(configuration);

        int x = Random.Range(0, 2);
        var rb = this.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        laptime = 0f;
        distance = 0f;
        onGras = false;
        offRoadTime = 0f;
        countEpisods = CompletedEpisodes;

        //this.transform.localPosition = new Vector3(115.62f, 0.4f, 268.8f);
        //this.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));

        /*
        if (isTraining)
        {
            if (CompletedEpisodes < 50000)
            {
                this.transform.localPosition = new Vector3(113.84f, 0.75f, 413.38f);
                this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }
            else
            {
                if (CompletedEpisodes < 100000)
                {
                    this.transform.localPosition = new Vector3(199.9f, 0.75f, 592.6f);
                    this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }
                else
                {
                    if (CompletedEpisodes < 150000)
                    {
                        this.transform.localPosition = new Vector3(353.43f, 0.75f, 704.4f);
                        this.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
                    }
                    else
                    {
                        if (CompletedEpisodes < 200000)
                        {
                            this.transform.localPosition = new Vector3(295.5f, 0.75f, 510.77f);
                            this.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
                        }
                        else
                        {
                            if (CompletedEpisodes < 250000)
                            {
                                this.transform.localPosition = new Vector3(199f, 0.75f, 414f);
                                this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                            }
                            else
                            {
                                if (CompletedEpisodes < 300000)
                                {
                                    this.transform.localPosition = new Vector3(449.74f, 0.75f, 613.58f);
                                    this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                                }
                                else
                                {
                                    if (CompletedEpisodes < 350000)
                                    {
                                        this.transform.localPosition = new Vector3(507.51f, 0.75f, 613.58f);
                                        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                                    }
                                    else
                                    {
                                        this.transform.localPosition = new Vector3(115.05f, 0.75f, 54.4f);
                                        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            this.transform.localPosition = new Vector3(276f, 0.75f, 131f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, -119.4f, 0f));
        }*/

        

    } 


    private void configAgent(float value)
    {
        if (value == 1f)
        {
            this.transform.localPosition = new Vector3(113.84f, 0.75f, 413.38f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }

        if (value == 2f)
        {
            this.transform.localPosition = new Vector3(199.9f, 0.75f, 592.6f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }

        if (value == 3f)
        {
            this.transform.localPosition = new Vector3(353.43f, 0.75f, 704.4f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        }

        if (value == 4f)
        {
            this.transform.localPosition = new Vector3(295.5f, 0.75f, 510.77f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        }

        if (value == 5f)
        {
            this.transform.localPosition = new Vector3(199f, 0.75f, 414f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }

        if (value == 6f)
        {
            this.transform.localPosition = new Vector3(449.74f, 0.75f, 613.58f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }

        if (value == 7f)
        {
            this.transform.localPosition = new Vector3(507.51f, 0.75f, 613.58f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }

        if(value == 8f)
        {
            this.transform.localPosition = new Vector3(115.05f, 0.75f, 54.4f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }


    }


    private int getHits(RayPerceptionSensorComponent3D sensor)
    {
        //to get the ray from the ray sensor component
        var input = sensor.GetRayPerceptionInput();
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


    //get the numbers of rays of the sensor
    private int getNumbRays(RayPerceptionSensorComponent3D sensor)
    {
        //to get the ray from the ray sensor component
        var input = sensor.GetRayPerceptionInput();
        var output = RayPerceptionSensor.Perceive(input);

        return output.RayOutputs.Length;
    }



    public override void OnActionReceived(ActionBuffers actions)
    {
        float acc = Mathf.Clamp(actions.ContinuousActions[0], 0f, 1f);
        float steer = Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);

       // Debug.Log(acc);

        //forward to vehicle;
        control.AgentAcc = acc;
        control.AgentSteer = steer;



        if (forwardView.OnRoad == false)
        {
            //onGras = true;



            // checks if the cart is on the road and let it drive 

            //if(offRoadTime > 0.5f)
            //{
                //float punishmentGras = distance * 10* offRoadTime / laptime;                    

                //AddReward((distance / laptime) - punishmentGras);
            if(laptime > 0)
            {
                AddReward(distance / laptime);
            }

            countStepps += StepCount;
            EndEpisode();
            //}
        }
        /*else
        {
            if(offRoadTime > 0f)
            {
                float punishmentGras = -distance * 10 * offRoadTime / laptime;
                AddReward(punishmentGras);
            }

            onGras = false;
            offRoadTime = 0;
        }*/

        Vector3 currposition = this.transform.position;
        if(Vector3.Distance(currposition, lastPosition) > 1)
        {
            int hitsMiddle = getHits(RaySensorMiddleLine);
            int raysMiddle = getNumbRays(RaySensorMiddleLine);

            int hitsCurve = getHits(RaySensorCurve);
            int raysCurve = getNumbRays(RaySensorCurve);

            distance += 1f;
            lastPosition = currposition;

            float reward = 0 + (0.5f * control.speed * (hitsMiddle + hitsCurve)) / (raysMiddle + raysCurve);

            AddReward(reward);
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

        /*
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
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        oldTrigger = other.gameObject.transform.localPosition; 
    }


    // RayPerceptionSensor to get the rays

}
