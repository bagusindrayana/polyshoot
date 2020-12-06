using System;
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
	[HideInInspector]
	public bool boom;
    // Start is called before the first frame update
    void Start()
    {
        explode();
		boom = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float dmg){
    	if(damagePoint > 0){
    		damagePoint -= dmg;
    	}

    	explode();

    }

    void explode(){
    	if(damagePoint <= 0 && !boom){
    		if(explodeEffect != null){
    			Instantiate(explodeEffect,transform.position,Quaternion.identity);
    		}
    		try {
    			Vector3 epos = transform.position;
				Collider[] cols = Physics.OverlapSphere(epos,radiusEffect);
				foreach(Collider c in cols){
					
					if(c.transform != transform && !boom && c.transform && (c.transform.tag == "Enemy" || c.transform.tag == "Damageable" || c.transform.tag == "Player")){
						boom = true;
		                // c.transform.SendMessage("ApplyDamage",damageGiven,SendMessageOptions.DontRequireReceiver);

						switch (c.transform.tag)
						{
							case "Damageable":
								var ef = c.GetComponent<ExplosionEffect>();
								if(ef != null && !ef.boom){
									ef.ApplyDamage(damageGiven);
								}
								break;
							case "Enemy":
								var es = c.GetComponent<EnemyStatus>();
								if(es != null){
									es.ApplyDamage(damageGiven);
								}
								break;
							case "Player":
								var ep = c.GetComponent<PlayerStatus>();
								if(ep != null){
									ep.ApplyDamage(damageGiven);
								}
								break;
							default:
								break;
						}
						
		            }
					Rigidbody rb = c.GetComponent<Rigidbody>();
					if(rb != null){
						rb.AddExplosionForce(powerEffect,epos,radiusEffect,3f);
					}
				}
			} catch(Exception e){
				Debug.Log(e.ToString());
			}

			if(explodeEffect != null){
				Destroy(gameObject);
			}
			
    	}
    }
}
