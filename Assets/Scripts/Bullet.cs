using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int damage;

	void Start(){
		Destroy(gameObject,0.1f);
	}

	void OnTriggerEnter(Collider col){
		if(col.transform.tag == "Enemy"){
			col.transform.SendMessage("ApplyDamage",damage);
		}
	}
}