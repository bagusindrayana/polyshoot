using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBosStatus : EnemyStatus
{
   	public List<EnemyStatus> bosBody;

   	bool checkBodyStatus(){
   		foreach(EnemyStatus body in bosBody){
   			if(body.enemyHealth > 0){
   				return false;
   			}
   		}

   		return true;
   	}

   	public void ApplyDamage(float dmg){
   		if(checkBodyStatus()){
   			enemyHealth -= dmg;
   		}
  
    }
}
