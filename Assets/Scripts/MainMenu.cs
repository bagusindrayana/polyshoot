using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{	
	public UnityEvent haveSaveData;

    // Start is called before the first frame update
    void Start()
    {   
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if(PlayerPrefs.GetString("LEVEL") != null && PlayerPrefs.GetString("LEVEL") != ""){
            Debug.Log(PlayerPrefs.GetString("LEVEL"));
        	haveSaveData.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openScene(string sceneName){
        LoadingData.sceneToLoad = sceneName;
         SceneManager.LoadScene("LoadingScene");
    }

    public void continueGame(){
    	if(PlayerPrefs.GetString("LEVEL") != null){
    		openScene(PlayerPrefs.GetString("LEVEL"));
    	}
    }

    public void exitGame(){
    	Application.Quit();
    }
}
