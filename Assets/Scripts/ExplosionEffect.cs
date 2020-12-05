using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{	
	public float radiusEffect = 5f;
	public float powerEffect = 10f;
	public float damagePoint = 0f;
	public float damageGiven = 0f;
	public GameObject explodeEffect;
    // Start is called before the first frame update
    void Start()
    {
        explode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float dmg){
    	if(damagePoint > 0){
    		damagePoint -= dmg;
    	}

    }

    void explode(){
    	if(damagePoint <= 0){
    		if(explodeEffect != null){
    			Instantiate(explodeEffect,transform.position,Quaternion.identity);
    		}
    		Vector3 epos = transform.position;
			Collider[] cols = Physics.OverlapSphere(epos,radiusEffect);
			foreach(Collider c in cols){
				if(c.transform.tag == "Enemy" || c.transform.tag == "Damageable" || c.transform.tag == "Player"){
	                c.transform.SendMessage("ApplyDamage",damageGiven);
	            }
				Rigidbody rb = c.GetComponent<Rigidbody>();
				if(rb != null){
					rb.AddExplosionForce(powerEffect,epos,radiusEffect,3f);
				}
			}

			if(explodeEffect != null){
				Destroy(gameObject);
			}
    	}
    }
}
