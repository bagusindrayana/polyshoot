using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{	
	public string weaponName;
	public Transform firePoint;
	public Vector3 positionOffset;
	public Vector3 rotationOffset;
	public Vector3 aimPositionOffset;
	public Vector3 aimRotationOffset;
	public LineRenderer bulletPrefab;
	public float fireDelay = 0.2f;
    public int weaponDamage = 10;
    public bool aim = false;
    public float aimZooom = 40f;
    public TextMesh ammoText; 
    Camera camera;
    [HideInInspector]
    public float originalZoom;


    // Start is called before the first frame update
    void Start()
    {
    	camera = Camera.main;
    	//originalZoom = camera.fieldOfView;
    	aim = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(aim){
        	aimWeapon();
        } else {
        	idle();
        }
    }

    public void idle(){
    	transform.localPosition = Vector3.Lerp(transform.localPosition,positionOffset,Time.deltaTime * 10f);    
    	transform.localRotation = Quaternion.Lerp(transform.localRotation,Quaternion.Euler(rotationOffset),Time.deltaTime * 10f);
    	camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,originalZoom,Time.deltaTime * 10f);
    }

    public void aimWeapon(){
    	transform.localPosition = Vector3.Lerp(transform.localPosition,aimPositionOffset,Time.deltaTime * 10f);    
    	transform.localRotation = Quaternion.Lerp(transform.localRotation,Quaternion.Euler(aimRotationOffset),Time.deltaTime * 10f);
    	camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,aimZooom,Time.deltaTime * 10f);
    }
}
