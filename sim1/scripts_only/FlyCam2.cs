// source: https://www.youtube.com/watch?v=XRe8Zt42Z1Y
// more: https://www.youtube.com/watch?v=1bFISMM2g2c
// flycam script in unifycommunity: http://wiki.unity3d.com/index.php/FlyCam_Extended

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCam2 : MonoBehaviour {

    private Transform myShip;
    private void Awake()
    {
        myShip = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private Vector3 velocityCameraFollow;
    public Vector3 behindposition = new Vector3(0, 2, -4);
    public float angle;
    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, myShip.transform.TransformPoint(behindposition) + Vector3.up * Input.GetAxis("Vertical"), ref velocityCameraFollow, 0.1f);
 
    }
}
