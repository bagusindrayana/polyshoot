using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{	
	public UnityEngine.AI.NavMeshAgent agent;
	public Transform target;
    public Animator animator;
    public float attackDistance = 10f;
    public float detectRange = 20f;
    public bool seeTarget;
    public AudioClip walkSound;
    public UnityEvent enemyMove;
    public UnityEvent enemyIdle;


    void Start()
    {
        if(agent != null){
            //agent.stoppingDistance = attackDistance;
        } 

        if(target == null){
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }   

        
    }

    // Update is called once per frame
    void Update()
    {
        
        agent.SetDestination(target.position);
        float dist = Vector3.Distance(transform.position,target.position);
        if(!CanNavigateToPoint(target.position)){
            if(animator != null){
                animator.SetBool("walk",false);
                
            }
            enemyIdle.Invoke();
            agent.Stop();
        }
        else if(!seeTarget && CanNavigateToPoint(target.position)){
            if(animator != null){
                animator.SetBool("walk",true);
                
            }
            enemyMove.Invoke();
            agent.Resume();
        }
        else if(seeTarget && CanNavigateToPoint(target.position) && dist > attackDistance){
            if(animator != null){
                animator.SetBool("walk",true);
                
            }
            enemyMove.Invoke();
            agent.Resume();
        }
        else if(dist <= attackDistance){
            if(animator != null){
                animator.SetBool("walk",false);
                
            }
            enemyIdle.Invoke();
            agent.Stop();
        } 
        else if(dist <= detectRange) {
            if(animator != null){
                animator.SetBool("walk",true);
                
            }
            agent.Resume();
            enemyMove.Invoke();
        } 
        
        else {
            if(animator != null){
                animator.SetBool("walk",false);
            }
            enemyIdle.Invoke();
            agent.Stop();
        }
    }

    public bool CanNavigateToPoint(Vector3 point)
    {
        UnityEngine.AI.NavMeshPath cache_navpath = new UnityEngine.AI.NavMeshPath();
        if (agent.CalculatePath(point, cache_navpath))
        {   
            return (cache_navpath.status == UnityEngine.AI.NavMeshPathStatus.PathComplete);
        }
        return false;
    }

    public void playWalkSound(){
        GameObject sfx = new GameObject();
        sfx.AddComponent<AudioSource>();
        var a_sfx = sfx.GetComponent<AudioSource>();
        a_sfx.clip = walkSound;
        a_sfx.maxDistance = 25f;
        a_sfx.volume = 0.2f;
        sfx.transform.SetParent(transform);
        a_sfx.Play();
        sfx.AddComponent<DestroyInSecond>();
        sfx.GetComponent<DestroyInSecond>().timeToDestroy = 2f;
    }
}
