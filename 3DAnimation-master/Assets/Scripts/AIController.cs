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
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        nextDecisionTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextDecisionTime)
        {
            agent.SetDestination(followTF.position);
            nextDecisionTime = Time.time + decisionDelay;
        }
        
    }
}
