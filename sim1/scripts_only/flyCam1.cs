// source: https://forum.unity.com/threads/fly-cam-simple-cam-script.67042/

// this does not work:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyCam : MonoBehaviour
{
    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }


    void LateUpdate()
    {
        Vector3 newPos = PlayerTransform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor); 
    }
}