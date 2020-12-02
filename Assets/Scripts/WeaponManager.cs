using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform handTransform;
    public List<MyWeapon> myWeapons;

    MyWeapon curWeapon;

    float curTime = 0f;
    void Update(){
    	if(Input.GetKeyDown(KeyCode.Alpha1)){
    		selectWeapon(0);
    	}

    	if(Input.GetKeyDown(KeyCode.Alpha2)){
    		selectWeapon(1);
    	}

    	if(Input.GetButton("Fire1")){
    		fireWeapon();
    	} else {
    		curTime = 0f;
    	}
    }

    public void selectWeapon(int index){
    	
    	MyWeapon w = myWeapons[index];
    	if(curWeapon == null || w.weapon.weaponName != curWeapon.weapon.weaponName){
    		if(curWeapon != null){
	    		Destroy(curWeapon.weapon.gameObject);
	    	}
    		GameObject wp = Instantiate(w.weapon.gameObject,handTransform.position,Quaternion.identity);
	    	wp.transform.SetParent(handTransform);
	    	curWeapon = w;
	    	curWeapon.weapon = wp.GetComponent<Weapon>();
    	}
    }

    
    public void fireWeapon(){
    	curTime += Time.deltaTime;
    	if(curTime > curWeapon.weapon.fireDelay){
    		var b = Instantiate(curWeapon.weapon.bulletPrefab,curWeapon.weapon.firePoint.position,Quaternion.identity);
    		b.GetComponent<Rigidbody>().AddForce(0,0,100f);
    		curTime = 0;
    	}

    }


}

[Serializable]
public class MyWeapon
{
	public Weapon weapon;
	public int weaponAmmo;
}


// [Serializable]
// class WeaponUse
// {
// 	public string weaponName;
// 	public GameObject weaponPrefab;
// }


