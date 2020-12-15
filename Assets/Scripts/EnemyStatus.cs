using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStatus : MonoBehaviour
{	

    [SerializeField]
    private float m_enemyHealth;

    public float enemyHealth
    {
       get { return m_enemyHealth; }
       set {
            m_enemyHealth = value;
            if(m_enemyHealth <= 0) 
            {
                m_enemyHealth = 0;
                this.Die();
            }

       }
    }
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
    }

    void Die(){
        Instantiate(deadBody,transform.position+new Vector3(0,2f,0),transform.rotation);
        dead = true;
        enemyDead.Invoke();
        Destroy(gameObject);
    } 
}
