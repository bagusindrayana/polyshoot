using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{	
	public float playerHealth;
    public Vector2 rangeShakeX = new Vector2(0,10);
    public Vector2 rangeShakeY = new Vector2(0,10);
    public Vector2 rangeShakeZ = new Vector2(0,10);
    public Slider healthBar;
    public UnityEvent playerDead;
    float shake;
    Quaternion originalPos;
    Quaternion totalShake;

    void Start()
    {
        originalPos = Camera.main.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        cameraShake();
        if(playerHealth > 100f){
            playerHealth = 100f;
        }
        healthBar.value = playerHealth;
    }

    public void ApplyDamage(float dmg){
    	playerHealth -= dmg;
        shake += 0.1f;
        if(playerHealth <= 0){
            playerDead.Invoke();
        }
    }

    

    public void cameraShake(){
        if(shake > 0){
            var r = new Vector3(UnityEngine.Random.Range(rangeShakeX.x,rangeShakeX.y),UnityEngine.Random.Range(rangeShakeY.x,rangeShakeY.y),UnityEngine.Random.Range(rangeShakeZ.x,rangeShakeZ.y));
            totalShake = Quaternion.Euler(r);
            Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation,totalShake,Time.deltaTime*10f);
            shake -= Time.deltaTime;
        } else {
            //Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation,originalPos,Time.deltaTime*20f);
            shake = 0f;
        }
    }
}
