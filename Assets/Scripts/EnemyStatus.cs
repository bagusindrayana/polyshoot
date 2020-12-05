﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{	

	public float enemyHealth;
	public GameObject deadBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float dmg){
    	enemyHealth -= dmg;
    	if(enemyHealth <= 0){
    		Instantiate(deadBody,transform.position+new Vector3(0,2f,0),transform.rotation);
    		Destroy(gameObject);
    	}
    }
}
