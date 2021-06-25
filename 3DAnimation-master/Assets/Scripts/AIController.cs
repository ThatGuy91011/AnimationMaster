using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform followTF;
    public float decisionDelay = 1f;
    private float nextDecisionTime;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        nextDecisionTime = Time.time;
        anim = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextDecisionTime)
        {
            //Update follow
            agent.SetDestination(followTF.position);
            //Set next decision time
            nextDecisionTime = Time.time + decisionDelay;
        }
        /** // TODO: animation of enemy
         //Get desired movement from agent
         Vector3 desiredMovement = agent.desiredVelocity;
        //Invert movement to work with root motion
        desiredMovement = this.transform.InverseTransformDirection(desiredMovement);
        //Remove speed by normalizing to 1
        desiredMovement = desiredMovement.normalized;
        //Use pawn speed
        desiredMovement *= 2f; //Pawn speed
        //Pass into animator
        anim.SetFloat("Forward", desiredMovement.z);
        anim.SetFloat("Right", desiredMovement.x);

    
    }

    private void OnAnimatorMove()
    {
        agent.velocity = anim.velocity;
    }
    **/
    }
}
