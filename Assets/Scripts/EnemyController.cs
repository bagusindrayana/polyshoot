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
    // Start is called before the first frame update
    void Start()
    {
        if(agent != null){
            agent.stoppingDistance = attackDistance;
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

        if(dist <= attackDistance){
            if(animator != null){
                animator.SetBool("walk",false);
                agent.Stop();
            }
        } else if(dist <= detectRange) {
            if(animator != null){
                animator.SetBool("walk",true);
                agent.Resume();
            }
        } else {
            animator.SetBool("walk",false);
            agent.Stop();
        }
    }
}
