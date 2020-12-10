using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{	
	public float radiusEffect = 5f;
	public float powerEffect = 10f;
	public GameObject hitEffect;
    public float bulletDamage;
	bool duar;
    Rigidbody myRb;
    // Start is called before the first frame update
    void Start()
    {
        duar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(myRb && duar){
            Destroy(myRb);
        }
    }

    void OnTriggerEnter(Collider col){
    	myRb = gameObject.GetComponent<Rigidbody>();
    	if(duar){
    		
    		return;
    	}
    	
    	Vector3 epos = transform.position;
		Collider[] cols = Physics.OverlapSphere(epos,radiusEffect);
		foreach(Collider c in cols){
            int dist = (int)Vector3.Distance(transform.position,c.transform.position);
            if(c.transform.tag == "Enemy" || c.transform.tag == "Damageable" || c.transform.tag == "Player"){
                c.transform.SendMessage("ApplyDamage",bulletDamage - (dist * 10),SendMessageOptions.DontRequireReceiver);
                c.transform.SendMessageUpwards("ApplyDamage",bulletDamage - (dist * 10),SendMessageOptions.DontRequireReceiver);
            }
            
			Rigidbody rb = c.GetComponent<Rigidbody>();
			if(rb != null && rb != myRb){
				rb.AddExplosionForce(powerEffect - (dist * 100),epos,radiusEffect,3f);
			}

            
		}
		Instantiate(hitEffect,epos,transform.rotation);
		duar = true;
		Destroy(gameObject,1f);
		
    }
}
