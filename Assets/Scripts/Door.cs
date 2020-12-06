using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{   

    public Transform door;
    public Vector3 openPos;
    public Vector3 closePos;
    public bool open;
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    float  c;
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
        open = !open;
        if(open){
            audioSource.clip = openSound;
            audioSource.Play();
        } else {
            audioSource.clip = closeSound;
                audioSource.Play();
        }
        c = 0f;
    }


}
