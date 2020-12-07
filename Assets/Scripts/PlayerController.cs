using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public PlayerStatus playerStatus;
    public WeaponManager weaponManager;
    public TextMeshProUGUI info;
    public AudioSource audioSource;
    public AudioClip interactSound;
    public AudioClip footStepSound;

    [HideInInspector]
    public bool canMove = true;

    float walkTime;
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
    
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // if(curSpeedX != 0 || curSpeedY != 0){
        //     walkTime += Time.deltaTime;
        // } else {
        //      walkTime = 0f;
        // }

        // if(walkTime >= (isRunning ? 0.2f : 0.5f)){
        //     playFootStepSound();
        //     walkTime = 0f;
        // }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        Interact();
    }

    void Interact(){
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit,2.5f))
        {
            if(hit.transform.tag == "Item"){
                var i = hit.transform.GetComponent<Item>();
                if(i){
                    if(info != null){
                        info.text = "Pickup "+i.desc;
                    }
                    if(Input.GetKeyDown(KeyCode.E)){
                        audioSource.clip = interactSound;
                        audioSource.Play();
                        switch (i.type)
                        {
                            case ItemType.Item:
                                switch (i.index)
                                {
                                    case 0:
                                        playerStatus.playerHealth += i.value;
                                        Destroy(hit.transform.gameObject);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ItemType.Weapon:
                                weaponManager.giveWeapon(i.index,i.value);
                                Destroy(hit.transform.gameObject);
                                break;
                            default:
                                break;
                        }
                        
                    }
                }
            } 
            else if(hit.transform.tag == "Door"){
                var d = hit.transform.GetComponent<Door>();
                if(d){
                    if(info != null){
                        if(d.open){
                            info.text = "Close Door";
                        } else {
                            info.text = "Open Door";
                        }
                    }
                    if(Input.GetKeyDown(KeyCode.E)){
                        audioSource.clip = interactSound;
                        audioSource.Play();
                        d.InteractDoor();
                    }
                }
            }
            else {
                if(info != null){
                    info.text = "";
                }
                //Debug.Log("Not Find Item");
            }
            
        } else {
            if(info != null){
                info.text = "";
            }
        }
    }

    public void playFootStepSound(){
        GameObject sfx = new GameObject();
        sfx.AddComponent<AudioSource>();
        sfx.GetComponent<AudioSource>().clip = footStepSound;
        sfx.GetComponent<AudioSource>().volume = 0.5f;
        sfx.transform.SetParent(transform);
        sfx.GetComponent<AudioSource>().Play();
        sfx.AddComponent<DestroyInSecond>();
        sfx.GetComponent<DestroyInSecond>().timeToDestroy = 2f;
    }
}
