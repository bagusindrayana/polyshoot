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
        if(PlayerPrefs.GetString("LEVEL") != null){
        	haveSaveData.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openScene(string sceneName){
         SceneManager.LoadScene(sceneName);
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
