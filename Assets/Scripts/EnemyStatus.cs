using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStatus : MonoBehaviour
{	

	public float enemyHealth;
	public GameObject deadBody;
    public UnityEvent enemyDead;
    bool dead;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float dmg){
    	enemyHealth -= dmg;
    	if(enemyHealth <= 0 && !dead){
    		Instantiate(deadBody,transform.position+new Vector3(0,2f,0),transform.rotation);
            dead = true;
            enemyDead.Invoke();
    		Destroy(gameObject);
    	}
    }
}
