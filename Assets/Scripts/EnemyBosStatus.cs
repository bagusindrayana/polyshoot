using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBosStatus : EnemyStatus
{
   	public List<EnemyStatus> bosBody;
      public UnityEvent onBodyDestroy;
      bool body;

      void Start(){
         body = true;
      }

   	bool checkBodyStatus(){
   		foreach(EnemyStatus body in bosBody){
   			if(body != null && body.enemyHealth > 0){
   				return false;
   			}
   		}
         if(body){
            onBodyDestroy.Invoke();
            body = false;
         }
   		return true;
   	}

      void Update(){
         checkBodyStatus();
      }

   	public void ApplyDamage(float dmg){
   		if(checkBodyStatus()){
   			enemyHealth -= dmg;
   		}
  
    }
}
