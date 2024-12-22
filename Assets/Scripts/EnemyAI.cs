using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float scareDistance = 3f;
    public float aggroDistance = 20f;
    public float aggroDistanceDifference = 10f;

    public float agentSpeed = 5f;
    public float agentSpeedDifference = 3f;

    public double aggroTime = 10;
    public float wanderDistance = 50f;
    public double wanderInterval = 8;

    public Transform playerPosition;
    public GameObject PlayerScript;

    Transform playerLastPosition;
    Vector3 target;
    NavMeshAgent agent;
    

    double currentAggroTime = 0;
    double currentWanderTime = 0;
    int collectedNotesCount;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        collectedNotesCount = PlayerScript.GetComponent<Player>().collectedNotesCount;
        agent.speed = 10f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.black;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0f, 0), scareDistance);
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0f, 0), aggroDistance);
        Gizmos.color = UnityEngine.Color.blue;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0f, 0), wanderDistance);
    }

    bool randomWanderPoint(out Vector3 result)
    {
        Vector3 randomWanderPoint = transform.position + Random.insideUnitSphere * wanderDistance;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomWanderPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            Debug.Log('a');
            
            result = hit.position;
            return false;
        }
        result = Vector3.zero;
        return true;
    }

    void Update()
    {
        agent.speed = agentSpeed + agentSpeedDifference * collectedNotesCount;
        if (Vector3.Distance(transform.position, playerPosition.position) < aggroDistance + aggroDistanceDifference * collectedNotesCount)
        {
            playerLastPosition = playerPosition;
            target = playerPosition.position;
            currentAggroTime = aggroTime;
        }
        else if (currentAggroTime > 0)
        {
            target = playerLastPosition.position;
        }
        else if (currentAggroTime < 0 && Vector3.Distance(agent.pathEndPosition,transform.position) <= 1f)
        {
            Vector3 point;
            while (randomWanderPoint(out point)) ;
            currentWanderTime = aggroTime;
            
            target = point;
        }

        currentAggroTime -= Time.deltaTime;
        currentWanderTime -= Time.deltaTime;

        Debug.DrawRay(target, new Vector3(0,10f,0), UnityEngine.Color.red, .1f);
        agent.SetDestination(target);
    }
}
