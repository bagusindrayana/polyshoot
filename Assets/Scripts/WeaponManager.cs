using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform handTransform;
    public List<MyWeapon> myWeapons;
    
    public float initialZoom = 60f;

    float recoil = 0f;
    GameObject curWeapon;
    float curTime = 0f;
    int curIndex;
    Quaternion originalPos;
    Quaternion totalRecoil;

    Camera camera;
   
    

    void Start(){
    	camera = Camera.main;
        originalPos = handTransform.localRotation;
    }

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
            if(curWeapon != null){
                curTime = curWeapon.GetComponent<Weapon>().fireDelay;
                //handTransform.transform.localRotation = Quaternion.Lerp(handTransform.transform.localRotation,originalPos,Time.deltaTime*20f);
            }
    		
    	}

    	if(Input.GetButtonDown("Fire2")){
    		if(curWeapon != null){
            	var w = curWeapon.GetComponent<Weapon>();
            	w.aim = !w.aim;
            }
    	}

        weaponRecoil();
    }

    public void selectWeapon(int index){

    	curIndex = index;
    	MyWeapon w = myWeapons[index];
    	if(curWeapon == null || w.weapon.weaponName != curWeapon.GetComponent<Weapon>().weaponName){
    		if(curWeapon != null){
	    		Destroy(curWeapon);
	    	}
    		GameObject wp = Instantiate(w.weapon.gameObject,handTransform.position,Quaternion.identity);
	    	wp.transform.SetParent(handTransform);
	    	curWeapon = wp;
	    	var ww = curWeapon.GetComponent<Weapon>();
            curTime = ww.fireDelay;
            ww.originalZoom = initialZoom;
            if(ww.ammoText != null){
            	ww.ammoText.text = w.weaponAmmo.ToString();
            }
    	}
    }



    //GameObject bullet;
    public void fireWeapon(){
    	curTime += Time.deltaTime;
    	var mw = myWeapons[curIndex];
        var w = curWeapon.GetComponent<Weapon>();
    	if(curTime > w.fireDelay){
            
            
            if(mw.weaponAmmo > 0){
                
                RaycastHit hit;
                if (Physics.Raycast(w.firePoint.position, w.firePoint.forward, out hit, 100f))
                {
                    // Debug.DrawRay(curWeapon.weapon.firePoint.position, curWeapon.weapon.firePoint.forward * hit.distance, Color.yellow);
                    // Debug.Log("Did Hit");
                    var b = Instantiate(w.bulletPrefab,w.firePoint.position,Quaternion.identity);
                    b.transform.SetParent(handTransform);
                    var lr = b.GetComponent<LineRenderer>();
                    //lr.SetPosition(0,b.transform.position);
                    lr.SetPosition(1,w.firePoint.forward * hit.distance);
                    // Vector3 objectScale = b.transform.localScale;
                    // float distance = Vector3.Distance(hit.point, curWeapon.weapon.firePoint.position);
                    // Vector3 newScale = new Vector3(objectScale.x, objectScale.y, distance);
                    // b.transform.localScale = newScale;

                    // var bullet = b.GetComponent<Bullet>();
                    // bullet.damage = w.weaponDamage;

                    if(hit.transform.GetComponent<Rigidbody>()){
                        hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(1000f * w.firePoint.forward, hit.point);
                    }
                    Destroy(b,0.1f);
                    
                } else {
                    var b = Instantiate(w.bulletPrefab,w.firePoint.position,Quaternion.identity);
                    b.transform.SetParent(handTransform);
                    var lr = b.GetComponent<LineRenderer>();
                    lr.SetPosition(1,w.firePoint.forward * 100f);
                    // var bullet = b.GetComponent<Bullet>();
                    // bullet.damage = w.weaponDamage;
                    Destroy(b,0.1f);
                    
                }
                
                mw.weaponAmmo -= 1;
                
                //b.GetComponent<Rigidbody>().AddForce(0,0,100f);
                curTime = 0;
                recoil += 0.1f;

            }
    	}

    	if(w.ammoText != null){
        	w.ammoText.text = mw.weaponAmmo.ToString();
        }

        

    }


    public void weaponRecoil(){
        
        

        if(recoil > 0){
            var r = new Vector3(UnityEngine.Random.Range(0,5f),UnityEngine.Random.Range(0,5f),UnityEngine.Random.Range(0,5f));
            totalRecoil = Quaternion.Euler(r);
            handTransform.transform.localRotation = Quaternion.Lerp(handTransform.transform.localRotation,totalRecoil,Time.deltaTime*10f);
            recoil -= Time.deltaTime;
        } else {
            handTransform.transform.localRotation = Quaternion.Lerp(handTransform.transform.localRotation,originalPos,Time.deltaTime*20f);
            recoil = 0f;
        }
        // if(tf > curWeapon.GetComponent<Weapon>().fireDelay){
        //     handTransform.transform.rotation = Quaternion.Euler(r);
        //     tf = 0f;
        // } else {
        //     tf += Time.deltaTime;
        // }
        //handTransform.transform.localRotation = Quaternion.Lerp(handTransform.transform.localRotation,Quaternion.Euler(r),Time.deltaTime*20f);
        //handTransform.transform.rotation = Quaternion.Euler(r);
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


