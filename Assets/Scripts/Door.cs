﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{   

    public bool interactable = true;
    public Transform door;
    public Vector3 openPos;
    public Vector3 closePos;
    public bool open;
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    float  c;

    public UnityEvent doorOpen;
    public UnityEvent doorClose;

    // Start is called before the first frame update
    void Start()
    {
        c = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(c < 3f){
            c += Time.deltaTime;
            if(open){
                door.localPosition = Vector3.Lerp(door.localPosition,openPos,5f * Time.deltaTime);
                
            } else {
                door.localPosition = Vector3.Lerp(door.localPosition,closePos,5f * Time.deltaTime);
                
            }   
        }
    }

    public void InteractDoor(){
        if(!interactable){
            return;
        }
        open = !open;
        if(open){
            if(audioSource != null && openSound != null){
                audioSource.clip = openSound;
                audioSource.Play();
            }
            doorOpen.Invoke();
        } else {
            if(audioSource != null && closeSound != null){
                audioSource.clip = closeSound;
                audioSource.Play();
            }
            doorClose.Invoke();
        }
        c = 0f;
    }

    public void LockDoor(){
        interactable = false;
        c = 0f;
    }


}
