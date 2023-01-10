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
    public float AttackRange;
    public float howcloseReset;
    public float AngryLevel;
    private bool AngryAnimateIsDone;
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
        AngryAnimateIsDone = false;
    }
    private void Update()
    {
        Distance = Vector3.Distance(player.position, transform.position);

        if (Distance <= howclose)
        {
            howclose = howclose + Time.deltaTime * AngryLevel;
            navMeshAgent.destination = player.position;
            if(AngryAnimateIsDone == false)
            {
                gameObject.GetComponent<Animator>().Play("EnemyAngry");
                AngryAnimateIsDone = true;
            }
        }
        else
        {
            transform.position = offsetPos.position;
            howclose = howcloseReset;
            if (AngryAnimateIsDone == true)
            {
                gameObject.GetComponent<Animator>().Play("EnemyCold");
                AngryAnimateIsDone = false;
            }
               
        }
        DoAttackInRange();
    }
    private void OnDrawGizmos()
    {
        // Angry Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, howclose);
        // Attack Range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AttackRange);

    }
    private void DoAttackInRange()
    {
        if (Distance <= AttackRange)
        {
            StartCoroutine(nameof(DoDamage));
        }
        else
        {
            StopCoroutine(nameof(DoDamage));
        }

    }

    IEnumerator DoDamage()
    {
        while (PlayerBehavior.Instance.PlayerHP > 0 && UIcontrol.Instance.GameWinUI.activeSelf == false) // Still Attack if player HP > 0;
        {
            PlayerBehavior.Instance.PlayerHP = PlayerBehavior.Instance.PlayerHP - 20;
            yield return new WaitForSeconds(1f);
        }
    }
}
