using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetLineRenderer : MonoBehaviour
{	
	public LineRenderer lineRenderer;
    public Transform target1;
    public Transform target2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        lineRenderer.SetPosition(0,target1.position);
        lineRenderer.SetPosition(1,target2.position);
    }
}
