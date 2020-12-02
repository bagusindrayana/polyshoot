using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{	
	public string weaponName;
	public Transform firePoint;
	public Vector3 positionOffset;
	public Vector3 rotationOffset;
	public GameObject bulletPrefab;
	public float fireDelay = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
    	transform.localPosition = positionOffset;    
    	transform.localRotation = Quaternion.Euler(rotationOffset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
