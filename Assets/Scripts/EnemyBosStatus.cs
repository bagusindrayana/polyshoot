using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBosStatus : EnemyStatus
{
   public List<EnemyStatus> bosBody;


   public void ApplyDamage(float dmg){
    	enemyHealth -= dmg;
    	// if(enemyHealth <= 0 && !dead){
    	// 	Instantiate(deadBody,transform.position+new Vector3(0,2f,0),transform.rotation);
    	// 	Destroy(gameObject);
        //     dead = true;
    	// }
    }
}
