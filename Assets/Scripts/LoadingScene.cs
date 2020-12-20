using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{	
	public string sceneToLoad;
	AsyncOperation loadingOperation;
	public Slider progressBar;

	

    // Start is called before the first frame update
    void Start()
    {	
    	sceneToLoad = LoadingData.sceneToLoad;
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    }
}
