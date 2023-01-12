using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class enemy : MonoBehaviour
{
    private Transform offsetPos;
    private Transform player;
    private float Distance;
    public float howclose;
    public float AttackRange;
    public float howcloseReset;
    public float AngryLevel;
    private bool inAttackRange = false;
    private bool AngryAnimateIsDone;
    private bool AngrySound;
    private bool WaitToStopSound = false;
    [SerializeField] private int EnemyDamage;
    [SerializeField ]private float TimeRepeatDamage;
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
    private void Start()
    {
        AngrySound = false;
    }
    private void Update()
    {
        Distance = Vector3.Distance(player.position, transform.position);

        CheckDistance();
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
        if (Distance <= AttackRange && !inAttackRange)
        {
            inAttackRange = true;
            StartCoroutine(nameof(DoDamage));
        }
        else if (Distance > AttackRange && inAttackRange)
        {
            inAttackRange = false;
            StopCoroutine(nameof(DoDamage));
        }

    }

    IEnumerator DoDamage()
    {
        while (PlayerBehavior.Instance.PlayerHP > 0 && UIcontrol.Instance.GameWinUI.activeSelf == false) // Still Attack if player HP > 0;
        {
            PlayerBehavior.Instance.PlayerHP = PlayerBehavior.Instance.PlayerHP - EnemyDamage;
            yield return new WaitForSeconds(TimeRepeatDamage);
        }
    }
    private async void SoundEF()
    {
        if ( SoundManager.Instance.CheckIsPlaying("ScareSound") == false)
        {
            SoundManager.Instance.PlayAnySoundSE("ScareSound");
            WaitToStopSound = true;
            return;
        }
        if (WaitToStopSound == true)
        {
            await WaitToStop();
            WaitToStopSound = false;
            return;

        }
    }
    private async Task WaitToStop()
    {
        await Task.Delay(500);
        SoundManager.Instance.StopAnySound("ScareSound");
    }
    private void CheckDistance()
    {
        if (Distance <= howclose)
        {
            if (AngrySound == false)
            {
                SoundEF();
                AngrySound = true;
            }
            howclose = howclose + Time.deltaTime * AngryLevel;
            navMeshAgent.destination = player.position;
            if (AngryAnimateIsDone == false)
            {
                gameObject.GetComponent<Animator>().Play("EnemyAngry");
                AngryAnimateIsDone = true;
            }
        }
        else if (Distance >= howclose)
        {
            if (AngrySound == true)
            {
                SoundEF();
                AngrySound = false;
            }
            transform.position = offsetPos.position;
            howclose = howcloseReset;
            if (AngryAnimateIsDone == true)
            {
                gameObject.GetComponent<Animator>().Play("EnemyCold");
                AngryAnimateIsDone = false;
            }

        }
    }
}
