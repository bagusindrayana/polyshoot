using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{	

	public Transform firePoint;
	public GameObject bulletPrefab;
	public bool bulletRigidBody;
	public AudioClip weaponSoundEffect;
	public float fireDelay;
	public Transform target;
	public GameObject hitEffect;
	public float weaponDamage;
	float curTime;

    // Start is called before the first frame update
    void Start()
    {
        if(target == null){
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newVec = (target.position - transform.position);
        Quaternion targetRot = Quaternion.LookRotation(newVec) * Quaternion.Euler(-90,0,0);
        transform.rotation = Quaternion.Slerp(transform.rotation,targetRot,2f * Time.deltaTime) ;
        curTime += Time.deltaTime;

        fireWeapon();
    }

    void fireWeapon(){
    	if(curTime >= fireDelay){
    		

            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100f) && !bulletRigidBody)
            {
                // Debug.DrawRay(curWeapon.weapon.firePoint.position, curWeapon.weapon.firePoint.forward * hit.distance, Color.yellow);
                if(hit.transform == target){
                    GameObject sfx = new GameObject();
                    sfx.AddComponent<AudioSource>();
                    sfx.GetComponent<AudioSource>().clip = weaponSoundEffect;
                    sfx.transform.SetParent(transform);
                    sfx.GetComponent<AudioSource>().Play();
                    sfx.AddComponent<DestroyInSecond>();
                    sfx.GetComponent<DestroyInSecond>().timeToDestroy = 2f;
                    var b = Instantiate(bulletPrefab,firePoint.position,Quaternion.identity);
                    b.transform.SetParent(transform);
                    var lr = b.GetComponent<LineRenderer>();
                    lr.SetPosition(1,firePoint.forward * hit.distance);


                    if(hit.transform.tag == "Player" || hit.transform.tag == "Damageable"){
                        hit.transform.SendMessage("ApplyDamage",weaponDamage);
                    }
                    if(hit.transform.GetComponent<Rigidbody>()){
                        hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(1000f * firePoint.forward, hit.point);
                    }


                    
                    var he = Instantiate(hitEffect,hit.point,Quaternion.LookRotation(hit.normal));
                    he.AddComponent<DestroyInSecond>();
                    he.GetComponent<DestroyInSecond>().timeToDestroy = 2f;

                    Destroy(b,0.1f);
                }
                
            } 
            // else {
            //     var b = Instantiate(bulletPrefab,firePoint.position,Quaternion.identity);
            //     if(bulletRigidBody){
            //         b.GetComponent<Rigidbody>().AddForce(1000 * firePoint.forward);
            //     } else {
            //         b.transform.SetParent(transform);
            //         var lr = b.GetComponent<LineRenderer>();
            //         lr.SetPosition(1,firePoint.forward * 100f);
            //         Destroy(b,0.1f);
            //     }
                
            // }
            
            curTime = 0f;
    	}
    }
}
