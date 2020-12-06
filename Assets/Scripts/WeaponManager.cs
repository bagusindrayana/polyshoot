using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{   
    public List<Weapon> weapons;
    public Transform handTransform;
    public List<MyWeapon> myWeapons;
    
    public float initialZoom = 60f;

    float recoil = 0f;
    GameObject curWeapon;
    float curTime = 0f;
    int curIndex = 0;
    Quaternion originalPos;
    Quaternion totalRecoil;

    Camera camera;
    Weapon cw;

    void Start(){
    	camera = Camera.main;
        originalPos = handTransform.localRotation;
        selectWeapon(curIndex);
    }

    void Update(){
        
    	if(Input.GetAxis("Mouse ScrollWheel") > 0){
    		curIndex += 1;
    		if(curIndex >= myWeapons.Count){
    			curIndex = 0;
    		}
    		selectWeapon(curIndex);
    	}

    	if(Input.GetAxis("Mouse ScrollWheel") < 0){
    		curIndex -= 1;
    		if(curIndex < 0){
    			curIndex = myWeapons.Count - 1;
    		}
    		selectWeapon(curIndex);
    	}

    	if(cw != null && ((Input.GetButton("Fire1") && !cw.singleFire) || (Input.GetButtonDown("Fire1") && cw.singleFire))){
    		fireWeapon();
            
    	} else {
            // if(curWeapon != null){
            //     curTime = curWeapon.GetComponent<Weapon>().fireDelay;
            //     //handTransform.transform.localRotation = Quaternion.Lerp(handTransform.transform.localRotation,originalPos,Time.deltaTime*20f);
            // }

            // if(curTime > 0){
            //     curTime -= Time.deltaTime;
            // }
    		
    	}

        if(cw != null && curTime < cw.fireDelay){
            curTime += Time.deltaTime;
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
        if(index > myWeapons.Count - 1){
            return;
        }
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
            cw = ww;
    	} else if(w.weapon.weaponName == curWeapon.GetComponent<Weapon>().weaponName){
            var ww = curWeapon.GetComponent<Weapon>();
            if(ww.ammoText != null){
            	ww.ammoText.text = w.weaponAmmo.ToString();
            }
        }

        
    }



    //GameObject bullet;
    public void fireWeapon(){
    	
    	var mw = myWeapons[curIndex];
        var w = cw;
    	if(curTime >= w.fireDelay){
            
            
            if(mw.weaponAmmo > 0){
                GameObject sfx = new GameObject();
                sfx.AddComponent<AudioSource>();
                sfx.GetComponent<AudioSource>().clip = w.weaponSoundEffect;
                sfx.transform.SetParent(handTransform);
                sfx.GetComponent<AudioSource>().Play();
                sfx.AddComponent<DestroyInSecond>();
                sfx.GetComponent<DestroyInSecond>().timeToDestroy = 2f;
                //Instantiate(sfx,transform.position,Quaternion.identity);
                RaycastHit hit;
                if (Physics.Raycast(w.firePoint.position, w.firePoint.forward, out hit, 100f) && !w.bulletRigidBody)
                {
                    // Debug.DrawRay(curWeapon.weapon.firePoint.position, curWeapon.weapon.firePoint.forward * hit.distance, Color.yellow);
                    // Debug.Log("Did Hit");
                    var b = Instantiate(w.bulletPrefab,w.firePoint.position,Quaternion.identity);
                    b.transform.SetParent(handTransform);
                    var lr = b.GetComponent<LineRenderer>();
                    lr.SetPosition(1,w.firePoint.forward * hit.distance);
  

                    if(hit.transform.GetComponent<Rigidbody>()){
                        hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(1000f * w.firePoint.forward, hit.point);
                    }

                    if(hit.transform.tag == "Enemy" || hit.transform.tag == "Damageable"){
                        hit.transform.SendMessage("ApplyDamage",w.weaponDamage);
                    }
                    
                    var he = Instantiate(w.hitEffect,hit.point,Quaternion.LookRotation(hit.normal));
                    he.AddComponent<DestroyInSecond>();
                    he.GetComponent<DestroyInSecond>().timeToDestroy = 2f;
                    Destroy(b,0.1f);
                    
                } else {
                    var b = Instantiate(w.bulletPrefab,w.firePoint.position,Quaternion.identity);
                    // b.transform.SetParent(handTransform);
                    if(w.bulletRigidBody){
                        b.GetComponent<Rigidbody>().AddForce(1000 * w.firePoint.forward);
                        var bullet = b.GetComponent<Bullet>();
                        bullet.bulletDamage = w.weaponDamage;
                    } else {
                        b.transform.SetParent(handTransform);
                        var lr = b.GetComponent<LineRenderer>();
                        lr.SetPosition(1,w.firePoint.forward * 100f);
                        Destroy(b,0.1f);
                    }
                    
                    
                    
                }
                
                mw.weaponAmmo -= 1;
                
                //b.GetComponent<Rigidbody>().AddForce(0,0,100f);
                
                recoil += 0.1f;
                curTime = 0f;

            }
    	}

    	if(w.ammoText != null){
        	w.ammoText.text = mw.weaponAmmo.ToString();
        }

        

    }


    public void weaponRecoil(){
        var w = cw;
        var f = 1;
        if(w == null){
        	return;
        }

        if(w.aim){
        	f = 2;
        }
        if(recoil > 0){
            var r = new Vector3(UnityEngine.Random.Range(w.rangeRecoilX.x,w.rangeRecoilX.y)/f,UnityEngine.Random.Range(w.rangeRecoilY.x,w.rangeRecoilY.y)/f,UnityEngine.Random.Range(w.rangeRecoilZ.x,w.rangeRecoilZ.y)/f);
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


    public void giveWeapon(int i,int val){
        var w = weapons[i];
        foreach (var ww in myWeapons)
        {
            if(ww.weapon.weaponName == w.weaponName){
                ww.weaponAmmo += val;
                return;
            }
        }
        myWeapons.Add(new MyWeapon(w,val));
        selectWeapon(i);
    }




}




[Serializable]
public class MyWeapon
{
	public Weapon weapon;
	public int weaponAmmo;

    public MyWeapon(Weapon w,int wa){
        weapon = w;
        weaponAmmo = wa;
    }
}


// [Serializable]
// class WeaponUse
// {
// 	public string weaponName;
// 	public GameObject weaponPrefab;
// }


