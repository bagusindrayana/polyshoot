using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{	
	public UnityEngine.AI.NavMeshAgent agent;
	public Transform target;
    public Animator animator;
    public float attackDistance = 10f;
    public float detectRange = 20f;
    public bool seeTarget;
    public AudioClip walkSound;


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
            agent.Stop();
        }
        else if(!seeTarget && CanNavigateToPoint(target.position)){
            if(animator != null){
                animator.SetBool("walk",true);
                
            }
            agent.Resume();
        }
        else if(dist <= attackDistance){
            if(animator != null){
                animator.SetBool("walk",false);
                
            }
            agent.Stop();
        } 
        else if(dist <= detectRange) {
            if(animator != null){
                animator.SetBool("walk",true);
                
            }
            agent.Resume();
        } 
        
        else {
            if(animator != null){
                animator.SetBool("walk",false);
            }
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
        sfx.GetComponent<AudioSource>().clip = walkSound;
        sfx.GetComponent<AudioSource>().volume = 0.2f;
        sfx.transform.SetParent(transform);
        sfx.GetComponent<AudioSource>().Play();
        sfx.AddComponent<DestroyInSecond>();
        sfx.GetComponent<DestroyInSecond>().timeToDestroy = 2f;
    }
}
