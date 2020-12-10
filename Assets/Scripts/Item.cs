using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{	

    public string desc;
	public string name;
    public int index;
    public ItemType type;
    public int value;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

[Serializable]
public enum ItemType {
    Weapon,
    WeaponAmmo,
    Item
}
