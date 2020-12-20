using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CinemachineAction : MonoBehaviour
{
    public UnityEvent onStartAction;
    public UnityEvent onEndAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startAction(){
    	onStartAction.Invoke();
    }

    void endAction(){
    	onEndAction.Invoke();
    }
}
