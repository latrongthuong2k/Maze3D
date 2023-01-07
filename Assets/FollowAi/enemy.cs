using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    private Transform offsetPos;
    private Transform player;
    private float Distance;
    public float howclose;
    public float howcloseReset;
    public float AngryLevel;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    player = GameObject.FindGameObjectWithTag("Player").transform;
    //}

    //// Update is called once per frame
    //void Update()
    //{
     

    //    if(dist <= howclose)
    //    {
    //        transform.LookAt(player);
    //        GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
    //    }
    //}
    
    private NavMeshAgent navMeshAgent;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offsetPos = transform.parent;
    }
    private void Update()
    {
        Distance = Vector3.Distance(player.position, transform.position);

        if (Distance <= howclose)
        {
            howclose = howclose + Time.deltaTime * AngryLevel;
            navMeshAgent.destination = player.position;
            gameObject.GetComponent<Animator>().Play("EnemyAngry");
        }
        else
        {
            transform.position = offsetPos.position;
            howclose = howcloseReset;
            gameObject.GetComponent<Animator>().Play("EnemyCold");
        }
            
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, howclose);
    }
}
