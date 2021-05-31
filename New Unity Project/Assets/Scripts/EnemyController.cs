using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    //Plays enemy attack and shout
    public AudioSource soundSource;
    public AudioClip[] audioList;

    [SerializeField] private float maxRange = 20f; //range for enemy vision
    [SerializeField] private float attackRange = 20f; //range for attack
    private float attackTimer = 1f; //every 3 seconds, enemy fires an electric bullet
    
    private float shoutTimer = 4f; //if it in range, enemy shouts to the player
    
    private GameObject target; //player object

    [SerializeField] private Transform initialPoint; //enemy ai patrol initial point
    [SerializeField] private Transform endPoint; //enemy ai patrol end point

   
    [SerializeField] private GameObject shotPoint; //barrel point

    [SerializeField] private ParticleSystem muzzleEffect;
    
    private bool isGoingInitial = true; //checks if ai goes to the initial patrol point

    private NavMeshAgent navAI;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        //soundSource = GetComponent<AudioSource>();
        
        navAI = GetComponent<NavMeshAgent>();
        target = PlayerController.instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //navAI.isStopped = false;
        Vector3 targerVector = target.transform.position - transform.forward;
        float angle = Vector3.Angle(targerVector, transform.forward);
        float distance = Vector3.Distance(target.transform.position, transform.position);
        
        if (distance <= maxRange)
        {
            if (angle <= 120) //enemy sees the player
            {
                //Shout(); //shout sound
                
                Chase(); 
                
                if (distance <= attackRange) //player in the attack range
                {
                    Debug.Log("EXTERMINATE");
                    Attack();
                }
            }
            else //player is in the enemy range but enemy doesn't see the player
            {
                if (attackTimer < 0.7f)
                {
                    attackTimer = 0.7f;
                }
                Patrol();
            }
        }
        else //player is not in the enemy range
        {
            Patrol();
        }

        
    }

    //shout sound plays in every 4 seconds 
    public void Shout()
    {
        shoutTimer += Time.deltaTime;
        if (shoutTimer >= 4f)
        {
            shoutTimer = 0;
            soundSource.PlayOneShot(audioList[0]);
        }
    }

    //enemy attack in every 3 seconds
    public void Attack()
    {
        //?
        //isAttacking = true;
        //navAI.isStopped = true;
        
        attackTimer += Time.deltaTime;
        if (attackTimer >= 0.7f)
        {
            attackTimer = 0;
            
            //soundSource.PlayOneShot(audioList[1]);
           

            Vector3 targetDirection = target.transform.position - transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * 40f);
            
            Debug.DrawRay(transform.localPosition, transform.forward, Color.green);

            muzzleEffect.Play();
            
            RaycastHit hit;
            if (Physics.Raycast(shotPoint.transform.position, transform.forward, out hit, attackRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("HIT BY THE ENEMY");
                    //TO DO
                    //ADJUST ECONOMY
                   
                    CanvasController.instance.UpdateHealthBar(-10f);
                }
            }

        }
        //?
        //isAttacking = false;


    }

    //sets destination to player
    private void Chase()
    {
        //attack in any time
        navAI.SetDestination((target.transform.position));
        
        //attack without walking 
        /*if (!isAttacking)
        {
            navAI.SetDestination(target.transform.position);
        }
        else
        {
            navAI.isStopped = true;
        }*/
    }

    //sets destination for patrolling
    private void Patrol()
    {
        Vector3 destination;

        if (isGoingInitial)
        {
            navAI.SetDestination(initialPoint.position);
            destination = transform.position - initialPoint.position;

            if (destination.magnitude < 6f)
            {
                isGoingInitial = false;
            }
        }
        else
        {
            navAI.SetDestination(endPoint.position);
            destination = transform.position - endPoint.position;

            if (destination.magnitude < 6f)
            {
                isGoingInitial = true;
            }
        }
    }

    //shows up enemy ranges
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position , maxRange);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position , attackRange);
    }
}
