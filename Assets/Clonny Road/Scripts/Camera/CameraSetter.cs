using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraSetter : MonoBehaviour {

    private
	// Use this for initialization
	void Start () {
        Camera myCamera = GetComponent<Camera>();
        if (myCamera.aspect < 1.0f)
        {
            myCamera.orthographicSize = 4.0f;
        }
    }
	
}
